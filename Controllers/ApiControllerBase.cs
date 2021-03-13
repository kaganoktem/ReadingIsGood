using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadingIsGood.DomainInterfaces;
using ReadingIsGood.Dtos;
using ReadingIsGood.Persistence;
using ReadingIsGood.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Controllers
{
    public class ApiControllerBase<T> : ControllerBase
    {
        public readonly ILogger<T> logger;
        public readonly IReadingIsGoodRepository readingIsGoodRepository;
        public readonly ICacheService cacheService;

        public ApiControllerBase(ILogger<T> logger, IReadingIsGoodRepository readingIsGoodRepository,
            ICacheService cacheService)
        {
            this.logger = logger;
            this.readingIsGoodRepository = readingIsGoodRepository;
            this.cacheService = cacheService;
        }

        public ServiceResultDto IsAuthorized(string token)
        {
            ServiceResultDto serviceResult = new ServiceResultDto();
            var validationEmptyParameterResult = ValidateEmptyStringParameter(nameof(token), token);
            if (!validationEmptyParameterResult.IsSuccess)
            {
                return validationEmptyParameterResult;
            }

            var validationCacheSessionResult = ValidateAndGetSessionFromCache();
            if (!validationCacheSessionResult.IsSuccess)
            {
                return validationCacheSessionResult;
            }

            List<SessionDto> sessionList = validationCacheSessionResult.Result as List<SessionDto>;
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

        private ServiceResultDto ValidateAndGetSessionFromCache()
        {
            ServiceResultDto serviceResult = new ServiceResultDto();
            ReadingIsGoodException rex;
            var sessionObject = cacheService.GetItem(ReadingIsGoodResources.CacheSessionListKey);
            var sessionList = sessionObject as List<SessionDto>;
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

            serviceResult = new ServiceResultDto
            {
                IsSuccess = true,
                Result = sessionList
            };

            return serviceResult;
        }

        public ServiceResultDto GetCustomerInfoFromSessionToken(string token)
        {
            ServiceResultDto serviceResult;
            ReadingIsGoodException rex;
            var validationResult =  ValidateEmptyStringParameter(nameof(token), token);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var validationCacheSessionResult = ValidateAndGetSessionFromCache();
            if (!validationCacheSessionResult.IsSuccess)
            {
                return validationCacheSessionResult;
            }

            List<SessionDto> sessionList = validationCacheSessionResult.Result as List<SessionDto>;

            foreach (var session in sessionList)
            {
                if (session.Token.Equals(token)) 
                {
                    serviceResult = new ServiceResultDto
                    {
                        IsSuccess = true,
                        Result = session
                    };

                    return serviceResult;
                }
            }

            rex = new ReadingIsGoodException
            {
                ExceptionMessage = ReadingIsGoodResources.Error_SessionNotFound
            };

            serviceResult = new ServiceResultDto
            {
                IsSuccess = false,
                Rex = rex
            };

            return serviceResult;
        }

        public ServiceResultDto ValidateEmptyStringParameter(string nameParameter, string parameterToValidate)
        {
            ReadingIsGoodException rex;
            ServiceResultDto serviceResult;
            if (string.IsNullOrEmpty(parameterToValidate))
            {
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameParameter)
                };

                logger.LogInformation(string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameParameter));
                serviceResult = new ServiceResultDto
                {
                    IsSuccess = false,
                    Rex = rex
                };

                return serviceResult;
            }

            serviceResult = new ServiceResultDto
            {
                IsSuccess = true
            };

            return serviceResult;
        }
    }
}
