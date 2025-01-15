using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001AC RID: 428
	internal sealed class UpdateExecutionSnapshotActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00037639 File Offset: 0x00035839
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x00037641 File Offset: 0x00035841
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

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0003764A File Offset: 0x0003584A
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x00037652 File Offset: 0x00035852
		public JobType JobType
		{
			get
			{
				return this.m_jobType;
			}
			set
			{
				this.m_jobType = value;
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0003765B File Offset: 0x0003585B
		internal override void Validate()
		{
			if (this.m_reportPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
		}

		// Token: 0x0400062C RID: 1580
		private string m_reportPath;

		// Token: 0x0400062D RID: 1581
		private JobType m_jobType;
	}
}
