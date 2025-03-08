using VelazquezYahir.Models.Domain;

namespace VelazquezYahir.Services.IServices
{
    public interface ICategoriaService
    {
        public List<Categoria> GetCategorias();
        public Categoria GetCategoriaById(int id);
        public bool CreateCategoria(Categoria request);
        public bool UpdateCategoria(Categoria request); 
        public bool DeleteCategoria(int PkCategoria);    
    }
}
