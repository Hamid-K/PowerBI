using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004E RID: 78
	public interface IEdmEnumType : IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600011A RID: 282
		IEdmPrimitiveType UnderlyingType { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600011B RID: 283
		IEnumerable<IEdmEnumMember> Members { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600011C RID: 284
		bool IsFlags { get; }
	}
}
