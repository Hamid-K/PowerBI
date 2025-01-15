using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003AD RID: 941
	internal sealed class GroupByOp : GroupByBaseOp
	{
		// Token: 0x06002D7E RID: 11646 RVA: 0x00091ED2 File Offset: 0x000900D2
		private GroupByOp()
			: base(OpType.GroupBy)
		{
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x00091EDC File Offset: 0x000900DC
		internal GroupByOp(VarVec keys, VarVec outputs)
			: base(OpType.GroupBy, keys, outputs)
		{
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x00091EE8 File Offset: 0x000900E8
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x00091EEB File Offset: 0x000900EB
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x00091EF5 File Offset: 0x000900F5
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F39 RID: 3897
		internal static readonly GroupByOp Pattern = new GroupByOp();
	}
}
