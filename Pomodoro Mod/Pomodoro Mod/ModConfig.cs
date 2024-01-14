using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro_Mod
{
    public sealed class ModConfig
    {
        public bool ExampleBoolean { get; set; }
        public int RestTime { get; set; }

        public string ExampleString { get; set; }
        = string.Empty;
        public int Time { get; set; }
        public int SessionNum { get; set; }
        public bool ExampleCheckbox { get; set; }
        public ModConfig() { }

    }
}
