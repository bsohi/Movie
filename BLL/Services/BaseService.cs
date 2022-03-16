using Common.Authentication;
using System;
using System.Linq;
using System.Threading;

namespace BLL.Services
{
    /// <summary>
    /// Base class for the DAL.
    /// </summary>
    public class BaseService
    {
        private readonly IAuthenticatedUser _authenticatedUser;
        public BaseService(IAuthenticatedUser authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }
        public IAuthenticatedUser CurrentUser
        {
            get
            {
                //in case there is no authentication??
                return _authenticatedUser;
            }
        }
        public void SetUserAndDateField<T>(T entity, bool isAdd) where T : class
        {
            if (isAdd)
            {
                SetPropertyValue(entity, "CreatedUserId", _authenticatedUser.Id);
                SetPropertyValue(entity, "CreatedOn", DateTime.UtcNow);
            }

            SetPropertyValue(entity, "UpdatedUserId", _authenticatedUser.Id);
            SetPropertyValue(entity, "UpdatedOn", DateTime.UtcNow);
        }
        private void SetPropertyValue<T>(T entity, string propertyName, object value)
        {
            var propertyInfo = entity.GetType().GetProperties().FirstOrDefault(x => string.Compare(x.Name, propertyName, true) == 0);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(entity, value);
            }
        }
    }
}
