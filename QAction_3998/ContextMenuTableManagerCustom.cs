using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Table.ContextMenu;

internal class ContextMenuTableManagerCustom : ContextMenuTableManagerBasic
{
	public ContextMenuTableManagerCustom(SLProtocol protocol, object contextMenuData, int tablePid, int autoIncPid)
		: base(protocol, contextMenuData, tablePid, autoIncPid)
	{
	}

	protected override void AddItem()
	{
		// Some pre-Action
		Protocol.Log("QA" + Protocol.QActionID + "|ContextMenuTableManagerCustom|AddItem|Some Pre-Action", LogType.DebugInfo, LogLevel.NoLogging);

		// Add row
		base.AddItem();

		// Some post-action
		Protocol.Log("QA" + Protocol.QActionID + "|ContextMenuTableManagerCustom|AddItem|Some Post-Action", LogType.DebugInfo, LogLevel.NoLogging);
	}
}
