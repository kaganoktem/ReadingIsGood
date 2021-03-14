using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadingIsGood.DomainInterfaces;
using ReadingIsGood.Dtos;
using ReadingIsGood.Persistence;
using ReadingIsGood.Resources;
using static ReadingIsGood.Dtos.Enumerations;

namespace ReadingIsGood.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookDeliveryController : ApiBaseController<BookDeliveryController>
    {
        public BookDeliveryController(ILogger<BookDeliveryController> logger, IReadingIsGoodRepository readingIsGoodRepository,
            ICacheService cacheService, ReadingIsGoodDbContext readingIsGoodDbContext) 
            : base(logger, readingIsGoodRepository, cacheService, readingIsGoodDbContext)
        {
        }

        [HttpPut]
        [ActionName("UpdateBookDeliveryStatus")]
        public IActionResult UpdateBookDeliveryStatus(string token, int orderId, BookDeliveryStatus bookDeliveryStatus) 
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

            var dbDeliveryResult = readingIsGoodRepository.GetBookDeliveryInformationByOrderId(orderId);
            if (dbDeliveryResult == null)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_NotFoundAnyDeliveryInfo, orderId)
                };

                logger.LogError(rex.ExceptionMessage);
                return Problem(rex.ExceptionMessage);
            }

            dbDeliveryResult.BookDeliveryStatus = bookDeliveryStatus;
            readingIsGoodRepository.UpdateDeliveryInformation(dbDeliveryResult);
            readingIsGoodDbContext.SaveChanges();
            logger.LogInformation(ReadingIsGoodResources.Infor_DeliveryInfoUpdateSuccess, dbDeliveryResult.Id);
            return Ok(dbDeliveryResult);
        }
    }
}
