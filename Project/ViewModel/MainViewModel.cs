using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;

        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SuplierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand ObjectCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand OutputCommand { get; set; }

        public MainViewModel()
        {
            
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
                if (loginVM.IsLogin)
                {
                    p.Show();
                    LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }
            }
             );

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UnitWindow wd = new UnitWindow(); wd.ShowDialog(); });
            SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SuplierWindow wd = new SuplierWindow(); wd.ShowDialog(); });
            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CustomerWindow wd = new CustomerWindow(); wd.ShowDialog(); });
            ObjectCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ObjectWindow wd = new ObjectWindow(); wd.ShowDialog(); });
            UserCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow wd = new UserWindow(); wd.ShowDialog(); });
            InputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { InputWindow wd = new InputWindow(); wd.ShowDialog(); });
            OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OutputWindow wd = new OutputWindow(); wd.ShowDialog(); });

            var a = DataProvider.Ins.DB.Users.ToList();
        }

        void LoadTonKhoData()
        {
            TonKhoList = new ObservableCollection<TonKho>();

            var objectList = DataProvider.Ins.DB.Objects;

            int i = 1;
            foreach (var item in objectList)
            {
                var inputList = DataProvider.Ins.DB.InputInfos.Where(p => p.IdObject == item.Id);
                var outputList = DataProvider.Ins.DB.OutputInfos.Where(p => p.IdObject == item.Id);

                int sumInput = 0;
                int sumOutput = 0;

                if (inputList != null && inputList.Count() > 0)
                {
                    sumInput = (int)inputList.Sum(p => p.Count);
                }
                if (outputList != null && outputList.Count() > 0)
                {
                    sumOutput = (int)outputList.Sum(p => p.Count);
                }

                TonKho tonkho = new TonKho();
                tonkho.STT = i;
                tonkho.Count = sumInput - sumOutput;
                tonkho.Object = item;

                TonKhoList.Add(tonkho);

                i++;
            }

        }

    }
}
