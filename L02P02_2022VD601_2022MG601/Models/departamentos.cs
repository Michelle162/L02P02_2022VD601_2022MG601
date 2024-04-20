using System.ComponentModel.DataAnnotations;

namespace L02P02_2022VD601_2022MG601.Models
{
    public class departamentos
    {
        [Key]
        public int id { get; set; }
        public string? departamento { get; set; }
        public char? estado { get; set; }
        public DateTime? created_at { get; set; }

    }
}
