using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000100 RID: 256
	public interface IEdmCollectionExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000733 RID: 1843
		IEdmTypeReference DeclaredType { get; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000734 RID: 1844
		IEnumerable<IEdmExpression> Elements { get; }
	}
}
