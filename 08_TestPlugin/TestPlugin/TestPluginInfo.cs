using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace TestPlugin
{
    public class TestPluginInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "TestPlugin";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("a59a39a3-4614-4f80-86d2-3c01ad790c2d");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
