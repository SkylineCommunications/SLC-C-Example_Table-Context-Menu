﻿using System;

using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Table.ContextMenu;

internal class ContextMenuTableManagerCustom : ContextMenuTableManagerBase
{
	private readonly int autoIncPid;

	public ContextMenuTableManagerCustom(SLProtocol protocol, object contextMenuData, int tablePid, int autoIncPid)
		: base(protocol, contextMenuData, tablePid)
	{
		if (autoIncPid < 0)
		{
			throw new ArgumentException(autoIncPid + " is < 0.", nameof(autoIncPid));
		}

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
		if (Protocol.Exists(TablePid, (string)rowData[0]))
		{
			Protocol.Log("QA" + Protocol.QActionID + "|AddItem|Table '" + TablePid + "' : Row with key '" + rowData[0] + "' already exists.", LogType.Error, LogLevel.NoLogging);
			return;
		}

		////Protocol.Log("QA" + Protocol.QActionID + "|AddItem|Table '" + tablePid + "' : " + String.Join(";", rowData), LogType.DebugInfo, LogLevel.NoLogging);

		// Add
		Protocol.AddRow(TablePid, rowData);
		if (autoIncPid > -1)
		{
			Protocol.SetParameter(autoIncPid, rowData[0]);
		}
	}

	protected override void DeleteItems()
	{
		////Protocol.Log("QA" + Protocol.QActionID + "|DeleteItems|Table '" + tablePid + "' : " + String.Join(";", Data), LogType.DebugInfo, LogLevel.NoLogging);

		Protocol.DeleteRow(TablePid, Data);
	}

	protected override void EditItem()
	{
		string rowKey = Data[Data.Length - 1];

		object[] rowData = new object[4];
		rowData[0] = null;
		rowData[1] = Data[0];
		rowData[2] = Data[1];
		rowData[3] = Data[0] + " : " + Data[1];

		Protocol.SetRow(TablePid, rowKey, rowData);
	}
}
