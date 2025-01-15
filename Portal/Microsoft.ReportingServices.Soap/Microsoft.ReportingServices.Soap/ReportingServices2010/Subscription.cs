using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200002E RID: 46
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Subscription
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000D000 File Offset: 0x0000B200
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0000D008 File Offset: 0x0000B208
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000D011 File Offset: 0x0000B211
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000D019 File Offset: 0x0000B219
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000D022 File Offset: 0x0000B222
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0000D02A File Offset: 0x0000B22A
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0000D033 File Offset: 0x0000B233
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0000D03B File Offset: 0x0000B23B
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000D044 File Offset: 0x0000B244
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0000D04C File Offset: 0x0000B24C
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000D055 File Offset: 0x0000B255
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0000D05D File Offset: 0x0000B25D
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000D066 File Offset: 0x0000B266
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0000D06E File Offset: 0x0000B26E
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000D077 File Offset: 0x0000B277
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0000D07F File Offset: 0x0000B27F
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000D088 File Offset: 0x0000B288
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000D090 File Offset: 0x0000B290
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000D099 File Offset: 0x0000B299
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000D0A1 File Offset: 0x0000B2A1
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000D0AA File Offset: 0x0000B2AA
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0000D0B2 File Offset: 0x0000B2B2
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000D0BB File Offset: 0x0000B2BB
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0000D0C3 File Offset: 0x0000B2C3
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000D0DD File Offset: 0x0000B2DD
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0000D0E5 File Offset: 0x0000B2E5
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000D0EE File Offset: 0x0000B2EE
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0000D0F6 File Offset: 0x0000B2F6
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

		// Token: 0x040001AA RID: 426
		private string subscriptionIDField;

		// Token: 0x040001AB RID: 427
		private string ownerField;

		// Token: 0x040001AC RID: 428
		private string pathField;

		// Token: 0x040001AD RID: 429
		private string virtualPathField;

		// Token: 0x040001AE RID: 430
		private string reportField;

		// Token: 0x040001AF RID: 431
		private ExtensionSettings deliverySettingsField;

		// Token: 0x040001B0 RID: 432
		private string descriptionField;

		// Token: 0x040001B1 RID: 433
		private string statusField;

		// Token: 0x040001B2 RID: 434
		private ActiveState activeField;

		// Token: 0x040001B3 RID: 435
		private DateTime lastExecutedField;

		// Token: 0x040001B4 RID: 436
		private bool lastExecutedFieldSpecified;

		// Token: 0x040001B5 RID: 437
		private string modifiedByField;

		// Token: 0x040001B6 RID: 438
		private DateTime modifiedDateField;

		// Token: 0x040001B7 RID: 439
		private string eventTypeField;

		// Token: 0x040001B8 RID: 440
		private bool isDataDrivenField;
	}
}
