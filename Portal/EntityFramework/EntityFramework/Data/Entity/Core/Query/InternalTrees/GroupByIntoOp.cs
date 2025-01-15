using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003AC RID: 940
	internal sealed class GroupByIntoOp : GroupByBaseOp
	{
		// Token: 0x06002D77 RID: 11639 RVA: 0x00091E8A File Offset: 0x0009008A
		private GroupByIntoOp()
			: base(OpType.GroupByInto)
		{
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x00091E94 File Offset: 0x00090094
		internal GroupByIntoOp(VarVec keys, VarVec inputs, VarVec outputs)
			: base(OpType.GroupByInto, keys, outputs)
		{
			this.m_inputs = inputs;
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x00091EA7 File Offset: 0x000900A7
		internal VarVec Inputs
		{
			get
			{
				return this.m_inputs;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x00091EAF File Offset: 0x000900AF
		internal override int Arity
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x00091EB2 File Offset: 0x000900B2
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x00091EBC File Offset: 0x000900BC
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F37 RID: 3895
		private readonly VarVec m_inputs;

		// Token: 0x04000F38 RID: 3896
		internal static readonly GroupByIntoOp Pattern = new GroupByIntoOp();
	}
}
