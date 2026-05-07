

namespace CodeGenerator_Modules
{
    public class clsColumnDataUILayer : clsColumnDataDataAccessLayer
    {
        public bool IsForeignKey { get; set; }
        public string ControlName { get; set; }
        public bool Generate { get; set; }
    }
}
