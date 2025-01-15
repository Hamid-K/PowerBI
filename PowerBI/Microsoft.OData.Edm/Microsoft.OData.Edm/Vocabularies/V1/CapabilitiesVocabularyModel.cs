using System;

namespace Microsoft.OData.Edm.Vocabularies.V1
{
	// Token: 0x02000138 RID: 312
	internal class CapabilitiesVocabularyModel
	{
		// Token: 0x0400034F RID: 847
		public static readonly IEdmModel Instance = VocabularyModelProvider.CapabilitesModel;

		// Token: 0x04000350 RID: 848
		public static readonly IEdmTerm ChangeTrackingTerm = VocabularyModelProvider.CapabilitesModel.FindDeclaredTerm("Org.OData.Capabilities.V1.ChangeTracking");
	}
}
