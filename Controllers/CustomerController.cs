using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadingIsGood.DomainInterfaces;
using ReadingIsGood.Dtos;
using ReadingIsGood.Persistence;
using ReadingIsGood.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadingIsGood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> logger;
        private readonly IReadingIsGoodRepository readingIsGoodRepository;
        private readonly ICacheService cacheService;

        public CustomerController(ILogger<CustomerController> logger, ICacheService cacheService,
            IReadingIsGoodRepository readingIsGoodRepository)
        {
            this.logger = logger;
            this.cacheService = cacheService;
            this.readingIsGoodRepository = readingIsGoodRepository;
            logger.LogInformation("Let's start coding");
        }

        [HttpGet]
        public IActionResult GetAllCustomers(string token)
        {
            var serviceResult = IsAuthorized(token);
            if (serviceResult.IsSuccess)
            {
                var dbResult = readingIsGoodRepository.GetAllCustomers();
                return Ok(dbResult);
            }

            return Ok(serviceResult.Rex);
        }



        private ServiceResultDto IsAuthorized(string token) 
        {
            ReadingIsGoodException rex;
            ServiceResultDto serviceResult;
            if (string.IsNullOrEmpty(token))
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_InvalidParameter, nameof(token))
                };

                logger.LogInformation(string.Format(ReadingIsGoodResources.Error_InvalidParameter, nameof(token)));
                serviceResult = new ServiceResultDto
                {
                    IsSuccess = false,
                    Rex = rex
                };

                return serviceResult;
            }

            var sessionObject = cacheService.GetItem(ReadingIsGoodResources.CacheSessionListKey);
            List<SessionDto> sessionList = sessionObject as List<SessionDto>;
            if (sessionList == null)
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = ReadingIsGoodResources.Error_NotAuthorizedUser
                };

                serviceResult = new ServiceResultDto
                {
                    IsSuccess = false,
                    Rex = rex
                };

                return serviceResult;
            }

            foreach (var session in sessionList)
            {
                if (session.Token.Equals(token))
                {
                    logger.LogInformation(string.Format(ReadingIsGoodResources.Info_AuthorizationSuccess, session.username));
                    serviceResult = new ServiceResultDto
                    {
                        IsSuccess = true,
                        Result = true
                    };

                    return serviceResult;
                }
            }

            logger.LogError(ReadingIsGoodResources.Error_NotAuthorizedUser);
            serviceResult = new ServiceResultDto
            {
                IsSuccess = false,
            };

            return serviceResult;
        }
    }
}
