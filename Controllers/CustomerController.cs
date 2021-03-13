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

namespace ReadingIsGood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ApiControllerBase<CustomerController>
    {
        public CustomerController(ILogger<CustomerController> logger, IReadingIsGoodRepository readingIsGoodRepository,
            ICacheService cacheService) : base(logger, readingIsGoodRepository, cacheService)
        {
        }

        [HttpGet]
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

            Customer newCustomer = new Customer();
            newCustomer.Address = address;
            newCustomer.Name = name;
            newCustomer.Password = password;
            newCustomer.Surname = surname;
            newCustomer.Username = username;

            var dbResult = readingIsGoodRepository.CreateNewCustomer(newCustomer);
            return Created(string.Empty, dbResult);

        }
    }
}
