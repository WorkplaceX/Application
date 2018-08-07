namespace Application
{
    using Framework.Application;
    using Framework.ComponentJson;
    using System;

    public class AppHelloWorld : App
    {
        protected override void Process()
        {
            base.Process();
            AppJson.Name = "HelloWorld " + DateTime.Now.ToUniversalTime().ToString();
            //
            new Button(AppJson) { Text = "Click" };
            new Button(AppJson) { Text = "Click2" };
        }
    }

    public class AppSelectorHelloWorld : AppSelector
    {
        protected override App CreateApp()
        {
            return new AppHelloWorld();
        }
    }
}
