using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000396 RID: 918
	internal sealed class ConstrainedSortOp : SortBaseOp
	{
		// Token: 0x06002CCE RID: 11470 RVA: 0x0009013A File Offset: 0x0008E33A
		private ConstrainedSortOp()
			: base(OpType.ConstrainedSort)
		{
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x00090144 File Offset: 0x0008E344
		internal ConstrainedSortOp(List<SortKey> sortKeys, bool withTies)
			: base(OpType.ConstrainedSort, sortKeys)
		{
			this.WithTies = withTies;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x00090156 File Offset: 0x0008E356
		// (set) Token: 0x06002CD1 RID: 11473 RVA: 0x0009015E File Offset: 0x0008E35E
		internal bool WithTies { get; set; }

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x00090167 File Offset: 0x0008E367
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x0009016A File Offset: 0x0008E36A
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x00090174 File Offset: 0x0008E374
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F0E RID: 3854
		internal static readonly ConstrainedSortOp Pattern = new ConstrainedSortOp();
	}
}
