using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(AplicacionAlmacenContext context, UserManager<Usuario> usuarioManager){
             if(!usuarioManager.Users.Any()){
                var usuario = new Usuario{NombreCompleto = "Maria Ramos", UserName="mramos", Email="mariaramos@gmail.com"};
                await usuarioManager.CreateAsync(usuario, "ValeSofia12$");
            }
        }
    }
}