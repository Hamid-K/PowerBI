using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000397 RID: 919
	internal sealed class CrossApplyOp : ApplyBaseOp
	{
		// Token: 0x06002CD6 RID: 11478 RVA: 0x0009018A File Offset: 0x0008E38A
		private CrossApplyOp()
			: base(OpType.CrossApply)
		{
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x00090194 File Offset: 0x0008E394
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x0009019E File Offset: 0x0008E39E
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F0F RID: 3855
		internal static readonly CrossApplyOp Instance = new CrossApplyOp();

		// Token: 0x04000F10 RID: 3856
		internal static readonly CrossApplyOp Pattern = CrossApplyOp.Instance;
	}
}
