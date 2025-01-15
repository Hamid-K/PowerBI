using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003F6 RID: 1014
	internal sealed class UnionAllOp : SetOp
	{
		// Token: 0x06002F56 RID: 12118 RVA: 0x00095BB5 File Offset: 0x00093DB5
		private UnionAllOp()
			: base(OpType.UnionAll)
		{
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x00095BBF File Offset: 0x00093DBF
		internal UnionAllOp(VarVec outputs, VarMap left, VarMap right, Var branchDiscriminator)
			: base(OpType.UnionAll, outputs, left, right)
		{
			this.m_branchDiscriminator = branchDiscriminator;
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x00095BD4 File Offset: 0x00093DD4
		internal Var BranchDiscriminator
		{
			get
			{
				return this.m_branchDiscriminator;
			}
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x00095BDC File Offset: 0x00093DDC
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x00095BE6 File Offset: 0x00093DE6
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FF9 RID: 4089
		private readonly Var m_branchDiscriminator;

		// Token: 0x04000FFA RID: 4090
		internal static readonly UnionAllOp Pattern = new UnionAllOp();
	}
}
