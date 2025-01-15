using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000EB RID: 235
	public interface IEdmVocabularyAnnotation : IEdmElement
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060006B1 RID: 1713
		string Qualifier { get; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060006B2 RID: 1714
		IEdmTerm Term { get; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060006B3 RID: 1715
		IEdmVocabularyAnnotatable Target { get; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060006B4 RID: 1716
		IEdmExpression Value { get; }
	}
}
