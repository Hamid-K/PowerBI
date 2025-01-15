using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B2 RID: 946
	internal abstract class JoinBaseOp : RelOp
	{
		// Token: 0x06002D9A RID: 11674 RVA: 0x00092000 File Offset: 0x00090200
		internal JoinBaseOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002D9B RID: 11675 RVA: 0x00092009 File Offset: 0x00090209
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}
	}
}
