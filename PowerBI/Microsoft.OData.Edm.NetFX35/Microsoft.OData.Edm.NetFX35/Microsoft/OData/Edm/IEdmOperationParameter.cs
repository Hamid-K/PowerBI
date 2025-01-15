using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001AD RID: 429
	public interface IEdmOperationParameter : IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060008D7 RID: 2263
		IEdmTypeReference Type { get; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060008D8 RID: 2264
		IEdmOperation DeclaringOperation { get; }
	}
}
