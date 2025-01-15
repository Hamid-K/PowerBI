using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000098 RID: 152
	public interface IEdmEnumMember : IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600046E RID: 1134
		IEdmEnumMemberValue Value { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600046F RID: 1135
		IEdmEnumType DeclaringType { get; }
	}
}
