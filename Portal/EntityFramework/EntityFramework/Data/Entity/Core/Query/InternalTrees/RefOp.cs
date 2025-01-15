using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003D3 RID: 979
	internal sealed class RefOp : ScalarOp
	{
		// Token: 0x06002EB3 RID: 11955 RVA: 0x00094D23 File Offset: 0x00092F23
		internal RefOp(EntitySet entitySet, TypeUsage type)
			: base(OpType.Ref, type)
		{
			this.m_entitySet = entitySet;
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x00094D35 File Offset: 0x00092F35
		private RefOp()
			: base(OpType.Ref)
		{
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x00094D3F File Offset: 0x00092F3F
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x00094D42 File Offset: 0x00092F42
		internal EntitySet EntitySet
		{
			get
			{
				return this.m_entitySet;
			}
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x00094D4A File Offset: 0x00092F4A
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x00094D54 File Offset: 0x00092F54
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FBF RID: 4031
		private readonly EntitySet m_entitySet;

		// Token: 0x04000FC0 RID: 4032
		internal static readonly RefOp Pattern = new RefOp();
	}
}
