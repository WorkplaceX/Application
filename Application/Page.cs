using Database.dbo;
using Framework.Application;
using Framework.Component;

namespace Application
{
    public class PageMain : Page
    {
        protected override void InitJson(App app)
        {
            new Label(this) { Text = "Main", CssClass = "labelGroup" };
            new Grid(this, "Grid1");
            Div div = new Div(this);
            new Label(div) { Text = "Hello World!", CssClass = "floatLeft" };
            new Label(div) { Text = "Hello World2!", CssClass = "floatLeft" };
            new Button(div) { Text = "Click", CssClass = "floatLeft" };
            new Literal(div) { TextHtml = "<b>Bold</b>" };
            new GridField(this, null, null, null);
            // Skyscraper
            var panel = new Div(this);
            new Label(panel) { Text = "Common Values", CssClass = "labelGroup" };
            new GridFieldWithLabel(panel, "Text", "Grid1", "Text");
            new GridFieldWithLabel(panel, "Number", "Grid1", "Number");
            new Label(panel) { Text = "Action", CssClass = "labelGroup" };
            new GridFieldWithLabel(panel, "Delete", "Grid1", "ButtonDelete");
            // Attribute
            new Label(this) { Text = "Attribute", CssClass = "labelGroup" };
            new Grid(this, "GridAttribute");
            // AttributeNote
            new Label(this) { Text = "Attribute Note", CssClass = "labelGroup" };
            new Grid(this, "GridAttributeNote");
            // Load
            app.GridData.LoadDatabase<HelloWorld>("Grid1");
            app.GridData.LoadDatabase<Attribute>("GridAttribute");
            app.GridData.LoadDatabase<AttributeNote>("GridAttributeNote");
        }
    }
}
