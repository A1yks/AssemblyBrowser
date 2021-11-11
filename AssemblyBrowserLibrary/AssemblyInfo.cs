using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowserLibrary
{
    public class AssemblyInfo
    {
        private readonly Assembly assembly;
        private readonly List<ISignatureInfo> infoGetters;
        
        public AssemblyInfo(string fileName)
        {
            assembly = Assembly.LoadFrom(fileName);
            infoGetters = new List<ISignatureInfo>
            {
                new MethodInfoGetter(),
                new FieldInfoGetter(),
                new PropertyInfoGetter()
            };
        }

        public List<string> GetNamespaces()
        {
            return assembly.GetTypes()
                .Select(t => t.Namespace)
                .Where(n => n != null)
                .Distinct()
                .Where(n => !n.StartsWith("System"))
                .Where(n => !n.StartsWith("Microsoft"))
                .ToList();
        }

        public List<string> GetTypes(string ns)
        {
            return assembly.GetTypes()
                .Where(t => t.Namespace != null)
                .Where(t => t.Namespace == ns)
                .Select(t => t.Name)
                .Where(n => !n.Contains('<'))
                .ToList();
        }

        public List<string> GetMethods(string ns, string tp)
        {
            return assembly.GetTypes()
                .Where(t => t.Namespace != null)
                .Where(t => t.Namespace == ns)
                .First(t => t.Name == tp)
                .GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                .Where(m => !m.Name.Contains('<'))
                .Select(GetSignature)
                .Where(s => s != null)
                .ToList();
        }

        private string GetSignature(MemberInfo info)
        {
            foreach (var printer in infoGetters)
            {
                if (printer.CanGetInfo(info))
                {
                    return printer.GetInfo(info);
                }
            }

            return null;
        }
    }
}