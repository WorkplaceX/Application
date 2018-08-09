namespace Application
{
    using Framework.Application;
    using Framework.ComponentJson;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Main application.
    /// </summary>
    public class AppMain : App
    {
        protected override void Init()
        {
            new Button(AppJson) { Text = "Click" };
            new Button(AppJson) { Text = "Click2" };
            //
            Grid grid = new Grid(AppJson);
            grid.Header = new GridHeader();
            grid.Header.ColumnList = new List<GridColumn>();
            grid.Header.ColumnList.Add(new GridColumn() { Text = "Name" });
            grid.Header.ColumnList.Add(new GridColumn() { Text = "Number", SearchText = "33" });
            grid.RowList = new List<GridRow>();

            GridRow gridRow0 = new GridRow();
            gridRow0.IsSelect = true;
            grid.RowList.Add(gridRow0);
            gridRow0.CellList = new List<GridCell>();
            gridRow0.CellList.Add(new GridCell() { Text = "My Text" });
            gridRow0.CellList.Add(new GridCell() { Text = "22" });

            GridRow gridRow1 = new GridRow();
            grid.RowList.Add(gridRow1);
            gridRow1.CellList = new List<GridCell>();
            gridRow1.CellList.Add(new GridCell() { Text = "Blue green" });
            gridRow1.CellList.Add(new GridCell() { Text = "2212" });
        }

        protected override void Process()
        {
            base.Process();
            AppJson.Name = "HelloWorld " + DateTime.Now.ToUniversalTime().ToString();
            //
            foreach (var grid in AppJson.ListAll().OfType<Grid>())
            {
                foreach (var row in grid.RowList)
                {
                    row.CellList.ForEach(cell => cell.IsModify = false);
                    if (row.IsClick)
                    {
                        grid.RowList.ForEach(item => item.IsSelect = false);
                        row.IsClick = false;
                        row.IsSelect = true;
                    }
                }
            }
        }
    }

    public class AppSelectorHelloWorld : AppSelector
    {
        protected override App CreateApp()
        {
            return new AppMain();
        }
    }
}
