using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000150 RID: 336
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Policy
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000176B2 File Offset: 0x000158B2
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x000176BA File Offset: 0x000158BA
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

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000176C3 File Offset: 0x000158C3
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x000176CB File Offset: 0x000158CB
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

		// Token: 0x04000459 RID: 1113
		private string groupUserNameField;

		// Token: 0x0400045A RID: 1114
		private Role[] rolesField;
	}
}
