using Database.dbo;
using Framework.Application;
using Framework.Component;

namespace Application
{
    public class PageMain : Page
    {
        protected override void InitJson(App app)
        {
            new Grid(this, "Grid1");
            Div div = new Div(this);
            new Label(div) { Text = "Hello World!", CssClass = "floatLeft" };
            new Label(div) { Text = "Hello World2!", CssClass = "floatLeft" };
            new Button(div) { Text = "Click", CssClass = "floatLeft" };
            new Literal(div) { TextHtml = "<b>Bold</b>" };
            new GridField(this, null, null, null);
            // Skyscraper
            var panel = new Div(this);
            new Label(panel) { Text = "Common Values", CssClass = "gridFieldWithLabelGroup" };
            new GridFieldWithLabel(panel, "Text", "Grid1", "Text");
            new GridFieldWithLabel(panel, "Number", "Grid1", "Number");
            new Label(panel) { Text = "Action", CssClass = "gridFieldWithLabelGroup" };
            new GridFieldWithLabel(panel, "Delete", "Grid1", "ButtonDelete");
            // Load
            app.GridData.LoadDatabase<HelloWorld>("Grid1");
        }
    }
}
