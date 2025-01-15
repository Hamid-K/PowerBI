using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000119 RID: 281
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Subscription
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x000167A4 File Offset: 0x000149A4
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x000167AC File Offset: 0x000149AC
		public string SubscriptionID
		{
			get
			{
				return this.subscriptionIDField;
			}
			set
			{
				this.subscriptionIDField = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x000167B5 File Offset: 0x000149B5
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x000167BD File Offset: 0x000149BD
		public string Owner
		{
			get
			{
				return this.ownerField;
			}
			set
			{
				this.ownerField = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000167C6 File Offset: 0x000149C6
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x000167CE File Offset: 0x000149CE
		public string Path
		{
			get
			{
				return this.pathField;
			}
			set
			{
				this.pathField = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x000167D7 File Offset: 0x000149D7
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x000167DF File Offset: 0x000149DF
		public string VirtualPath
		{
			get
			{
				return this.virtualPathField;
			}
			set
			{
				this.virtualPathField = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x000167E8 File Offset: 0x000149E8
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x000167F0 File Offset: 0x000149F0
		public string Report
		{
			get
			{
				return this.reportField;
			}
			set
			{
				this.reportField = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000167F9 File Offset: 0x000149F9
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00016801 File Offset: 0x00014A01
		public ExtensionSettings DeliverySettings
		{
			get
			{
				return this.deliverySettingsField;
			}
			set
			{
				this.deliverySettingsField = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0001680A File Offset: 0x00014A0A
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x00016812 File Offset: 0x00014A12
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

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0001681B File Offset: 0x00014A1B
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x00016823 File Offset: 0x00014A23
		public string Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0001682C File Offset: 0x00014A2C
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x00016834 File Offset: 0x00014A34
		public ActiveState Active
		{
			get
			{
				return this.activeField;
			}
			set
			{
				this.activeField = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0001683D File Offset: 0x00014A3D
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00016845 File Offset: 0x00014A45
		public DateTime LastExecuted
		{
			get
			{
				return this.lastExecutedField;
			}
			set
			{
				this.lastExecutedField = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0001684E File Offset: 0x00014A4E
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00016856 File Offset: 0x00014A56
		[XmlIgnore]
		public bool LastExecutedSpecified
		{
			get
			{
				return this.lastExecutedFieldSpecified;
			}
			set
			{
				this.lastExecutedFieldSpecified = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0001685F File Offset: 0x00014A5F
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00016867 File Offset: 0x00014A67
		public string ModifiedBy
		{
			get
			{
				return this.modifiedByField;
			}
			set
			{
				this.modifiedByField = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00016870 File Offset: 0x00014A70
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x00016878 File Offset: 0x00014A78
		public DateTime ModifiedDate
		{
			get
			{
				return this.modifiedDateField;
			}
			set
			{
				this.modifiedDateField = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00016881 File Offset: 0x00014A81
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00016889 File Offset: 0x00014A89
		public string EventType
		{
			get
			{
				return this.eventTypeField;
			}
			set
			{
				this.eventTypeField = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00016892 File Offset: 0x00014A92
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x0001689A File Offset: 0x00014A9A
		public bool IsDataDriven
		{
			get
			{
				return this.isDataDrivenField;
			}
			set
			{
				this.isDataDrivenField = value;
			}
		}

		// Token: 0x04000353 RID: 851
		private string subscriptionIDField;

		// Token: 0x04000354 RID: 852
		private string ownerField;

		// Token: 0x04000355 RID: 853
		private string pathField;

		// Token: 0x04000356 RID: 854
		private string virtualPathField;

		// Token: 0x04000357 RID: 855
		private string reportField;

		// Token: 0x04000358 RID: 856
		private ExtensionSettings deliverySettingsField;

		// Token: 0x04000359 RID: 857
		private string descriptionField;

		// Token: 0x0400035A RID: 858
		private string statusField;

		// Token: 0x0400035B RID: 859
		private ActiveState activeField;

		// Token: 0x0400035C RID: 860
		private DateTime lastExecutedField;

		// Token: 0x0400035D RID: 861
		private bool lastExecutedFieldSpecified;

		// Token: 0x0400035E RID: 862
		private string modifiedByField;

		// Token: 0x0400035F RID: 863
		private DateTime modifiedDateField;

		// Token: 0x04000360 RID: 864
		private string eventTypeField;

		// Token: 0x04000361 RID: 865
		private bool isDataDrivenField;
	}
}
