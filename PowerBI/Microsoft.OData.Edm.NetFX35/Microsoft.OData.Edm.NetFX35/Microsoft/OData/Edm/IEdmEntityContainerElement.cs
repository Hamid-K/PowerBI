using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000062 RID: 98
	public interface IEdmEntityContainerElement : IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600017C RID: 380
		EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600017D RID: 381
		IEdmEntityContainer Container { get; }
	}
}
