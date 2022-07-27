using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;
using Skyline.Protocol.Extentions;

using SLNetMessages = Skyline.DataMiner.Net.Messages;

/// <summary>
/// DataMiner QAction Class: After Startup.
/// </summary>
public static class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocol protocol)
	{
		try
		{
			MyTable4Init(protocol);
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}

	private static void MyTable4Init(SLProtocol protocol)
	{
		if (protocol.RowCount(Parameter.Mytable1.tablePid) > 0)
		{
			return;
		}

		Dictionary<object, List<object>> myTable4SetColumns = new Dictionary<object, List<object>>
		{
			{ Parameter.Mytable1.tablePid, new List<object>() },
			{ Parameter.Mytable1.Pid.mytable1_column2, new List<object>() },
			{ Parameter.Mytable1.Pid.mytable1_column3, new List<object>() },
			{ Parameter.Mytable1.Pid.mytable1_column4, new List<object>() },
		};

		for (int i = 0; i < 3; i++)
		{
			string key = Convert.ToString(i);

			myTable4SetColumns[Parameter.Mytable1.tablePid].Add(key);
			myTable4SetColumns[Parameter.Mytable1.Pid.mytable1_column2].Add("2." + key);
			myTable4SetColumns[Parameter.Mytable1.Pid.mytable1_column3].Add("3." + key);
			myTable4SetColumns[Parameter.Mytable1.Pid.mytable1_column4].Add("4." + key);
		}

		protocol.SetColumns(myTable4SetColumns);
	}
}
