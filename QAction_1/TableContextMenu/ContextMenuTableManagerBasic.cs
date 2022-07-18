namespace Skyline.Protocol.TableContextMenu
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Skyline.DataMiner.Scripting;

	public class ContextMenuTableManagerBasic : ContextMenuTableManagerBase
	{
		private readonly int tablePid;
		private int autoIncPid = -1;

		public ContextMenuTableManagerBasic(SLProtocol protocol, object contextMenuData, int tablePid) : base(protocol, contextMenuData)
		{
			this.tablePid = tablePid;

			////protocol.Log(
			////	"QA" + protocol.QActionID + "|MyTable1ContextMenu|ContextMenu Data" + Environment.NewLine +
			////		"User GUID '" + UserGuid + "'" + Environment.NewLine +
			////		"Action '" + ActionRaw + "'" + Environment.NewLine +
			////		"Data '" + String.Join(" - ", Data) + "'",
			////	LogType.DebugInfo,
			////	LogLevel.NoLogging);
		}

		public ContextMenuTableManagerBasic WithAutoInc(int autoIncPid)
		{
			if (autoIncPid < 0)
			{
				throw new ArgumentException(autoIncPid + " is < 0.", nameof(autoIncPid));
			}

			this.autoIncPid = autoIncPid;
			return this;
		}

		public override void AddItem()
		{
			// Prepare
			object[] rowData;
			if (autoIncPid > -1)
			{
				int autoIncKey = Convert.ToInt32(Protocol.GetParameter(autoIncPid)) + 1;
				rowData = new object[] {Convert.ToString(autoIncKey)}.Concat(Data).ToArray();
			}
			else
			{
				rowData = Data.ToArray<object>();
			}

			// Sanity Check
			if (Protocol.Exists(tablePid, (string)rowData[0]))
			{
				Protocol.Log("QA" + Protocol.QActionID + "|AddItem|Table '" + tablePid + "' : Row with key '" + rowData[0] + "' already exists.", LogType.Error, LogLevel.NoLogging);
				return;
			}

			////Protocol.Log("QA" + Protocol.QActionID + "|AddItem|Table '" + tablePid + "' : " + String.Join(";", rowData), LogType.DebugInfo, LogLevel.NoLogging);

			// Add
			Protocol.AddRow(tablePid, rowData);
			if (autoIncPid > -1)
			{
				Protocol.SetParameter(autoIncPid, rowData[0]);
			}
		}

		public override void DeleteItems()
		{
			////Protocol.Log("QA" + Protocol.QActionID + "|DeleteItems|Table '" + tablePid + "' : " + String.Join(";", Data), LogType.DebugInfo, LogLevel.NoLogging);

			Protocol.DeleteRow(tablePid, Data);
		}

		public override void EditItem()
		{
			string rowKey = Data[Data.Length - 1];
			string[] rowData = Data.Take(Data.Length - 1).ToArray();
			////Protocol.Log("QA" + Protocol.QActionID + "|EditItemEditItem|Table '" + tablePid + "' - rowKey '" + rowKey + "' : " + String.Join(";", rowData), LogType.DebugInfo, LogLevel.NoLogging);

			object[] setRowData = new string[] { null }.Concat(rowData).ToArray<object>();
			Protocol.SetRow(tablePid, rowKey, setRowData);
		}
	}
}
