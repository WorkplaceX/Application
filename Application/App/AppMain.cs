namespace Application
{
    using Framework.Application;
    using Framework.Json;
    using Framework.Dal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Database.Person;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Main application.
    /// </summary>
    public class AppMain : App
    {
        protected override void Init()
        {
            new Button(AppJson) { Text = "Click" };
            new Button(AppJson) { Text = "Click2" };

            Grid grid = new Grid(AppJson);
            var list = UtilDal.Query<vAdditionalContactInfo>().ToList();
            grid.Load(this, list);

            var grid2 = new Grid(AppJson);
            var list2 = UtilDal.Query<Person>().Where(item => item.FirstName == "Kim").ToList();
            grid2.Load(this, list2);
        }

        private void ProcessSave()
        {
            foreach (var grid in AppJson.ListAll().OfType<Grid>())
            {
                int gridIndex = grid.Id;
                for (int rowIndex = 0; rowIndex < grid.RowList.Count; rowIndex++)
                {
                    for (int cellIndex = 0; cellIndex < grid.RowList[rowIndex].CellList.Count; cellIndex++)
                    {
                        if (grid.RowList[rowIndex].CellList[cellIndex].IsModify)
                        { 
}
                    }
                }
            }
        }

        protected override void Process()
        {
            AppJson.Name = "HelloWorld " + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");

            ProcessSave();

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
