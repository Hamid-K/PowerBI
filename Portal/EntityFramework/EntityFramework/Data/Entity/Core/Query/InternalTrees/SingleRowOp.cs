using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003E9 RID: 1001
	internal sealed class SingleRowOp : RelOp
	{
		// Token: 0x06002F12 RID: 12050 RVA: 0x000955D7 File Offset: 0x000937D7
		private SingleRowOp()
			: base(OpType.SingleRow)
		{
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000955E1 File Offset: 0x000937E1
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000955E4 File Offset: 0x000937E4
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000955EE File Offset: 0x000937EE
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FDB RID: 4059
		internal static readonly SingleRowOp Instance = new SingleRowOp();

		// Token: 0x04000FDC RID: 4060
		internal static readonly SingleRowOp Pattern = SingleRowOp.Instance;
	}
}
