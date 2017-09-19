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
            var skyscraper = new Div(this) { Css = "backgroundColorBlue" };
            new Label(skyscraper) { Text = "Skyskraper Label", Css = "backgroundColorGreen" };
            new GridField(skyscraper, "Grid1", "Text", null) { Css = "backgroundColorGreen" };
            new GridField(skyscraper, "Grid1", "Number", null) { Css = "backgroundColorGreen" };
            // Load
            app.GridData.LoadDatabase<HelloWorld>("Grid1");
        }
    }
}
