using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x0200006F RID: 111
	public interface IEdmCollectionExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001B6 RID: 438
		IEdmTypeReference DeclaredType { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001B7 RID: 439
		IEnumerable<IEdmExpression> Elements { get; }
	}
}
