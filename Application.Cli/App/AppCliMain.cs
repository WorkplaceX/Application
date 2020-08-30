namespace Application.Cli
{
    using Database.dbo;
    using DatabaseIntegrate.dbo;
    using Framework.Cli;
    using Framework.Cli.Config;
    using Framework.DataAccessLayer;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Command line interface application.
    /// </summary>
    public class AppCliMain : AppCli
    {
        public AppCliMain() :
            base(
                typeof(HelloWorld).Assembly, // Register Application.Database dll
                typeof(AppMain).Assembly) // Register Application dll
        {

        }

        /// <summary>
        /// Set default values if file ConfigCli.json does not exist.
        /// </summary>
        protected override void InitConfigCli(ConfigCli configCli)
        {
            string appTypeName = typeof(AppMain).FullName + ", " + typeof(AppMain).Namespace;
            configCli.WebsiteList.Add(new ConfigCliWebsite()
            {
                DomainNameList = new List<ConfigCliWebsiteDomain>(new ConfigCliWebsiteDomain[] { new ConfigCliWebsiteDomain { EnvironmentName = "DEV", DomainName = "localhost", AppTypeName = appTypeName } }),
                FolderNameNpmBuild = "Application.Website/MasterDefault/",
                FolderNameDist = "Application.Website/MasterDefault/dist/",
            });
        }

        /// <summary>
        /// Cli Generate.
        /// </summary>
        protected override void CommandGenerateIntegrate(GenerateIntegrateResult result)
        {
            // Hello World
            result.Add(Data.Query<HelloWorldIntegrate>().OrderBy(item => item.IdName), isApplication: true);
            result.AddKey<HelloWorld>(nameof(HelloWorld.Name));
        }

        /// <summary>
        /// Cli Deploy.
        /// </summary>
        protected override void CommandDeployDbIntegrate(DeployDbIntegrateResult result)
        {
            // Hello World
            var rowList = HelloWorldIntegrateApplication.RowList;
            result.Add(rowList);
        }
    }
}
