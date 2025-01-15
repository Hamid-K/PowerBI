using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000236 RID: 566
	[XmlInclude(typeof(SearchCondition))]
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Property
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00022667 File Offset: 0x00020867
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x0002266F File Offset: 0x0002086F
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

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00022678 File Offset: 0x00020878
		// (set) Token: 0x0600157E RID: 5502 RVA: 0x00022680 File Offset: 0x00020880
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

		// Token: 0x040006B6 RID: 1718
		private string nameField;

		// Token: 0x040006B7 RID: 1719
		private string valueField;
	}
}
