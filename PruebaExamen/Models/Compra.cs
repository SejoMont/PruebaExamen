using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaExamen.Models
{
    [Table("COMPRAS")]
    public class Compra
    {
        [Key]
        [Column("IDCOMPRA")]
        public int IdCompra { get; set; }
        [Column("FECHACOMPRA")]
        public DateTime FechaCompra { get; set; }
        [Column("IDPELICULA")]
        public int IdPelicula { get; set; }
        [Column("USUARIOID")]
        public int IdUsuario { get; set; }
        [Column("CANTIDAD")]
        public int Cantidad { get; set; }
    }
}
