using System.Collections.Generic;
using AssemblyBrowser.BrowserInfo;
using AssemblyBrowserLibrary;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private AssemblyInfo assembly;
        private List<NamespaceBrowserInfo> namespaces;
        
        [SetUp]
        public void Setup()
        {
            assembly = new AssemblyInfo("AssemblyBrowserLibrary.dll");
            
            namespaces = new List<NamespaceBrowserInfo>();
            assembly.GetNamespaces().ForEach(namespaceName =>
            {
                var namespaceBrowserInfo = new NamespaceBrowserInfo
                {
                    Name = namespaceName
                };
                assembly.GetTypes(namespaceName).ForEach(type =>
                {
                    var typeBrowserInfo = new TypeBrowserInfo
                    {
                        Name = type
                    };
                    assembly.GetMethods(namespaceName, type).ForEach(methodName => { typeBrowserInfo.Signatures.Add(methodName); });
                    namespaceBrowserInfo.Types.Add(typeBrowserInfo);
                });
                namespaces.Add(namespaceBrowserInfo);
            });
        }
        
        [Test]
        public void TestAssemblyName()
        {
            Assert.AreEqual("AssemblyBrowserLibrary", namespaces[0].Name);
        }
        
        [Test]
        public void TestTypesCount()
        {
            Assert.AreEqual(5, namespaces[0].Types.Count);
        }
        
        [Test]
        public void TestMethodsInfoName()
        {
            Assert.AreEqual("private String GetSignature(MemberInfo)", namespaces[0].Types[0].Signatures[3]);
        }
    }
}