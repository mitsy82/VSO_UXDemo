using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PopOver_Sample
{

    public class LauncherAction : ResultItem
    {
        #region Constructor

        public LauncherAction()
        { }

        public LauncherAction(string itemId, string displayName, LauncherItemType type)
        {
            Id = string.Format("{0}-{1}-launcheraction", itemId, type);
            DisplayName = displayName;
            ItemType = type;
        }

        #endregion

        #region Properties

        public ResultItem Item { get; set; }

        public LauncherItemType ItemType { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string ItemTypeString { get; set; }

        public string ShareTitle { get; set; }

        #endregion

        #region Methods

        protected override void OnServiceRequested(object param)
        {
            try
            {
                if (ItemType == LauncherItemType.Phone)
                {
                    PhoneCallTask phoneCallTask = new PhoneCallTask();
                    phoneCallTask.PhoneNumber = DisplayName;
                    phoneCallTask.Show();
                }
                else if (ItemType == LauncherItemType.SMS)
                {
                    SmsComposeTask smsTask = new SmsComposeTask();
                    smsTask.To = DisplayName;
                    smsTask.Body = Body;
                    smsTask.Show();
                }
                else if (ItemType == LauncherItemType.Email)
                {
                    EmailComposeTask emailTask = new EmailComposeTask();
                    emailTask.To = DisplayName;
                    emailTask.Subject = Subject;
                    emailTask.Body = Body;
                    emailTask.Show();
                }
                else if (ItemType == LauncherItemType.Browse)
                {
                    Uri url = new Uri(Url, UriKind.RelativeOrAbsolute);
                    WebBrowserTask browse = new WebBrowserTask() { Uri = url };
                    browse.Show();
                }
                else if (ItemType == LauncherItemType.Video)
                {
                    MediaPlayerLauncher player = new MediaPlayerLauncher() { Media = new Uri(Url, UriKind.RelativeOrAbsolute), Orientation = MediaPlayerOrientation.Landscape };
                    player.Show();
                }
                else if (ItemType == LauncherItemType.AddContact)
                {
                    SavePhoneNumberTask savePhoneNumberTask = new SavePhoneNumberTask();
                    savePhoneNumberTask.PhoneNumber = DisplayName;
                    savePhoneNumberTask.Show();
                }
                else if (ItemType == LauncherItemType.Share)
                {
                    ShareLinkTask shareLinkTask = new ShareLinkTask();
                    shareLinkTask.Title = ShareTitle;
                    shareLinkTask.LinkUri = new Uri(Url, UriKind.Absolute);
                    shareLinkTask.Message = string.Format(MainViewModel.Instance.GetStringResource("WP8_VIDEO_SharedViaXboxVideoText"), ShareTitle);
                    shareLinkTask.Show();
                }
            }
            catch
            {
            }
        }

        public override void BinaryWrite(System.IO.BinaryWriter writer)
        {
            base.BinaryWrite(writer);
            ItemTypeString = ItemType.ToString();
            writer.WriteString(Body);
            writer.WriteString(Subject);
            writer.WriteString(Url);
            writer.WriteString(ItemTypeString);

        }

        public override void BinaryRead(System.IO.BinaryReader reader)
        {
            base.BinaryRead(reader);
            Body = reader.ReadString();
            Subject = reader.ReadString();
            Url = reader.ReadString();
            ItemTypeString = reader.ReadString();
            ItemType = (LauncherItemType)Enum.Parse(typeof(LauncherItemType), ItemTypeString, true);
        }

        #endregion
    }

    public enum LauncherItemType
    {
        Browse,
        Phone,
        SMS,
        Email,
        Video,
        AddContact,
        Share
    }
}