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
    public class CustomerController : ApiBaseController<CustomerController>
    {
        public CustomerController(ILogger<CustomerController> logger, IReadingIsGoodRepository readingIsGoodRepository,
            ICacheService cacheService, ReadingIsGoodDbContext readingIsGoodDbContext)
            : base(logger, readingIsGoodRepository, cacheService, readingIsGoodDbContext)
        {
        }

        [HttpGet]
        [ActionName("GetAllCustomers")]
        public IActionResult GetAllCustomers(string token)
        {
            var authorizationResult = IsAuthorized(token);
            if (authorizationResult.IsSuccess)
            {
                var dbResult = readingIsGoodRepository.GetAllCustomers();
                return Ok(dbResult);
            }

            return Ok(authorizationResult.Rex);
        }

        [HttpPost]
        [ActionName("CreateNewCustomer")]
        public IActionResult CreateNewCustomer(string name, string surname, string username, string password, string passwordAgain, string address)
        {
            ReadingIsGoodException rex;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(passwordAgain) || string.IsNullOrEmpty(address)) 
            {
                if (string.IsNullOrEmpty(name)) 
                {
                    logger.LogError(string.Format(ReadingIsGoodResources.Error_EmptyParameter, name));
                    rex = new ReadingIsGoodException
                    {
                        ExceptionMessage = string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameof(name))
                    };

                    return Problem(rex.ExceptionMessage);
                }

                if (string.IsNullOrEmpty(surname))
                {
                    logger.LogError(string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameof(surname)));
                    rex = new ReadingIsGoodException
                    {
                        ExceptionMessage = string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameof(surname))
                    };

                    return Problem(rex.ExceptionMessage);
                }

                if (string.IsNullOrEmpty(password))
                {
                    logger.LogError(string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameof(password)));
                    rex = new ReadingIsGoodException
                    {
                        ExceptionMessage = string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameof(password))
                    };

                    return Problem(rex.ExceptionMessage);
                }

                if (string.IsNullOrEmpty(passwordAgain))
                {
                    logger.LogError(string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameof(passwordAgain)));
                    rex = new ReadingIsGoodException
                    {
                        ExceptionMessage = string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameof(passwordAgain))
                    };

                    return Problem(rex.ExceptionMessage);
                }

                if (string.IsNullOrEmpty(address))
                {
                    logger.LogError(string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameof(address)));
                    rex = new ReadingIsGoodException
                    {
                        ExceptionMessage = string.Format(ReadingIsGoodResources.Error_EmptyParameter, nameof(address))
                    };

                    return Problem(rex.ExceptionMessage);
                }
            }

            if (!password.Equals(passwordAgain))
            {
                logger.LogError(ReadingIsGoodResources.Error_PasswordIsNotMatched);
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = ReadingIsGoodResources.Error_PasswordIsNotMatched
                };

                return Problem(rex.ExceptionMessage);
            }

            var existingUser = readingIsGoodRepository.HasExistingCustomer(username);
            if (existingUser)
            {
                logger.LogError(ReadingIsGoodResources.Error_ExistingUser);
                rex = new ReadingIsGoodException
                {
                    ExceptionMessage = ReadingIsGoodResources.Error_ExistingUser
                };
                
                return Problem(rex.ExceptionMessage);
            }

            Customer newCustomer = new Customer();
            newCustomer.Address = address;
            newCustomer.Name = name;
            newCustomer.Password = password;
            newCustomer.Surname = surname;
            newCustomer.Username = username;

            readingIsGoodRepository.CreateNewCustomer(newCustomer);
            readingIsGoodDbContext.SaveChanges();
            logger.LogInformation(string.Format(ReadingIsGoodResources.Infor_UserCreateSuccess, username));
            return Created(string.Empty, newCustomer);
        }
    }
}
