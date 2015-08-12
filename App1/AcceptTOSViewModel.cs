using IdentityMine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBoxVideo.ViewModel
{
    /// <summary>
    /// Defines the purchase options view model
    /// </summary>
    public class AcceptTOSViewModel : ResultItem
    {
        #region Fields

        private bool isBusy = true;

        #endregion

        #region Properties

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        public string Token { get; set; }

        #endregion

        #region Constructor

        public AcceptTOSViewModel()
        {
           
        }

        #endregion

    }
}
