using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000203 RID: 515
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ActiveState
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x000218C4 File Offset: 0x0001FAC4
		// (set) Token: 0x060013DF RID: 5087 RVA: 0x000218CC File Offset: 0x0001FACC
		public bool DeliveryExtensionRemoved
		{
			get
			{
				return this.deliveryExtensionRemovedField;
			}
			set
			{
				this.deliveryExtensionRemovedField = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x000218D5 File Offset: 0x0001FAD5
		// (set) Token: 0x060013E1 RID: 5089 RVA: 0x000218DD File Offset: 0x0001FADD
		[XmlIgnore]
		public bool DeliveryExtensionRemovedSpecified
		{
			get
			{
				return this.deliveryExtensionRemovedFieldSpecified;
			}
			set
			{
				this.deliveryExtensionRemovedFieldSpecified = value;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x000218E6 File Offset: 0x0001FAE6
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x000218EE File Offset: 0x0001FAEE
		public bool SharedDataSourceRemoved
		{
			get
			{
				return this.sharedDataSourceRemovedField;
			}
			set
			{
				this.sharedDataSourceRemovedField = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x000218F7 File Offset: 0x0001FAF7
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x000218FF File Offset: 0x0001FAFF
		[XmlIgnore]
		public bool SharedDataSourceRemovedSpecified
		{
			get
			{
				return this.sharedDataSourceRemovedFieldSpecified;
			}
			set
			{
				this.sharedDataSourceRemovedFieldSpecified = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00021908 File Offset: 0x0001FB08
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x00021910 File Offset: 0x0001FB10
		public bool MissingParameterValue
		{
			get
			{
				return this.missingParameterValueField;
			}
			set
			{
				this.missingParameterValueField = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00021919 File Offset: 0x0001FB19
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x00021921 File Offset: 0x0001FB21
		[XmlIgnore]
		public bool MissingParameterValueSpecified
		{
			get
			{
				return this.missingParameterValueFieldSpecified;
			}
			set
			{
				this.missingParameterValueFieldSpecified = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0002192A File Offset: 0x0001FB2A
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00021932 File Offset: 0x0001FB32
		public bool InvalidParameterValue
		{
			get
			{
				return this.invalidParameterValueField;
			}
			set
			{
				this.invalidParameterValueField = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x0002193B File Offset: 0x0001FB3B
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x00021943 File Offset: 0x0001FB43
		[XmlIgnore]
		public bool InvalidParameterValueSpecified
		{
			get
			{
				return this.invalidParameterValueFieldSpecified;
			}
			set
			{
				this.invalidParameterValueFieldSpecified = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0002194C File Offset: 0x0001FB4C
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x00021954 File Offset: 0x0001FB54
		public bool UnknownReportParameter
		{
			get
			{
				return this.unknownReportParameterField;
			}
			set
			{
				this.unknownReportParameterField = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0002195D File Offset: 0x0001FB5D
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00021965 File Offset: 0x0001FB65
		[XmlIgnore]
		public bool UnknownReportParameterSpecified
		{
			get
			{
				return this.unknownReportParameterFieldSpecified;
			}
			set
			{
				this.unknownReportParameterFieldSpecified = value;
			}
		}

		// Token: 0x040005BD RID: 1469
		private bool deliveryExtensionRemovedField;

		// Token: 0x040005BE RID: 1470
		private bool deliveryExtensionRemovedFieldSpecified;

		// Token: 0x040005BF RID: 1471
		private bool sharedDataSourceRemovedField;

		// Token: 0x040005C0 RID: 1472
		private bool sharedDataSourceRemovedFieldSpecified;

		// Token: 0x040005C1 RID: 1473
		private bool missingParameterValueField;

		// Token: 0x040005C2 RID: 1474
		private bool missingParameterValueFieldSpecified;

		// Token: 0x040005C3 RID: 1475
		private bool invalidParameterValueField;

		// Token: 0x040005C4 RID: 1476
		private bool invalidParameterValueFieldSpecified;

		// Token: 0x040005C5 RID: 1477
		private bool unknownReportParameterField;

		// Token: 0x040005C6 RID: 1478
		private bool unknownReportParameterFieldSpecified;
	}
}
