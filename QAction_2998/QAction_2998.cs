using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: Managed Table 3 - ContextMenu.
/// </summary>
public static class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	/// <param name="contextMenuData"><see cref="object"/> containing the table ContextMenu data.</param>
	public static void Run(SLProtocol protocol, object contextMenuData)
	{
		try
		{
			ContextMenuTableManagerCustom contextMenu = new ContextMenuTableManagerCustom(
				protocol,
				contextMenuData,
				Parameter.Managedtable3.tablePid,
				Parameter.managedtable3_autoinc);
			contextMenu.ProcessContextMenuAction();
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}