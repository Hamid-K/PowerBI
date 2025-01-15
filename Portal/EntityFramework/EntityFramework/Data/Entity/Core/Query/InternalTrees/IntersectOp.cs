using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B0 RID: 944
	internal sealed class IntersectOp : SetOp
	{
		// Token: 0x06002D8D RID: 11661 RVA: 0x00091F73 File Offset: 0x00090173
		private IntersectOp()
			: base(OpType.Intersect)
		{
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x00091F7D File Offset: 0x0009017D
		internal IntersectOp(VarVec outputs, VarMap left, VarMap right)
			: base(OpType.Intersect, outputs, left, right)
		{
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x00091F8A File Offset: 0x0009018A
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x00091F94 File Offset: 0x00090194
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F3D RID: 3901
		internal static readonly IntersectOp Pattern = new IntersectOp();
	}
}
