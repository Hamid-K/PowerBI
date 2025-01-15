using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000142 RID: 322
	public interface IEdmNavigationProperty : IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600062F RID: 1583
		IEdmNavigationProperty Partner { get; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000630 RID: 1584
		EdmOnDeleteAction OnDelete { get; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000631 RID: 1585
		bool ContainsTarget { get; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000632 RID: 1586
		IEdmReferentialConstraint ReferentialConstraint { get; }
	}
}
