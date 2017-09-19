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
            new Label(div) { Text = "Hello World!", Css = "floatLeft" };
            new Label(div) { Text = "Hello World2!", Css = "floatLeft" };
            new Button(div) { Text = "Click", Css = "floatLeft" };
            new Literal(div) { TextHtml = "<b>Bold</b>" };
            new GridField(this, null, null, null);
            // Skyscraper
            var skyscraper = new Div(this);
            new Label(skyscraper) { Text = "Common Values", Css = "gridFieldWithLabelGroup" };
            new GridFieldWithLabel(skyscraper, "Text", "Grid1", "Text");
            new GridFieldWithLabel(skyscraper, "Number", "Grid1", "Number");
            new Label(skyscraper) { Text = "Action", Css = "gridFieldWithLabelGroup" };
            new GridFieldWithLabel(skyscraper, "Delete", "Grid1", "ButtonDelete");
            // Load
            app.GridData.LoadDatabase<HelloWorld>("Grid1");
        }
    }
}
