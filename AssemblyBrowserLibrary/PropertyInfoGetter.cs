using System.Reflection;

namespace AssemblyBrowserLibrary
{
    public class PropertyInfoGetter : ISignatureInfo
    {
        public string GetInfo(MemberInfo info)
        {
            bool isGetter = false;
            bool isSetter = false;
            string getterAccessModifier = "";
            string setterAccessModifier = "";
            string type = "";
            var propertyInfo = info as PropertyInfo;
            var accessors = propertyInfo.GetAccessors(true);
            foreach (var methodInfo in accessors)
            {
                if (methodInfo.ReturnType == typeof(void))
                {
                    isSetter = true;
                    setterAccessModifier = methodInfo.IsPublic ? "" : "private ";
                    type = methodInfo.GetParameters()[0].ParameterType.Name;
                }
                else
                {
                    isGetter = true;
                    getterAccessModifier = methodInfo.IsPublic ? "public" : "private";
                    type = methodInfo.ReturnType.Name;
                }
            }

            return $"{(isGetter ? getterAccessModifier : setterAccessModifier)} {type} {info.Name} {{ {(isGetter ? "get;" : "")} {setterAccessModifier}{(isSetter ? "set;" : "")} }}";
        }

        public bool CanGetInfo(MemberInfo info)
        {
            return info is PropertyInfo;
        }
    }
}