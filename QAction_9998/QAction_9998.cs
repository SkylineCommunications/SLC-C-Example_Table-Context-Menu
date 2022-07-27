using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: My Table 4 - ContextMenu.
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
			ContextMenuMyTable1 contextMenu = new ContextMenuMyTable1(
				protocol,
				contextMenuData,
				Parameter.Mytable1.tablePid);
			contextMenu.ProcessContextMenuAction();
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}