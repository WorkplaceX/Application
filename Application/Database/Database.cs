namespace Database.dbo
{
    using System.Linq;
    using Framework.Application;
    using Framework.Component;
    using Framework.DataAccessLayer;

    public partial class AttributeNote
    {
        protected override void MasterIsClick(App app, string gridNameMaster, Row rowMaster, ref bool isReload)
        {
            if (gridNameMaster == "GridAttribute")
            {
                isReload = true;
            }
        }

        protected override IQueryable Where(App app, string gridName)
        {
            IQueryable result = null;
            Attribute rowMaster = app.GridData.RowSelected("GridAttribute") as Attribute;
            if (rowMaster != null)
            {
                result = UtilDataAccessLayer.Query<AttributeNote>().Where(item => item.AttributeId == rowMaster.Id);
            }
            return result;
        }

        protected override void Insert(App app)
        {
            Attribute rowMaster = app.GridData.RowSelected("GridAttribute") as Attribute;
            if (rowMaster != null)
            {
                this.AttributeId = rowMaster.Id;
            }
            base.Insert(app);
        }
    }

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
            IQueryable result = null;
            HelloWorld rowMaster = app.GridData.RowSelected("Grid1") as HelloWorld;
            if (rowMaster != null)
            {
                result = UtilDataAccessLayer.Query<Attribute>().Where(item => item.HelloWorldId == rowMaster.Id);
            }
            return result;
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

        [SqlColumn(null, typeof(Attribute_Delete))]
        public string Delete { get; set; }
    }

    public  class Attribute_Delete : Cell<Attribute>
    {
        protected override void InfoCell(App app, string gridName, Index index, InfoCell result)
        {
            if (index.Enum == IndexEnum.Index)
            {
                result.CellEnum = GridCellEnum.Button;
            }
        }

        protected override void CellButtonIsClick(App app, string gridName, Index index, Row row, string fieldName, ref bool isReload)
        {
            UtilDataAccessLayer.Delete(row);
            isReload = true;
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