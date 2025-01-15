using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003FA RID: 1018
	internal sealed class VarDefOp : AncillaryOp
	{
		// Token: 0x06002F6E RID: 12142 RVA: 0x00095CE3 File Offset: 0x00093EE3
		internal VarDefOp(Var v)
			: this()
		{
			this.m_var = v;
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x00095CF2 File Offset: 0x00093EF2
		private VarDefOp()
			: base(OpType.VarDef)
		{
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x00095CFC File Offset: 0x00093EFC
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x00095CFF File Offset: 0x00093EFF
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x00095D07 File Offset: 0x00093F07
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x00095D11 File Offset: 0x00093F11
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001003 RID: 4099
		private readonly Var m_var;

		// Token: 0x04001004 RID: 4100
		internal static readonly VarDefOp Pattern = new VarDefOp();
	}
}
