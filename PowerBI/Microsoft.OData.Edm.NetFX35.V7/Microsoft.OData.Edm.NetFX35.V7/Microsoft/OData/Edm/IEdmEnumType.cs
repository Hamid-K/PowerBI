using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200009A RID: 154
	public interface IEdmEnumType : IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000471 RID: 1137
		IEdmPrimitiveType UnderlyingType { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000472 RID: 1138
		IEnumerable<IEdmEnumMember> Members { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000473 RID: 1139
		bool IsFlags { get; }
	}
}
