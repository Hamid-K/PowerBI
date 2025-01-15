using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000041 RID: 65
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Property
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0000D70A File Offset: 0x0000B90A
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x0000D712 File Offset: 0x0000B912
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000D71B File Offset: 0x0000B91B
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0000D723 File Offset: 0x0000B923
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04000215 RID: 533
		private string nameField;

		// Token: 0x04000216 RID: 534
		private string valueField;
	}
}
