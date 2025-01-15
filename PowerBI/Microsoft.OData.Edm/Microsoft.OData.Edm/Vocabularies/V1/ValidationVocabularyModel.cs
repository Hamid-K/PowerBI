using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Edm.Vocabularies.V1
{
	// Token: 0x0200013B RID: 315
	public static class ValidationVocabularyModel
	{
		// Token: 0x04000373 RID: 883
		public static readonly string Namespace = "Org.OData.Validation.V1";

		// Token: 0x04000374 RID: 884
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmModel is immutable")]
		public static readonly IEdmModel Instance = VocabularyModelProvider.ValidationModel;

		// Token: 0x04000375 RID: 885
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm DerivedTypeConstraintTerm = VocabularyModelProvider.ValidationModel.FindDeclaredTerm(ValidationVocabularyModel.Namespace + ".DerivedTypeConstraint");
	}
}
