using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000036 RID: 54
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Field
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000D370 File Offset: 0x0000B570
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000D378 File Offset: 0x0000B578
		public string Alias
		{
			get
			{
				return this.aliasField;
			}
			set
			{
				this.aliasField = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000D381 File Offset: 0x0000B581
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000D389 File Offset: 0x0000B589
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

		// Token: 0x040001DA RID: 474
		private string aliasField;

		// Token: 0x040001DB RID: 475
		private string nameField;
	}
}
