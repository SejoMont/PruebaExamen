using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaExamen.Models
{
    [Table("PELICULAS")]
    public class Pelicula
    {
        [Key]
        [Column("IdPelicula")]
        public int IdLibro { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Director")]
        public string Director { get; set; }
        [Column("Imagen")]
        public string Imagen { get; set; }
        [Column("Sinopsis")]
        public string Sinopsis { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
        [Column("IdGenero")]
        public int IdGenero { get; set; }
    }
}
