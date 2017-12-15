using Database.dbo;
using Framework;
using Framework.Application;
using Framework.Component;

namespace Application
{
    public class PageMain : Page
    {
        protected override void InitJson(App app)
        {
            new Label(this) { Text = "Application Start" };
            new Grid(this, new GridName<HelloWorld>());
            app.GridData.LoadDatabase(new GridName<HelloWorld>());
            new Label(this) { Text = "VersionServer=" + UtilFramework.VersionServer };

        }
    }
}
