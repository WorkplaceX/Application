namespace Database.dbo
{
    using Framework.DataAccessLayer;
    using System;
    using Framework.Application;

    public partial class HelloWorld_Text
    {
        protected override void InfoCell(App app, string gridName, string index, InfoCell result)
        {

        }
    }

    public partial class HelloWorld_Number
    {
        protected override void InfoCell(App app, string gridName, string index, InfoCell result)
        {
            if (UtilApplication.IndexEnumFromText(index) == IndexEnum.Index)
            {
                if (Row.Number > 0)
                {
                    result.Css.Add("gridUp");
                }
                else
                {
                    if (Row.Number < 0)
                    {
                        result.Css.Add("gridDown");
                    }
                    else
                    {
                        if (Row.Number == 0)
                        {
                            result.Css.Add("gridEqual");
                        }
                    }
                }
            }
        }
    }
}