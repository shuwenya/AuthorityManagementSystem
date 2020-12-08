using AMS.Util;
using AMS.Util.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace AMS.DAL
{
    public class AMSDBContextFactory : IDesignTimeDbContextFactory<AMSDBContext>
    {
        public AMSDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            GlobalContext.SystemConfig = configuration.GetSection("SystemConfig").Get<SystemConfig>();
            Console.WriteLine("systemConfig:"+GlobalContext.SystemConfig.WorkPlace);
            var builder = new DbContextOptionsBuilder<AMSDBContext>();
            string sqlConnectionString = "";
            if (GlobalContext.SystemConfig.WorkPlace == "work")
            {
                sqlConnectionString = GlobalContext.SystemConfig.SqlServerConnection_work;
            }
            else
            {
                sqlConnectionString = GlobalContext.SystemConfig.SqlServerConnection_home;
            }
            
            builder.UseSqlServer(sqlConnectionString, p => p.CommandTimeout(GlobalContext.SystemConfig.DBCommandTimeout));
            return new AMSDBContext(builder.Options);
        }
    }
}
