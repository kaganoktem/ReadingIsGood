using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadingIsGood.DomainInterfaces;
using ReadingIsGood.Dtos;
using ReadingIsGood.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerAuthenticationController : ControllerBase
    {
        private readonly ILogger<CustomerController> logger;
        private readonly IReadingIsGoodRepository readingIsGoodRepository;
        private readonly ICacheService cacheService;

        public CustomerAuthenticationController(ILogger<CustomerController> logger, IReadingIsGoodRepository readingIsGoodRepository,
            ICacheService cacheService)
        {
            this.logger = logger;
            this.cacheService = cacheService;
            this.readingIsGoodRepository = readingIsGoodRepository;
        }

        [HttpGet]
        public IActionResult Authenticate(string username, string password)
        {
            var dbResult = readingIsGoodRepository.Authenticate(username, password);
            string token = null;
            if (dbResult)
            {
                token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                List<SessionDto> sessionList = (List<SessionDto>)cacheService.GetItem("sessionList");
                if (sessionList == null)
                {
                    sessionList = new List<SessionDto>();
                }
                
                SessionDto sessionDto = new SessionDto
                {
                    Token = token,
                    username = username
                };
                
                if (sessionList != null)
                {
                    sessionList.Add(sessionDto);
                }
                

                cacheService.AddItem("sessionList", sessionList);
                logger.LogInformation(username + " kullanıcısı giriş yaptı");
            }

            return Ok(token);
        }
    }
}
