using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200003B RID: 59
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ItemReference
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000D4D3 File Offset: 0x0000B6D3
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000D4DB File Offset: 0x0000B6DB
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000D4E4 File Offset: 0x0000B6E4
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000D4EC File Offset: 0x0000B6EC
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

		// Token: 0x040001F1 RID: 497
		private string nameField;

		// Token: 0x040001F2 RID: 498
		private string referenceField;
	}
}
