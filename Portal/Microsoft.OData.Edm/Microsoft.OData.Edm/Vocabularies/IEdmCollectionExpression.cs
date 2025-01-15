using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F9 RID: 249
	public interface IEdmCollectionExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000760 RID: 1888
		IEdmTypeReference DeclaredType { get; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000761 RID: 1889
		IEnumerable<IEdmExpression> Elements { get; }
	}
}
