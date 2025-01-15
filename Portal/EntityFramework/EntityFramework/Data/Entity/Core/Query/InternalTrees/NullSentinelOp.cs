using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003C5 RID: 965
	internal sealed class NullSentinelOp : ConstantBaseOp
	{
		// Token: 0x06002E24 RID: 11812 RVA: 0x00093C00 File Offset: 0x00091E00
		internal NullSentinelOp(TypeUsage type, object value)
			: base(OpType.NullSentinel, type, value)
		{
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x00093C0B File Offset: 0x00091E0B
		private NullSentinelOp()
			: base(OpType.NullSentinel)
		{
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x00093C14 File Offset: 0x00091E14
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x00093C1E File Offset: 0x00091E1E
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F60 RID: 3936
		internal static readonly NullSentinelOp Pattern = new NullSentinelOp();
	}
}
