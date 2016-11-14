using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace vs2ghx
{
    class Program
    {
        static void Main(string[] args)
        {
            var basicPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "base.ghx");
            string ghxBase = "";
            if (File.Exists(basicPath)) {
                ghxBase = File.ReadAllText(basicPath);
            }else
            {
                var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("vs2ghx.base.ghx");
                var sr = new StreamReader(stream);
                ghxBase = sr.ReadToEnd();
            }

            var resultBuilder = new StringBuilder();
            foreach(var item in args)
            {
                if (File.Exists(item))
                {
                    var text = System.IO.File.ReadAllText(item,Encoding.UTF8);
                    resultBuilder.Append("#region " + item + "\r\n");
                    var regex = new System.Text.RegularExpressions.Regex(@"namespace (.+?)\{(.+)\}", System.Text.RegularExpressions.RegexOptions.Singleline);
                    var match= regex.Match(text);
                    if (!match.Success)
                    {
                        Console.Error.WriteLine("regex fail.");
                    }
                    resultBuilder.Append(match.Groups[2]);
                    resultBuilder.Append("#endregion\r\n\r\n");
                }
            }

            ghxBase = ghxBase.Replace("[--Additional Source--]", System.Security.SecurityElement.Escape(resultBuilder.ToString()));
            ghxBase = ghxBase.Replace("[--Main Program--]", "");

            Console.Write(ghxBase);
        }
    }
}
