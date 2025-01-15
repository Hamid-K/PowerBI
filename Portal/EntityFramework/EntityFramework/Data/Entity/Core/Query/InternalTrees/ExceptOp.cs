using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003A2 RID: 930
	internal sealed class ExceptOp : SetOp
	{
		// Token: 0x06002D36 RID: 11574 RVA: 0x00091A1A File Offset: 0x0008FC1A
		private ExceptOp()
			: base(OpType.Except)
		{
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x00091A24 File Offset: 0x0008FC24
		internal ExceptOp(VarVec outputs, VarMap left, VarMap right)
			: base(OpType.Except, outputs, left, right)
		{
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x00091A31 File Offset: 0x0008FC31
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x00091A3B File Offset: 0x0008FC3B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F21 RID: 3873
		internal static readonly ExceptOp Pattern = new ExceptOp();
	}
}
