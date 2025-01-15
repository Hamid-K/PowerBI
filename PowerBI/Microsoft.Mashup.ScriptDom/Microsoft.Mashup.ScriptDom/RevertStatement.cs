using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000406 RID: 1030
	[Serializable]
	internal class RevertStatement : TSqlStatement
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x0016E379 File Offset: 0x0016C579
		// (set) Token: 0x06003061 RID: 12385 RVA: 0x0016E381 File Offset: 0x0016C581
		public ScalarExpression Cookie
		{
			get
			{
				return this._cookie;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._cookie = value;
			}
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x0016E391 File Offset: 0x0016C591
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x0016E39D File Offset: 0x0016C59D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Cookie != null)
			{
				this.Cookie.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E25 RID: 7717
		private ScalarExpression _cookie;
	}
}
