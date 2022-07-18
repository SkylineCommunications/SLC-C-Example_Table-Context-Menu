namespace Skyline.Protocol.TableContextMenu
{
	using System;

	using Skyline.DataMiner.Scripting;

	public abstract class ContextMenuTableManagerBase : ContextMenu
	{
		private readonly Action action;

		protected ContextMenuTableManagerBase(SLProtocol protocol, object contextMenuData)
			: base(protocol, contextMenuData)
		{
			action = (Action)Convert.ToInt32(ActionRaw);
		}

		private enum Action
		{
			AddItem = 1,
			DuplicateItem = 2,
			EditItem = 10,
			DeleteSelectedRows = 20,
		}

		public override void ProcessContextMenuAction()
		{
			switch (action)
			{
				case Action.AddItem:
				case Action.DuplicateItem:
					// DuplicateItem is exactly the same as AddItem.
					// It just provides user-friendliness by setting default values in all dependency parameters.
					AddItem();
					break;
				case Action.EditItem:
					EditItem();
					break;
				case Action.DeleteSelectedRows:
					DeleteItems();
					break;
				default:
					Protocol.Log("QA" + Protocol.QActionID + "|ContextMenuHandler|Process|Unexpected ContextMenu value '" + Data[1] + "'", LogType.Error, LogLevel.NoLogging);
					return;
			}
		}

		public abstract void AddItem();

		public abstract void EditItem();

		public abstract void DeleteItems();
	}
}