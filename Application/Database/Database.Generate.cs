namespace Database.dbo
{
    using Framework.DataAccessLayer;
    using System;

    [SqlTable("dbo", "HelloWorld")]
    public partial class HelloWorld : Row
    {
        [SqlColumn("Id", typeof(HelloWorld_Id))]
        public int Id { get; set; }

        [SqlColumn("Text", typeof(HelloWorld_Text))]
        public string Text { get; set; }

        [SqlColumn("Number", typeof(HelloWorld_Number))]
        public double? Number { get; set; }
    }

    public partial class HelloWorld_Id : Cell<HelloWorld> { }

    public partial class HelloWorld_Text : Cell<HelloWorld> { }

    public partial class HelloWorld_Number : Cell<HelloWorld> { }
}
