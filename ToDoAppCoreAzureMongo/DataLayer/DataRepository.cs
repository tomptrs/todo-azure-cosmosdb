using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace DataLayer
{
    public interface IDataRepository
    {
        Task<Document> CreateItemAsync(ToDo item);

        IEnumerable<ToDo> GetItemsAsync();

    }
    public class DataRepository:IDataRepository
    {
        private readonly string Endpoint = "https://tomp.documents.azure.com:443/";
        private readonly string Key = "hhQz9YRLbupvhaTnRa7EETXwS2jA7rx2GMyMYL11dBEtqAiVnTlIRCLL8Mf9K4vSo76CzHpUbWK9Cn5t2fnc9w==";
        private readonly string DatabaseId = "ToDoList";
        private readonly string CollectionId = "ToDo";
        private DocumentClient client;

        public DataRepository()
        {
            this.client = new DocumentClient(new Uri(Endpoint), Key);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }
        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }

        //insert new ToDo Item
        async Task<Document> IDataRepository.CreateItemAsync(ToDo item)
        {
         
            var document = await client.CreateDocumentAsync(
                     UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId)
                     , item);
            return document;
            
        }

        public IEnumerable<ToDo> GetItemsAsync()
        {
            //Query Document
            IEnumerable<ToDo> lst = client.CreateDocumentQuery<ToDo>(
            UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));

            List<ToDo> results = new List<ToDo>();
            foreach(var item in lst)
            {
                results.Add(item);
            }
            return results;
           
        }
        /*
* //Query Document
Volcano rainierVolcano = client.CreateDocumentQuery<Volcano>(
                UriFactory.CreateDocumentCollectionUri(dbName,collectionName))
                .Where(v => v.VolcanoName == "Rainier")
                .AsEnumerable()
                .FirstOrDefault();   
*/

    }
}
