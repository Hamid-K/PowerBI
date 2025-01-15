using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003AB RID: 939
	internal abstract class GroupByBaseOp : RelOp
	{
		// Token: 0x06002D71 RID: 11633 RVA: 0x00091E46 File Offset: 0x00090046
		protected GroupByBaseOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x00091E4F File Offset: 0x0009004F
		internal GroupByBaseOp(OpType opType, VarVec keys, VarVec outputs)
			: this(opType)
		{
			this.m_keys = keys;
			this.m_outputs = outputs;
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x00091E66 File Offset: 0x00090066
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x00091E6E File Offset: 0x0009006E
		internal VarVec Outputs
		{
			get
			{
				return this.m_outputs;
			}
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x00091E76 File Offset: 0x00090076
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x00091E80 File Offset: 0x00090080
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F35 RID: 3893
		private readonly VarVec m_keys;

		// Token: 0x04000F36 RID: 3894
		private readonly VarVec m_outputs;
	}
}
