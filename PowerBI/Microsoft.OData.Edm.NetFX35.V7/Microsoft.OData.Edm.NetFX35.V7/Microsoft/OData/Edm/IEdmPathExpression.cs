using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AF RID: 175
	public interface IEdmPathExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004A3 RID: 1187
		IEnumerable<string> PathSegments { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004A4 RID: 1188
		string Path { get; }
	}
}
