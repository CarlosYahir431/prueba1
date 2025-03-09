using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VelazquezYahir.Context;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Services.IServices;

namespace VelazquezYahir.Services.Services
{
    public class ComentariosService : IComentariosService
    {

        private readonly ApplicationDBContext _context;
        public ComentariosService(ApplicationDBContext context)
        {
            _context = context;
        }
        public List<Comentario> GetComentarios()
        {
            try
            {
                return _context.Comentarios.Include(x => x.Book).ToList();
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
        public Comentario GetComentarioById(int id)
        {
            return _context.Comentarios.FirstOrDefault(u => u.PkComentario == id);
        }

        public bool CreateComentario(Comentario request)
        {
            try
            {
                Comentario comentario = new()
                {
                    PkBook = request.PkBook,
                    Comentarios = request.Comentarios,
                };
                _context.Comentarios.Add(comentario);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error al intentar crear el comentario: " + ex.Message);
            }
        }
        public bool UpdateComentario(Comentario request)
        {
            try
            {
                _context.Comentarios.Update(request);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error al intentar actualizar el libro " + ex.Message);
            }
        }

        public bool DeleteComentario(int PkComentario)
        {
            try
            {
                var Comentario = GetComentarioById(PkComentario);
                _context.Comentarios.Remove(Comentario);
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
