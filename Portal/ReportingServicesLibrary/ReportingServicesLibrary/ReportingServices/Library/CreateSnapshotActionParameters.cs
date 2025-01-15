using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001B0 RID: 432
	internal sealed class CreateSnapshotActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00037958 File Offset: 0x00035B58
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x00037960 File Offset: 0x00035B60
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

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00037969 File Offset: 0x00035B69
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x00037971 File Offset: 0x00035B71
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

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003797A File Offset: 0x00035B7A
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x00037982 File Offset: 0x00035B82
		public string HistoryID
		{
			get
			{
				return this.m_historyID;
			}
			set
			{
				this.m_historyID = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0003798B File Offset: 0x00035B8B
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x00037993 File Offset: 0x00035B93
		public Warning[] Warnings
		{
			get
			{
				return this.m_warnings;
			}
			set
			{
				this.m_warnings = value;
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0003799C File Offset: 0x00035B9C
		internal override void Validate()
		{
			if (this.m_reportPath == null)
			{
				throw new MissingParameterException("Report");
			}
		}

		// Token: 0x0400062F RID: 1583
		private string m_reportPath;

		// Token: 0x04000630 RID: 1584
		private JobType m_jobType;

		// Token: 0x04000631 RID: 1585
		private string m_historyID;

		// Token: 0x04000632 RID: 1586
		private Warning[] m_warnings;
	}
}
