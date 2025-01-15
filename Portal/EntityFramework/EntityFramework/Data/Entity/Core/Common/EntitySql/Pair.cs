using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000662 RID: 1634
	internal sealed class Pair<L, R>
	{
		// Token: 0x06004E0D RID: 19981 RVA: 0x00118801 File Offset: 0x00116A01
		internal Pair(L left, R right)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x00118817 File Offset: 0x00116A17
		internal KeyValuePair<L, R> GetKVP()
		{
			return new KeyValuePair<L, R>(this.Left, this.Right);
		}

		// Token: 0x04001C53 RID: 7251
		internal L Left;

		// Token: 0x04001C54 RID: 7252
		internal R Right;
	}
}
