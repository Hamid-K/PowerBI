using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Converters
{
	// Token: 0x02000161 RID: 353
	internal interface IPropertyConverter
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600162C RID: 5676
		ObjectType ObjectType { get; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600162D RID: 5677
		string PropertyName { get; }

		// Token: 0x0600162E RID: 5678
		TmdlProperty GetProperty(MetadataObject metadataObject);

		// Token: 0x0600162F RID: 5679
		void SetProperty(MetadataObject instance, TmdlProperty property);
	}
}
