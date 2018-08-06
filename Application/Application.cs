namespace Application
{
    using Framework.Application;
    using System;

    public class AppHelloWorld : App
    {
        protected override void Process()
        {
            base.Process();
            AppJson.Name = "HelloWorld " + DateTime.Now.ToUniversalTime().ToString();
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
