using Project.ViewModel;
using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class OutputInfo : BaseViewModel
    {
        public string Id { get; set; } = null!;

        public string IdObject { get; set; } = null!;

        public string IdOutputInfo { get; set; } = null!;

        public int IdCustomer { get; set; }




        private int _Count;
        public int? Count { get => _Count; set { _Count = (int)value; OnPropertyChanged(); } }


        private string? _Status;
        public string? Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        public string? IdInputInfo { get; set; }

        //public virtual Customer IdCustomerNavigation { get; set; } = null!;

        private Customer _IdCustomerNavigation;
        public virtual Customer IdCustomerNavigation { get => _IdCustomerNavigation; set { _IdCustomerNavigation = value; OnPropertyChanged(); } }
        
        //public virtual InputInfo? IdInputInfoNavigation { get; set; }

        private InputInfo _IdInputInfoNavigation;
        public virtual InputInfo IdInputInfoNavigation { get => _IdInputInfoNavigation; set { _IdInputInfoNavigation = value; OnPropertyChanged(); } }

        //public virtual Output IdNavigation { get; set; } = null!;

        private Output _IdNavigation;
        public virtual Output IdNavigation { get => _IdNavigation; set { _IdNavigation = value; OnPropertyChanged(); } }

        //public virtual Object IdObjectNavigation { get; set; } = null!;

        private Object _IdObjectNavigation;
        public virtual Object IdObjectNavigation { get => _IdObjectNavigation; set { _IdObjectNavigation = value; OnPropertyChanged(); } }
    }
}
