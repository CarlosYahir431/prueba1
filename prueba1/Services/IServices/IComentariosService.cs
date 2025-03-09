using VelazquezYahir.Models.Domain;

namespace VelazquezYahir.Services.IServices
{
    public interface IComentariosService
    {
        public List<Comentario> GetComentarios();
        public Comentario GetComentarioById(int id);
        public bool CreateComentario(Comentario request);
        public bool UpdateComentario(Comentario request);
        public bool DeleteComentario(int PkComentario);

    }
}
