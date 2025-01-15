using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000228 RID: 552
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ScheduleExpiration : ExpirationDefinition
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x000221CE File Offset: 0x000203CE
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x000221D6 File Offset: 0x000203D6
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

		// Token: 0x04000658 RID: 1624
		private ScheduleDefinitionOrReference itemField;
	}
}
