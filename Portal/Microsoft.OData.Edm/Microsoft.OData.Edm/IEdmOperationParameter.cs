using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000099 RID: 153
	public interface IEdmOperationParameter : IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003C4 RID: 964
		IEdmTypeReference Type { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003C5 RID: 965
		IEdmOperation DeclaringOperation { get; }
	}
}
