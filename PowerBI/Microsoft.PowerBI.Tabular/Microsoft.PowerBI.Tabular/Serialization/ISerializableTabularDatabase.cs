using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000164 RID: 356
	internal interface ISerializableTabularDatabase
	{
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001640 RID: 5696
		// (set) Token: 0x06001641 RID: 5697
		Model Model { get; set; }

		// Token: 0x06001642 RID: 5698
		void LoadMetadata(SerializationActivityContext context, IMetadataReader reader);

		// Token: 0x06001643 RID: 5699
		void SaveMetadata(SerializationActivityContext context, IMetadataWriter writer);
	}
}
