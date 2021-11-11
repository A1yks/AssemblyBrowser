using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.BrowserInfo
{
    public class NamespaceBrowserInfo : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        
        private List<TypeBrowserInfo> types;

        public List<TypeBrowserInfo> Types
        {
            get => types;
            set
            {
                types = value;
                OnPropertyChanged(nameof(Types));
            }
        }

        public NamespaceBrowserInfo()
        {
            types = new List<TypeBrowserInfo>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}