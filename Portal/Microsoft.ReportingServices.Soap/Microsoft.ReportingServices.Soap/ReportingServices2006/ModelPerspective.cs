using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000113 RID: 275
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ModelPerspective
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0001667D File Offset: 0x0001487D
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x00016685 File Offset: 0x00014885
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

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0001668E File Offset: 0x0001488E
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x00016696 File Offset: 0x00014896
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

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0001669F File Offset: 0x0001489F
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x000166A7 File Offset: 0x000148A7
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

		// Token: 0x0400033F RID: 831
		private string idField;

		// Token: 0x04000340 RID: 832
		private string nameField;

		// Token: 0x04000341 RID: 833
		private string descriptionField;
	}
}
