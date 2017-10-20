namespace Database.dbo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Application;
    using Framework.Component;
    using Framework.DataAccessLayer;
    using Database.Calculated;

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

        protected override void Insert(App app, GridName gridName, Index index)
        {
            Attribute rowMaster = app.GridData.RowSelected(new GridName<Attribute>());
            if (rowMaster != null)
            {
                this.AttributeId = rowMaster.Id;
            }
            base.Insert(app, gridName, index);
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

        protected override void Insert(App app, GridName gridName, Index index)
        {
            HelloWorld rowMaster = app.GridData.RowSelected(HelloWorld.GridName);
            if (rowMaster != null)
            {
                this.HelloWorldId = rowMaster.Id;
            }
            base.Insert(app, gridName, index);
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

        protected override void CellLookup(out Type typeRow, out List<Row> rowList)
        {
            typeRow = typeof(HelloWorld_NumberLookup);
            rowList = new List<Row>();
            rowList.Add(new HelloWorld_NumberLookup() { Number = 100, Text = "One hundered" });
            rowList.Add(new HelloWorld_NumberLookup() { Number = -200, Text = "Two hundered" });
            rowList.Add(new HelloWorld_NumberLookup() { Number = 300, Text = "Three hundered" });
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

namespace Database.Calculated
{
    using System.Linq;
    using Framework.Application;
    using Framework.DataAccessLayer;
    using System.Collections.Generic;

    public class HelloWorld_NumberLookup : Row
    {
        public int Number { get; set; }

        public string Text { get; set; }
    }

    public class MyTable : Row
    {
        public string Description { get; set; }

        public double Value { get; set; }

        protected override IQueryable Where(App app, GridName gridName)
        {
            List<MyTable> result = new List<MyTable>();
            result.Add(new MyTable() { Description = "North", Value = 0 });
            result.Add(new MyTable() { Description = "East", Value = 90 });
            result.Add(new MyTable() { Description = "South", Value = 180 });
            result.Add(new MyTable() { Description = "West", Value = 270 });
            return result.AsQueryable();
        }

        protected override void Update(App app, GridName gridName, Index index, Row row, Row rowNew)
        {
            base.Update(app, gridName, index, row, rowNew);
        }

        protected override void Insert(App app, GridName gridName, Index index)
        {
            base.Insert(app, gridName, index);
        }
    }

    public partial class MyTable_Description : Cell<MyTable>
    {
        protected override void CellTextParse(App app, GridName gridName, Index index, ref string result)
        {
            base.CellTextParse(app, gridName, index, ref result);
        }
    }
}