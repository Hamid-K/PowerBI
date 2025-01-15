using System;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x02000301 RID: 769
	public class ExecutionInfo2 : ExecutionInfo
	{
		// Token: 0x06001B0C RID: 6924 RVA: 0x0006DCAC File Offset: 0x0006BEAC
		internal static ExecutionInfo2 ConvertFromExecutionInfo3(ExecutionInfo3 executionInfo)
		{
			return new ExecutionInfo2
			{
				HasSnapshot = executionInfo.HasSnapshot,
				NeedsProcessing = executionInfo.NeedsProcessing,
				AllowQueryExecution = executionInfo.AllowQueryExecution,
				CredentialsRequired = executionInfo.CredentialsRequired,
				ParametersRequired = executionInfo.ParametersRequired,
				ExpirationDateTime = executionInfo.ExpirationDateTime,
				ExecutionDateTime = executionInfo.ExecutionDateTime,
				NumPages = executionInfo.NumPages,
				Parameters = executionInfo.Parameters,
				DataSourcePrompts = executionInfo.DataSourcePrompts,
				HasDocumentMap = executionInfo.HasDocumentMap,
				ExecutionID = executionInfo.ExecutionID,
				ReportPath = executionInfo.ReportPath,
				HistoryID = executionInfo.HistoryID,
				ReportPageSettings = executionInfo.ReportPageSettings,
				AutoRefreshInterval = executionInfo.AutoRefreshInterval,
				PageCountMode = executionInfo.PageCountMode
			};
		}

		// Token: 0x04000A5D RID: 2653
		public PageCountMode PageCountMode;
	}
}
