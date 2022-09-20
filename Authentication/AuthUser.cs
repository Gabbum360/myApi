using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstApi_Project.Authentication.AuthModels;
using FirstApi_Project.Authentication.Controllers;
using FirstApi_Project.DataBase;
using Microsoft.AspNetCore.Mvc;



namespace FirstApi_Project.Authentication
{
    public class AuthUser : IRepository<User>
    {
        private readonly SchoolManagementDbContext SMDContext;
        private readonly ILogger _logger;

        public AuthUser(SchoolManagementDbContext SMDb)
        {
            SMDContext = SMDb;
        }

        public AuthUser(ILogger<User> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("createUser")]
        public async Task<User> Create([FromBody]User user)
        {
            try
            {
                if (user !=null)
                {
                    var obj = SMDContext.Add<User>(user);
                    await SMDContext.SaveChangesAsync();
                    return obj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<User> Createl(User _object)
        {
            throw new NotImplementedException();
        }

        public void Delete(User user)
        {
            try
            {
                if(user !=null)
                {
                    var obj = SMDContext.Remove(user);
                    if (obj !=null)
                    {
                        SMDContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                var obj = SMDContext.Users.ToList();
                if (obj != null)
                    return obj;
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User GetById(int id)
        {
            try
            {
                if(id !=0)
                {
                    var obj = SMDContext.Users.FirstOrDefault(x => x.Id == id.ToString());
                    if (obj != null)
                        return obj;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(User user)
        {
            try
            {
                if(user !=null)
                {
                    var obj = SMDContext.Update(user);
                    if (obj != null)
                        SMDContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
