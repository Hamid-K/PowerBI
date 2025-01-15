using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003FE RID: 1022
	internal sealed class VarRefOp : ScalarOp
	{
		// Token: 0x06002F81 RID: 12161 RVA: 0x00095F56 File Offset: 0x00094156
		internal VarRefOp(Var v)
			: base(OpType.VarRef, v.Type)
		{
			this.m_var = v;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x00095F6C File Offset: 0x0009416C
		private VarRefOp()
			: base(OpType.VarRef)
		{
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x00095F75 File Offset: 0x00094175
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x00095F78 File Offset: 0x00094178
		internal override bool IsEquivalent(Op other)
		{
			VarRefOp varRefOp = other as VarRefOp;
			return varRefOp != null && varRefOp.Var.Equals(this.Var);
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x00095FA2 File Offset: 0x000941A2
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x00095FAA File Offset: 0x000941AA
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x00095FB4 File Offset: 0x000941B4
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001006 RID: 4102
		private readonly Var m_var;

		// Token: 0x04001007 RID: 4103
		internal static readonly VarRefOp Pattern = new VarRefOp();
	}
}
