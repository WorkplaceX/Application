namespace BuildTool
{
    using System.Collections.Generic;
    using Framework.BuildTool;
    using Framework.Application;
    using Application;
    using Database.dbo;
    using Framework;

    /// <summary>
    /// Override method RegisterCommand(); to add additional custom build tool commands.
    /// </summary>
    public class AppBuildToolMain : AppBuildTool
    {
        public AppBuildToolMain(App app) 
            : base(app)
        {

        }

        protected override void DbFrameworkApplicationView(List<FrameworkApplicationView> result)
        {
            result.Add(new FrameworkApplicationView() { Text = "Main Application", Path = null, Type = UtilFramework.TypeToName(typeof(AppMain)), IsActive = true });
        }
    }
}
