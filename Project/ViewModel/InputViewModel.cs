using System;
using System.Collections.Generic;
using Project.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public class InputViewModel : BaseViewModel
    {
        private ObservableCollection<InputInfo> _List;
        public ObservableCollection<InputInfo> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private InputInfo _SelectedItem;
        public InputInfo SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    ObjectDisplayName = SelectedItem.IdObjectNavigation.DisplayName;
                    Count = SelectedItem.Count;
                    InputPrice = SelectedItem.InputPrice;
                    OutputPrice = SelectedItem.OutputPrice;
                    Status = SelectedItem.Status;
                    DateInput = SelectedItem.IdInputNavigation.DateInput;

                }
            }
        }
       

        private string _ObjectDisplayName;
        public string? ObjectDisplayName { get => _ObjectDisplayName; set { _ObjectDisplayName = value; OnPropertyChanged(); } }

        private int _Count;
        public int? Count { get => _Count; set { _Count = (int)value; OnPropertyChanged(); } }

        private double _InputPrice;
        public double? InputPrice { get => _InputPrice; set { _InputPrice = (double)value; OnPropertyChanged(); } }

        private double _OutputPrice;
        public double? OutputPrice { get => _OutputPrice; set { _OutputPrice = (double)value; OnPropertyChanged(); } }

        private string _Status;
        public string? Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private DateTime? _DateInput;
        public DateTime? DateInput { get => _DateInput; set { _DateInput = value; OnPropertyChanged(); } }

        public InputViewModel()
        {
            List = new ObservableCollection<InputInfo>(DataProvider.Ins.DB.InputInfos);
        }
    }
}
