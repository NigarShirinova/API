using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Constants;

namespace Business.Dtos.Product
{
	public class ProductCreateDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Photo { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public ProductType Type { get; set; }
	}
}
