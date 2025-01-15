using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000122 RID: 290
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Field
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00016B1C File Offset: 0x00014D1C
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x00016B24 File Offset: 0x00014D24
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

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00016B2D File Offset: 0x00014D2D
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x00016B35 File Offset: 0x00014D35
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

		// Token: 0x04000383 RID: 899
		private string aliasField;

		// Token: 0x04000384 RID: 900
		private string nameField;
	}
}
