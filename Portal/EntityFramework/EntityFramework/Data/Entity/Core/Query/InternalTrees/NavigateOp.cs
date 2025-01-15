using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B9 RID: 953
	internal sealed class NavigateOp : ScalarOp
	{
		// Token: 0x06002DBD RID: 11709 RVA: 0x00092325 File Offset: 0x00090525
		internal NavigateOp(TypeUsage type, RelProperty relProperty)
			: base(OpType.Navigate, type)
		{
			this.m_property = relProperty;
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x00092337 File Offset: 0x00090537
		private NavigateOp()
			: base(OpType.Navigate)
		{
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x00092341 File Offset: 0x00090541
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x00092344 File Offset: 0x00090544
		internal RelProperty RelProperty
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x0009234C File Offset: 0x0009054C
		internal RelationshipType Relationship
		{
			get
			{
				return this.m_property.Relationship;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x00092359 File Offset: 0x00090559
		internal RelationshipEndMember FromEnd
		{
			get
			{
				return this.m_property.FromEnd;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x00092366 File Offset: 0x00090566
		internal RelationshipEndMember ToEnd
		{
			get
			{
				return this.m_property.ToEnd;
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x00092373 File Offset: 0x00090573
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x0009237D File Offset: 0x0009057D
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F4B RID: 3915
		private readonly RelProperty m_property;

		// Token: 0x04000F4C RID: 3916
		internal static readonly NavigateOp Pattern = new NavigateOp();
	}
}
