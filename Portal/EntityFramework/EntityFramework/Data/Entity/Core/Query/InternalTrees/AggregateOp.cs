using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200037B RID: 891
	internal sealed class AggregateOp : ScalarOp
	{
		// Token: 0x06002B06 RID: 11014 RVA: 0x0008D5A3 File Offset: 0x0008B7A3
		internal AggregateOp(EdmFunction aggFunc, bool distinctAgg)
			: base(OpType.Aggregate, aggFunc.ReturnParameter.TypeUsage)
		{
			this.m_aggFunc = aggFunc;
			this.m_distinctAgg = distinctAgg;
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x0008D5C6 File Offset: 0x0008B7C6
		private AggregateOp()
			: base(OpType.Aggregate)
		{
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x0008D5D0 File Offset: 0x0008B7D0
		internal EdmFunction AggFunc
		{
			get
			{
				return this.m_aggFunc;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x0008D5D8 File Offset: 0x0008B7D8
		internal bool IsDistinctAggregate
		{
			get
			{
				return this.m_distinctAgg;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x0008D5E0 File Offset: 0x0008B7E0
		internal override bool IsAggregateOp
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x0008D5E3 File Offset: 0x0008B7E3
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x0008D5ED File Offset: 0x0008B7ED
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000ED8 RID: 3800
		private readonly EdmFunction m_aggFunc;

		// Token: 0x04000ED9 RID: 3801
		private readonly bool m_distinctAgg;

		// Token: 0x04000EDA RID: 3802
		internal static readonly AggregateOp Pattern = new AggregateOp();
	}
}
