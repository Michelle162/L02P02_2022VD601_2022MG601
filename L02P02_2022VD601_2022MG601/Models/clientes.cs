using System.ComponentModel.DataAnnotations;

namespace L02P02_2022VD601_2022MG601.Models
{
    public class clientes
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? email { get; set; }
        public string? direccion { get; set; }
        public char? genero { get; set; }
        public int id_departamento { get; set; }
        public int id_puesto { get; set; }
        public char? estado_registro { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
