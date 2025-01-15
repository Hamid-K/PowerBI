using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000B0 RID: 176
	internal sealed class LimitWindowExpansionValue
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00007878 File Offset: 0x00005A78
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00007880 File Offset: 0x00005A80
		public List<Expression> Values { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00007889 File Offset: 0x00005A89
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x00007891 File Offset: 0x00005A91
		public WindowKind WindowKind { get; set; }
	}
}
