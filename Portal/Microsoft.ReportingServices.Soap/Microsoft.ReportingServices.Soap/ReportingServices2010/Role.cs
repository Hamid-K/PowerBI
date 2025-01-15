using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000026 RID: 38
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Role
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000CE63 File Offset: 0x0000B063
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0000CE6B File Offset: 0x0000B06B
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000CE74 File Offset: 0x0000B074
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0000CE7C File Offset: 0x0000B07C
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

		// Token: 0x04000190 RID: 400
		private string nameField;

		// Token: 0x04000191 RID: 401
		private string descriptionField;
	}
}
