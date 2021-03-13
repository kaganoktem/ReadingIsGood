using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadingIsGood.DomainInterfaces;
using ReadingIsGood.Dtos;
using ReadingIsGood.Persistence;
using ReadingIsGood.Resources;
using System;
using System.Collections.Generic;

namespace ReadingIsGood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerAuthenticationController : ApiControllerBase<CustomerAuthenticationController>
    {
        public CustomerAuthenticationController(ILogger<CustomerAuthenticationController> logger,
            IReadingIsGoodRepository readingIsGoodRepository, ICacheService cacheService)
            : base(logger, readingIsGoodRepository, cacheService)
        {
        }

        [HttpGet]
        public IActionResult Authenticate(string username, string password)
        {
            var userId = readingIsGoodRepository.Authenticate(username, password);
            string token = null;
            if (userId != 0)
            {
                token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                List<SessionDto> sessionList = (List<SessionDto>)cacheService.GetItem(ReadingIsGoodResources.CacheSessionListKey);
                if (sessionList == null)
                {
                    sessionList = new List<SessionDto>();
                }
                
                SessionDto sessionDto = new SessionDto
                {
                    Token = token,
                    username = username,
                    userId = userId
                };
                
                if (sessionList != null)
                {
                    sessionList.Add(sessionDto);
                }
                

                cacheService.AddItem(ReadingIsGoodResources.CacheSessionListKey, sessionList);
                logger.LogInformation(string.Format(ReadingIsGoodResources.Info_AuthenticationSuccess, username));
                return Ok(token);
            }

            return Problem(ReadingIsGoodResources.Error_UserInfosAreNotValid);
        }
    }
}
