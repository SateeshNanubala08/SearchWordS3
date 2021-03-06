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
         
        private readonly ServiceConfiguration _settings;
        private readonly IAmazonS3 _S3Bucket;

        private ILogger<SearchwordController> logger;
        IAmazonS3 S3Client { get; set; }
        public SearchwordController(IOptions<ServiceConfiguration> settings, ILogger<SearchwordController> logger)
        { 
            this.S3Client = new AmazonS3Client(RegionEndpoint.USEast1); // s3Client;

            this._settings = settings.Value;
            this._S3Bucket = new AmazonS3Client(this._settings.AWSS3.AccessKey, this._settings.AWSS3.SecretKey, RegionEndpoint.USEast1); 

            this.logger = logger;
        }
         

        [HttpGet("{word}")]
        public async Task<List<string>> Get(string word)
        { 

            int ObjCnt = 0;
            int filCnt = 0;
            int wordCnt = 0;
            List<string> list = new List<string>();
            List<string> listObjlist = new List<string>(); 

            try
            {

                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = _settings.AWSS3.BucketName,
                    MaxKeys = 10
                };

                ListObjectsV2Response response;
                do
                {


                    response = await S3Client.ListObjectsV2Async(request);

                    foreach (S3Object entery in response.S3Objects)
                    {
                        list.Add(entery.Key);

                        GetObjectRequest request1 = new GetObjectRequest();

                        request1.BucketName = _settings.AWSS3.BucketName;
                        request1.Key = entery.Key.ToString();

                        GetObjectResponse response1 = await S3Client.GetObjectAsync(request1);

                        StreamReader reader = new StreamReader(response1.ResponseStream);

                        string content = reader.ReadToEnd();

                        if (entery.Key.Contains(word))
                        {
                            ObjCnt++;
                        }

                        if (content != "")
                        { 
                            string[] Spltwords = content.Split(new Char[] { ' ', ',' });

                            wordCnt = Spltwords.Count();
                            foreach (string Spltword in Spltwords)
                            {
                                if (Spltword.Contains(word))
                                { 
                                    filCnt++;
                                }
                            }
                           
                        }


                        listObjlist.Add(entery.Key.ToString() + ":: Count of Word - " + filCnt.ToString() + ":: No of words Count - " + wordCnt.ToString());
                         

                        filCnt = 0;
                        wordCnt = 0;
                    }



                } while (response.IsTruncated);

                listObjlist.Add("");
                listObjlist.Add("No of File Count " + "::" + ObjCnt.ToString());

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    logger.LogInformation("400");
                    listObjlist.Add("400");
                }
                else
                {
                    logger.LogInformation("amazonS3Exception.Message");
                    listObjlist.Add(amazonS3Exception.Message);
                }


            }
            catch (Exception ex)
            {
                logger.LogInformation("Error in process Create Data" + ex.Message);
                listObjlist.Add("Error in process Create Data" + ex.Message);
            }


            return listObjlist;
        }


         

    }
}
