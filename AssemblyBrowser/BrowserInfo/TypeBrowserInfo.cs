using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.BrowserInfo
{
    public class TypeBrowserInfo : INotifyPropertyChanged
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

        private List<string> signatures;

        public List<string> Signatures
        {
            get => signatures;
            set
            {
                signatures = value;
                OnPropertyChanged(nameof(Signatures));
            }
        }

        public TypeBrowserInfo()
        {
            signatures = new List<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}