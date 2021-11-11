using System.Reflection;

namespace AssemblyBrowserLibrary
{
    public interface ISignatureInfo
    {
        string GetInfo(MemberInfo info);

        bool CanGetInfo(MemberInfo info);
    }
}