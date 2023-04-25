using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Table.ContextMenu;

internal enum Action
{
	Action1 = 1,
	Action2 = 2,
	Action10 = 10,
}

internal class ContextMenuMyTable1 : ContextMenu<Action>
{
	public ContextMenuMyTable1(SLProtocol protocol, object contextMenuData, int tablePid)
		: base(protocol, contextMenuData, tablePid)
	{
	}

	public override void ProcessContextMenuAction()
	{
		switch (Action)
		{
			case Action.Action1:
				Protocol.Log("QA" + Protocol.QActionID + "|ContextMenuTable4|ProcessContextMenuAction|Action 1", LogType.DebugInfo, LogLevel.NoLogging);
				break;
			case Action.Action2:
				Protocol.Log("QA" + Protocol.QActionID + "|ContextMenuTable4|ProcessContextMenuAction|Action 2", LogType.DebugInfo, LogLevel.NoLogging);
				break;
			case Action.Action10:
				Protocol.Log("QA" + Protocol.QActionID + "|ContextMenuTable4|ProcessContextMenuAction|Action 10", LogType.DebugInfo, LogLevel.NoLogging);
				break;
			default:
				Protocol.Log("QA" + Protocol.QActionID + "|ContextMenuTable4|Process|Unexpected ContextMenu value '" + ActionRaw + "'", LogType.Error, LogLevel.NoLogging);
				break;
		}
	}
}