using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003A7 RID: 935
	internal sealed class FullOuterJoinOp : JoinBaseOp
	{
		// Token: 0x06002D5A RID: 11610 RVA: 0x00091D27 File Offset: 0x0008FF27
		private FullOuterJoinOp()
			: base(OpType.FullOuterJoin)
		{
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x00091D31 File Offset: 0x0008FF31
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x00091D3B File Offset: 0x0008FF3B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F2F RID: 3887
		internal static readonly FullOuterJoinOp Instance = new FullOuterJoinOp();

		// Token: 0x04000F30 RID: 3888
		internal static readonly FullOuterJoinOp Pattern = FullOuterJoinOp.Instance;
	}
}
