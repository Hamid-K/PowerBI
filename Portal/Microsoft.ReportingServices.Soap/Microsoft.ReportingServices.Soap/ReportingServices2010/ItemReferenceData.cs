using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200003A RID: 58
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ItemReferenceData
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000D498 File Offset: 0x0000B698
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000D4A9 File Offset: 0x0000B6A9
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x0000D4B1 File Offset: 0x0000B6B1
		public string Reference
		{
			get
			{
				return this.referenceField;
			}
			set
			{
				this.referenceField = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0000D4BA File Offset: 0x0000B6BA
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x0000D4C2 File Offset: 0x0000B6C2
		public string ReferenceType
		{
			get
			{
				return this.referenceTypeField;
			}
			set
			{
				this.referenceTypeField = value;
			}
		}

		// Token: 0x040001EE RID: 494
		private string nameField;

		// Token: 0x040001EF RID: 495
		private string referenceField;

		// Token: 0x040001F0 RID: 496
		private string referenceTypeField;
	}
}
