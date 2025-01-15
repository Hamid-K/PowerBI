using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005FF RID: 1535
	[Serializable]
	internal sealed class DrillthroughInfo
	{
		// Token: 0x06005488 RID: 21640 RVA: 0x00162CDC File Offset: 0x00160EDC
		internal DrillthroughInfo(string reportName, DrillthroughParameters parameters)
		{
			this.m_reportName = reportName;
			this.m_reportParameters = parameters;
		}

		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x06005489 RID: 21641 RVA: 0x00162CF2 File Offset: 0x00160EF2
		internal string ReportName
		{
			get
			{
				return this.m_reportName;
			}
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x0600548A RID: 21642 RVA: 0x00162CFA File Offset: 0x00160EFA
		internal DrillthroughParameters ReportParameters
		{
			get
			{
				return this.m_reportParameters;
			}
		}

		// Token: 0x04002CFA RID: 11514
		private string m_reportName;

		// Token: 0x04002CFB RID: 11515
		private DrillthroughParameters m_reportParameters;
	}
}
