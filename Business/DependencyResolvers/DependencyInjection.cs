using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Contexts;
using DataAccess.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<HirdavatContext>();
            services.AddScoped<IUoW, UoW>();
            services.AddScoped<IIlDal, IlDal>();
            services.AddScoped<IIlService, IlManager>();
            services.AddScoped<IIlceDal, IlceDal>();
            services.AddScoped<IIlceService, IlceManager>();
            services.AddScoped<IKategoriDal, KategoriDal>();
            services.AddScoped<IKategoriService, KategoriManager>();
            services.AddScoped<IMarkaDal, MarkaDal>();
            services.AddScoped<IMarkaService, MarkaManager>();
            services.AddScoped<IMusteriDal, MusteriDal>();
            services.AddScoped<IMusteriService, MusteriManager>();
            services.AddScoped<ISiparisDal, SiparisDal>();
            services.AddScoped<ISiparisService, SiparisManager>();
            services.AddScoped<ISiparisDetayDal, SiparisDetayDal>();
            services.AddScoped<ISiparisDetayService, SiparisDetayManager>();
            services.AddScoped<ISiparisDurumDal, SiparisDurumDal>();
            services.AddScoped<ISiparisDurumService, SiparisDurumManager>();
            services.AddScoped<IUrunDal, UrunDal>();
            services.AddScoped<IUrunService, UrunManager>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IRolDal, RolDal>();
            services.AddScoped<IRolService, RolManager>();
            services.AddScoped<IMusteriRolDal, MusteriRolDal>();
            services.AddScoped<IMusteriRolService, MusteriRolManager>();

            return services;
        }
    }
}
