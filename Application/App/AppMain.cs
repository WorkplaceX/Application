﻿namespace Application
{
    using Framework.Application;
    using Framework.Json;
    using Framework.Dal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Linq.Dynamic.Core;
    using Database.dbo;
    using Database.Person;
    using Database.Memory;
    using Framework.Session;

    public class AppMain : AppJson
    {
        public AppMain() : this(null) { }

        public AppMain(ComponentJson owner)
            : base(owner)
        {

        }

        protected override async Task InitAsync()
        {
            await this.PageShowAsync<NavigationPage>();
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
            return this.ComponentGetOrCreate<Button>("Yes", (button) => button.Text = "Yes");
        }

        public Button ButtonNo()
        {
            return this.ComponentGetOrCreate<Button>("No", (button) => button.Text = "No");
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

    public class NavigationPage : Page
    {
        public NavigationPage() { }

        public NavigationPage(ComponentJson owner)
            : base(owner)
        {

        }

        protected override async Task InitAsync()
        {
            BootstrapNavbar();
            await Grid().LoadAsync();
            Grid().IsHide = true;
            BootstrapNavbar().GridIndex = Grid().Index;
        }

        protected override Task ProcessAsync()
        {
            if (this.ComponentOwner<AppJson>().IsSessionExpired)
            {
                this.BootstrapAlert("alert", "Session expired!", BootstrapAlertEnum.Warning, BootstrapNavbar().ComponentIndex() + 1);
            }
            else
            {
                this.ComponentRemoveItem("alert");
            }
            return base.ProcessAsync();
        }

        public BootstrapNavbar BootstrapNavbar()
        {
            return this.ComponentGetOrCreate<BootstrapNavbar>((bootstrapNavbar) => { bootstrapNavbar.BrandTextHtml = "<b>Hello</b>World"; });
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            List<Database.Memory.Navigation> list = UtilDal.MemoryRowList<Database.Memory.Navigation>();
            if (list.Count == 0)
            {
                list.Add(new Database.Memory.Navigation() { Id = 1, Text = "<i class='fas fa-home'></i> Home" });
                list.Add(new Database.Memory.Navigation() { Id = 2, Text = "<i class='fas fa-user'></i> User" });
                list.Add(new Database.Memory.Navigation() { Id = 2, Text = "<i class='fas fa-key'></i> UserRole" });
                list.Add(new Database.Memory.Navigation() { Id = 3, Text = "<i class='far fa-address-card'></i> Contact" });
                list.Add(new Database.Memory.Navigation() { Id = 4, Text = "<span class='flag-icon flag-icon-gb'></span> English" });
            }

            return UtilDal.Query<Database.Memory.Navigation>(DatabaseEnum.MemorySingleton);
        }

        public Grid Grid()
        {
            return this.ComponentGetOrCreate<Grid>((grid) => { grid.CssClass = "container"; });
        }

        protected override async Task GridRowSelectedAsync(Grid grid)
        {
            Navigation navigation = grid.RowSelected() as Navigation;
            if (navigation != null && navigation.Text.Contains("Home"))
            {
                await this.PageShowAsync<HomePage>(ComponentJsonExtension.PageShowEnum.SiblingRemove);
            }
            if (navigation != null && navigation.Text.Contains("User"))
            {
                await this.PageShowAsync<LoginUserPage>(ComponentJsonExtension.PageShowEnum.SiblingRemove);
            }
            if (navigation != null && navigation.Text.Contains("UserRole"))
            {
                await this.PageShowAsync<LoginUserRolePage>(ComponentJsonExtension.PageShowEnum.SiblingRemove);
            }
            if (navigation != null && navigation.Text.Contains("Contact"))
            {
                await this.PageShowAsync<ContactPage>(ComponentJsonExtension.PageShowEnum.SiblingRemove);
            }
        }
    }

    public class LanguagePage : Page
    {
        public LanguagePage() { }

        public LanguagePage(ComponentJson owner)
            : base(owner)
        {

        }

        protected override async Task InitAsync()
        {
            await Grid().LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            List<Database.Memory.Language> list = UtilDal.MemoryRowList<Database.Memory.Language>();
            if (list.Count == 0)
            {
                list.Add(new Database.Memory.Language() { Id = 1, Text = "English", FlagIcon = "flag-icon-gb" });
                list.Add(new Database.Memory.Language() { Id = 2, Text = "German", FlagIcon = "flag-icon-de" });
                list.Add(new Database.Memory.Language() { Id = 3, Text = "French", FlagIcon = "flag-icon-fr" });
                list.Add(new Database.Memory.Language() { Id = 4, Text = "Italien", FlagIcon = "flag-icon-it" });
            }

            return UtilDal.Query<Database.Memory.Language>(DatabaseEnum.MemorySingleton);
        }

        public Grid Grid()
        {
            return this.ComponentGetOrCreate<Grid>();
        }
    }

    public class LoginUserPage : Page
    {
        public LoginUserPage() { }

        public LoginUserPage(ComponentJson owner)
            : base(owner)
        {
            new Html(this).TextHtml = "<h1>User</h1>";
            GridUser();
            new Html(this).TextHtml = "<h1>User Role</h1>";
            GridUserRoleDisplay();
        }

        public Grid GridUser()
        {
            return this.ComponentGetOrCreate<Grid>("User");
        }

        public Grid GridUserRoleDisplay()
        {
            return this.ComponentGetOrCreate<Grid>("UserRoleDisplay");
        }

        protected override async Task InitAsync()
        {
            await GridUser().LoadAsync();
        }

        protected override async Task GridRowSelectedAsync(Grid grid)
        {
            if (grid == GridUser())
            {
                await GridUserRoleDisplay().LoadAsync();
            }
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            if (grid == GridUser())
            {
                return UtilDal.Query<LoginUser>();
            }
            if (grid == GridUserRoleDisplay())
            {
                int userId = ((LoginUser)GridUser().RowSelected()).Id;
                return UtilDal.Query<LoginUserRoleDisplay>().Where(item => item.UserId == userId);
            }
            return base.GridQuery(grid);
        }

        protected override string CellText(Grid grid, Row row, string fieldName)
        {
            LoginUser loginUser = row as LoginUser;
            if (loginUser != null)
            {
                if (fieldName == nameof(LoginUser.IsActive))
                {
                    if (loginUser.IsActive)
                    {
                        return "Yes";
                    }
                    else
                    {
                        return "No";
                    }
                }
                if (fieldName == nameof(LoginUser.Value))
                {
                    string text = loginUser.Value.ToString();
                    if (loginUser.ValueUOM != null)
                    {
                        text += " " + loginUser.ValueUOM;
                    }
                    return text;
                }
                if (fieldName == nameof(LoginUser.Password))
                {
                    return "*****";
                }
            }
            return base.CellText(grid, row, fieldName);
        }

        protected override void CellTextParse(Grid grid, string fieldName, string text, Row row, out bool isHandled)
        {
            isHandled = false;
            LoginUser loginUser = row as LoginUser;
            if (loginUser != null)
            {
                if (fieldName == nameof(LoginUser.IsActive))
                {
                    if (text.ToLower().StartsWith("n"))
                    {
                        loginUser.IsActive = false;
                        isHandled = true;
                        return;
                    }
                    if (text.ToLower().StartsWith("y"))
                    {
                        loginUser.IsActive = true;
                        isHandled = true;
                        return;
                    }
                    throw new Exception("Invalid value!");
                }
                if (fieldName == nameof(LoginUser.Value))
                {
                    string value = null;
                    string valueUOM = null;
                    foreach (var item in text)
                    {
                        if ((item >= '0' && item <= '9') || item == '.')
                        {
                            value += item;
                        }
                        else
                        {
                            if (item != ' ')
                            {
                                valueUOM += item;
                            }
                        }
                    }
                    loginUser.Value = double.Parse(value);
                    loginUser.ValueUOM = valueUOM;
                    isHandled = true;

                    this.BootstrapAlert("alert", "My <b>message</b>", (BootstrapAlertEnum)loginUser.Value);
                }
            }
        }

        protected override void CellTextParseFilter(Grid grid, Type typeRow, string fieldName, string text, Filter filter, out bool isHandled)
        {
            isHandled = false;
            if (typeRow == typeof(LoginUser))
            {
                if (fieldName == nameof(LoginUser.IsActive))
                {
                    if (text.ToLower().StartsWith("n"))
                    {
                        filter.SetValue(fieldName, false, FilterOperator.Equal, "No");
                        isHandled = true;
                    }
                    if (text.ToLower().StartsWith("y"))
                    {
                        filter.SetValue(fieldName, true, FilterOperator.Equal, "Yes");
                        isHandled = true;
                    }
                }
            }
        }

        protected override async Task<bool> GridUpdateAsync(Grid grid, Row row, Row rowNew, DatabaseEnum databaseEnum)
        {
            LoginUserRoleDisplay loginUserRoleDisplay = rowNew as LoginUserRoleDisplay;
            if (loginUserRoleDisplay != null)
            {
                if (loginUserRoleDisplay.UserUserRoleId == null)
                {
                    var loginUserUserRole = new LoginUserUserRole() { UserId = loginUserRoleDisplay.UserId, UserRoleId = loginUserRoleDisplay.UserRoleId, IsActive = loginUserRoleDisplay.IsActive.GetValueOrDefault() };
                    await UtilDal.InsertAsync(loginUserUserRole);
                }
                else
                {
                   var loginUserUserRole = new LoginUserUserRole() { Id = loginUserRoleDisplay.UserUserRoleId.Value, UserId = loginUserRoleDisplay.UserId, UserRoleId = loginUserRoleDisplay.UserRoleId, IsActive = loginUserRoleDisplay.IsActive.GetValueOrDefault() };
                    await UtilDal.UpdateAsync(loginUserUserRole, loginUserUserRole);
                }
            }
            return true;
        }
    }

    public class LoginUserRolePage : Page
    {
        public LoginUserRolePage() { }

        public LoginUserRolePage(ComponentJson owner)
            : base(owner)
        {
            this.ComponentGetOrCreate<Grid>();
        }

        protected override async Task InitAsync()
        {
            await this.ComponentGetOrCreate<Grid>().LoadAsync();
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return UtilDal.Query<LoginUserRole>();
        }
    }

    public class HomePage : Page
    {
        public HomePage() { }

        public HomePage(ComponentJson owner)
            : base(owner)
        {
            this.ComponentOwner<AppJson>().IsEmbedded(out string requestUrl);
            new Html(this) { TextHtml = @"
            <section id='#'>
                <div class='container'>
                    <h1>Home</h1>
                    <p>Welcome to Hello World!</p>
                    <figure class='figure'>
                        <img src='{{RequestUrl}}logo.jpg' class='figure-img img-fluid rounded' alt='Company'>
                        <figcaption class='figure-caption'>Hello world application.</figcaption>
                    </figure>
                </div>
            </section>
            ".Replace("{{RequestUrl}}", requestUrl) };
        }
    }

    public class ContactPage : Page
    {
        public ContactPage() { }

        public ContactPage(ComponentJson owner)
            : base(owner)
        {

        }

        protected override async Task InitAsync()
        {
            Label().TextHtml = "MyLabel";
            await DivContainer().PageShowAsync<LanguagePage>();
            await DivContainer().PageShowAsync<MyPage>();

            new Html(DivContainer()) { TextHtml = "Delete item: " };
            ButtonDelete();

            var grid = GridContact();
            GridPerson();

            await grid.LoadAsync();
        }

        public Html Label()
        {
            return this.ComponentGetOrCreate<Html>();
        }

        public Div DivContainer()
        {
            return this.ComponentGetOrCreate<Div>(div => div.CssClass = "container");
        }

        public Grid GridContact()
        {
            return DivContainer().ComponentGetOrCreate<Grid>("Contact");
        }

        public Grid GridPerson()
        {
            return DivContainer().ComponentGetOrCreate<Grid>("Person");
        }

        public Button ButtonDelete()
        {
            return DivContainer().ComponentGetOrCreate<Button>((button) => button.Text = "Delete");
        }

        protected override IQueryable GridLookupQuery(Grid grid, Row row, string fieldName, string text)
        {
            return UtilDal.Query<HelloWorld>().Where(item => item.Text.StartsWith(text));
        }

        protected override string GridLookupSelected(Grid grid, Row row, string fieldName, Row rowLookupSelected)
        {
            return ((HelloWorld)rowLookupSelected).Text;
        }

        protected override IQueryable GridQuery(Grid grid)
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

        protected override async Task GridRowSelectedAsync(Grid grid)
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
            MessageBox messageBox = this.ComponentGet<MessageBox>();
            if (messageBox?.IsYes != null)
            {
                if (messageBox.IsYes == true)
                {
                    new Html(this).TextHtml = "Deleted!";
                }
                messageBox.ComponentRemove();
            }

            Name = "HelloWorld " + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");

            Language language = DivContainer().ComponentGet<LanguagePage>().Grid().RowSelected() as Language;

            Label().TextHtml = language?.Text;

            return base.ProcessAsync();
        }
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
            return this.ComponentGetOrCreate<Grid>();
        }

        public Button ButtonDelete()
        {
            return this.ComponentGetOrCreate((Button button) => button.Text = "Delete");
        }

        protected override IQueryable GridQuery(Grid grid)
        {
            return UtilDal.Query<HelloWorld>();
        }

        protected override void GridQueryConfig(Grid grid, Config config)
        {
            config.ConfigGridQuery = new[] { new FrameworkConfigGridBuiltIn { RowCountMax = 3 } }.AsQueryable();
            config.ConfigFieldQuery = new[] {
                new FrameworkConfigFieldBuiltIn { FieldNameCSharp = "Text", Text = "My Text" },
            }.AsQueryable();
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
            var messageBox = this.ComponentGet<MessageBox>();
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
                this.ComponentGet<MessageBox>().ComponentRemove();
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
