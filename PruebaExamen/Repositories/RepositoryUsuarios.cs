using PruebaExamen.Data;
using PruebaExamen.Models;

namespace PruebaExamen.Repositories
{
    public class RepositoryUsuarios
    {
        private PeliculasContext context;

        public RepositoryUsuarios(PeliculasContext context)
        {
            this.context = context;
        }
        public async Task<Usuario> GetUserByEmailPasswordAsync(string email, string password)
        {
            return this.context.Usuarios.Where(x => x.Email == email && x.Password == password).AsEnumerable().FirstOrDefault();
        }
    }
}
