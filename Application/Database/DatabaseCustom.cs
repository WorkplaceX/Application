// Custom implementation of database table and field.

using Framework.Dal;

namespace Database.Memory
{
    public class Navigation : Row
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }

    public class Language : Row
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string FlagIcon { get; set; }
    }
}