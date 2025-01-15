using System;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Sql
{
	// Token: 0x020003AC RID: 940
	internal static class SqlClientHelper
	{
		// Token: 0x06002100 RID: 8448 RVA: 0x00059278 File Offset: 0x00057478
		public static void TraceSqlInfoMessage(IEngineHost engineHost, string message, string connectionId)
		{
			using (IHostTrace hostTrace = TracingService.CreateTrace(engineHost, "Engine/IO/Microsoft SQL/InfoMessage", TraceEventType.Information, null))
			{
				SqlClientHelper.ExtractAndLogRequestAndStatementId(hostTrace, message, true);
				if (connectionId != null)
				{
					hostTrace.Add("ConnectionId", connectionId, false);
				}
			}
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000592C8 File Offset: 0x000574C8
		private static void ExtractAndLogRequestAndStatementId(IHostTrace trace, string message, bool logMessage)
		{
			if (message.Contains("Statement ID:"))
			{
				if (logMessage)
				{
					trace.Add("Message", message, true);
				}
				string[] array = message.Split(new char[] { '|' });
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[] { ':' });
					if (array2.Length == 2)
					{
						string text = array2[0].Trim();
						if (text == "Statement ID")
						{
							trace.Add("StatementId", array2[1].Trim(), false);
						}
						else if (text == "Distributed request ID")
						{
							trace.Add("DistributedRequestId", array2[1].Trim(), false);
						}
					}
				}
			}
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0005937C File Offset: 0x0005757C
		public static void GenerateRequestIdTrace(IHostTrace trace, string message)
		{
			SqlClientHelper.ExtractAndLogRequestAndStatementId(trace, message, false);
		}
	}
}
