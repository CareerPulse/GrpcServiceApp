using GrpcServiceApp.Services;
namespace GrpcServiceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddGrpc();

            var app = builder.Build();

            app.MapGrpcService<ResumeServiceImpl>();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
