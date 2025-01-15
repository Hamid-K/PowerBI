using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000091 RID: 145
	public interface IEdmEntityContainerElement : IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000468 RID: 1128
		EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000469 RID: 1129
		IEdmEntityContainer Container { get; }
	}
}
