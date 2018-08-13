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
    using Database.dbo;

    public class AppMain : AppJson
    {
        public AppMain() : this(null) { }

        public AppMain(ComponentJson owner)
            : base(owner)
        {

        }

        protected override async Task InitAsync()
        {
            await this.PageShowAsync<MyPage>();

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
                var rowSelected = (vAdditionalContactInfo)GridContact().RowSelected();
                if (rowSelected != null) // Otherwise "new row" has been selected.
                {
                    string firstName = ((vAdditionalContactInfo)GridContact().RowSelected()).FirstName;
                    result = UtilDal.Query<Person>().Where(item => item.FirstName == firstName);
                }
                else
                {
                    return UtilDal.QueryEmpty<vAdditionalContactInfo>();
                }
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
                await this.PageShowAsync<MessageBox>();
            }
        }

        protected override Task ProcessAsync()
        {
            MessageBox messageBox = this.Get<MessageBox>();
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

    public class MessageBox : Page
    {
        public MessageBox() : this(null) { }

        public MessageBox(ComponentJson owner)
            : base(owner)
        {

        }

        protected override Task InitAsync()
        {
            new Html() { TextHtml = "Delete record?" };
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

    public class MyPage : Page
    {
        public MyPage() { }

        public MyPage(ComponentJson owner)
            : base(owner)
        {

        }

        protected override async Task InitAsync()
        {
            await Grid().LoadAsync();
            ButtonDelete();
        }

        public Grid Grid()
        {
            return this.GetOrCreate<Grid>();
        }

        public Button ButtonDelete()
        {
            return this.GetOrCreate((Button button) => button.Text = "Delete");
        }

        protected override IQueryable GridLoadQuery(Grid grid)
        {
            return UtilDal.Query<My>();
        }

        protected override Task ButtonClickAsync(Button button)
        {
            if (button == ButtonDelete())
            {
                this.PageShowAsync<MessageBox>();
            }
            return base.ButtonClickAsync(button);
        }

        protected override async Task ProcessAsync()
        {
            var messageBox = this.Get<MessageBox>();
            if (messageBox?.IsYes == true)
            {
                var row = Grid().RowSelected();
                if (row != null)
                {
                    await UtilDal.Delete(row);
                    await Grid().LoadAsync();
                }
            }
            if (messageBox?.IsYes != null)
            {
                this.Get<MessageBox>().Remove();
            }
        }
    }

    public class AppSelectorHelloWorld : AppSelector
    {
        protected override Type TypeAppJson()
        {
            return typeof(AppMain);
        }
    }
}
