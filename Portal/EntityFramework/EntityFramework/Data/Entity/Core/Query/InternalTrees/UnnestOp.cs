using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003F7 RID: 1015
	internal sealed class UnnestOp : RelOp
	{
		// Token: 0x06002F5C RID: 12124 RVA: 0x00095BFC File Offset: 0x00093DFC
		internal UnnestOp(Var v, Table t)
			: this()
		{
			this.m_var = v;
			this.m_table = t;
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x00095C12 File Offset: 0x00093E12
		private UnnestOp()
			: base(OpType.Unnest)
		{
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x00095C1C File Offset: 0x00093E1C
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x00095C24 File Offset: 0x00093E24
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x00095C2C File Offset: 0x00093E2C
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x00095C2F File Offset: 0x00093E2F
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x00095C39 File Offset: 0x00093E39
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FFB RID: 4091
		private readonly Table m_table;

		// Token: 0x04000FFC RID: 4092
		private readonly Var m_var;

		// Token: 0x04000FFD RID: 4093
		internal static readonly UnnestOp Pattern = new UnnestOp();
	}
}
