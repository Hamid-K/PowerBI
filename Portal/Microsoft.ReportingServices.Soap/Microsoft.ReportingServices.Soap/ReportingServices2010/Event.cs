using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000009 RID: 9
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Event
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000C6E7 File Offset: 0x0000A8E7
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000C6EF File Offset: 0x0000A8EF
		public string Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x04000123 RID: 291
		private string typeField;
	}
}
