namespace Database.dbo
{
    using Framework.DataAccessLayer;
    using System;
    using Framework.Application;

    public partial class HelloWorld_Text
    {
        protected override void CellCssClass(App app, string gridName, string index, ref string result)
        {
            if (UtilApplication.IndexEnumFromText(index) == IndexEnum.Filter)
            {
                result = "gridFilter";
            }
        }
    }

    public partial class HelloWorld_Number
    {
        protected override void CellCssClass(App app, string gridName, string index, ref string result)
        {
            if (UtilApplication.IndexEnumFromText(index) == IndexEnum.Filter)
            {
                result = "gridFilter";
            }
            if (UtilApplication.IndexEnumFromText(index) == IndexEnum.Index)
            {
                if (Row.Number > 0)
                {
                    result = "gridUp";
                }
                else
                {
                    if (Row.Number < 0)
                    {
                        result = "gridDown";
                    }
                    else
                    {
                        result = "gridEqual";
                    }
                }
            }
            if (UtilApplication.IndexEnumFromText(index) == IndexEnum.New)
            {
                result = "gridNew";
            }
        }
    }
}