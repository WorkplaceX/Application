// Do not modify this file. It's generated by Framework.Cli.

namespace DatabaseIntegrate.dbo
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Framework.DataAccessLayer;
    using Framework.DataAccessLayer.Integrate;
    using Database.dbo;

    public static class HelloWorldIntegrateApplication
    {
        public enum IdNameEnum { [IdNameEnum(null)]None = 0, [IdNameEnum("Example1")]Example1 = 1, [IdNameEnum("Example2")]Example2 = 2 }

        public static HelloWorldIntegrate Row(this IdNameEnum value)
        {
            return RowList.Where(item => item.IdName == IdNameEnumAttribute.IdNameFromEnum(value)).SingleOrDefault();
        }

        public static IdNameEnum IdName(string value)
        {
            return IdNameEnumAttribute.IdNameToEnum<IdNameEnum>(value);
        }

        public static string IdName(this IdNameEnum value)
        {
            return IdNameEnumAttribute.IdNameFromEnum(value);
        }

        public static List<HelloWorldIntegrate> RowList
        {
            get
            {
                var result = new List<HelloWorldIntegrate>
                {
                    new HelloWorldIntegrate {Id = 1, IdName = "Example1", Name = "Example1", Text = "Hello example" },
                    new HelloWorldIntegrate {Id = 2, IdName = "Example2", Name = "Example2", Text = "Hello other example" },
                };
                return result;
            }
        }
    }
}