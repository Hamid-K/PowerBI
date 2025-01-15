using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001BC RID: 444
	internal sealed class ListHistorySnapshotsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0003839F File Offset: 0x0003659F
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x000383A7 File Offset: 0x000365A7
		public string ReportPath
		{
			get
			{
				return this.m_reportPath;
			}
			set
			{
				this.m_reportPath = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x000383B0 File Offset: 0x000365B0
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x000383B8 File Offset: 0x000365B8
		public HistorySnapshot[] ReportHistory
		{
			get
			{
				return this.m_reportHistory;
			}
			set
			{
				this.m_reportHistory = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0003839F File Offset: 0x0003659F
		internal override string InputTrace
		{
			get
			{
				return this.m_reportPath;
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000383C1 File Offset: 0x000365C1
		internal override void Validate()
		{
			if (this.m_reportPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
		}

		// Token: 0x0400063D RID: 1597
		private string m_reportPath;

		// Token: 0x0400063E RID: 1598
		private HistorySnapshot[] m_reportHistory;
	}
}
