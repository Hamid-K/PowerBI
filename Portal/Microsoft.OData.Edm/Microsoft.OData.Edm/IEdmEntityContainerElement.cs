using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000095 RID: 149
	public interface IEdmEntityContainerElement : IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003BF RID: 959
		EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003C0 RID: 960
		IEdmEntityContainer Container { get; }
	}
}
