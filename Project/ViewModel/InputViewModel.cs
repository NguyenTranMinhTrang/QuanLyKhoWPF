using System;
using System.Collections.Generic;
using Project.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModel
{
    public class InputViewModel : BaseViewModel
    {
        private ObservableCollection<InputInfo> _List;
        public ObservableCollection<InputInfo> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Input> _InputList;
        public ObservableCollection<Input> InputList { get => _InputList; set { _InputList = value; OnPropertyChanged(); } }

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
                    SelectedInput = SelectedItem.IdInputNavigation;
                    DateInput = SelectedInput.DateInput;

                }
            }
        }

        private Input _SelectedInput;
        public Input SelectedInput
        {
            get => _SelectedInput;
            set
            {
                _SelectedInput = value;
                OnPropertyChanged();
                DateInput = SelectedInput.DateInput;
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

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public InputViewModel()
        {
            List = new ObservableCollection<InputInfo>(DataProvider.Ins.DB.InputInfos);
            InputList = new ObservableCollection<Input>(DataProvider.Ins.DB.Inputs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;

            }, (p) =>
            {
                var addObject = DataProvider.Ins.DB.Objects.Where(x => x.DisplayName == ObjectDisplayName).SingleOrDefault();
                if (addObject != null )
                {
                    var Object = new InputInfo() { Id = Guid.NewGuid().ToString(), Count = Count, IdInput = SelectedInput.Id, IdObject = addObject.Id , InputPrice = InputPrice, OutputPrice = OutputPrice, Status = Status };

                    DataProvider.Ins.DB.InputInfos.Add(Object);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Add(Object);

                    
                }
                
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedInput == null)
                    return false;

                var displayList = DataProvider.Ins.DB.InputInfos.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var Object = DataProvider.Ins.DB.InputInfos.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

                var addObject = DataProvider.Ins.DB.Objects.Where(x => x.DisplayName == ObjectDisplayName).SingleOrDefault();
                if (addObject != null)
                {
                    if (addObject.Id != SelectedItem.IdObject)
                    {
                        Object.IdObject = addObject.Id;
                        Object.IdObjectNavigation = addObject;
                    }
                }

                Object.Count = Count;
                Object.InputPrice = InputPrice;
                Object.OutputPrice = OutputPrice;
                Object.Status = Status;
                Object.IdInput = SelectedInput.Id;
                Object.IdInputNavigation = SelectedInput;
                DataProvider.Ins.DB.SaveChanges();
            });
        }
    }
}
