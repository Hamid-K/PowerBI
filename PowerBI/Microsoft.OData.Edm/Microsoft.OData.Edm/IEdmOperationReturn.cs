using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200009A RID: 154
	public interface IEdmOperationReturn : IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003C6 RID: 966
		IEdmTypeReference Type { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003C7 RID: 967
		IEdmOperation DeclaringOperation { get; }
	}
}
