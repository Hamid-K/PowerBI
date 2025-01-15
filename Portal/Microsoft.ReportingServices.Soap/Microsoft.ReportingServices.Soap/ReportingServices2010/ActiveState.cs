using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000033 RID: 51
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ActiveState
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000D19E File Offset: 0x0000B39E
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x0000D1A6 File Offset: 0x0000B3A6
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000D1AF File Offset: 0x0000B3AF
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0000D1B7 File Offset: 0x0000B3B7
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000D1D1 File Offset: 0x0000B3D1
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0000D1D9 File Offset: 0x0000B3D9
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0000D1E2 File Offset: 0x0000B3E2
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0000D1EA File Offset: 0x0000B3EA
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000D1F3 File Offset: 0x0000B3F3
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0000D1FB File Offset: 0x0000B3FB
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000D204 File Offset: 0x0000B404
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x0000D20C File Offset: 0x0000B40C
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000D215 File Offset: 0x0000B415
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0000D21D File Offset: 0x0000B41D
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000D226 File Offset: 0x0000B426
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0000D22E File Offset: 0x0000B42E
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000D237 File Offset: 0x0000B437
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0000D23F File Offset: 0x0000B43F
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000D248 File Offset: 0x0000B448
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0000D250 File Offset: 0x0000B450
		public bool DisabledByUser
		{
			get
			{
				return this.disabledByUserField;
			}
			set
			{
				this.disabledByUserField = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000D259 File Offset: 0x0000B459
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0000D261 File Offset: 0x0000B461
		[XmlIgnore]
		public bool DisabledByUserSpecified
		{
			get
			{
				return this.disabledByUserFieldSpecified;
			}
			set
			{
				this.disabledByUserFieldSpecified = value;
			}
		}

		// Token: 0x040001C0 RID: 448
		private bool deliveryExtensionRemovedField;

		// Token: 0x040001C1 RID: 449
		private bool deliveryExtensionRemovedFieldSpecified;

		// Token: 0x040001C2 RID: 450
		private bool sharedDataSourceRemovedField;

		// Token: 0x040001C3 RID: 451
		private bool sharedDataSourceRemovedFieldSpecified;

		// Token: 0x040001C4 RID: 452
		private bool missingParameterValueField;

		// Token: 0x040001C5 RID: 453
		private bool missingParameterValueFieldSpecified;

		// Token: 0x040001C6 RID: 454
		private bool invalidParameterValueField;

		// Token: 0x040001C7 RID: 455
		private bool invalidParameterValueFieldSpecified;

		// Token: 0x040001C8 RID: 456
		private bool unknownReportParameterField;

		// Token: 0x040001C9 RID: 457
		private bool unknownReportParameterFieldSpecified;

		// Token: 0x040001CA RID: 458
		private bool disabledByUserField;

		// Token: 0x040001CB RID: 459
		private bool disabledByUserFieldSpecified;
	}
}
