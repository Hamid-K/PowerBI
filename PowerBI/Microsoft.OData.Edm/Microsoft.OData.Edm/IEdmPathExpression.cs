using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200002E RID: 46
	public interface IEdmPathExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000BE RID: 190
		IEnumerable<string> PathSegments { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000BF RID: 191
		string Path { get; }
	}
}
