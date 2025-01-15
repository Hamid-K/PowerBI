using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200037D RID: 893
	internal abstract class ApplyBaseOp : RelOp
	{
		// Token: 0x06002B10 RID: 11024 RVA: 0x0008D60F File Offset: 0x0008B80F
		internal ApplyBaseOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x0008D618 File Offset: 0x0008B818
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}
	}
}
