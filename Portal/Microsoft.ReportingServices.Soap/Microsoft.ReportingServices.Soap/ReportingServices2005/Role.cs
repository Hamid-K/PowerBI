using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000239 RID: 569
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Role
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x000226BB File Offset: 0x000208BB
		// (set) Token: 0x06001586 RID: 5510 RVA: 0x000226C3 File Offset: 0x000208C3
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

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x000226CC File Offset: 0x000208CC
		// (set) Token: 0x06001588 RID: 5512 RVA: 0x000226D4 File Offset: 0x000208D4
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

		// Token: 0x040006BD RID: 1725
		private string nameField;

		// Token: 0x040006BE RID: 1726
		private string descriptionField;
	}
}
