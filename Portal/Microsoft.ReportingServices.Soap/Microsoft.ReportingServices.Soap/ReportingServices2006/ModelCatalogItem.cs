using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000114 RID: 276
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ModelCatalogItem
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x000166B8 File Offset: 0x000148B8
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x000166C0 File Offset: 0x000148C0
		public string Model
		{
			get
			{
				return this.modelField;
			}
			set
			{
				this.modelField = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x000166C9 File Offset: 0x000148C9
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x000166D1 File Offset: 0x000148D1
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

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x000166DA File Offset: 0x000148DA
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x000166E2 File Offset: 0x000148E2
		public ModelPerspective[] Perspectives
		{
			get
			{
				return this.perspectivesField;
			}
			set
			{
				this.perspectivesField = value;
			}
		}

		// Token: 0x04000342 RID: 834
		private string modelField;

		// Token: 0x04000343 RID: 835
		private string descriptionField;

		// Token: 0x04000344 RID: 836
		private ModelPerspective[] perspectivesField;
	}
}
