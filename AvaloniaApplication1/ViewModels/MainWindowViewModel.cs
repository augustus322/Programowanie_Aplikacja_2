using AvaloniaApplication1.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Host> hosts;
        public Backend Backend { get; set; }

        public MainWindowViewModel()
        {
            Backend = new Backend();
        }

        public ObservableCollection<Host> Hosts 
        {
            get
            {
                List<Host> hosty = FetchHosts();
                return new ObservableCollection<Host> (hosty);
            }
            set
            {
                hosts = value;   
            }
        }
        public List<Host> FetchHosts()
        {
            var task = Task.Run(async () => await Backend.fetch());
            var result = task.Result;
            return result;
        }
    }
}