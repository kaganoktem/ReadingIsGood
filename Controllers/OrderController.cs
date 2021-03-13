using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadingIsGood.DomainInterfaces;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using ReadingIsGood.Persistence;
using ReadingIsGood.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ApiControllerBase<OrderController>
    {
        public OrderController(ILogger<OrderController> logger, IReadingIsGoodRepository readingIsGoodRepository,
            ICacheService cacheService) : base(logger, readingIsGoodRepository, cacheService)
        {
        }

        [HttpPost]
        public IActionResult CreateNewOrder(string token, string bookName, int numberOfBookCustomerWants)
        {
            ReadingIsGoodException rex;
            var validationEmptyParameterResult = ValidateEmptyStringParameter(nameof(bookName), bookName);
            if (!validationEmptyParameterResult.IsSuccess)
            {
                return Problem(validationEmptyParameterResult.Rex.ExceptionMessage);
            }

            if (numberOfBookCustomerWants <= 0) 
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_ParameterCantBeLessThanOrEqualToZero, nameof(numberOfBookCustomerWants))
                };

                return Problem(rex.ExceptionMessage);
            }

            var authorizationResult = IsAuthorized(token);
            if (!authorizationResult.IsSuccess)
            {
                return Problem(authorizationResult.Rex.ExceptionMessage);
            }

            var customerInfoResult = GetCustomerInfoFromSessionToken(token);
            if (!customerInfoResult.IsSuccess) 
            {
                return Problem(customerInfoResult.Rex.ExceptionMessage);
            }

            var bookFromStock = readingIsGoodRepository.GetBookFromStockByBookName(bookName);
            
            if (bookFromStock == null)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = ReadingIsGoodResources.Error_NotFoundBookAtStock
                };

                return Problem(rex.ExceptionMessage);
            }

            if (bookFromStock.NumberofBooks < numberOfBookCustomerWants)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = ReadingIsGoodResources.Error_NotEnoughBookAtStock
                };

                return Problem(rex.ExceptionMessage);
            }

            // Create Order and Delivery Information
            var customerInfo = customerInfoResult.Result as SessionDto;
            Order newOrder = new Order();
            newOrder.BookId = bookFromStock.Id;
            newOrder.CustomerId = customerInfo.userId;
            newOrder.OrderDate = DateTime.Now;

            BookDeliveryInformation bookDeliveryInformation = new BookDeliveryInformation();
            bookDeliveryInformation.OrderDate = newOrder.OrderDate;
            bookDeliveryInformation.IsDelivered = false;
            bookDeliveryInformation.BookDeliveryStatus = Enumerations.BookDeliveryStatus.GettingPrepared;
            bookDeliveryInformation.BookId = newOrder.BookId;

            var dbResultOrder = readingIsGoodRepository.CreateNewOrder(newOrder);
            var dbResultDeliveryInfo = readingIsGoodRepository.CreateNewDeliveryInformation(bookDeliveryInformation);
            return Created(string.Empty, dbResultOrder);
        }
    }
}
