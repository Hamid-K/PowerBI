using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000115 RID: 277
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Task
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x000166F3 File Offset: 0x000148F3
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x000166FB File Offset: 0x000148FB
		public string TaskID
		{
			get
			{
				return this.taskIDField;
			}
			set
			{
				this.taskIDField = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00016704 File Offset: 0x00014904
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0001670C File Offset: 0x0001490C
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

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00016715 File Offset: 0x00014915
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x0001671D File Offset: 0x0001491D
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x04000345 RID: 837
		private string taskIDField;

		// Token: 0x04000346 RID: 838
		private string nameField;

		// Token: 0x04000347 RID: 839
		private string descriptionField;
	}
}
