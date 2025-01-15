using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200038F RID: 911
	internal sealed class ComparisonOp : ScalarOp
	{
		// Token: 0x06002CA9 RID: 11433 RVA: 0x0008FF23 File Offset: 0x0008E123
		internal ComparisonOp(OpType opType, TypeUsage type)
			: base(opType, type)
		{
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x0008FF2D File Offset: 0x0008E12D
		private ComparisonOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x0008FF36 File Offset: 0x0008E136
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x0008FF39 File Offset: 0x0008E139
		// (set) Token: 0x06002CAD RID: 11437 RVA: 0x0008FF41 File Offset: 0x0008E141
		internal bool UseDatabaseNullSemantics { get; set; }

		// Token: 0x06002CAE RID: 11438 RVA: 0x0008FF4A File Offset: 0x0008E14A
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x0008FF54 File Offset: 0x0008E154
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F02 RID: 3842
		internal static readonly ComparisonOp PatternEq = new ComparisonOp(OpType.EQ);
	}
}
