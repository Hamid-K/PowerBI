using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E9 RID: 233
	public interface IEdmTerm : IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060006AE RID: 1710
		IEdmTypeReference Type { get; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060006AF RID: 1711
		string AppliesTo { get; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060006B0 RID: 1712
		string DefaultValue { get; }
	}
}
