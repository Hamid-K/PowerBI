using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001B2 RID: 434
	internal sealed class ReportHistoryOptionsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00037B6C File Offset: 0x00035D6C
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x00037B74 File Offset: 0x00035D74
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

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00037B7D File Offset: 0x00035D7D
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00037B85 File Offset: 0x00035D85
		public bool ManualCreationEnabled
		{
			get
			{
				return this.m_enableManualCreation;
			}
			set
			{
				this.m_enableManualCreation = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00037B8E File Offset: 0x00035D8E
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00037B96 File Offset: 0x00035D96
		public bool KeepExecutionSnapshots
		{
			get
			{
				return this.m_keepExecutionSnapshots;
			}
			set
			{
				this.m_keepExecutionSnapshots = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00037B9F File Offset: 0x00035D9F
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x00037BA7 File Offset: 0x00035DA7
		public ScheduleDefinitionOrReference Schedule
		{
			get
			{
				return this.m_schedule;
			}
			set
			{
				this.m_schedule = value;
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00037BB0 File Offset: 0x00035DB0
		internal override void Validate()
		{
			if (this.m_reportPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
			if (this.Schedule != null && this.Schedule is ScheduleDefinition && !((ScheduleDefinition)this.Schedule).IsValid())
			{
				throw new InvalidParameterException("Schedule");
			}
		}

		// Token: 0x04000633 RID: 1587
		private string m_reportPath;

		// Token: 0x04000634 RID: 1588
		private bool m_enableManualCreation;

		// Token: 0x04000635 RID: 1589
		private bool m_keepExecutionSnapshots;

		// Token: 0x04000636 RID: 1590
		private ScheduleDefinitionOrReference m_schedule;
	}
}
