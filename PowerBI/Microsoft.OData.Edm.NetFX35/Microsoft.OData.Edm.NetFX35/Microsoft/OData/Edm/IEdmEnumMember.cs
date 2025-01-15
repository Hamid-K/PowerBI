using System;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000094 RID: 148
	public interface IEdmEnumMember : IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600027D RID: 637
		IEdmPrimitiveValue Value { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600027E RID: 638
		IEdmEnumType DeclaringType { get; }
	}
}
