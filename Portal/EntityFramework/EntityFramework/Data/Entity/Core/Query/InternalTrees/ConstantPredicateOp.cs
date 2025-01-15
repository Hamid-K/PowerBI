using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000395 RID: 917
	internal sealed class ConstantPredicateOp : ConstantBaseOp
	{
		// Token: 0x06002CC6 RID: 11462 RVA: 0x000900E1 File Offset: 0x0008E2E1
		internal ConstantPredicateOp(TypeUsage type, bool value)
			: base(OpType.ConstantPredicate, type, value)
		{
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000900F1 File Offset: 0x0008E2F1
		private ConstantPredicateOp()
			: base(OpType.ConstantPredicate)
		{
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x000900FA File Offset: 0x0008E2FA
		internal new bool Value
		{
			get
			{
				return (bool)base.Value;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x00090107 File Offset: 0x0008E307
		internal bool IsTrue
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x0009010F File Offset: 0x0008E30F
		internal bool IsFalse
		{
			get
			{
				return !this.Value;
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x0009011A File Offset: 0x0008E31A
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x00090124 File Offset: 0x0008E324
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F0C RID: 3852
		internal static readonly ConstantPredicateOp Pattern = new ConstantPredicateOp();
	}
}
