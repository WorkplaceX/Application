using Application;

namespace BuildTool
{
    class Program
    {
        static void Main(string[] args)
        {
            new AppBuildToolMain(new AppMain()).Run(args);
        }
    }
}