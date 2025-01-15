using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200011A RID: 282
	public interface IInflector
	{
		// Token: 0x060005CF RID: 1487
		IEnumerable<InflectionResult> Inflect(string baseForm, PosTagKind tag);
	}
}
