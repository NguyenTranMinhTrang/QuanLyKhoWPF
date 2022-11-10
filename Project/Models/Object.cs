using Project.ViewModel;
using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class Object : BaseViewModel
    {
        public Object()
        {
            InputInfos = new HashSet<InputInfo>();
            OutputInfos = new HashSet<OutputInfo>();
        }

        public string Id { get; set; } = null!;

        private string _DisplayName;
        public string? DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private int _IdUnit;
        public int? IdUnit { get => _IdUnit; set { _IdUnit = (int)value; OnPropertyChanged(); } }

        private int _IdSuplier;
        public int? IdSuplier { get => _IdSuplier; set { _IdSuplier = (int)value; OnPropertyChanged(); } }

        private string _Qrcode;
        public string? Qrcode { get => _Qrcode; set { _Qrcode = value; OnPropertyChanged(); } }

        private string _BarCode;
        public string? BarCode { get => _BarCode; set { _BarCode = value; OnPropertyChanged(); } }

        //public virtual Suplier IdSuplierNavigation { get; set; } = null!;

        private Suplier _IdSuplierNavigation;
        public virtual Suplier IdSuplierNavigation { get => _IdSuplierNavigation; set { _IdSuplierNavigation = value; OnPropertyChanged(); } }
        //public virtual Unit IdUnitNavigation { get; set; } = null!;

        private Unit _IdUnitNavigation;
        public virtual Unit IdUnitNavigation { get => _IdUnitNavigation; set { _IdUnitNavigation = value; OnPropertyChanged(); } }
        public virtual ICollection<InputInfo> InputInfos { get; set; }
        public virtual ICollection<OutputInfo> OutputInfos { get; set; }
    }
}
