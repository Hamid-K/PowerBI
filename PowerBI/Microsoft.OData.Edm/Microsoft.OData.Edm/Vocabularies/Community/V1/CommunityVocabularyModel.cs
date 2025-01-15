using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.OData.Edm.Vocabularies.Community.V1
{
	// Token: 0x02000135 RID: 309
	public static class CommunityVocabularyModel
	{
		// Token: 0x04000347 RID: 839
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmModel is immutable")]
		public static readonly IEdmModel Instance = VocabularyModelProvider.CommunityModel;

		// Token: 0x04000348 RID: 840
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm UrlEscapeFunctionTerm = VocabularyModelProvider.CommunityModel.FindDeclaredTerm("Org.OData.Community.V1.UrlEscapeFunction");
	}
}
