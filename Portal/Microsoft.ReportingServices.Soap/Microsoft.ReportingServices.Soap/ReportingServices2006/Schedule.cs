using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200012B RID: 299
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Schedule
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00016D52 File Offset: 0x00014F52
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00016D5A File Offset: 0x00014F5A
		public string ScheduleID
		{
			get
			{
				return this.scheduleIDField;
			}
			set
			{
				this.scheduleIDField = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00016D63 File Offset: 0x00014F63
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x00016D6B File Offset: 0x00014F6B
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00016D74 File Offset: 0x00014F74
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x00016D7C File Offset: 0x00014F7C
		public ScheduleDefinition Definition
		{
			get
			{
				return this.definitionField;
			}
			set
			{
				this.definitionField = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00016D85 File Offset: 0x00014F85
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00016D8D File Offset: 0x00014F8D
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00016D96 File Offset: 0x00014F96
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x00016D9E File Offset: 0x00014F9E
		public string Creator
		{
			get
			{
				return this.creatorField;
			}
			set
			{
				this.creatorField = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00016DA7 File Offset: 0x00014FA7
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x00016DAF File Offset: 0x00014FAF
		public DateTime NextRunTime
		{
			get
			{
				return this.nextRunTimeField;
			}
			set
			{
				this.nextRunTimeField = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00016DB8 File Offset: 0x00014FB8
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x00016DC0 File Offset: 0x00014FC0
		[XmlIgnore]
		public bool NextRunTimeSpecified
		{
			get
			{
				return this.nextRunTimeFieldSpecified;
			}
			set
			{
				this.nextRunTimeFieldSpecified = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00016DC9 File Offset: 0x00014FC9
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x00016DD1 File Offset: 0x00014FD1
		public DateTime LastRunTime
		{
			get
			{
				return this.lastRunTimeField;
			}
			set
			{
				this.lastRunTimeField = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00016DDA File Offset: 0x00014FDA
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00016DE2 File Offset: 0x00014FE2
		[XmlIgnore]
		public bool LastRunTimeSpecified
		{
			get
			{
				return this.lastRunTimeFieldSpecified;
			}
			set
			{
				this.lastRunTimeFieldSpecified = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00016DEB File Offset: 0x00014FEB
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x00016DF3 File Offset: 0x00014FF3
		public bool ReferencesPresent
		{
			get
			{
				return this.referencesPresentField;
			}
			set
			{
				this.referencesPresentField = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00016DFC File Offset: 0x00014FFC
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x00016E04 File Offset: 0x00015004
		public ScheduleStateEnum State
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		// Token: 0x040003AA RID: 938
		private string scheduleIDField;

		// Token: 0x040003AB RID: 939
		private string nameField;

		// Token: 0x040003AC RID: 940
		private ScheduleDefinition definitionField;

		// Token: 0x040003AD RID: 941
		private string descriptionField;

		// Token: 0x040003AE RID: 942
		private string creatorField;

		// Token: 0x040003AF RID: 943
		private DateTime nextRunTimeField;

		// Token: 0x040003B0 RID: 944
		private bool nextRunTimeFieldSpecified;

		// Token: 0x040003B1 RID: 945
		private DateTime lastRunTimeField;

		// Token: 0x040003B2 RID: 946
		private bool lastRunTimeFieldSpecified;

		// Token: 0x040003B3 RID: 947
		private bool referencesPresentField;

		// Token: 0x040003B4 RID: 948
		private ScheduleStateEnum stateField;
	}
}
