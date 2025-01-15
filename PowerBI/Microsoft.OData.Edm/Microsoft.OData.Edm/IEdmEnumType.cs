using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000075 RID: 117
	public interface IEdmEnumType : IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600025B RID: 603
		IEdmPrimitiveType UnderlyingType { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600025C RID: 604
		IEnumerable<IEdmEnumMember> Members { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600025D RID: 605
		bool IsFlags { get; }
	}
}
