using Common.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Common.Dto
{
    public class BaseDto
    {
        [JsonIgnore]
        public int CreatedUserId { get; set; }
        public string CreatedUser { get; set; }
        [JsonIgnore]
        public int UpdatedUserId { get; set; }
        public string UpdatedUser { get; set; }
        [JsonIgnore]
        public DateTime CreatedOn { get; set; }
        [JsonIgnore]
        public DateTime UpdatedOn { get; set; }

        public IList<string> PropertiesToUpdate { get; set; }
        public IDictionary<string, IEnumerable<SelectListValueDto>> ListValues { get; set; }

    }
}
