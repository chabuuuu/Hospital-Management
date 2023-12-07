using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTTQ_DoAn.Model;

namespace LTTQ_DoAn.Resource
{
    public class BindingImage
    {
        public string ConvertPath(string imageName)
        {
            string projectPath = Environment.CurrentDirectory;
            string externalPath = "";
            for (int i = 0; i < projectPath.Length; i++)
            {
                if (projectPath[i] == '\\')
                {
                    if (projectPath.Length >= i + 31)
                    {
                        string temp = projectPath.Substring(i + 1, 30);
                        if (temp == "LTTQ_DoAn")
                        {
                            externalPath = projectPath.Substring(0, i);
                            break;
                        }
                    }
                }
            }
            return externalPath + "\\LTTQ_DoAn\\Photo" + imageName;
        }
    }
}
