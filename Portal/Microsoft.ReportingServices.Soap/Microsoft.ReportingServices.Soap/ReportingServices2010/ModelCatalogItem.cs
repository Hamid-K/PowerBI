using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000020 RID: 32
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ModelCatalogItem
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0000CD44 File Offset: 0x0000AF44
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000CD4D File Offset: 0x0000AF4D
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0000CD55 File Offset: 0x0000AF55
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000CD5E File Offset: 0x0000AF5E
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000CD66 File Offset: 0x0000AF66
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

		// Token: 0x0400017E RID: 382
		private string modelField;

		// Token: 0x0400017F RID: 383
		private string descriptionField;

		// Token: 0x04000180 RID: 384
		private ModelPerspective[] perspectivesField;
	}
}
