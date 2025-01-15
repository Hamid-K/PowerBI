using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000037 RID: 55
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class DataSetDefinition
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000D39A File Offset: 0x0000B59A
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0000D3A2 File Offset: 0x0000B5A2
		public Field[] Fields
		{
			get
			{
				return this.fieldsField;
			}
			set
			{
				this.fieldsField = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000D3AB File Offset: 0x0000B5AB
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0000D3B3 File Offset: 0x0000B5B3
		public QueryDefinition Query
		{
			get
			{
				return this.queryField;
			}
			set
			{
				this.queryField = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
		public SensitivityEnum CaseSensitivity
		{
			get
			{
				return this.caseSensitivityField;
			}
			set
			{
				this.caseSensitivityField = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0000D3CD File Offset: 0x0000B5CD
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0000D3D5 File Offset: 0x0000B5D5
		[XmlIgnore]
		public bool CaseSensitivitySpecified
		{
			get
			{
				return this.caseSensitivityFieldSpecified;
			}
			set
			{
				this.caseSensitivityFieldSpecified = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000D3DE File Offset: 0x0000B5DE
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0000D3E6 File Offset: 0x0000B5E6
		public string Collation
		{
			get
			{
				return this.collationField;
			}
			set
			{
				this.collationField = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000D3EF File Offset: 0x0000B5EF
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0000D3F7 File Offset: 0x0000B5F7
		public SensitivityEnum AccentSensitivity
		{
			get
			{
				return this.accentSensitivityField;
			}
			set
			{
				this.accentSensitivityField = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000D400 File Offset: 0x0000B600
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0000D408 File Offset: 0x0000B608
		[XmlIgnore]
		public bool AccentSensitivitySpecified
		{
			get
			{
				return this.accentSensitivityFieldSpecified;
			}
			set
			{
				this.accentSensitivityFieldSpecified = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000D411 File Offset: 0x0000B611
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0000D419 File Offset: 0x0000B619
		public SensitivityEnum KanatypeSensitivity
		{
			get
			{
				return this.kanatypeSensitivityField;
			}
			set
			{
				this.kanatypeSensitivityField = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0000D422 File Offset: 0x0000B622
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x0000D42A File Offset: 0x0000B62A
		[XmlIgnore]
		public bool KanatypeSensitivitySpecified
		{
			get
			{
				return this.kanatypeSensitivityFieldSpecified;
			}
			set
			{
				this.kanatypeSensitivityFieldSpecified = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0000D433 File Offset: 0x0000B633
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x0000D43B File Offset: 0x0000B63B
		public SensitivityEnum WidthSensitivity
		{
			get
			{
				return this.widthSensitivityField;
			}
			set
			{
				this.widthSensitivityField = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000D444 File Offset: 0x0000B644
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x0000D44C File Offset: 0x0000B64C
		[XmlIgnore]
		public bool WidthSensitivitySpecified
		{
			get
			{
				return this.widthSensitivityFieldSpecified;
			}
			set
			{
				this.widthSensitivityFieldSpecified = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000D455 File Offset: 0x0000B655
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x0000D45D File Offset: 0x0000B65D
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

		// Token: 0x040001DC RID: 476
		private Field[] fieldsField;

		// Token: 0x040001DD RID: 477
		private QueryDefinition queryField;

		// Token: 0x040001DE RID: 478
		private SensitivityEnum caseSensitivityField;

		// Token: 0x040001DF RID: 479
		private bool caseSensitivityFieldSpecified;

		// Token: 0x040001E0 RID: 480
		private string collationField;

		// Token: 0x040001E1 RID: 481
		private SensitivityEnum accentSensitivityField;

		// Token: 0x040001E2 RID: 482
		private bool accentSensitivityFieldSpecified;

		// Token: 0x040001E3 RID: 483
		private SensitivityEnum kanatypeSensitivityField;

		// Token: 0x040001E4 RID: 484
		private bool kanatypeSensitivityFieldSpecified;

		// Token: 0x040001E5 RID: 485
		private SensitivityEnum widthSensitivityField;

		// Token: 0x040001E6 RID: 486
		private bool widthSensitivityFieldSpecified;

		// Token: 0x040001E7 RID: 487
		private string nameField;
	}
}
