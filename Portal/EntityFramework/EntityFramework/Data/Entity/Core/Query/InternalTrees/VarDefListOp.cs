using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003F9 RID: 1017
	internal sealed class VarDefListOp : AncillaryOp
	{
		// Token: 0x06002F6A RID: 12138 RVA: 0x00095CAF File Offset: 0x00093EAF
		private VarDefListOp()
			: base(OpType.VarDefList)
		{
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x00095CB9 File Offset: 0x00093EB9
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x00095CC3 File Offset: 0x00093EC3
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001001 RID: 4097
		internal static readonly VarDefListOp Instance = new VarDefListOp();

		// Token: 0x04001002 RID: 4098
		internal static readonly VarDefListOp Pattern = VarDefListOp.Instance;
	}
}
