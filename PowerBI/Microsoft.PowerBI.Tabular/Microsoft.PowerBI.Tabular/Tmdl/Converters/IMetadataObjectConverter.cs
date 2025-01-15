using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Converters
{
	// Token: 0x02000160 RID: 352
	internal interface IMetadataObjectConverter
	{
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001627 RID: 5671
		ObjectType ObjectType { get; }

		// Token: 0x06001628 RID: 5672
		MetadataObject FromTMDL(TmdlObject tmdlObject);

		// Token: 0x06001629 RID: 5673
		MetadataObject FromTMDL(TmdlObject tmdlObject, MetadataObject parent, MetadataObject existingInstance);

		// Token: 0x0600162A RID: 5674
		TmdlObject ToTMDL(MetadataObject metadataObject);

		// Token: 0x0600162B RID: 5675
		IPropertyConverter GetPropertyConverter(string propertyName);
	}
}
