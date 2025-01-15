using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x0200006B RID: 107
	public interface IEdmApplyExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600019B RID: 411
		IEdmExpression AppliedOperation { get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600019C RID: 412
		IEnumerable<IEdmExpression> Arguments { get; }
	}
}
