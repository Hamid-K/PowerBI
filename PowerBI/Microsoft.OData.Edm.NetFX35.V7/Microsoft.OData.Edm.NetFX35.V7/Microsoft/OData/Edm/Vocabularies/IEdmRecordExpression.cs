using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200010F RID: 271
	public interface IEdmRecordExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600073F RID: 1855
		IEdmStructuredTypeReference DeclaredType { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000740 RID: 1856
		IEnumerable<IEdmPropertyConstructor> Properties { get; }
	}
}
