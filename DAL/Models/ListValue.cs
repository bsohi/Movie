using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ListValue
    {
        public ListValue()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int ListCategoryId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedUserId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int? UpdatedUserId { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
