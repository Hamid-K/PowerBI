using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001A8 RID: 424
	internal sealed class ExecutionOptionsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x000370A1 File Offset: 0x000352A1
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x000370A9 File Offset: 0x000352A9
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

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x000370B2 File Offset: 0x000352B2
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x000370BA File Offset: 0x000352BA
		public ExecutionSettingEnum ExecutionSettings
		{
			get
			{
				return this.m_executionSetting;
			}
			set
			{
				this.m_executionSetting = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x000370C3 File Offset: 0x000352C3
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x000370CB File Offset: 0x000352CB
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

		// Token: 0x06000F58 RID: 3928 RVA: 0x000370D4 File Offset: 0x000352D4
		internal override void Validate()
		{
			if (this.ReportPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
			if (this.Schedule != null && this.Schedule is ScheduleDefinition && !((ScheduleDefinition)this.Schedule).IsValid())
			{
				throw new InvalidParameterException("Schedule");
			}
		}

		// Token: 0x04000626 RID: 1574
		private string m_reportPath;

		// Token: 0x04000627 RID: 1575
		private ExecutionSettingEnum m_executionSetting;

		// Token: 0x04000628 RID: 1576
		private ScheduleDefinitionOrReference m_schedule;
	}
}
