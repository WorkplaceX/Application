namespace Application.Cli
{
    using Database.dbo;
    using Framework.Cli.Command;
    using Framework.Cli.Config;
    using Microsoft.Extensions.CommandLineUtils;
    using System;

    /// <summary>
    /// Command line interface application.
    /// </summary>
    public class AppCliMain : AppCli
    {
        public AppCliMain() : 
            base(typeof(AWBuildVersion).Assembly, typeof(AppMain).Assembly)
        {

        }

        protected override void RegisterCommand()
        {
            new MyCommand(this);

            base.RegisterCommand();
        }

        protected override void InitConfigCli(ConfigCli configCli)
        {
            configCli.WebsiteList.Add(new ConfigCliWebsite()
            {
                DomainName = "default",
                FolderNameNpmBuild = "Website/",
                FolderNameDist = "Website/dist/",
            });
        }
    }

    public class MyCommand : CommandBase
    {
        public MyCommand(AppCli appCli) 
            : base(appCli, "My", "My command")
        {

        }

        public CommandOption My;

        protected override void Register(CommandLineApplication configuration)
        {
            this.My = configuration.Option("-m", "My Option", CommandOptionType.NoValue);
        }

        protected override void Execute()
        {
            Console.WriteLine("My");
            if (My.Value() == "on")
            {
                Console.WriteLine("With option");
            }
        }
    }
}
