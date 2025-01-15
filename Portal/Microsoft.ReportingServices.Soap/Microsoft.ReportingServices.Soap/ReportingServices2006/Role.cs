using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200014F RID: 335
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Role
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00017688 File Offset: 0x00015888
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x00017690 File Offset: 0x00015890
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

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00017699 File Offset: 0x00015899
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x000176A1 File Offset: 0x000158A1
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

		// Token: 0x04000457 RID: 1111
		private string nameField;

		// Token: 0x04000458 RID: 1112
		private string descriptionField;
	}
}
