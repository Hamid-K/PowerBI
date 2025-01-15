using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AD RID: 173
	public interface IEdmOperationParameter : IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004A0 RID: 1184
		IEdmTypeReference Type { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004A1 RID: 1185
		IEdmOperation DeclaringOperation { get; }
	}
}
