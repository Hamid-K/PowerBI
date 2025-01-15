using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020001FE RID: 510
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Subscription
	{
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00021726 File Offset: 0x0001F926
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x0002172E File Offset: 0x0001F92E
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

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00021737 File Offset: 0x0001F937
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x0002173F File Offset: 0x0001F93F
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

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00021748 File Offset: 0x0001F948
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x00021750 File Offset: 0x0001F950
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

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00021759 File Offset: 0x0001F959
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x00021761 File Offset: 0x0001F961
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

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0002176A File Offset: 0x0001F96A
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x00021772 File Offset: 0x0001F972
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

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0002177B File Offset: 0x0001F97B
		// (set) Token: 0x060013B8 RID: 5048 RVA: 0x00021783 File Offset: 0x0001F983
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

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0002178C File Offset: 0x0001F98C
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x00021794 File Offset: 0x0001F994
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

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0002179D File Offset: 0x0001F99D
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x000217A5 File Offset: 0x0001F9A5
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

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x000217AE File Offset: 0x0001F9AE
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x000217B6 File Offset: 0x0001F9B6
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x000217BF File Offset: 0x0001F9BF
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x000217C7 File Offset: 0x0001F9C7
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

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x000217D0 File Offset: 0x0001F9D0
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x000217D8 File Offset: 0x0001F9D8
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

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x000217E1 File Offset: 0x0001F9E1
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x000217E9 File Offset: 0x0001F9E9
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

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000217F2 File Offset: 0x0001F9F2
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x000217FA File Offset: 0x0001F9FA
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

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x00021803 File Offset: 0x0001FA03
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x0002180B File Offset: 0x0001FA0B
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

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00021814 File Offset: 0x0001FA14
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x0002181C File Offset: 0x0001FA1C
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

		// Token: 0x040005A7 RID: 1447
		private string subscriptionIDField;

		// Token: 0x040005A8 RID: 1448
		private string ownerField;

		// Token: 0x040005A9 RID: 1449
		private string pathField;

		// Token: 0x040005AA RID: 1450
		private string virtualPathField;

		// Token: 0x040005AB RID: 1451
		private string reportField;

		// Token: 0x040005AC RID: 1452
		private ExtensionSettings deliverySettingsField;

		// Token: 0x040005AD RID: 1453
		private string descriptionField;

		// Token: 0x040005AE RID: 1454
		private string statusField;

		// Token: 0x040005AF RID: 1455
		private ActiveState activeField;

		// Token: 0x040005B0 RID: 1456
		private DateTime lastExecutedField;

		// Token: 0x040005B1 RID: 1457
		private bool lastExecutedFieldSpecified;

		// Token: 0x040005B2 RID: 1458
		private string modifiedByField;

		// Token: 0x040005B3 RID: 1459
		private DateTime modifiedDateField;

		// Token: 0x040005B4 RID: 1460
		private string eventTypeField;

		// Token: 0x040005B5 RID: 1461
		private bool isDataDrivenField;
	}
}
