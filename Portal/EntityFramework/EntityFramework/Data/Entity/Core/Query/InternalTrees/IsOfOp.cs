using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B1 RID: 945
	internal sealed class IsOfOp : ScalarOp
	{
		// Token: 0x06002D92 RID: 11666 RVA: 0x00091FAA File Offset: 0x000901AA
		internal IsOfOp(TypeUsage isOfType, bool isOfOnly, TypeUsage type)
			: base(OpType.IsOf, type)
		{
			this.m_isOfType = isOfType;
			this.m_isOfOnly = isOfOnly;
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x00091FC3 File Offset: 0x000901C3
		private IsOfOp()
			: base(OpType.IsOf)
		{
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x00091FCD File Offset: 0x000901CD
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x00091FD0 File Offset: 0x000901D0
		internal TypeUsage IsOfType
		{
			get
			{
				return this.m_isOfType;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x00091FD8 File Offset: 0x000901D8
		internal bool IsOfOnly
		{
			get
			{
				return this.m_isOfOnly;
			}
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x00091FE0 File Offset: 0x000901E0
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x00091FEA File Offset: 0x000901EA
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F3E RID: 3902
		private readonly TypeUsage m_isOfType;

		// Token: 0x04000F3F RID: 3903
		private readonly bool m_isOfOnly;

		// Token: 0x04000F40 RID: 3904
		internal static readonly IsOfOp Pattern = new IsOfOp();
	}
}
