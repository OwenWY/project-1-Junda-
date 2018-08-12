using Foundation;
using System;
using UIKit;
using Plugin.Messaging;  


namespace StoryboardTables1
{
    public partial class TaskDetailViewController : UITableViewController
    {
   

        ContactModel currentTask { get; set; }
        public ItemViewController Delegate { get; set; }

        public TaskDetailViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			SaveButton.TouchUpInside += (sender, e) =>
			{
                currentTask.Name = NameText.Text;
                currentTask.Phone = PhoneText.Text;
				Delegate.SaveTask(currentTask);
			};

			DeleteButton.TouchUpInside += (sender, e) => Delegate.DeleteTask(currentTask);

            CallButton.TouchUpInside += (sender, e) =>
            {
                if (PhoneText.Text.Length <= 0) return;
                var url = new NSUrl("tel:" + PhoneText.Text );
                UIApplication.SharedApplication.OpenUrl(url);
            };
            MessageButton.TouchUpInside += (sender, e) =>
            {
                var SMSTask = CrossMessaging.Current.SmsMessenger;
                if (SMSTask.CanSendSms)
                {
                    SMSTask.SendSms(PhoneText.Text, "Hello world");
                }
            };

        }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			NameText.Text = currentTask.Name;
			PhoneText.Text = currentTask.Phone;
		}

		
        public void SetTask(ItemViewController d, ContactModel task)
		{
			Delegate = d;
			currentTask = task;
		}
    }
}