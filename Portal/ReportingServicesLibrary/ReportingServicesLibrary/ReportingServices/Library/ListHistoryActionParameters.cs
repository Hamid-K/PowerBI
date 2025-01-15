using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001BA RID: 442
	internal sealed class ListHistoryActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x000382CA File Offset: 0x000364CA
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x000382D2 File Offset: 0x000364D2
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

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x000382DB File Offset: 0x000364DB
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x000382E3 File Offset: 0x000364E3
		public ReportHistorySnapshot[] ReportHistory
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

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x000382CA File Offset: 0x000364CA
		internal override string InputTrace
		{
			get
			{
				return this.m_reportPath;
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000382EC File Offset: 0x000364EC
		internal override void Validate()
		{
			if (this.m_reportPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
		}

		// Token: 0x0400063B RID: 1595
		private string m_reportPath;

		// Token: 0x0400063C RID: 1596
		private ReportHistorySnapshot[] m_reportHistory;
	}
}
