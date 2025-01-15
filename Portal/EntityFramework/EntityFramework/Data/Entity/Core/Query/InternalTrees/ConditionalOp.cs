using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000392 RID: 914
	internal sealed class ConditionalOp : ScalarOp
	{
		// Token: 0x06002CB7 RID: 11447 RVA: 0x0008FFC6 File Offset: 0x0008E1C6
		internal ConditionalOp(OpType optype, TypeUsage type)
			: base(optype, type)
		{
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x0008FFD0 File Offset: 0x0008E1D0
		private ConditionalOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x0008FFD9 File Offset: 0x0008E1D9
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x0008FFE3 File Offset: 0x0008E1E3
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F05 RID: 3845
		internal static readonly ConditionalOp PatternAnd = new ConditionalOp(OpType.And);

		// Token: 0x04000F06 RID: 3846
		internal static readonly ConditionalOp PatternOr = new ConditionalOp(OpType.Or);

		// Token: 0x04000F07 RID: 3847
		internal static readonly ConditionalOp PatternIn = new ConditionalOp(OpType.In);

		// Token: 0x04000F08 RID: 3848
		internal static readonly ConditionalOp PatternNot = new ConditionalOp(OpType.Not);

		// Token: 0x04000F09 RID: 3849
		internal static readonly ConditionalOp PatternIsNull = new ConditionalOp(OpType.IsNull);
	}
}
