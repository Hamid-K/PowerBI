using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B5 RID: 949
	internal sealed class LeftOuterJoinOp : JoinBaseOp
	{
		// Token: 0x06002DA9 RID: 11689 RVA: 0x00092184 File Offset: 0x00090384
		private LeftOuterJoinOp()
			: base(OpType.LeftOuterJoin)
		{
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x0009218E File Offset: 0x0009038E
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x00092198 File Offset: 0x00090398
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F45 RID: 3909
		internal static readonly LeftOuterJoinOp Instance = new LeftOuterJoinOp();

		// Token: 0x04000F46 RID: 3910
		internal static readonly LeftOuterJoinOp Pattern = LeftOuterJoinOp.Instance;
	}
}
