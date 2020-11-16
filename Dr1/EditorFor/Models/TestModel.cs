namespace EditorFor.Models
{
    public class TestModel
    {
        public int IntProperty { get; set; }
        public long LongProperty { get; set; }
        public bool BoolProperty { get; set; }
        public string StringProperty { get; set; }
        public Enum EnumProperty { get; set; }
        public NestedClass ClassProperty { get; set; }
    }

    public enum Enum
    {
        Option1,
        Option2, 
        Option3,
        Option4
    }

    public class NestedClass
    {
        public int NestedIntProperty { get; set; }
        public string NestedStringProperty { get; set; }
    }
}