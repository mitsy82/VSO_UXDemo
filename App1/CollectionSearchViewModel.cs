using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityMine.ViewModel;
using System.Windows;

namespace XBoxVideo.ViewModel
{
    public class CollectionSearchViewModel : SearchVMBase
    {

        #region Properties

        public string NoInternetErrorMessage
        {
            get
            {
                return string.Empty;
            }
        }

        public bool ShowContent
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Constructor

        public CollectionSearchViewModel()
        {
            MainViewModel.Instance.IsCollectionResults = true;
        }

        #endregion

        #region Methods

        public void PrepareResult()
        {
            if (MainViewModel.Instance.PurchaseHistory.Count == 0 && !PurchaseHistoryHelper.IsDownloadCompleted)
            {
                PurchaseHistoryHelper.DownloadCompleted += PurchaseHistoryHelper_DownloadCompleted;
            }
            else
            {
                SetResult();
            }

        }

        void PurchaseHistoryHelper_DownloadCompleted(object sender, EventArgs e)
        {
            PurchaseHistoryHelper.DownloadCompleted -= PurchaseHistoryHelper_DownloadCompleted;
            SetResult();
        }

        private void SetResult()
        {
            try
            {
                MainVMBase.InstanceBase.Dispatcher.BeginInvoke(new Action<String>((s) =>
                {

                    Movies.Collection.Clear();
                    TVSeries.Collection.Clear();
                    AllVideos.Collection.Clear();

                    foreach (Video item in PurchaseHistoryHelper.SearchAllCollectionVideos())
                    {
                        item.LoadIsoImages();
                        AllVideos.Collection.Add(item);
                    }

                    foreach (Video item in PurchaseHistoryHelper.SearchMovieCollection())
                    {
                        Movies.Collection.Add(item);
                    }
                    foreach (Video item in PurchaseHistoryHelper.SearchTVCollection())
                    {
                        TVSeries.Collection.Add(item);
                    }

                    //AssignVideoSize();
                }), "");
            }
            catch (Exception)
            {
                //TODO :Error Handling
            }

        }

        public override void SetErrorMessageValues(bool isBack)
        {
            Movies.Header = MainViewModel.Instance.IsAuthenticated ? MainViewModel.Instance.GetStringResource("WP8_VIDEO_CollectionResultsText") : null;
            TVSeries.Header = MainViewModel.Instance.IsAuthenticated ? MainViewModel.Instance.GetStringResource("WP8_VIDEO_CollectionResultsText") : null;


            AllPivotErrorMessage = AllVideos.Collection.Count == 0 ? MainViewModel.Instance.GetStringResource("WP8_VIDEO_NoResultFoundText") : null;
            if (!MainViewModel.Instance.IsAuthenticated)
            {
                MoviesPivotErrorMessage = Movies.Collection.Count == 0 ? MainViewModel.Instance.GetStringResource("WP8_VIDEO_NoResultFoundText") : null;
                TVSeriesPivotErrorMessage = TVSeries.Collection.Count == 0 ? MainViewModel.Instance.GetStringResource("WP8_VIDEO_NoResultFoundText") : null;
                Movies.ErrorMessage = TVSeries.ErrorMessage = null;
            }
            else
            {
                Movies.ErrorMessage = Movies.Collection.Count == 0 ? MainViewModel.Instance.GetStringResource("WP8_VIDEO_NoResultFoundText") : null;
                TVSeries.ErrorMessage = TVSeries.Collection.Count == 0 ? MainViewModel.Instance.GetStringResource("WP8_VIDEO_NoResultFoundText") : null;
                MoviesPivotErrorMessage = TVSeriesPivotErrorMessage = null;
            }
            base.SetErrorMessageValues(isBack);
        }

        public override void SetResult(string data)
        {

        }

        #endregion
    }
}
