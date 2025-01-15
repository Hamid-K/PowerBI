using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200049D RID: 1181
	[Serializable]
	internal class AlterFederationStatement : TSqlStatement
	{
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x001715D0 File Offset: 0x0016F7D0
		// (set) Token: 0x060033A5 RID: 13221 RVA: 0x001715D8 File Offset: 0x0016F7D8
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x001715E8 File Offset: 0x0016F7E8
		// (set) Token: 0x060033A7 RID: 13223 RVA: 0x001715F0 File Offset: 0x0016F7F0
		public AlterFederationKind Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				this._kind = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x001715F9 File Offset: 0x0016F7F9
		// (set) Token: 0x060033A9 RID: 13225 RVA: 0x00171601 File Offset: 0x0016F801
		public Identifier DistributionName
		{
			get
			{
				return this._distributionName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._distributionName = value;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x00171611 File Offset: 0x0016F811
		// (set) Token: 0x060033AB RID: 13227 RVA: 0x00171619 File Offset: 0x0016F819
		public ScalarExpression Boundary
		{
			get
			{
				return this._boundary;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._boundary = value;
			}
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x00171629 File Offset: 0x0016F829
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x00171638 File Offset: 0x0016F838
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.DistributionName != null)
			{
				this.DistributionName.Accept(visitor);
			}
			if (this.Boundary != null)
			{
				this.Boundary.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001F00 RID: 7936
		private Identifier _name;

		// Token: 0x04001F01 RID: 7937
		private AlterFederationKind _kind;

		// Token: 0x04001F02 RID: 7938
		private Identifier _distributionName;

		// Token: 0x04001F03 RID: 7939
		private ScalarExpression _boundary;
	}
}
