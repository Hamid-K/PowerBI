using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003A6 RID: 934
	internal sealed class FilterOp : RelOp
	{
		// Token: 0x06002D55 RID: 11605 RVA: 0x00091CF0 File Offset: 0x0008FEF0
		private FilterOp()
			: base(OpType.Filter)
		{
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x00091CFA File Offset: 0x0008FEFA
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x00091CFD File Offset: 0x0008FEFD
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x00091D07 File Offset: 0x0008FF07
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F2D RID: 3885
		internal static readonly FilterOp Instance = new FilterOp();

		// Token: 0x04000F2E RID: 3886
		internal static readonly FilterOp Pattern = FilterOp.Instance;
	}
}
