using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E1 RID: 225
	public interface IEdmTerm : IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006CE RID: 1742
		IEdmTypeReference Type { get; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006CF RID: 1743
		string AppliesTo { get; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006D0 RID: 1744
		string DefaultValue { get; }
	}
}
