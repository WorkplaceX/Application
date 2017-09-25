namespace Database.dbo
{
    using System.Linq;
    using Framework.Application;
    using Framework.Component;
    using Framework.DataAccessLayer;

    public partial class AttributeNote
    {
        protected override void MasterIsClick(App app, GridName gridNameMaster, Row rowMaster, ref bool isReload)
        {
            if (gridNameMaster == new GridName<Attribute>())
            {
                isReload = true;
            }
        }

        protected override IQueryable Where(App app, GridName gridName)
        {
            IQueryable result = null;
            Attribute rowMaster = app.GridData.RowSelected(new GridName<Attribute>());
            if (rowMaster != null)
            {
                result = UtilDataAccessLayer.Query<AttributeNote>().Where(item => item.AttributeId == rowMaster.Id);
            }
            return result;
        }

        protected override void Insert(App app)
        {
            Attribute rowMaster = app.GridData.RowSelected(new GridName<Attribute>());
            if (rowMaster != null)
            {
                this.AttributeId = rowMaster.Id;
            }
            base.Insert(app);
        }
    }

    public partial class Attribute
    {
        protected override void MasterIsClick(App app, GridName gridNameMaster, Row rowMaster, ref bool isReload)
        {
            if (gridNameMaster == HelloWorld.GridName)
            {
                isReload = true;
            }
        }

        protected override IQueryable Where(App app, GridName gridName)
        {
            IQueryable result = null;
            HelloWorld rowMaster = app.GridData.RowSelected(HelloWorld.GridName);
            if (rowMaster != null)
            {
                result = UtilDataAccessLayer.Query<Attribute>().Where(item => item.HelloWorldId == rowMaster.Id);
            }
            return result;
        }

        protected override void Insert(App app)
        {
            HelloWorld rowMaster = app.GridData.RowSelected(HelloWorld.GridName);
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
        protected override void InfoCell(App app, GridName gridName, Index index, InfoCell result)
        {
            if (index.Enum == IndexEnum.Index)
            {
                result.CellEnum = GridCellEnum.Button;
            }
        }

        protected override void CellButtonIsClick(App app, GridName gridName, Index index, Row row, string fieldName, ref bool isReload)
        {
            UtilDataAccessLayer.Delete(row);
            isReload = true;
        }
    }

    public partial class HelloWorld
    {
        public static GridName<HelloWorld> GridName = new GridName<HelloWorld>();

        [SqlColumn(null, typeof(HelloWorld_ButtonDelete))]
        public string ButtonDelete { get; set; }
    }

    public partial class HelloWorld_Text
    {
        protected override void InfoCell(App app, GridName gridName, Index index, InfoCell result)
        {
            // result.CellEnum = GridCellEnum.Html;
        }
    }

    public partial class HelloWorld_Number
    {
        protected override void InfoCell(App app, GridName gridName, Index index, InfoCell result)
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
        protected override void InfoCell(App app, GridName gridName, Index index, InfoCell result)
        {
            if (index.Enum == IndexEnum.Index)
            {
                result.CellEnum = GridCellEnum.Button;
            }
        }

        protected override void CellValueToText(App app, GridName gridName, Index index, ref string result)
        {
            if (index.Enum == IndexEnum.Index)
            {
                result = "Delete";
            }
        }

        protected override void CellButtonIsClick(App app, GridName gridName, Index index, Row row, string fieldName, ref bool isReload)
        {
            UtilDataAccessLayer.Delete(row);
            isReload = true;
        }
    }
}