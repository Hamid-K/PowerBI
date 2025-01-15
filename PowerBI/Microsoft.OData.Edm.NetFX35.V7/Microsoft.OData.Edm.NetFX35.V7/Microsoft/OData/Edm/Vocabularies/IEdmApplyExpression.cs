using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000FC RID: 252
	public interface IEdmApplyExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600072F RID: 1839
		IEdmFunction AppliedFunction { get; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000730 RID: 1840
		IEnumerable<IEdmExpression> Arguments { get; }
	}
}
