using AMS.DAL;
using AMS.Util;
using AMS.Util.Model;
using AMS.Web.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace AMS.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebHostEnvironment = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (WebHostEnvironment.IsDevelopment())
            {
                //在视图中修改之后，只需保存之后，刷新页面即可看到最新的效果
                services.AddRazorPages().AddRazorRuntimeCompilation();
            }
            //解决视图中的中文被html编码
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddControllersWithViews(options =>
            {
                //添加全局异常过滤器
                options.Filters.Add<GlobalExceptionFilter>();
                //Controller Model Binding 处理
                options.ModelMetadataDetailsProviders.Add(new ModelBindingMetadataProvider());
            }).AddNewtonsoftJson(options =>
            {
                //返回数据首字母不小写，CamelCasePropertyNamesContractResolver是小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            GlobalContext.SystemConfig = Configuration.GetSection("SystemConfig").Get<SystemConfig>();
            var connectionString = "";
            if (GlobalContext.SystemConfig.WorkPlace == "work")
            {
                connectionString = GlobalContext.SystemConfig.SqlServerConnection_work;
            }
            else
            {
                connectionString = GlobalContext.SystemConfig.SqlServerConnection_home;
            }
            services.AddDbContext<AMSDBContext>(options => options.UseSqlServer(connectionString, b => b.CommandTimeout(GlobalContext.SystemConfig.DBCommandTimeout)));

            services.AddMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
