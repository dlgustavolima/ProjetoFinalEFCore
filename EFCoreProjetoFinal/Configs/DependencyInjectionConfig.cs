using EFCoreProjetoFinal.Data;
using EFCoreProjetoFinal.Data.Repository;
using EFCoreProjetoFinal.Services;

namespace EFCoreProjetoFinal.Configs
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSqlServer<ApplicationContext>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFCoreProjetoFinal;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=False; MultipleActiveResultSets=true;");

            services.AddScoped<ICodigoAcessoRepository, CodigoAcessoRepository>();
            services.AddScoped<IEstudioRepository, EstudioRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IPlataformaRepository, PlataformaRepository>();

            services.AddScoped<ICodigoAcessoService, CodigoAcessoService>();
            services.AddScoped<IEstudioService, EstudioService>();
            services.AddScoped<IGeneroService, GeneroService>();
            services.AddScoped<IJogoService, JogoService>();
            services.AddScoped<IPlataformaService, PlataformaService>();

            return services;
        }

    }
}
