using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E3 RID: 227
	public interface IEdmVocabularyAnnotation : IEdmElement
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006D1 RID: 1745
		string Qualifier { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060006D2 RID: 1746
		IEdmTerm Term { get; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060006D3 RID: 1747
		IEdmVocabularyAnnotatable Target { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060006D4 RID: 1748
		IEdmExpression Value { get; }
	}
}
