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
            new Label(this) { Text = "Main", CssClass = "labelGroup" };
            new Grid(this, HelloWorld.GridName);
            Div div = new Div(this);
            new Label(div) { Text = "Hello World!", CssClass = "floatLeft" };
            new Label(div) { Text = "Hello World2!", CssClass = "floatLeft" };
            new Button(div) { Text = "Click", CssClass = "floatLeft" };
            new Literal(div) { TextHtml = "<b>Bold</b>" };
            new GridFieldSingle(this);
            // Skyscraper
            var panel = new Div(this);
            new Label(panel) { Text = "Common Values", CssClass = "labelGroup" };
            new GridFieldWithLabel(panel, "Text", HelloWorld.GridName, "Text");
            new GridFieldWithLabel(panel, "Number", HelloWorld.GridName, "Number");
            new Label(panel) { Text = "Action", CssClass = "labelGroup" };
            new GridFieldWithLabel(panel, "Delete", HelloWorld.GridName, "ButtonDelete");
            // Attribute
            new Label(this) { Text = "Attribute", CssClass = "labelGroup" };
            new Grid(this, new GridName<Attribute>());
            // AttributeNote
            new Label(this) { Text = "Attribute Note", CssClass = "labelGroup" };
            new Grid(this, new GridName<AttributeNote>());
            // Load
            app.GridData.LoadDatabase(HelloWorld.GridName);
            app.GridData.LoadDatabaseInit(new GridName<Attribute>());
            app.GridData.LoadDatabaseInit(new GridName<AttributeNote>());
            //
            new Label(this) { Text = "Version=" + UtilFramework.VersionServer };
        }
    }
}
