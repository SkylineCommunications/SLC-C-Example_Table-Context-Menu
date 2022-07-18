namespace Skyline.Protocol.TableContextMenu
{
	using System;
	using System.Linq;

	using Skyline.DataMiner.Scripting;

	public abstract class ContextMenu
	{
		protected ContextMenu(SLProtocol protocol, object contextMenuData)
		{
			Protocol = protocol;
			InitializeContextMenuData(contextMenuData);
		}

		public string UserGuid { get; private set; }

		public string ActionRaw { get; private set; }

		public string[] Data { get; private set; }

		protected SLProtocol Protocol { get; }

		public abstract void ProcessContextMenuAction();

		private void InitializeContextMenuData(object contextMenuData)
		{
			if (contextMenuData == null)
			{
				throw new ArgumentNullException(nameof(contextMenuData));
			}

			if (!(contextMenuData is string[] allData))
			{
				throw new ArgumentException(contextMenuData + " is not a string[].", nameof(contextMenuData));
			}

			if (allData.Length < 2)
			{
				throw new ArgumentException(
					contextMenuData + " doesn't have the expected length. Expected Length '2 or more' - Actual Length '" + allData.Length + "'.",
					nameof(contextMenuData));
			}

			UserGuid = allData[0];
			ActionRaw = allData[1];
			Data = allData.Skip(2).ToArray();
		}
	}
}