using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000116 RID: 278
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Event
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0001672E File Offset: 0x0001492E
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00016736 File Offset: 0x00014936
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

		// Token: 0x04000348 RID: 840
		private string typeField;
	}
}
