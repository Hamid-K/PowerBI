using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200023A RID: 570
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Policy
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x000226E5 File Offset: 0x000208E5
		// (set) Token: 0x0600158B RID: 5515 RVA: 0x000226ED File Offset: 0x000208ED
		public string GroupUserName
		{
			get
			{
				return this.groupUserNameField;
			}
			set
			{
				this.groupUserNameField = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x000226F6 File Offset: 0x000208F6
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x000226FE File Offset: 0x000208FE
		public Role[] Roles
		{
			get
			{
				return this.rolesField;
			}
			set
			{
				this.rolesField = value;
			}
		}

		// Token: 0x040006BF RID: 1727
		private string groupUserNameField;

		// Token: 0x040006C0 RID: 1728
		private Role[] rolesField;
	}
}
