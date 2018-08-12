using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using System.CodeDom;

namespace StoryboardTables1
{
    public partial class ItemViewController : UITableViewController
    {
        List<ContactModel> contacts;

        public ItemViewController (IntPtr handle) : base (handle)
        {
            contacts = new List<ContactModel> {
                new ContactModel {Name="Groceries", Phone="13287982030"},
                new ContactModel {Name="Devices", Phone="1429482038427"}
	        };
        }

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "TaskSegue")
			{ // set in Storyboard
				var navctlr = segue.DestinationViewController as TaskDetailViewController;
				if (navctlr != null)
				{
					var source = TableView.Source as RootTableSource;
					var rowPath = TableView.IndexPathForSelectedRow;
					var item = source.GetItem(rowPath.Row);
					navctlr.SetTask(this, item); 
				}
			}
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            addButton.Clicked += (sender, e) => CreateTask();
            LogoutButton.Clicked += (sender, r) =>
            {
                this.DismissViewController(true, null);
            };

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            TableView.Source = new RootTableSource(contacts.ToArray());
        }

        public void SaveTask(ContactModel contact)
		{
            var oldTask = contacts.Find(t => t.Id == contact.Id);
            NavigationController.PopViewController(true);
		}

        public void DeleteTask(ContactModel contact)
		{
            var oldTask = contacts.Find(t => t.Id == contact.Id);
            contacts.Remove(oldTask);
            NavigationController.PopViewController(true);
		}

		public void CreateTask()
		{
			
            var newId = contacts[contacts.Count - 1].Id + 1;
            var newChore = new ContactModel { Id = newId };
            contacts.Add(newChore);

			
			var detail = Storyboard.InstantiateViewController("detail") as TaskDetailViewController;
			detail.SetTask(this, newChore);
			NavigationController.PushViewController(detail, true);
		}
    }
}