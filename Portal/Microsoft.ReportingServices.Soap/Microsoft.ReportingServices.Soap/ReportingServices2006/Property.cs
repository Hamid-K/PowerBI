using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200014C RID: 332
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Property
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00017634 File Offset: 0x00015834
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x0001763C File Offset: 0x0001583C
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

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00017645 File Offset: 0x00015845
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x0001764D File Offset: 0x0001584D
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

		// Token: 0x04000450 RID: 1104
		private string nameField;

		// Token: 0x04000451 RID: 1105
		private string valueField;
	}
}
