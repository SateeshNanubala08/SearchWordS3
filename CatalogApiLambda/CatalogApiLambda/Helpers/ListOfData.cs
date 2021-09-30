using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiLambda.Helpers
{
    public class ListOfData
    {
        public List<string> GetListofData()
        {
            //List<string> list = new List<string>();
            //List<string> listObjlist = new List<string>();

            //ListObjectsV2Request request = new ListObjectsV2Request
            //{
            //    BucketName = settings.AWSS3.BucketName,
            //    MaxKeys = 10
            //};

            //ListObjectsV2Response response;
            //do
            //{
            //    response = await S3Client.ListObjectsV2Async(request);

            //    foreach (S3Object entery in response.S3Objects)
            //    {
            //        list.Add(entery.Key);

            //        GetObjectRequest request1 = new GetObjectRequest();

            //        request1.BucketName = _settings.AWSS3.BucketName;
            //        request1.Key = entery.Key.ToString();

            //        GetObjectResponse response1 = await S3Client.GetObjectAsync(request1);

            //        StreamReader reader = new StreamReader(response1.ResponseStream);

            //        string content = reader.ReadToEnd();

            //        if (content == "")
            //        {
            //            ObjCnt++;
            //        }
            //        else
            //        {
            //            if (content.Contains("sateesh"))
            //            {
            //                filCnt++;
            //            }
            //        }


            //    }


            //} while (response.IsTruncated);

             

            //return list;
            return null;

        }
    }
}
