using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Core.Convertor;
using AryanITC.Core.Services.Implementations;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.IRepository;
using AryanITC.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;


namespace AryanITC.Infra.IOC.Dependency
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            #region Services Injection

            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IViewRenderService, RenderViewToString>();

            #endregion

            #region Repositories Injection 

            service.AddScoped<IUserRepository, UserRepository>();
         

            #endregion
        }
    }
}
