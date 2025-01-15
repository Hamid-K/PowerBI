using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200001A RID: 26
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ScheduleReference : ScheduleDefinitionOrReference
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000CA31 File Offset: 0x0000AC31
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x0000CA39 File Offset: 0x0000AC39
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000CA42 File Offset: 0x0000AC42
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0000CA4A File Offset: 0x0000AC4A
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

		// Token: 0x04000153 RID: 339
		private string scheduleIDField;

		// Token: 0x04000154 RID: 340
		private ScheduleDefinition definitionField;
	}
}
