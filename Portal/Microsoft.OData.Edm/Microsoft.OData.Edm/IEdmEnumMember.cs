using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000074 RID: 116
	public interface IEdmEnumMember : IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000259 RID: 601
		IEdmEnumMemberValue Value { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600025A RID: 602
		IEdmEnumType DeclaringType { get; }
	}
}
