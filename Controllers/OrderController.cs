using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadingIsGood.DomainInterfaces;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using ReadingIsGood.Persistence;
using ReadingIsGood.Resources;
using System;

namespace ReadingIsGood.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ApiBaseController<OrderController>
    {
        public OrderController(ILogger<OrderController> logger, IReadingIsGoodRepository readingIsGoodRepository,
            ICacheService cacheService, ReadingIsGoodDbContext readingIsGoodDbContext) 
            : base(logger, readingIsGoodRepository, cacheService, readingIsGoodDbContext)
        {
        }

        [HttpGet]
        [ActionName("GelAllOrdersFromAuthenticatedUser")]
        public IActionResult GelAllOrdersFromAuthenticatedUser(string token)
        {
            var authorizationResult = IsAuthorized(token);
            if (!authorizationResult.IsSuccess)
            {
                logger.LogError(authorizationResult.Rex.ExceptionMessage);
                return Problem(authorizationResult.Rex.ExceptionMessage);
            }

            var customerInfoResult = GetCustomerInfoFromSessionToken(token);
            if (!customerInfoResult.IsSuccess)
            {
                logger.LogError(customerInfoResult.Rex.ExceptionMessage);
                return Problem(customerInfoResult.Rex.ExceptionMessage);
            }

            var customerInfo = customerInfoResult.Result as SessionDto;

            var dbResult = readingIsGoodRepository.GetAllOrders();
            if (dbResult == null) 
            {
                ReadingIsGoodException rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_NotFoundAnyOrder, customerInfo.username)
                };

                logger.LogError(rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            return Ok(dbResult);
        }

        [HttpGet]
        [ActionName("GetCustomerOrderForSpecificOrderId")]
        public IActionResult GetCustomerOrderForSpecificOrderId(string token, int orderId)
        {
            ReadingIsGoodException rex;
            var authorizationResult = IsAuthorized(token);
            if (!authorizationResult.IsSuccess)
            {
                logger.LogError(authorizationResult.Rex.ExceptionMessage);
                return Problem(authorizationResult.Rex.ExceptionMessage);
            }

            if (orderId <= 0)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_ParameterCantBeLessThanOrEqualToZero, nameof(orderId))
                };

                logger.LogError(rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            var dbResult = readingIsGoodRepository.GetOrderById(orderId);

            return Ok(dbResult);
        }

        [HttpPost]
        [ActionName("CreateNewOrder")]
        public IActionResult CreateNewOrder(string token, string bookName, int numberOfBookCustomerWants)
        {
            ReadingIsGoodException rex;
            var validationEmptyParameterResult = ValidateEmptyStringParameter(nameof(bookName), bookName);
            if (!validationEmptyParameterResult.IsSuccess)
            {
                logger.LogError(validationEmptyParameterResult.Rex.ExceptionMessage);
                return Problem(validationEmptyParameterResult.Rex.ExceptionMessage);
            }

            if (numberOfBookCustomerWants <= 0) 
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_ParameterCantBeLessThanOrEqualToZero, nameof(numberOfBookCustomerWants))
                };

                logger.LogError(rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            var authorizationResult = IsAuthorized(token);
            if (!authorizationResult.IsSuccess)
            {
                logger.LogError(authorizationResult.Rex.ExceptionMessage);
                return Problem(authorizationResult.Rex.ExceptionMessage);
            }

            var customerInfoResult = GetCustomerInfoFromSessionToken(token);
            if (!customerInfoResult.IsSuccess) 
            {
                logger.LogError(customerInfoResult.Rex.ExceptionMessage);
                return Problem(customerInfoResult.Rex.ExceptionMessage);
            }

            var bookFromStock = readingIsGoodRepository.GetBookFromStockByBookName(bookName);
            
            if (bookFromStock == null)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = ReadingIsGoodResources.Error_NotFoundBookAtStock
                };

                logger.LogError(rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            if (bookFromStock.NumberofBooks < numberOfBookCustomerWants)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = ReadingIsGoodResources.Error_NotEnoughBookAtStock
                };

                logger.LogError(rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            // Create Order and Delivery Information and allocate books from bookstock
            var customerInfo = customerInfoResult.Result as SessionDto;
            Order newOrder = new Order();
            newOrder.BookId = bookFromStock.Id;
            newOrder.CustomerId = customerInfo.userId;
            newOrder.OrderDate = DateTime.Now;
            
            readingIsGoodRepository.CreateNewOrder(newOrder);
            readingIsGoodDbContext.SaveChanges();
            logger.LogInformation(string.Format(ReadingIsGoodResources.Info_NewOrderAdded, newOrder.Id));

            BookDeliveryInformation bookDeliveryInformation = new BookDeliveryInformation();
            bookDeliveryInformation.OrderDate = newOrder.OrderDate;
            bookDeliveryInformation.OrderId = newOrder.Id;
            bookDeliveryInformation.IsDelivered = false;
            bookDeliveryInformation.BookDeliveryStatus = Enumerations.BookDeliveryStatus.GettingPrepared;
            bookDeliveryInformation.BookId = newOrder.BookId;

            readingIsGoodRepository.CreateNewDeliveryInformation(bookDeliveryInformation);

            bookFromStock.NumberofBooks = bookFromStock.NumberofBooks - numberOfBookCustomerWants;
            readingIsGoodRepository.UpdateBookAtBooksStock(bookFromStock);
            readingIsGoodDbContext.SaveChanges();
            logger.LogInformation(string.Format(ReadingIsGoodResources.Info_NewDeliveryInfoAdded, bookDeliveryInformation.Id));
            logger.LogInformation(string.Format(ReadingIsGoodResources.Info_BookStockUpdated, bookFromStock.Id));
            return Created(string.Empty, newOrder);
        }
    }
}
