using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003ED RID: 1005
	internal abstract class SortBaseOp : RelOp
	{
		// Token: 0x06002F29 RID: 12073 RVA: 0x000956D1 File Offset: 0x000938D1
		internal SortBaseOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000956DA File Offset: 0x000938DA
		internal SortBaseOp(OpType opType, List<SortKey> sortKeys)
			: this(opType)
		{
			this.m_keys = sortKeys;
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000956EA File Offset: 0x000938EA
		internal List<SortKey> Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x04000FE3 RID: 4067
		private readonly List<SortKey> m_keys;
	}
}
