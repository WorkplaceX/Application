namespace Application
{
    using Framework.Application;
    using Framework.Json;
    using Framework.Dal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseCustom.Person;
    using System.Threading.Tasks;
    using System.Linq.Dynamic.Core;

    public class AppMain : AppJson
    {
        public AppMain() : this(null) { }

        public AppMain(ComponentJson owner)
            : base(owner)
        {

        }

        protected override async Task InitAsync()
        {
            new Html(this) { TextHtml = "Delete item: " };
            ButtonDelete();

            var grid = GridContact();
            GridPerson();

            await grid.LoadAsync();
        }

        public Grid GridContact()
        {
            return this.GetOrCreate<Grid>("Contact");
        }

        public Grid GridPerson()
        {
            return this.GetOrCreate<Grid>("Person");
        }

        public Button ButtonDelete()
        {
            return this.GetOrCreate<Button>((button) => button.Text = "Delete");
        }

        protected override IQueryable GridLoadQuery(Grid grid)
        {
            IQueryable result = null;
            if (grid == GridContact())
            {
                result = UtilDal.Query<vAdditionalContactInfo>();
            }
            if (grid == GridPerson())
            {
                string firstName = ((vAdditionalContactInfo)GridContact().RowSelected()).FirstName;
                result = UtilDal.Query<Person>().Where(item => item.FirstName == firstName);
            }
            return result;
        }

        protected override async Task GridRowSelectChangeAsync(Grid grid)
        {
            if (grid == GridContact())
            {
                await GridPerson().LoadAsync();
            }
        }

        protected override async Task ButtonClickAsync(Button button)
        {
            if (button == ButtonDelete())
            {
                await this.PageShowAsync<PageMessageBox>();
            }
        }

        protected override Task ProcessAsync()
        {
            PageMessageBox messageBox = this.Get<PageMessageBox>();
            if (messageBox?.IsYes != null)
            {
                if (messageBox.IsYes == true)
                {
                    new Html(this).TextHtml = "Deleted!";
                }
                messageBox.Remove();
            }

            Name = "HelloWorld " + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");
            return base.ProcessAsync();
        }
    }

    public class PageMessageBox : Page
    {
        public PageMessageBox() : this(null) { }

        public PageMessageBox(ComponentJson owner)
            : base(owner)
        {

        }

        protected override Task InitAsync()
        {
            ButtonYes();
            ButtonNo();
            return base.InitAsync();
        }

        public Button ButtonYes()
        {
            return this.GetOrCreate<Button>("Yes", (button) => button.Text = "Yes");
        }

        public Button ButtonNo()
        {
            return this.GetOrCreate<Button>("No", (button) => button.Text = "No");
        }

        protected override Task ButtonClickAsync(Button button)
        {
            if (button == ButtonYes())
            {
                new Html(this).TextHtml = "<b>Ok</b>";
                this.IsYes = true;
            }
            if (button == ButtonNo())
            {
                this.IsYes = false;
            }
            return base.ButtonClickAsync(button);
        }

        public bool? IsYes;
    }

    public class AppSelectorHelloWorld : AppSelector
    {
        protected override Type TypeAppJson()
        {
            return typeof(AppMain);
        }
    }
}
