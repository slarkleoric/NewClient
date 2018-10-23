
using ArcFace.Core;
using ArcFace.Core.AppService;
using ArcFace.Core.Dtos;
using ArcFace.Core.Models.Entities;
using ArcFaceClient.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArcFaceClient.ViewModel
{
   public class VActivity : VBase
    {

        #region 活动列表

        private ObservableCollection<Meeting> _activitylist;
        public ObservableCollection<Meeting> ActivityList
        {
            get
            {
                return _activitylist;
            }
            set
            {
                _activitylist = value;
                OnPropertyChanged(() => ActivityList);
                OnPropertyChanged(() => HasActivity);
            }
        }

        public bool HasActivity
        {
            get
            {
                return ActivityList?.Count > 0;
            }
        }

        #endregion

        #region ICommand

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }

        public ICommand LogoutCommand { get; }

        public ICommand LoadedCommand { get; }

        #endregion

        public VActivity()
        {
            LoadedCommand = new RelayCommand(()=> {
                loadActivity();
            });
            SearchCommand = new RelayCommand<string>(key=>
            {
                loadActivity(key);
            });
            LogoutCommand = new RelayCommand(() =>
            {
                App.CurrentUser.PassWord = string.Empty;
                AdminDataService.Instance.InsertOrUpdate(GlobalKeys.LoginAccount, App.CurrentUser);

                LocalSysCmds.RestartApp();
            });
        }

        void loadActivity(string keyword = "")
        {
            var list = MeetingAppService.Instance.Query(keyword.Trim());

            ActivityList = new ObservableCollection<Meeting>(list);
        }
    }
}
