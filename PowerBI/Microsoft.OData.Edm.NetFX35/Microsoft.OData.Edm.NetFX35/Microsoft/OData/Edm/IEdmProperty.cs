using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200011E RID: 286
	public interface IEdmProperty : IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060005AE RID: 1454
		EdmPropertyKind PropertyKind { get; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060005AF RID: 1455
		IEdmTypeReference Type { get; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060005B0 RID: 1456
		IEdmStructuredType DeclaringType { get; }
	}
}
