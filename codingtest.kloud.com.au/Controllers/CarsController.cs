using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace codingtest.kloud.com.au
{
    /// <summary>
    /// Cars Controller to handle car details requests
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ILogger _logger;
        private ICarsService carsService;
        public CarsController(ILogger<CarsController> logger)
        {
            _logger = logger;
            carsService = new CarsService(logger);
        }
        // GET api/values
        /// <summary>
        /// Get a number of cars, their colours and their owners in JSON
        /// </summary>
        /// <returns>An ApiResponse with JSON data with number of cars, their colours and their owners with an HTTP 200, or other relevant HTTP response codes.</returns>
        /// <response code="200">An ApiResponse with json string with an HTTP 200</response>
        /// <response code="404">Returns HTTP 404 if there is no data found</response>  
        [HttpGet]
        public ActionResult<string> Get()
        {
            var json = carsService.GetCarsJson();
            if (!string.IsNullOrEmpty(json))
                return json;
            else
            {
                _logger.LogError($"Unable to get data from cars.json");
                return StatusCode((int)HttpStatusCode.NotFound, "");
            }
        }
        /// <summary>
        ///  Get a number of cars, their colours and brand for specific owner
        /// </summary>
        /// <param name="ownername">Enter the owner name</param>
        /// <returns>An ApiResponse with JSON data with number of cars, their colours and brand for specific owner</returns>
        /// <response code="200">An ApiResponse with json string with an HTTP 200</response>
        /// <response code="404">Returns HTTP 404 if there is no data found</response>  
        // GET api/values/David
        [HttpGet("GetJsonByOwner")]
        public ActionResult<string> GetJsonByOwner(string ownername)
        {
            //if(string.IsNullOrEmpty(ownername))
            //    return StatusCode((int)HttpStatusCode.NotAcceptable);
            var json = carsService.GetCarsJsonByOwner(ownername);
            if (!string.IsNullOrEmpty(json))
                return json;
            else
            {
                _logger.LogError($"Unable to get data from cars.json or owner doesnot exists");
                return StatusCode((int)HttpStatusCode.NotFound, "");
            }
        }
        /// <summary>
        /// Get owner list based on the given car colour
        /// </summary>
        /// <param name="colour"></param>
        /// <returns>An ApiResponse with owner name list for specific car colour</returns>
        /// <response code="200">An ApiResponse with json string with an HTTP 200</response>
        /// <response code="404">Returns HTTP 404 if there is no data found</response>
        [HttpGet("GetOwnersByColour")]
        public ActionResult<string[]> GetOwnersByColour(string colour)
        {
            var owners = carsService.GetOwnersByColour(colour);
            if (owners.Count() > 0)
                return owners;
            else
            {
                _logger.LogError($"Unable to get data from cars.json or colour doesnot exists");
                return StatusCode((int)HttpStatusCode.NotFound, "");
            }
        }
        /// <summary>
        /// Get owner list based on the given car brand
        /// </summary>
        /// <param name="brand"></param>
        /// <returns>An ApiResponse with owner name list for specific car colour</returns>
        /// <response code="200">An ApiResponse with list of owner names with an HTTP 200</response>
        /// <response code="404">Returns HTTP 404 if there is no data found</response>
        [HttpGet("GetOwnersByBrand")]
        public ActionResult<string[]> GetOwnersByBrand(string brand)
        {
            var owners = carsService.GetOwnersByBrand(brand);
            if (owners.Count() > 0)
                return owners;
            else
            {
                _logger.LogError($"Unable to get data from cars.json or brand doesnot exists");
                return StatusCode((int)HttpStatusCode.NotFound, "");
            }
        }
    }
}
