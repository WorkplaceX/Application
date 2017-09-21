namespace Database.dbo
{
    using System.Linq;
    using Framework.Application;
    using Framework.Component;
    using Framework.DataAccessLayer;

    public partial class Attribute
    {
        protected override void MasterIsClick(App app, string gridNameMaster, Row rowMaster, ref bool isReload)
        {
            if (gridNameMaster == "Grid1")
            {
                isReload = true;
            }
        }

        protected override IQueryable Where(App app, string gridName)
        {
            HelloWorld rowMaster = app.GridData.RowSelected("Grid1") as HelloWorld;
            if (rowMaster != null)
            {
                return UtilDataAccessLayer.Query<Attribute>().Where(item => item.HelloWorldId == rowMaster.Id);
            }
            else
            {
                return base.Where(app, gridName);
            }
        }

        protected override void Insert(App app)
        {
            HelloWorld rowMaster = app.GridData.RowSelected("Grid1") as HelloWorld;
            if (rowMaster != null)
            {
                this.HelloWorldId = rowMaster.Id;
            }
            base.Insert(app);
        }
    }

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
                    result.CssClass.Add("gridUp");
                }
                else
                {
                    if (Row.Number < 0)
                    {
                        result.CssClass.Add("gridDown");
                    }
                    else
                    {
                        if (Row.Number == 0)
                        {
                            result.CssClass.Add("gridEqual");
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