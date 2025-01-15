using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public class AceStruct
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00002364 File Offset: 0x00000564
		public AceStruct(string name)
		{
			this.PrincipalName = name;
			this.CatalogOperations = new CatalogOperationsCollection();
			this.ReportOperations = new ReportOperationsCollection();
			this.FolderOperations = new FolderOperationsCollection();
			this.ResourceOperations = new ResourceOperationsCollection();
			this.DatasourceOperations = new DatasourceOperationsCollection();
			this.ModelOperations = new ModelOperationsCollection();
			this.ModelItemOperations = new ModelItemOperationsCollection();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000023CC File Offset: 0x000005CC
		public AceStruct(AceStruct other)
		{
			this.PrincipalName = other.PrincipalName;
			this.CatalogOperations = other.CatalogOperations;
			this.ReportOperations = other.ReportOperations;
			this.FolderOperations = other.FolderOperations;
			this.ResourceOperations = other.ResourceOperations;
			this.DatasourceOperations = other.DatasourceOperations;
			this.ModelOperations = other.ModelOperations;
			this.ModelItemOperations = other.ModelItemOperations;
		}

		// Token: 0x040001EA RID: 490
		public string PrincipalName;

		// Token: 0x040001EB RID: 491
		public CatalogOperationsCollection CatalogOperations;

		// Token: 0x040001EC RID: 492
		public ReportOperationsCollection ReportOperations;

		// Token: 0x040001ED RID: 493
		public FolderOperationsCollection FolderOperations;

		// Token: 0x040001EE RID: 494
		public ResourceOperationsCollection ResourceOperations;

		// Token: 0x040001EF RID: 495
		public DatasourceOperationsCollection DatasourceOperations;

		// Token: 0x040001F0 RID: 496
		public ModelOperationsCollection ModelOperations;

		// Token: 0x040001F1 RID: 497
		public ModelItemOperationsCollection ModelItemOperations;
	}
}
