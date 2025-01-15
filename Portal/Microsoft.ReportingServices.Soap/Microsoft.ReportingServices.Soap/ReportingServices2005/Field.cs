using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000207 RID: 519
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Field
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00021A9E File Offset: 0x0001FC9E
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x00021AA6 File Offset: 0x0001FCA6
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

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x00021AAF File Offset: 0x0001FCAF
		// (set) Token: 0x06001419 RID: 5145 RVA: 0x00021AB7 File Offset: 0x0001FCB7
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

		// Token: 0x040005D7 RID: 1495
		private string aliasField;

		// Token: 0x040005D8 RID: 1496
		private string nameField;
	}
}
