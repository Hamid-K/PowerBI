using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x02000061 RID: 97
	internal static class ExecutionLogInfoExtensions
	{
		// Token: 0x06000306 RID: 774 RVA: 0x000148B8 File Offset: 0x00012AB8
		internal static ReportExecutionInfo ToReportExecutionLog(this ExecutionLogInfo executionLog)
		{
			if (executionLog == null)
			{
				throw new ArgumentNullException("executionLog");
			}
			ReportExecutionInfo reportExecutionInfo = new ReportExecutionInfo();
			reportExecutionInfo.ItemPath = new ExternalItemPath(executionLog.ItemPath);
			reportExecutionInfo.Format = executionLog.Format;
			reportExecutionInfo.Parameters = executionLog.Parameters;
			reportExecutionInfo.Source = (Microsoft.ReportingServices.Diagnostics.ExecutionLogExecType)Enum.Parse(typeof(Microsoft.ReportingServices.Diagnostics.ExecutionLogExecType), executionLog.Source.ToString(), true);
			reportExecutionInfo.ExecutionLogLevel = (Microsoft.ReportingServices.Diagnostics.ExecutionLogLevel)Enum.Parse(typeof(Microsoft.ReportingServices.Diagnostics.ExecutionLogLevel), executionLog.ExecutionLogLevel.ToString(), true);
			reportExecutionInfo.Status = (ErrorCode)executionLog.Status;
			reportExecutionInfo.ByteCount = executionLog.ByteCount;
			reportExecutionInfo.RowCount = reportExecutionInfo.RowCount;
			reportExecutionInfo.ProcessingTime = TimeSpan.FromMilliseconds((double)executionLog.ProcessingTime);
			reportExecutionInfo.RenderingTime = TimeSpan.FromMilliseconds((double)executionLog.RenderingTime);
			reportExecutionInfo.DataRetrievalTime = TimeSpan.FromMilliseconds((double)executionLog.DataRetrievalTime);
			reportExecutionInfo.ExecutionId = executionLog.ExecutionId;
			reportExecutionInfo.EventType = (ReportEventType)Enum.Parse(typeof(ReportEventType), executionLog.EventType.ToString(), true);
			return reportExecutionInfo;
		}
	}
}
