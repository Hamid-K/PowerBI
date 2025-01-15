using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x02000058 RID: 88
	public interface IEdmPathExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000142 RID: 322
		IEnumerable<string> Path { get; }
	}
}
