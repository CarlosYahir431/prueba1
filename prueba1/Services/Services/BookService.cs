using Azure.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VelazquezYahir.Context;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Services.IServices;

namespace VelazquezYahir.Services.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDBContext _context;
        public BookService(ApplicationDBContext context)
        {
            _context = context;
        }
        public List<Book> GetBooks()
        {
            try
            {
                return _context.Books.Include(x=> x.Categorias).ToList();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de SQL Server: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado: " + ex.Message);
            }
        }

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(u => u.PkBook == id  );
        }

        public bool CreateBook(Book request)
        {
            try
            {

                Book libro = new()
                {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    Autor = request.Autor,
                    Img = request.Img,
                    Categoria = request.Categoria,
                };

                _context.Books.Add(libro);
                int result = _context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("sucedio un error al intentar crear el usuario" + ex.Message);
            }
        }
        public bool UpdateBook(Book request)
        {
            try
            {
                _context.Books.Update(request);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error al intentar actualizar el libro " + ex.Message);
            }
        }

        public bool DeleteBook(int PkBook)
        {
            try
            {
                var libro = GetBookById(PkBook);
                _context.Books.Remove(libro);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error al intentar eliminar el usuario" + ex.Message);
            }
        }
    }
}

