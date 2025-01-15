using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200001F RID: 31
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ModelPerspective
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000CD01 File Offset: 0x0000AF01
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0000CD09 File Offset: 0x0000AF09
		public string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000CD12 File Offset: 0x0000AF12
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000CD1A File Offset: 0x0000AF1A
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000CD23 File Offset: 0x0000AF23
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0000CD2B File Offset: 0x0000AF2B
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

		// Token: 0x0400017B RID: 379
		private string idField;

		// Token: 0x0400017C RID: 380
		private string nameField;

		// Token: 0x0400017D RID: 381
		private string descriptionField;
	}
}
