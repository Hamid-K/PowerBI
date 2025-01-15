using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000142 RID: 322
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ScheduleExpiration : ExpirationDefinition
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00017233 File Offset: 0x00015433
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x0001723B File Offset: 0x0001543B
		[XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))]
		[XmlElement("ScheduleReference", typeof(ScheduleReference))]
		public ScheduleDefinitionOrReference Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x04000404 RID: 1028
		private ScheduleDefinitionOrReference itemField;
	}
}
