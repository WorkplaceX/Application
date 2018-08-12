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
    using Database.dbo;
    using System.Threading.Tasks;
    using System.Linq.Dynamic.Core;
    using Framework.Server;

    /// <summary>
    /// Main application.
    /// </summary>
    public class AppMain : App
    {
        protected override async Task InitAsync()
        {
            new Button(AppJson) { Text = "Click" };
            new Button(AppJson) { Text = "Click2" };

            var grid = GridContact();
            GridPerson();

            await grid.LoadAsync();
        }

        public Grid GridContact()
        {
            return AppJson.CreateOrGet<Grid>("Contact");
        }

        public Grid GridPerson()
        {
            return AppJson.CreateOrGet<Grid>("Person");
        }

        /// <summary>
        /// Returns query to load data grid.
        /// </summary>
        protected override IQueryable GridLoadQuery(Grid grid)
        {
            if (grid == GridContact())
            {
                return UtilDal.Query<vAdditionalContactInfo>();
            }
            if (grid == GridPerson())
            {
                string firstName = ((vAdditionalContactInfo)GridContact().RowSelected()).FirstName;
                return UtilDal.Query<Person>().Where(item => item.FirstName == firstName);
            }
            return null;
        }

        /// <summary>
        /// Override this method to execute action after selected row changed. For example master, detail.
        /// </summary>
        protected override async Task GridRowSelectChangeAsync(Grid grid)
        {
            if (grid == GridContact())
            {
                await GridPerson().LoadAsync();
            }
        }

        protected override Task ProcessAsync()
        {
            AppJson.Name = "HelloWorld " + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");
            return base.ProcessAsync();
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
