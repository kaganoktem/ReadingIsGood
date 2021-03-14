using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadingIsGood.DomainInterfaces;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using ReadingIsGood.Persistence;
using ReadingIsGood.Resources;

namespace ReadingIsGood.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BooksStockController : ApiBaseController<BooksStockController>
    {
        public BooksStockController(ILogger<BooksStockController> logger, IReadingIsGoodRepository readingIsGoodRepository, 
            ICacheService cacheService, ReadingIsGoodDbContext readingIsGoodDbContext)
            : base(logger, readingIsGoodRepository, cacheService, readingIsGoodDbContext)
        {
        }

        [HttpPost]
        [ActionName("CreateNewBookAtStock")]
        public IActionResult CreateNewBookAtStock(string token, string bookName, int numberOfBooks)
        {
            ReadingIsGoodException rex;
            var authorizationResult = IsAuthorized(token);
            if (!authorizationResult.IsSuccess)
            {
                logger.LogError(authorizationResult.Rex.ExceptionMessage);
                return Problem(authorizationResult.Rex.ExceptionMessage);
            }

            var validationEmptyParameterResult = ValidateEmptyStringParameter(nameof(bookName), bookName);
            if (!validationEmptyParameterResult.IsSuccess)
            {
                logger.LogError(validationEmptyParameterResult.Rex.ExceptionMessage);
                return Problem(validationEmptyParameterResult.Rex.ExceptionMessage);
            }

            if (numberOfBooks <= 0)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_ParameterCantBeLessThanOrEqualToZero, nameof(numberOfBooks))
                };

                logger.LogError(rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            var customerInfoResult = GetCustomerInfoFromSessionToken(token);
            if (!customerInfoResult.IsSuccess)
            {
                logger.LogError(customerInfoResult.Rex.ExceptionMessage);
                return Problem(customerInfoResult.Rex.ExceptionMessage);
            }

            var customerInfo = customerInfoResult.Result as SessionDto;

            var existingBook = readingIsGoodRepository.HasExistingBook(bookName);
            if (existingBook) 
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = ReadingIsGoodResources.Error_ExistingBook,
                };

                logger.LogError(rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            BooksStock booksStock = new BooksStock
            {
                Name = bookName,
                NumberofBooks = numberOfBooks
            };

            readingIsGoodRepository.CreateNewBooksStock(booksStock);
            readingIsGoodDbContext.SaveChanges();
            logger.LogInformation(string.Format(ReadingIsGoodResources.Infor_BookCreateSuccess, bookName));
            return Created("", booksStock);
        }

        [HttpPut]
        [ActionName("UpdateBooksStock")]
        public IActionResult UpdateBooksInStock(string token, int updatedBookId, string updatedbookName, int updatedNumberOfBooks)
        {
            ReadingIsGoodException rex;
            var authorizationResult = IsAuthorized(token);
            if (!authorizationResult.IsSuccess)
            {
                logger.LogError(authorizationResult.Rex.ExceptionMessage);
                return Problem(authorizationResult.Rex.ExceptionMessage);
            }

            if (updatedBookId <= 0)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_ParameterCantBeLessThanOrEqualToZero, nameof(updatedBookId))
                };

                logger.LogError(authorizationResult.Rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            if (updatedNumberOfBooks <= 0)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_ParameterCantBeLessThanOrEqualToZero, nameof(updatedNumberOfBooks))
                };

                logger.LogError(rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            var validationEmptyParameterResult = ValidateEmptyStringParameter(nameof(updatedbookName), updatedbookName);
            if (!validationEmptyParameterResult.IsSuccess)
            {
                logger.LogError(validationEmptyParameterResult.Rex.ExceptionMessage);
                return Problem(validationEmptyParameterResult.Rex.ExceptionMessage);
            }

            var customerInfoResult = GetCustomerInfoFromSessionToken(token);
            if (!customerInfoResult.IsSuccess)
            {
                logger.LogError(customerInfoResult.Rex.ExceptionMessage);
                return Problem(customerInfoResult.Rex.ExceptionMessage);
            }

            var customerInfo = customerInfoResult.Result as SessionDto;

            var bookEntity = readingIsGoodRepository.GetBookFromBooksStockById(updatedBookId);
            if (bookEntity == null)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_NotFoundBookAtStock, nameof(updatedNumberOfBooks))
                };

                logger.LogError(rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            bookEntity.NumberofBooks = updatedNumberOfBooks;
            bookEntity.Name = updatedbookName;

            readingIsGoodRepository.UpdateBookAtBooksStock(bookEntity);
            readingIsGoodDbContext.SaveChanges();
            logger.LogInformation(ReadingIsGoodResources.Infor_BookUpdateSuccess, bookEntity.Id);
            return Ok(bookEntity);
        }
    }
}
