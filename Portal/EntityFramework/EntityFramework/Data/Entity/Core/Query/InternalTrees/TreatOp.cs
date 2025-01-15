using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003F4 RID: 1012
	internal sealed class TreatOp : ScalarOp
	{
		// Token: 0x06002F4E RID: 12110 RVA: 0x00095B63 File Offset: 0x00093D63
		internal TreatOp(TypeUsage type, bool isFake)
			: base(OpType.Treat, type)
		{
			this.m_isFake = isFake;
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x00095B75 File Offset: 0x00093D75
		private TreatOp()
			: base(OpType.Treat)
		{
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x00095B7F File Offset: 0x00093D7F
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x00095B82 File Offset: 0x00093D82
		internal bool IsFakeTreat
		{
			get
			{
				return this.m_isFake;
			}
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x00095B8A File Offset: 0x00093D8A
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x00095B94 File Offset: 0x00093D94
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FF7 RID: 4087
		private readonly bool m_isFake;

		// Token: 0x04000FF8 RID: 4088
		internal static readonly TreatOp Pattern = new TreatOp();
	}
}
