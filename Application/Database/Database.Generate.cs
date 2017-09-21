namespace Database.dbo
{
    using Framework.DataAccessLayer;
    using System;

    [SqlTable("dbo", "Attribute")]
    public partial class Attribute : Row
    {
        [SqlColumn("Id", typeof(Attribute_Id))]
        public int Id { get; set; }

        [SqlColumn("HelloWorldId", typeof(Attribute_HelloWorldId))]
        public int HelloWorldId { get; set; }

        [SqlColumn("Name", typeof(Attribute_Name))]
        public string Name { get; set; }

        [SqlColumn("Text", typeof(Attribute_Text))]
        public string Text { get; set; }
    }

    public partial class Attribute_Id : Cell<Attribute> { }

    public partial class Attribute_HelloWorldId : Cell<Attribute> { }

    public partial class Attribute_Name : Cell<Attribute> { }

    public partial class Attribute_Text : Cell<Attribute> { }

    [SqlTable("dbo", "AttributeNote")]
    public partial class AttributeNote : Row
    {
        [SqlColumn("Id", typeof(AttributeNote_Id))]
        public int Id { get; set; }

        [SqlColumn("AttributeId", typeof(AttributeNote_AttributeId))]
        public int AttributeId { get; set; }

        [SqlColumn("Text", typeof(AttributeNote_Text))]
        public string Text { get; set; }
    }

    public partial class AttributeNote_Id : Cell<AttributeNote> { }

    public partial class AttributeNote_AttributeId : Cell<AttributeNote> { }

    public partial class AttributeNote_Text : Cell<AttributeNote> { }

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
