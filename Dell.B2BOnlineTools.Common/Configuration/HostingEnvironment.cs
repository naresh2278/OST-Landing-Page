using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.PlatformAbstractions;

namespace Dell.B2BOnlineTools.Common.Configuration
{
    public class HostingEnvironment : IHostingEnvironment
    {
        public HostingEnvironment()
        {
            EnvironmentName = Config.HOSTING_ENVIRONMENT;
            ApplicationName = PlatformServices.Default.Application.ApplicationName;
            ContentRootPath = PlatformServices.Default.Application.ApplicationBasePath;
        }

        public string EnvironmentName { get; set; }
        public string ApplicationName { get; set; }
        public string WebRootPath { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
