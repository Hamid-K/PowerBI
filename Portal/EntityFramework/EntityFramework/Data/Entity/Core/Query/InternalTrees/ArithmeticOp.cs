using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200037E RID: 894
	internal sealed class ArithmeticOp : ScalarOp
	{
		// Token: 0x06002B12 RID: 11026 RVA: 0x0008D61B File Offset: 0x0008B81B
		internal ArithmeticOp(OpType opType, TypeUsage type)
			: base(opType, type)
		{
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0008D625 File Offset: 0x0008B825
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x0008D62F File Offset: 0x0008B82F
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}
	}
}
