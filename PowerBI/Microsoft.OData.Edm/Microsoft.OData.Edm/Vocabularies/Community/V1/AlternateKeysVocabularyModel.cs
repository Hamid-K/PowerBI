using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.OData.Edm.Vocabularies.Community.V1
{
	// Token: 0x02000133 RID: 307
	public static class AlternateKeysVocabularyModel
	{
		// Token: 0x04000342 RID: 834
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmModel is immutable")]
		public static readonly IEdmModel Instance = VocabularyModelProvider.AlternateKeyModel;

		// Token: 0x04000343 RID: 835
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm AlternateKeysTerm = VocabularyModelProvider.AlternateKeyModel.FindDeclaredTerm("OData.Community.Keys.V1.AlternateKeys");

		// Token: 0x04000344 RID: 836
		internal static readonly IEdmComplexType AlternateKeyType = VocabularyModelProvider.AlternateKeyModel.FindDeclaredType("OData.Community.Keys.V1.AlternateKey") as IEdmComplexType;

		// Token: 0x04000345 RID: 837
		internal static readonly IEdmComplexType PropertyRefType = VocabularyModelProvider.AlternateKeyModel.FindDeclaredType("OData.Community.Keys.V1.PropertyRef") as IEdmComplexType;
	}
}
