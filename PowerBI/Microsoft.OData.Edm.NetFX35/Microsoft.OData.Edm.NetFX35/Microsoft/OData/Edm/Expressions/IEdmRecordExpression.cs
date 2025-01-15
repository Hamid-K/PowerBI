using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x020000B6 RID: 182
	public interface IEdmRecordExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000318 RID: 792
		IEdmStructuredTypeReference DeclaredType { get; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000319 RID: 793
		IEnumerable<IEdmPropertyConstructor> Properties { get; }
	}
}
