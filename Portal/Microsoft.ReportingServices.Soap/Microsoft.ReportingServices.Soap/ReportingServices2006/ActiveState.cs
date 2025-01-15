using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200011E RID: 286
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ActiveState
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00016942 File Offset: 0x00014B42
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x0001694A File Offset: 0x00014B4A
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

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00016953 File Offset: 0x00014B53
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x0001695B File Offset: 0x00014B5B
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

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00016964 File Offset: 0x00014B64
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x0001696C File Offset: 0x00014B6C
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

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00016975 File Offset: 0x00014B75
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x0001697D File Offset: 0x00014B7D
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

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00016986 File Offset: 0x00014B86
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x0001698E File Offset: 0x00014B8E
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

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00016997 File Offset: 0x00014B97
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x0001699F File Offset: 0x00014B9F
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

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x000169A8 File Offset: 0x00014BA8
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x000169B0 File Offset: 0x00014BB0
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

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000169B9 File Offset: 0x00014BB9
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x000169C1 File Offset: 0x00014BC1
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

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x000169CA File Offset: 0x00014BCA
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x000169D2 File Offset: 0x00014BD2
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

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x000169DB File Offset: 0x00014BDB
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x000169E3 File Offset: 0x00014BE3
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

		// Token: 0x04000369 RID: 873
		private bool deliveryExtensionRemovedField;

		// Token: 0x0400036A RID: 874
		private bool deliveryExtensionRemovedFieldSpecified;

		// Token: 0x0400036B RID: 875
		private bool sharedDataSourceRemovedField;

		// Token: 0x0400036C RID: 876
		private bool sharedDataSourceRemovedFieldSpecified;

		// Token: 0x0400036D RID: 877
		private bool missingParameterValueField;

		// Token: 0x0400036E RID: 878
		private bool missingParameterValueFieldSpecified;

		// Token: 0x0400036F RID: 879
		private bool invalidParameterValueField;

		// Token: 0x04000370 RID: 880
		private bool invalidParameterValueFieldSpecified;

		// Token: 0x04000371 RID: 881
		private bool unknownReportParameterField;

		// Token: 0x04000372 RID: 882
		private bool unknownReportParameterFieldSpecified;
	}
}
