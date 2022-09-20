using FirstApi_Project.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FirstApi_Project.Authentication;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using FirstApi_Project.Authentication.AuthModels;

namespace FirstApi_Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       // private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SchoolManagementDbContext>(X => X.UseSqlServer(Configuration.GetConnectionString("CoreServiceDb")));
            // services.AddResponseCaching();
            services.AddControllers();
            services.AddRazorPages();
            //another pattern of the neXt line is already used...
            /*            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            */
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FirstApi_Project", Version = "v1" });
            });
            //AddCors for Policy
            services.AddCors(X =>
            {
                X.AddPolicy( "CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()   
                    .AllowCredentials()
                    .Build());


            });

            //for identity
            services.AddIdentity<User,IdentityRole>()
                .AddEntityFrameworkStores<SchoolManagementDbContext>()
                .AddDefaultTokenProviders();

            //adding authentication
            services.AddAuthentication(X =>
            {
                X.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                X.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                X.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            //Adding Jwt Bearer
            .AddJwtBearer(X =>
            {
                X.SaveToken = true;
                X.RequireHttpsMetadata = false;
                X.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudiece"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:IssuerSigningKey"]))
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FirstApi_Project v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("CorsPolicy");

            // app.UseResponseCaching();

            app.UseAuthorization();

            app.UseAuthentication();
            
            app.UseEndpoints(endpoints =>
            {
               /* endpoints.MapGet("/echo",
                    context => context.Response.WriteAsync("echo"))
                .RequireCors(MyAllowSpecificOrigins);*/

                endpoints.MapControllers(); //from Boss
                endpoints.MapRazorPages();
            });
        }
    }
}
