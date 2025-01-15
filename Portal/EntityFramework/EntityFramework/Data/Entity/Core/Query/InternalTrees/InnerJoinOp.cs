using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003AE RID: 942
	internal sealed class InnerJoinOp : JoinBaseOp
	{
		// Token: 0x06002D84 RID: 11652 RVA: 0x00091F0B File Offset: 0x0009010B
		private InnerJoinOp()
			: base(OpType.InnerJoin)
		{
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x00091F15 File Offset: 0x00090115
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x00091F1F File Offset: 0x0009011F
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F3A RID: 3898
		internal static readonly InnerJoinOp Instance = new InnerJoinOp();

		// Token: 0x04000F3B RID: 3899
		internal static readonly InnerJoinOp Pattern = InnerJoinOp.Instance;
	}
}
