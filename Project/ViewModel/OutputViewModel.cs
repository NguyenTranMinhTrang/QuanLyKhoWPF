using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModel
{
    public class OutputViewModel : BaseViewModel
    {
        private ObservableCollection<OutputInfo> _List;
        public ObservableCollection<OutputInfo> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Output> _OutputList;
        public ObservableCollection<Output> OutputList { get => _OutputList; set { _OutputList = value; OnPropertyChanged(); } }

        private ObservableCollection<Customer> _Customer;
        public ObservableCollection<Customer> Customer { get => _Customer; set { _Customer = value; OnPropertyChanged(); } }

        private ObservableCollection<Input> _Input;
        public ObservableCollection<Input> Input { get => _Input; set { _Input = value; OnPropertyChanged(); } }

        private string _ObjectDisplayName;
        public string? ObjectDisplayName { get => _ObjectDisplayName; set { _ObjectDisplayName = value; OnPropertyChanged(); } }

        private int _Count;
        public int? Count { get => _Count; set { _Count = (int)value; OnPropertyChanged(); } }

        private double _OutputPrice;
        public double? OutputPrice { get => _OutputPrice; set { _OutputPrice = (double)value; OnPropertyChanged(); } }

        private string _Status;
        public string? Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private DateTime? _DateOutput;
        public DateTime? DateOutput { get => _DateOutput; set { _DateOutput = value; OnPropertyChanged(); } }

        private Output _SelectedOutput;
        public Output SelectedOutput
        {
            get => _SelectedOutput;
            set
            {
                _SelectedOutput = value;
                OnPropertyChanged();
                DateOutput = SelectedOutput.DateOutput;
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
            }
        }

        private OutputInfo _SelectedItem;
        public OutputInfo SelectedItem
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
                    Status = SelectedItem.Status;
                    OutputPrice = SelectedItem.IdInputInfoNavigation.OutputPrice;
                    SelectedCustomer = SelectedItem.IdCustomerNavigation;
                    SelectedOutput = SelectedItem.IdNavigation;
                    DateOutput = SelectedOutput.DateOutput;
                    SelectedInput = DataProvider.Ins.DB.Inputs.Where(x => x.Id == SelectedItem.IdInputInfoNavigation.IdInput).SingleOrDefault();
                }
            }
        }

        private Customer _SelectedCustomer;
        public Customer SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public OutputViewModel()
        {
            List = new ObservableCollection<OutputInfo>(DataProvider.Ins.DB.OutputInfos);
            OutputList = new ObservableCollection<Output>(DataProvider.Ins.DB.Outputs);
            Customer = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers);
            Input = new ObservableCollection<Input>(DataProvider.Ins.DB.Inputs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedOutput == null || SelectedCustomer == null || ObjectDisplayName.Equals("") || Count == 0 || SelectedInput == null )
                    return false;
                var addObject = DataProvider.Ins.DB.Objects.Where(x => x.DisplayName == ObjectDisplayName).SingleOrDefault();
                if (addObject != null)
                {
                    var inputInfo = DataProvider.Ins.DB.InputInfos.Where(x => x.IdObject == addObject.Id && x.IdInput == SelectedInput.Id).SingleOrDefault();
                    if (inputInfo != null)
                    {
                        OutputPrice = inputInfo.OutputPrice;
                        return true;
                    }
                }

                return false;

            }, (p) =>
            {
                var addObject = DataProvider.Ins.DB.Objects.Where(x => x.DisplayName == ObjectDisplayName).SingleOrDefault();
                if (addObject != null)
                {
                    var inputInfo = DataProvider.Ins.DB.InputInfos.Where(x => x.IdObject == addObject.Id && x.IdInput == SelectedInput.Id).SingleOrDefault();
                    if (inputInfo != null)
                    {
                        OutputPrice = inputInfo.OutputPrice;
                        var Object = new OutputInfo() { Id = SelectedOutput.Id, Count = Count, IdObject = addObject.Id, IdOutputInfo = Guid.NewGuid().ToString(), IdCustomer = SelectedCustomer.Id, Status = Status, IdInputInfo = inputInfo.IdInput };
                        DataProvider.Ins.DB.OutputInfos.Add(Object);
                        DataProvider.Ins.DB.SaveChanges();

                        List.Add(Object);
                    }
                }


            });

        }

    }
}
