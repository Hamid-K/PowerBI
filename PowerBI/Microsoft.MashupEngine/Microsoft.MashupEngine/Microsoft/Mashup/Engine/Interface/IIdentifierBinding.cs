using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200003D RID: 61
	public interface IIdentifierBinding
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000137 RID: 311
		DocumentRange Definition { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000138 RID: 312
		IList<DocumentRange> References { get; }
	}
}
