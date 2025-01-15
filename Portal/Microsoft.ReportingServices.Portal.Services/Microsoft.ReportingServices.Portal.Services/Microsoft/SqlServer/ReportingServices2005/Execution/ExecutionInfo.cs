using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000089 RID: 137
	[XmlInclude(typeof(ExecutionInfo2))]
	[XmlInclude(typeof(ExecutionInfo3))]
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ExecutionInfo
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001B6EB File Offset: 0x000198EB
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x0001B6F3 File Offset: 0x000198F3
		public bool HasSnapshot
		{
			get
			{
				return this.hasSnapshotField;
			}
			set
			{
				this.hasSnapshotField = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001B6FC File Offset: 0x000198FC
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0001B704 File Offset: 0x00019904
		public bool NeedsProcessing
		{
			get
			{
				return this.needsProcessingField;
			}
			set
			{
				this.needsProcessingField = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0001B70D File Offset: 0x0001990D
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0001B715 File Offset: 0x00019915
		public bool AllowQueryExecution
		{
			get
			{
				return this.allowQueryExecutionField;
			}
			set
			{
				this.allowQueryExecutionField = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001B71E File Offset: 0x0001991E
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0001B726 File Offset: 0x00019926
		public bool CredentialsRequired
		{
			get
			{
				return this.credentialsRequiredField;
			}
			set
			{
				this.credentialsRequiredField = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001B72F File Offset: 0x0001992F
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0001B737 File Offset: 0x00019937
		public bool ParametersRequired
		{
			get
			{
				return this.parametersRequiredField;
			}
			set
			{
				this.parametersRequiredField = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0001B740 File Offset: 0x00019940
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0001B748 File Offset: 0x00019948
		public DateTime ExpirationDateTime
		{
			get
			{
				return this.expirationDateTimeField;
			}
			set
			{
				this.expirationDateTimeField = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001B751 File Offset: 0x00019951
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x0001B759 File Offset: 0x00019959
		public DateTime ExecutionDateTime
		{
			get
			{
				return this.executionDateTimeField;
			}
			set
			{
				this.executionDateTimeField = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001B762 File Offset: 0x00019962
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x0001B76A File Offset: 0x0001996A
		public int NumPages
		{
			get
			{
				return this.numPagesField;
			}
			set
			{
				this.numPagesField = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001B773 File Offset: 0x00019973
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0001B77B File Offset: 0x0001997B
		public ReportParameter[] Parameters
		{
			get
			{
				return this.parametersField;
			}
			set
			{
				this.parametersField = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001B784 File Offset: 0x00019984
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0001B78C File Offset: 0x0001998C
		public DataSourcePrompt[] DataSourcePrompts
		{
			get
			{
				return this.dataSourcePromptsField;
			}
			set
			{
				this.dataSourcePromptsField = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001B795 File Offset: 0x00019995
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0001B79D File Offset: 0x0001999D
		public bool HasDocumentMap
		{
			get
			{
				return this.hasDocumentMapField;
			}
			set
			{
				this.hasDocumentMapField = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001B7A6 File Offset: 0x000199A6
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0001B7AE File Offset: 0x000199AE
		public string ExecutionID
		{
			get
			{
				return this.executionIDField;
			}
			set
			{
				this.executionIDField = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001B7B7 File Offset: 0x000199B7
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0001B7BF File Offset: 0x000199BF
		public string ReportPath
		{
			get
			{
				return this.reportPathField;
			}
			set
			{
				this.reportPathField = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001B7C8 File Offset: 0x000199C8
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0001B7D0 File Offset: 0x000199D0
		public string HistoryID
		{
			get
			{
				return this.historyIDField;
			}
			set
			{
				this.historyIDField = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001B7D9 File Offset: 0x000199D9
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0001B7E1 File Offset: 0x000199E1
		public PageSettings ReportPageSettings
		{
			get
			{
				return this.reportPageSettingsField;
			}
			set
			{
				this.reportPageSettingsField = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001B7EA File Offset: 0x000199EA
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x0001B7F2 File Offset: 0x000199F2
		public int AutoRefreshInterval
		{
			get
			{
				return this.autoRefreshIntervalField;
			}
			set
			{
				this.autoRefreshIntervalField = value;
			}
		}

		// Token: 0x040001AE RID: 430
		private bool hasSnapshotField;

		// Token: 0x040001AF RID: 431
		private bool needsProcessingField;

		// Token: 0x040001B0 RID: 432
		private bool allowQueryExecutionField;

		// Token: 0x040001B1 RID: 433
		private bool credentialsRequiredField;

		// Token: 0x040001B2 RID: 434
		private bool parametersRequiredField;

		// Token: 0x040001B3 RID: 435
		private DateTime expirationDateTimeField;

		// Token: 0x040001B4 RID: 436
		private DateTime executionDateTimeField;

		// Token: 0x040001B5 RID: 437
		private int numPagesField;

		// Token: 0x040001B6 RID: 438
		private ReportParameter[] parametersField;

		// Token: 0x040001B7 RID: 439
		private DataSourcePrompt[] dataSourcePromptsField;

		// Token: 0x040001B8 RID: 440
		private bool hasDocumentMapField;

		// Token: 0x040001B9 RID: 441
		private string executionIDField;

		// Token: 0x040001BA RID: 442
		private string reportPathField;

		// Token: 0x040001BB RID: 443
		private string historyIDField;

		// Token: 0x040001BC RID: 444
		private PageSettings reportPageSettingsField;

		// Token: 0x040001BD RID: 445
		private int autoRefreshIntervalField;
	}
}
