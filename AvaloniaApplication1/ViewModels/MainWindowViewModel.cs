using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using AvaloniaApplication1.Models;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Backend = new Backend();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public Backend Backend { get; set; }
        private IAssetLoader? assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
        public ObservableCollection<CountryFilterListViewModel> SelectedCountries { get; } = new ObservableCollection<CountryFilterListViewModel>();

        #region Filter_variables
        public string hostMessage;
        public string HostMessage 
        {
            get => hostMessage;
            set => this.RaiseAndSetIfChanged(ref hostMessage, value);
        }

        public bool OnlyJoinable { get; set; }

        private ObservableCollection<CountryFilterListViewModel> _countryFilterList = new ObservableCollection<CountryFilterListViewModel>();
        public ObservableCollection<CountryFilterListViewModel> CountryFilterList
        {
            get { return _countryFilterList; }
            set { _countryFilterList = value; }
        }

        private string _selectedModel;
        public string SelectedModel
        {
            get { return _selectedModel; }
            set { _selectedModel = value; NotifyPropertyChanged("SelectedModel"); }
        }

        #endregion

        public ObservableCollection<Host> hosts;
        public ObservableCollection<Host> Hosts
        {
            get
            {
                return hosts;
            }
            set
            {
                hosts = this.RaiseAndSetIfChanged(ref hosts, value); 
                NotifyPropertyChanged("Hosts");
            }
        }

        public List<Host> FetchHosts()
        {
            var task = Task.Run(async () => await Backend.fetch());
            var result = task.Result;
            return result;
        }

        public void OnClickCommand()
        {
            // temporary country list
            List<string> countryFilterList = new List<string>();
            countryFilterList = SelectedCountries.Select(c => c.CountryCode).ToList();

            // fetch hosts
            List<Host> NewHosts = FetchHosts();

            #region Filters
            if (HostMessage is null)
            {
                HostMessage = "";
            }

            NewHosts = Backend.MessageFilter(NewHosts, HostMessage);
            NewHosts = Backend.OnlyJoinableFilter(NewHosts, OnlyJoinable);
            NewHosts = Backend.CountryFilter(NewHosts, countryFilterList);
            
            foreach (var item in NewHosts)
            {
                if (!CountryFilterList.Select(c=>c.CountryCode).Contains(item.host_country))
                {
                    var countryIcon = new Bitmap(assets.Open(new Uri("avares://AvaloniaApplication1/Assets/flags/" + item.host_country + ".png")));
                    CountryFilterList.Add(new CountryFilterListViewModel() { CountryCode = item.host_country, CountryIcon =  countryIcon});
                }
            }
            #endregion

            #region Fill list
            Hosts = new ObservableCollection<Host>();
            Hosts.Clear();
            foreach (var item in NewHosts)
            {
                Hosts.Add(item);
            }
            #endregion
        }
    }
}