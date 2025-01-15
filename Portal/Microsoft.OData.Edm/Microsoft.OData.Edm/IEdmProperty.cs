using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000A3 RID: 163
	public interface IEdmProperty : IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003DB RID: 987
		EdmPropertyKind PropertyKind { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003DC RID: 988
		IEdmTypeReference Type { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003DD RID: 989
		IEdmStructuredType DeclaringType { get; }
	}
}
