namespace Database.dbo
{
    using Framework.DataAccessLayer;
    using System;
    using Framework.Application;

    public partial class HelloWorld_Number
    {
        protected override void CellCssClass(App app, string gridName, string index, ref string result)
        {
            if (Row != null)
            {
                if (Row.Number > 0)
                {
                    result = "indicatorUp";
                }
                else
                {
                    if (Row.Number < 0)
                    {
                        result = "indicatorDown";
                    }
                    else
                    {
                        result = "indicatorEqual";
                    }
                }
            }
        }
    }
}