using System.Reflection;
using System.Text;

namespace AssemblyBrowserLibrary
{
    public class FieldInfoGetter : ISignatureInfo
    {
        public string GetInfo(MemberInfo info)
        {
            var fieldInfo = info as FieldInfo;
            var builder = new StringBuilder();
            builder.Append(fieldInfo.IsPublic ? "public " : "private ");
            builder.Append(fieldInfo.FieldType.Name);
            builder.Append(' ');
            builder.Append(fieldInfo.Name);
            return builder.ToString();
        }

        public bool CanGetInfo(MemberInfo info)
        {
            return info is FieldInfo;
        }
    }
}