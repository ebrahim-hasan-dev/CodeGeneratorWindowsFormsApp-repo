

namespace CodeGenerator_Modules
{
    public class clsColumnDataBusinessLayer
    {
        private string _Name = "";
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;

                NameWithout_ = _Name?.Replace("_", "");
            }
        }

        public string NameWithout_ { get; set; } = "";
        public string Type { get; set; } = "";
        public bool IsPrimaryKey { get; set; }
    }
}

