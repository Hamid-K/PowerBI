using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003CA RID: 970
	internal sealed class OuterApplyOp : ApplyBaseOp
	{
		// Token: 0x06002E89 RID: 11913 RVA: 0x00094A9A File Offset: 0x00092C9A
		private OuterApplyOp()
			: base(OpType.OuterApply)
		{
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x00094AA4 File Offset: 0x00092CA4
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x00094AAE File Offset: 0x00092CAE
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FB2 RID: 4018
		internal static readonly OuterApplyOp Instance = new OuterApplyOp();

		// Token: 0x04000FB3 RID: 4019
		internal static readonly OuterApplyOp Pattern = OuterApplyOp.Instance;
	}
}
