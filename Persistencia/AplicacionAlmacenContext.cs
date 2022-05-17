using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class AplicacionAlmacenContext : IdentityDbContext<Usuario>
    {
        public AplicacionAlmacenContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
        }
        public DbSet<CorrelativoMovimientos> CorrelativoMovimientos { get; set; }
        public DbSet<DMovimientoAlmacen> DMovimientoAlmacen { get; set; }
        public DbSet<DocumentoCompra> DocumentoCompra { get; set; }
        public DbSet<GrupoMateriales> GrupoMateriales { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<MaterialStock> MaterialStock { get; set; }
        public DbSet<Moneda> Moneda { get; set; }
        public DbSet<OperacionesAlmacen> OperacionesAlmacen { get; set; }
        public DbSet<MovimientosAlmacen> MovimientosAlmacen { get; set; }
        public DbSet<Precio> Precio { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<UnidadMedida> UnidadMedida { get; set; }
    }
}