using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using API_Eindopdracht_DeviceProgramming.Models;
using API_Eindopdracht_DeviceProgramming.TableEntitys;
using System.Collections.Generic;
using Eindopdracht.Models;

namespace API_Eindopdracht_DeviceProgramming
{
    public class Function1
    {
        [FunctionName("PutPostReviewV1")]
        public async Task<IActionResult> PutPostReviewV1(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", "post", Route = "v1/reviews")] HttpRequest req,
            ILogger log)
        {

            try
            {
                
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Review rev = JsonConvert.DeserializeObject<Review>(requestBody);
                if (rev.Id == null || rev.Id == "")
                {
                    rev.Id = Guid.NewGuid().ToString();
                }
                var connectionString = Environment.GetEnvironmentVariable("ConnectionStringStorage");

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference("reviews");

                ReviewEntity registrationEntity = new ReviewEntity(rev.BookId.ToString(), rev.Id)
                {
                    Message = rev.Message,
                    Stars = rev.Stars,
                    
                    
                };

                //if(req.Method == "post")
                //{
                    TableOperation insertOperation = TableOperation.Insert(registrationEntity);
                    await table.ExecuteAsync(insertOperation);
                //}
                //else
                //{
                //    TableOperation UpdateOperation = TableOperation.Replace(registrationEntity);
                //    await table.ExecuteAsync(UpdateOperation);
                //}
                
                //TableOperation update = TableOperation.Update()
                


               

                return new OkObjectResult(rev);
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex);
            }
        }

        [FunctionName("SelectReviewV1")]
        public async Task<IActionResult> SelectReviewV1(
              [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/reviews/{bookId}")] HttpRequest req,
              string bookId,
              ILogger log)
        {
            try
            {
                

                var connectionString = Environment.GetEnvironmentVariable("ConnectionStringStorage");

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference("reviews");

                TableQuery<ReviewEntity> query = new TableQuery<ReviewEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, bookId));
                var result = await table.ExecuteQuerySegmentedAsync<ReviewEntity>(query, null);

                List<Review> registrations = new List<Review>();

                foreach (var review in result.Results)
                {
                    registrations.Add(new Review()
                    {
                        BookId = Convert.ToInt32(review.PartitionKey),
                        Id = review.RowKey,
                        Message = review.Message,
                        Stars = review.Stars,
                        Date = review.Timestamp.LocalDateTime


                    }) ;
                }
                registrations.Sort();
                return new OkObjectResult(registrations);
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex);
            }

        }

        [FunctionName("PutHartV1")]
        public async Task<IActionResult> PutHartV1(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/hart")] HttpRequest req,
            ILogger log)
        {

            try
            {
                var connectionString = Environment.GetEnvironmentVariable("ConnectionStringStorage");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Hart hart = JsonConvert.DeserializeObject<Hart>(requestBody);
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference("Harts");


                if (hart.Id == "" && hart.Id == null)
                {
                    hart.Id = Guid.NewGuid().ToString();
                    HartEntity registrationEntity = new HartEntity(hart.BookId.ToString(), hart.Id)
                    {
                        Like = hart.Like
                    };
                    TableOperation insertOperation = TableOperation.Insert(registrationEntity);
                    await table.ExecuteAsync(insertOperation);
                }
                else
                {
                    HartEntity registrationEntity = new HartEntity(hart.BookId.ToString(), hart.Id)
                    {
                        Like = hart.Like
                    };
                    TableOperation DeleteOperation = TableOperation.Merge(registrationEntity);
                    var i = await table.ExecuteAsync(DeleteOperation);
                    Console.WriteLine(i.ToString());
                }
                

                

               

                //if(req.Method == "post")
                //{
                
                //}
                //else
                //{
                //    TableOperation UpdateOperation = TableOperation.Replace(registrationEntity);
                //    await table.ExecuteAsync(UpdateOperation);
                //}

                //TableOperation update = TableOperation.Update()





                return new OkObjectResult(hart);
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex);
            }
        }

        [FunctionName("SelectHartV1")]
        public async Task<IActionResult> SelectHartV1(
              [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/hart/{bookId}")] HttpRequest req,
              string bookId,
              ILogger log)
        {
            try
            {


                var connectionString = Environment.GetEnvironmentVariable("ConnectionStringStorage");

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference("Harts");

                TableQuery<HartEntity> query = new TableQuery<HartEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, bookId));
                var result = await table.ExecuteQuerySegmentedAsync<HartEntity>(query, null);

                List<Hart> harts = new List<Hart>();

                foreach (var hart in result.Results)
                {
                    harts.Add(new Hart()
                    {
                        BookId = Convert.ToInt32(hart.PartitionKey),
                        Id = hart.RowKey,
                        Like = hart.Like


                    });
                }
                
                return new OkObjectResult(harts);
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex);
            }

        }
    }
}
