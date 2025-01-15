using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000A7 RID: 167
	public interface IEdmNavigationProperty : IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600048C RID: 1164
		IEdmNavigationProperty Partner { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600048D RID: 1165
		EdmOnDeleteAction OnDelete { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600048E RID: 1166
		bool ContainsTarget { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600048F RID: 1167
		IEdmReferentialConstraint ReferentialConstraint { get; }
	}
}
