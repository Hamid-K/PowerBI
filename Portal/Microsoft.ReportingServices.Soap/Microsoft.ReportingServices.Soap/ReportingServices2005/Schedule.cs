using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000210 RID: 528
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Schedule
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00021CD4 File Offset: 0x0001FED4
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x00021CDC File Offset: 0x0001FEDC
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

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x00021CE5 File Offset: 0x0001FEE5
		// (set) Token: 0x0600145C RID: 5212 RVA: 0x00021CED File Offset: 0x0001FEED
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

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00021CF6 File Offset: 0x0001FEF6
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x00021CFE File Offset: 0x0001FEFE
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

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00021D07 File Offset: 0x0001FF07
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x00021D0F File Offset: 0x0001FF0F
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

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x00021D18 File Offset: 0x0001FF18
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x00021D20 File Offset: 0x0001FF20
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

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x00021D29 File Offset: 0x0001FF29
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x00021D31 File Offset: 0x0001FF31
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

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00021D3A File Offset: 0x0001FF3A
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x00021D42 File Offset: 0x0001FF42
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

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x00021D4B File Offset: 0x0001FF4B
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x00021D53 File Offset: 0x0001FF53
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

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x00021D5C File Offset: 0x0001FF5C
		// (set) Token: 0x0600146A RID: 5226 RVA: 0x00021D64 File Offset: 0x0001FF64
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

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x00021D6D File Offset: 0x0001FF6D
		// (set) Token: 0x0600146C RID: 5228 RVA: 0x00021D75 File Offset: 0x0001FF75
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

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x00021D7E File Offset: 0x0001FF7E
		// (set) Token: 0x0600146E RID: 5230 RVA: 0x00021D86 File Offset: 0x0001FF86
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

		// Token: 0x040005FE RID: 1534
		private string scheduleIDField;

		// Token: 0x040005FF RID: 1535
		private string nameField;

		// Token: 0x04000600 RID: 1536
		private ScheduleDefinition definitionField;

		// Token: 0x04000601 RID: 1537
		private string descriptionField;

		// Token: 0x04000602 RID: 1538
		private string creatorField;

		// Token: 0x04000603 RID: 1539
		private DateTime nextRunTimeField;

		// Token: 0x04000604 RID: 1540
		private bool nextRunTimeFieldSpecified;

		// Token: 0x04000605 RID: 1541
		private DateTime lastRunTimeField;

		// Token: 0x04000606 RID: 1542
		private bool lastRunTimeFieldSpecified;

		// Token: 0x04000607 RID: 1543
		private bool referencesPresentField;

		// Token: 0x04000608 RID: 1544
		private ScheduleStateEnum stateField;
	}
}
