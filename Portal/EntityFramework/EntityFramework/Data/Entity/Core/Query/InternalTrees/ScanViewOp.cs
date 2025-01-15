using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003E1 RID: 993
	internal sealed class ScanViewOp : ScanTableBaseOp
	{
		// Token: 0x06002EF7 RID: 12023 RVA: 0x00095346 File Offset: 0x00093546
		internal ScanViewOp(Table table)
			: base(OpType.ScanView, table)
		{
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x00095351 File Offset: 0x00093551
		private ScanViewOp()
			: base(OpType.ScanView)
		{
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002EF9 RID: 12025 RVA: 0x0009535B File Offset: 0x0009355B
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0009535E File Offset: 0x0009355E
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x00095368 File Offset: 0x00093568
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FD5 RID: 4053
		internal static readonly ScanViewOp Pattern = new ScanViewOp();
	}
}
