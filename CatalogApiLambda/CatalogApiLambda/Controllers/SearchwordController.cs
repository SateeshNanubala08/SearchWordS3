using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using CatalogApiLambda.Helpers;
using CatalogApiLambda.Models; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiLambda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchwordController : ControllerBase
    {
        private readonly IListRepository listRepository; 
        private ILogger<ListRepository> logger;

        public SearchwordController(ILogger<ListRepository> logger ,IListRepository _lr)
        {
            listRepository = _lr; 
            this.logger = logger;
        } 
 
        [HttpGet("GetData")]
        public async Task<List<string>> GetData(string word)
        {

            List<string> list = new List<string>();

            try
            {
                list = await listRepository.GetListOfData(word);
                return list;
            }
            catch (Exception ex)
            { 
                logger.LogInformation("Error in process Data" + ex.Message);
                list.Add("Error in process Data" + ex.Message);
                return list;
            }

          
        }



    }
}
