using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B4 RID: 180
	public interface IEdmProperty : IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004A6 RID: 1190
		EdmPropertyKind PropertyKind { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004A7 RID: 1191
		IEdmTypeReference Type { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004A8 RID: 1192
		IEdmStructuredType DeclaringType { get; }
	}
}
