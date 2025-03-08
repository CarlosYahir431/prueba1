using VelazquezYahir.Models.Domain;

namespace VelazquezYahir.Services.IServices
{
        public interface IBookService
        {
            public List<Book> GetBooks();
            public bool CreateBook(Book request);
            public Book GetBookById(int id);
            public bool UpdateBook(Book request);
            public bool DeleteBook(int PkBook);
        }
}
