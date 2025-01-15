using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F5 RID: 245
	public interface IEdmApplyExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600075C RID: 1884
		IEdmFunction AppliedFunction { get; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600075D RID: 1885
		IEnumerable<IEdmExpression> Arguments { get; }
	}
}
