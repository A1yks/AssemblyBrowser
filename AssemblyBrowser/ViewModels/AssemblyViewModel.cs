using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssemblyBrowser.BrowserInfo;
using AssemblyBrowser.Command;
using AssemblyBrowserLibrary;
using Microsoft.Win32;

namespace AssemblyBrowser.ViewModels
{
    public class AssemblyViewModel : INotifyPropertyChanged
    {
        private List<NamespaceBrowserInfo> namespaces;

        public List<NamespaceBrowserInfo> Namespaces
        {
            get => namespaces;
            set
            {
                if (Equals(value, namespaces)) return;
                namespaces = value;
                OnPropertyChanged(nameof(Namespaces));
            }
        }

        private string selectedFile;

        public string SelectedFile
        {
            get => selectedFile;
            set
            {
                selectedFile = value;
                var browser = new AssemblyInfo(value);
                var ns = new List<NamespaceBrowserInfo>();
                browser.GetNamespaces().ForEach(namespaceName =>
                {
                    var namespaceBrowserInfo = new NamespaceBrowserInfo
                    {
                        Name = namespaceName
                    };
                    browser.GetTypes(namespaceName).ForEach(type =>
                    {
                        var typeBrowserInfo = new TypeBrowserInfo
                        {
                            Name = type
                        };
                        browser.GetMethods(namespaceName, type).ForEach(methodName => { typeBrowserInfo.Signatures.Add(methodName); });
                        namespaceBrowserInfo.Types.Add(typeBrowserInfo);
                    });
                    ns.Add(namespaceBrowserInfo);
                });
                Namespaces = ns;
            }
        }

        private RelayCommand openCommand;

        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                       (openCommand = new RelayCommand(obj =>
                       {
                           var d = new OpenFileDialog();
                           d.Multiselect = false;
                           d.Filter = "Assembly | *.dll";
                           if (d.ShowDialog() == true)
                           {
                               SelectedFile = d.FileName;
                           }
                       }));
            }
        }

        public AssemblyViewModel()
        {
            namespaces = new List<NamespaceBrowserInfo>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}