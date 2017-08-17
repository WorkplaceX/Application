namespace Application
{
    using System;
    using Framework.Application;

    public class AppMain : App
    {
        protected override Type TypePageMain()
        {
            return typeof(PageMain);
        }
    }
}
