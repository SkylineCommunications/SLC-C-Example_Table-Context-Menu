using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.TableContextMenu;

internal class ContextMenuTableManagerCustom : ContextMenuTableManagerBase
{
	private readonly int tablePid;
	private readonly int autoIncPid;

	public ContextMenuTableManagerCustom(SLProtocol protocol, object contextMenuData, int tablePid, int autoIncPid)
		: base(protocol, contextMenuData, tablePid)
	{
		if (tablePid < 0)
		{
			throw new ArgumentException(tablePid + " is < 0.", nameof(tablePid));
		}

		if (autoIncPid < 0)
		{
			throw new ArgumentException(autoIncPid + " is < 0.", nameof(autoIncPid));
		}

		this.tablePid = tablePid;
		this.autoIncPid = autoIncPid;
	}

	protected override void AddItem()
	{
		// Prepare
		int autoIncKey = Convert.ToInt32(Protocol.GetParameter(autoIncPid)) + 1;
		object[] rowData = new object[4];
		rowData[0] = Convert.ToString(autoIncKey);
		rowData[1] = Data[0];
		rowData[2] = Data[1];
		rowData[3] = Data[0] + " : " + Data[1];

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

	protected override void DeleteItems()
	{
		////Protocol.Log("QA" + Protocol.QActionID + "|DeleteItems|Table '" + tablePid + "' : " + String.Join(";", Data), LogType.DebugInfo, LogLevel.NoLogging);

		Protocol.DeleteRow(tablePid, Data);
	}

	protected override void EditItem()
	{
		string rowKey = Data[Data.Length - 1];

		object[] rowData = new object[4];
		rowData[0] = null;
		rowData[1] = Data[0];
		rowData[2] = Data[1];
		rowData[3] = Data[0] + " : " + Data[1];

		Protocol.SetRow(tablePid, rowKey, rowData);
	}
}
