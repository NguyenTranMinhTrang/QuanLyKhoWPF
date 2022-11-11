using Project.ViewModel;
using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class InputInfo : BaseViewModel
    {
        
        public string Id { get; set; } = null!;
        public string IdObject { get; set; } = null!;

        public string IdInput { get; set; } = null!;

        private int _Count;
        public int? Count { get => _Count; set { _Count = (int)value; OnPropertyChanged(); } }

        private double _InputPrice;
        public double? InputPrice { get => _InputPrice; set { _InputPrice = (double)value; OnPropertyChanged(); } }

        private double _OutputPrice;
        public double? OutputPrice { get => _OutputPrice; set { _OutputPrice = (double)value; OnPropertyChanged(); } }

        private string _Status;
        public string? Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        //public virtual Input IdInputNavigation { get; set; } = null!;

        private Input _IdInputNavigation;
        public virtual Input IdInputNavigation { get => _IdInputNavigation; set { _IdInputNavigation = value; OnPropertyChanged(); } }
        //public virtual Object IdObjectNavigation { get; set; } = null!;

        private Object _IdObjectNavigation;
        public virtual Object IdObjectNavigation { get => _IdObjectNavigation; set { _IdObjectNavigation = value; OnPropertyChanged(); } }
    }
}
