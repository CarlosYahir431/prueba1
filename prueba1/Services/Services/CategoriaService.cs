using Microsoft.Data.SqlClient;
using VelazquezYahir.Context;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Services.IServices;

namespace VelazquezYahir.Services.Services
{
    public class CategoriaService:ICategoriaService
    {

            private readonly ApplicationDBContext _context;
            public CategoriaService(ApplicationDBContext context)
            {
                _context = context;
            }
            public List<Categoria> GetCategorias()
            {
                try
                {
                    return _context.Categorias.ToList();
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

            public Categoria GetCategoriaById(int id)
            {
                return _context.Categorias.FirstOrDefault(u => u.PkCategoria == id);
            }

            public bool CreateCategoria(Categoria request)
            {
                try
                {
                    Categoria categoria = new()
                    {
                        Nombre = request.Nombre,
                    };
                    _context.Categorias.Add(categoria);
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
            public bool UpdateCategoria(Categoria request)
            {
                try
                {
                    _context.Categorias.Update(request);
                    int result = _context.SaveChanges();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Sucedio un error al intentar actualizar el libro " + ex.Message);
                }
            }

            public bool DeleteCategoria(int PkCategoria)
            {
                try
                {
                    var categoria = GetCategoriaById(PkCategoria);
                    _context.Categorias.Remove(categoria);
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




