using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000234 RID: 564
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ModelCatalogItem
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x000225CF File Offset: 0x000207CF
		// (set) Token: 0x0600156A RID: 5482 RVA: 0x000225D7 File Offset: 0x000207D7
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

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x000225E0 File Offset: 0x000207E0
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x000225E8 File Offset: 0x000207E8
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

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x000225F1 File Offset: 0x000207F1
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x000225F9 File Offset: 0x000207F9
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

		// Token: 0x040006AE RID: 1710
		private string modelField;

		// Token: 0x040006AF RID: 1711
		private string descriptionField;

		// Token: 0x040006B0 RID: 1712
		private ModelPerspective[] perspectivesField;
	}
}
