using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000074 RID: 116
	internal sealed class RestartToken : List<Candidate<ScalarValue>>
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00006311 File Offset: 0x00004511
		internal RestartToken()
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00006319 File Offset: 0x00004519
		internal RestartToken(IEnumerable<Candidate<ScalarValue>> values)
			: base(values)
		{
		}
	}
}
