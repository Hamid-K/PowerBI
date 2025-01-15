using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200011E RID: 286
	public interface IPosTaggerCache
	{
		// Token: 0x060005D6 RID: 1494
		bool TryGet(string value, out IReadOnlyList<PosTag> posTags);

		// Token: 0x060005D7 RID: 1495
		void Add(string value, IReadOnlyList<PosTag> posTags);
	}
}
