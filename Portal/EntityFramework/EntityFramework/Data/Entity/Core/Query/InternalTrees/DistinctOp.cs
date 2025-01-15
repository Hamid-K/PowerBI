using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200039D RID: 925
	internal sealed class DistinctOp : RelOp
	{
		// Token: 0x06002CF5 RID: 11509 RVA: 0x0009038D File Offset: 0x0008E58D
		private DistinctOp()
			: base(OpType.Distinct)
		{
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x00090397 File Offset: 0x0008E597
		internal DistinctOp(VarVec keyVars)
			: this()
		{
			this.m_keys = keyVars;
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x000903A6 File Offset: 0x0008E5A6
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x000903A9 File Offset: 0x0008E5A9
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000903B1 File Offset: 0x0008E5B1
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000903BB File Offset: 0x0008E5BB
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F1A RID: 3866
		private readonly VarVec m_keys;

		// Token: 0x04000F1B RID: 3867
		internal static readonly DistinctOp Pattern = new DistinctOp();
	}
}
