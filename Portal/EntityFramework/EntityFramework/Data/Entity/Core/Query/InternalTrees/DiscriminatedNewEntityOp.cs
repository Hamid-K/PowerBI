using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200039C RID: 924
	internal sealed class DiscriminatedNewEntityOp : NewEntityBaseOp
	{
		// Token: 0x06002CEF RID: 11503 RVA: 0x00090345 File Offset: 0x0008E545
		internal DiscriminatedNewEntityOp(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap, EntitySet entitySet, List<RelProperty> relProperties)
			: base(OpType.DiscriminatedNewEntity, type, true, entitySet, relProperties)
		{
			this.m_discriminatorMap = discriminatorMap;
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x0009035B File Offset: 0x0008E55B
		private DiscriminatedNewEntityOp()
			: base(OpType.DiscriminatedNewEntity)
		{
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x00090365 File Offset: 0x0008E565
		internal ExplicitDiscriminatorMap DiscriminatorMap
		{
			get
			{
				return this.m_discriminatorMap;
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x0009036D File Offset: 0x0008E56D
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x00090377 File Offset: 0x0008E577
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F18 RID: 3864
		private readonly ExplicitDiscriminatorMap m_discriminatorMap;

		// Token: 0x04000F19 RID: 3865
		internal static readonly DiscriminatedNewEntityOp Pattern = new DiscriminatedNewEntityOp();
	}
}
