namespace Application
{
    using Database.dbo;
    using Framework.Application;
    using Framework.Component;

    public class PageMain : Page
    {
        protected override void InitJson(App app)
        {
            new Label(this) { Text = "Hello World!" };
            new Grid(this, "Grid1");
            app.GridData.LoadDatabase<HelloWorld>("Grid1");
        }
    }
}
