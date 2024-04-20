using Microsoft.EntityFrameworkCore;

namespace L02P02_2022VD601_2022MG601.Models
{
	public class usuariosDBContex : DbContext	{
		public usuariosDBContex(DbContextOptions options) : base(options) 
		{
		
		}
	}
}
