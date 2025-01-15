using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000394 RID: 916
	internal sealed class ConstantOp : ConstantBaseOp
	{
		// Token: 0x06002CC1 RID: 11457 RVA: 0x000900AD File Offset: 0x0008E2AD
		internal ConstantOp(TypeUsage type, object value)
			: base(OpType.Constant, type, value)
		{
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000900B8 File Offset: 0x0008E2B8
		private ConstantOp()
			: base(OpType.Constant)
		{
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000900C1 File Offset: 0x0008E2C1
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000900CB File Offset: 0x0008E2CB
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F0B RID: 3851
		internal static readonly ConstantOp Pattern = new ConstantOp();
	}
}
