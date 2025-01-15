using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Edm.Vocabularies.V1
{
	// Token: 0x0200013A RID: 314
	public static class CoreVocabularyModel
	{
		// Token: 0x04000362 RID: 866
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmModel is immutable")]
		public static readonly IEdmModel Instance = VocabularyModelProvider.CoreModel;

		// Token: 0x04000363 RID: 867
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm ConcurrencyTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.OptimisticConcurrency");

		// Token: 0x04000364 RID: 868
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm DescriptionTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.Description");

		// Token: 0x04000365 RID: 869
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm LongDescriptionTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.LongDescription");

		// Token: 0x04000366 RID: 870
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm IsLanguageDependentTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.IsLanguageDependent");

		// Token: 0x04000367 RID: 871
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm RequiresTypeTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.RequiresType");

		// Token: 0x04000368 RID: 872
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm ResourcePathTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.ResourcePath");

		// Token: 0x04000369 RID: 873
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm DereferenceableIDsTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.DereferenceableIDs");

		// Token: 0x0400036A RID: 874
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm ConventionalIDsTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.ConventionalIDs");

		// Token: 0x0400036B RID: 875
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm ImmutableTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.Immutable");

		// Token: 0x0400036C RID: 876
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm ComputedTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.Computed");

		// Token: 0x0400036D RID: 877
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm OptionalParameterTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.OptionalParameter");

		// Token: 0x0400036E RID: 878
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm IsURLTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.IsURL");

		// Token: 0x0400036F RID: 879
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm AcceptableMediaTypesTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.AcceptableMediaTypes");

		// Token: 0x04000370 RID: 880
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm MediaTypeTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.MediaType");

		// Token: 0x04000371 RID: 881
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm IsMediaTypeTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.IsMediaType");

		// Token: 0x04000372 RID: 882
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm PermissionsTerm = VocabularyModelProvider.CoreModel.FindDeclaredTerm("Org.OData.Core.V1.Permissions");
	}
}
