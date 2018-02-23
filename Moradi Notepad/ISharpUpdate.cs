using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Moradi_Notepad
{
    public interface ISharpUpdate
    {
        string ApplicationName { get; }
        string ApplicationID { get; }
        Assembly ApplicationAssembly { get; }
        Icon ApplicationIcon { get; }
        Uri UpdateXmlLocation { get; }
        Form Context { get; }
    }
}
