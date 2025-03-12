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
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;
        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }
        public List<Usuario> GetUsers()
        {
            try
            {
                return _context.Usuarios
                    .Include(x => x.Roles)
                    .ToList();
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

        public Usuario GetUserById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.PkUsuario == id  );
        }

        public bool CreateUser(Usuario request)
        {
            try
            {
                Usuario usuario = new()
                {
                    Nombre = request.Nombre,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRole = request.FkRole
                };
                _context.Usuarios.Add(usuario);
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
        public bool CreateUserverdadero(Usuario request)
        {
            try
            {
                Usuario usuario = new()
                {
                    Nombre = request.Nombre,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRole = request.FkRole = 2
                };
                _context.Usuarios.Add(usuario);
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
        public bool UpdateUser(Usuario request)
        {
            try
            {
                _context.Usuarios.Update(request);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error al intentar actualizar el usuario " + ex.Message);
            }
        }

        public bool DeleteUser(int PkUsuario)
        {
            try
            {
                var user = GetUserById(PkUsuario);
                _context.Usuarios.Remove(user);
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

