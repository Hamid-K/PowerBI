using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000027 RID: 39
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Task
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000CE8D File Offset: 0x0000B08D
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x0000CE95 File Offset: 0x0000B095
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0000CE9E File Offset: 0x0000B09E
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x0000CEA6 File Offset: 0x0000B0A6
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000CEAF File Offset: 0x0000B0AF
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0000CEB7 File Offset: 0x0000B0B7
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

		// Token: 0x04000192 RID: 402
		private string taskIDField;

		// Token: 0x04000193 RID: 403
		private string nameField;

		// Token: 0x04000194 RID: 404
		private string descriptionField;
	}
}
