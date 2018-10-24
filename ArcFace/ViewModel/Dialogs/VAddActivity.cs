using ArcFace.Core.AppService;
using ArcFace.Core.Models;
using ArcFace.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcFaceClient.ViewModel.Dialogs
{
   public class VAddActivity:VBase
    {
        private ActivityDto _activityDto;
        public ActivityDto ActivityDto
        {
            get { return _activityDto; }
            set
            {
                _activityDto = value;
                OnPropertyChanged(() => ActivityDto);
            }
        }

        public string WTitle { get; set; }

        public VAddActivity(string id = "")
        {
            if(string.IsNullOrEmpty(id))
            {
                var count = MeetingAppService.Instance.GetNewCode();

                ActivityDto = new ActivityDto
                {
                    id =  Guid.NewGuid(),
                    meeting_code = count
                };

                WTitle = "新建活动";
            }
            else
            {
                var meeting = MeetingAppService.Instance.QuerybyId(id);

                ActivityDto = new ActivityDto
                {
                    id = meeting.id,
                    begin_date = meeting.begin_date,
                    end_date = meeting.end_date,
                    meeting_name = meeting.meeting_name,
                    meeting_code = meeting.meeting_code,
                    is_del = meeting.is_del
                };
                WTitle = "编辑活动";
            }
        }
    }
}
