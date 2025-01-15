using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003E0 RID: 992
	internal sealed class ScanTableOp : ScanTableBaseOp
	{
		// Token: 0x06002EF1 RID: 12017 RVA: 0x0009530E File Offset: 0x0009350E
		internal ScanTableOp(Table table)
			: base(OpType.ScanTable, table)
		{
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x00095319 File Offset: 0x00093519
		private ScanTableOp()
			: base(OpType.ScanTable)
		{
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x00095323 File Offset: 0x00093523
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x00095326 File Offset: 0x00093526
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x00095330 File Offset: 0x00093530
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FD4 RID: 4052
		internal static readonly ScanTableOp Pattern = new ScanTableOp();
	}
}
