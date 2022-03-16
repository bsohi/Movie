using Common.Authentication;
using Common.DTO;
using Common.Helper;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public interface IListValuesRepo : IRepo<ListValue>
    {
        IEnumerable<SelectListValueDto> ListValuesByCategory(Enums.ListCategory listCategory);
    }
    public class ListValuesRepo : BaseRepo<ListValue>, IListValuesRepo
    {
        public ListValuesRepo(IAuthenticatedUser authenticatedUser, MovieSaaSContext saasDb) : base(authenticatedUser, saasDb) { }
        
        public IEnumerable<SelectListValueDto> ListValuesByCategory(Enums.ListCategory listCategory)
        {
            var selectListValueDto = from lv in _saasDB.ListValues
                                     where lv.ListCategoryId == (int)listCategory
                                     select new SelectListValueDto
                                     {
                                         Id = lv.Id,
                                         Name = lv.Name
                                     };

            return selectListValueDto.ToList();
        }
    }
}
