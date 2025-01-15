using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200001E RID: 30
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Schedule
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000CC3E File Offset: 0x0000AE3E
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000CC46 File Offset: 0x0000AE46
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000CC4F File Offset: 0x0000AE4F
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0000CC57 File Offset: 0x0000AE57
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000CC60 File Offset: 0x0000AE60
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0000CC68 File Offset: 0x0000AE68
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000CC71 File Offset: 0x0000AE71
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000CC79 File Offset: 0x0000AE79
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000CC82 File Offset: 0x0000AE82
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0000CC8A File Offset: 0x0000AE8A
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000CC93 File Offset: 0x0000AE93
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0000CC9B File Offset: 0x0000AE9B
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0000CCAC File Offset: 0x0000AEAC
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

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000CCB5 File Offset: 0x0000AEB5
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000CCBD File Offset: 0x0000AEBD
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000CCC6 File Offset: 0x0000AEC6
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0000CCCE File Offset: 0x0000AECE
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000CCD7 File Offset: 0x0000AED7
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0000CCDF File Offset: 0x0000AEDF
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		public string ScheduleStateName
		{
			get
			{
				return this.scheduleStateNameField;
			}
			set
			{
				this.scheduleStateNameField = value;
			}
		}

		// Token: 0x04000170 RID: 368
		private string scheduleIDField;

		// Token: 0x04000171 RID: 369
		private string nameField;

		// Token: 0x04000172 RID: 370
		private ScheduleDefinition definitionField;

		// Token: 0x04000173 RID: 371
		private string descriptionField;

		// Token: 0x04000174 RID: 372
		private string creatorField;

		// Token: 0x04000175 RID: 373
		private DateTime nextRunTimeField;

		// Token: 0x04000176 RID: 374
		private bool nextRunTimeFieldSpecified;

		// Token: 0x04000177 RID: 375
		private DateTime lastRunTimeField;

		// Token: 0x04000178 RID: 376
		private bool lastRunTimeFieldSpecified;

		// Token: 0x04000179 RID: 377
		private bool referencesPresentField;

		// Token: 0x0400017A RID: 378
		private string scheduleStateNameField;
	}
}
