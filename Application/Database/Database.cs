namespace Database.dbo
{
    using Framework.Application;
    using Framework.Component;
    using Framework.DataAccessLayer;

    public partial class HelloWorld
    {
        [SqlColumn(null, typeof(HelloWorld_ButtonDelete))]
        public string ButtonDelete { get; set; }
    }

    public partial class HelloWorld_Text
    {
        protected override void InfoCell(App app, string gridName, Index index, InfoCell result)
        {
            // result.CellEnum = GridCellEnum.Html;
        }
    }

    public partial class HelloWorld_Number
    {
        protected override void InfoCell(App app, string gridName, Index index, InfoCell result)
        {
            if (index.Enum == IndexEnum.Index)
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

    public partial class HelloWorld_ButtonDelete : Cell<HelloWorld>
    {
        protected override void InfoCell(App app, string gridName, Index index, InfoCell result)
        {
            if (index.Enum == IndexEnum.Index)
            {
                result.CellEnum = GridCellEnum.Button;
            }
        }

        protected override void CellValueToText(App app, string gridName, Index index, ref string result)
        {
            if (index.Enum == IndexEnum.Index)
            {
                result = "Delete";
            }
        }

        protected override void CellButtonIsClick(App app, string gridName, Index index, Row row, string fieldName, ref bool isReload)
        {
            UtilDataAccessLayer.Delete(row);
            isReload = true;
        }
    }
}