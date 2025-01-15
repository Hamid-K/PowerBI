using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000233 RID: 563
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ModelPerspective
	{
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x00022594 File Offset: 0x00020794
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x0002259C File Offset: 0x0002079C
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

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x000225A5 File Offset: 0x000207A5
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x000225AD File Offset: 0x000207AD
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

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x000225B6 File Offset: 0x000207B6
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x000225BE File Offset: 0x000207BE
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

		// Token: 0x040006AB RID: 1707
		private string idField;

		// Token: 0x040006AC RID: 1708
		private string nameField;

		// Token: 0x040006AD RID: 1709
		private string descriptionField;
	}
}
