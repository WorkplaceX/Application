namespace Application
{
    using Database.dbo;
    using Framework.Json;
    using System.Threading.Tasks;

    public class AppMain : AppJson
    {
        public override async Task InitAsync()
        {
            await new Grid<HelloWorld>(this).LoadAsync();
        }
    }
}
