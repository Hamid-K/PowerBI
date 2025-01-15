using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020001FB RID: 507
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Event
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x000216B0 File Offset: 0x0001F8B0
		// (set) Token: 0x060013A0 RID: 5024 RVA: 0x000216B8 File Offset: 0x0001F8B8
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

		// Token: 0x0400059C RID: 1436
		private string typeField;
	}
}
