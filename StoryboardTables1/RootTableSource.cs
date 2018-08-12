using System;
using Foundation;
using UIKit;

namespace StoryboardTables1
{
    public class RootTableSource : UITableViewSource
    {
		
        ContactModel[] tableItems;
		string cellIdentifier = "taskcell"; 

        public RootTableSource(ContactModel[] items)
        {
            tableItems = items;
        }

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return tableItems.Length;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			 
			var cell = tableView.DequeueReusableCell(cellIdentifier);
			
			cell.TextLabel.Text = tableItems[indexPath.Row].Name;
            cell.DetailTextLabel.Text = tableItems[indexPath.Row].Phone;
			
			
			cell.Accessory = UITableViewCellAccessory.None;
			return cell;
		}
        public ContactModel GetItem(int id)
		{
			return tableItems[id];
		}
    }
}
