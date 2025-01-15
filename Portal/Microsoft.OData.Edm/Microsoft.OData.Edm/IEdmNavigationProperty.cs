using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000A0 RID: 160
	public interface IEdmNavigationProperty : IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003D7 RID: 983
		IEdmNavigationProperty Partner { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003D8 RID: 984
		EdmOnDeleteAction OnDelete { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003D9 RID: 985
		bool ContainsTarget { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003DA RID: 986
		IEdmReferentialConstraint ReferentialConstraint { get; }
	}
}
