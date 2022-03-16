using System;
using System.Collections.Generic;

namespace Common.Dto
{
    public class MovieDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal SalePrice { get; set; }
        public int GenreId { get; set; }
        public string Genre { get; set; }
    }
}
