using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal SalePrice { get; set; }
        public int GenreId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedUserId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int? UpdatedUserId { get; set; }

        public virtual ListValue Genre { get; set; }
    }
}
