using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000025 RID: 37
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Policy
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000CE39 File Offset: 0x0000B039
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0000CE41 File Offset: 0x0000B041
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0000CE4A File Offset: 0x0000B04A
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0000CE52 File Offset: 0x0000B052
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

		// Token: 0x0400018E RID: 398
		private string groupUserNameField;

		// Token: 0x0400018F RID: 399
		private Role[] rolesField;
	}
}
