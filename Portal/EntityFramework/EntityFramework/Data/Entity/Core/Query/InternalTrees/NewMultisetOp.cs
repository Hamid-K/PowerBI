using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003BE RID: 958
	internal sealed class NewMultisetOp : ScalarOp
	{
		// Token: 0x06002DDA RID: 11738 RVA: 0x0009247A File Offset: 0x0009067A
		internal NewMultisetOp(TypeUsage type)
			: base(OpType.NewMultiset, type)
		{
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x00092485 File Offset: 0x00090685
		private NewMultisetOp()
			: base(OpType.NewMultiset)
		{
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x0009248F File Offset: 0x0009068F
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x00092499 File Offset: 0x00090699
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F55 RID: 3925
		internal static readonly NewMultisetOp Pattern = new NewMultisetOp();
	}
}
