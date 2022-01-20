using MongoDB.Driver;
using MongoDBTestApp.Models;

namespace MongoDBTestApp.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _book;

        public BookService(IConfiguration config)
        {
            // Connects to MongoDB.
            var client = new MongoClient(config.GetConnectionString("BookDB"));
            // Gets the BookDB.
            var database = client.GetDatabase("BookStore");
            //Fetches the Book collection.
            _book = database.GetCollection<Book>("Books");
        }

        public async Task<List<Book>> Get()
        {
            //Gets all Books. 
            return await _book.Find(s => true).ToListAsync();
        }

        public async Task<Book> Get(string id)
        {
            //Get a single Book. 
            return await _book.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Book> Create(Book s)
        {
            //Create a Book.
            await _book.InsertOneAsync(s);
            return s;
        }

        public async Task<Book> Update(string id, Book s)
        {
            // Updates and existing Book. 
            await _book.ReplaceOneAsync(su => su.Id == id, s);
            return s;
        }


        public async Task Remove(string id)
        {
            //Removes a Book.
            await _book.DeleteOneAsync(su => su.Id == id);
        }

    }
}
