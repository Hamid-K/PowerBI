using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000137 RID: 311
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ScheduleReference : ScheduleDefinitionOrReference
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x000170A7 File Offset: 0x000152A7
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x000170AF File Offset: 0x000152AF
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

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x000170B8 File Offset: 0x000152B8
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x000170C0 File Offset: 0x000152C0
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

		// Token: 0x040003DD RID: 989
		private string scheduleIDField;

		// Token: 0x040003DE RID: 990
		private ScheduleDefinition definitionField;
	}
}
