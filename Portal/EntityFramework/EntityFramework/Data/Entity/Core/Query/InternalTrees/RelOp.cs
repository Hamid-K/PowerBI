using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003D4 RID: 980
	internal abstract class RelOp : Op
	{
		// Token: 0x06002EBA RID: 11962 RVA: 0x00094D6A File Offset: 0x00092F6A
		internal RelOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x00094D73 File Offset: 0x00092F73
		internal override bool IsRelOp
		{
			get
			{
				return true;
			}
		}
	}
}
