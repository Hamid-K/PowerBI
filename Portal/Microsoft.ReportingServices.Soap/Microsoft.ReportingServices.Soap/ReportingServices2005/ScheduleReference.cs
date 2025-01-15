using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200021C RID: 540
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ScheduleReference : ScheduleDefinitionOrReference
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00022029 File Offset: 0x00020229
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x00022031 File Offset: 0x00020231
		public string ScheduleID
		{
			get
			{
				return this.scheduleIDField;
			}
			set
			{
				this.scheduleIDField = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0002203A File Offset: 0x0002023A
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x00022042 File Offset: 0x00020242
		public ScheduleDefinition Definition
		{
			get
			{
				return this.definitionField;
			}
			set
			{
				this.definitionField = value;
			}
		}

		// Token: 0x04000631 RID: 1585
		private string scheduleIDField;

		// Token: 0x04000632 RID: 1586
		private ScheduleDefinition definitionField;
	}
}
