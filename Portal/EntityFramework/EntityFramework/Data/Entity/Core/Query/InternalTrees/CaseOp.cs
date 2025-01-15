using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000382 RID: 898
	internal sealed class CaseOp : ScalarOp
	{
		// Token: 0x06002BB6 RID: 11190 RVA: 0x0008DE02 File Offset: 0x0008C002
		internal CaseOp(TypeUsage type)
			: base(OpType.Case, type)
		{
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x0008DE0D File Offset: 0x0008C00D
		private CaseOp()
			: base(OpType.Case)
		{
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x0008DE17 File Offset: 0x0008C017
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x0008DE21 File Offset: 0x0008C021
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000EDB RID: 3803
		internal static readonly CaseOp Pattern = new CaseOp();
	}
}
