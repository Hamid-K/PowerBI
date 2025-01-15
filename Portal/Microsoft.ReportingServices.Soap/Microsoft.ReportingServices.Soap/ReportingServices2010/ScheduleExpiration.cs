using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200000D RID: 13
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ScheduleExpiration : ExpirationDefinition
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000C77E File Offset: 0x0000A97E
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000C786 File Offset: 0x0000A986
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

		// Token: 0x0400012A RID: 298
		private ScheduleDefinitionOrReference itemField;
	}
}
