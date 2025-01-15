using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200031B RID: 795
	[Serializable]
	internal class AlterDatabaseCollateStatement : AlterDatabaseStatement, ICollationSetter
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x0016837B File Offset: 0x0016657B
		// (set) Token: 0x06002A90 RID: 10896 RVA: 0x00168383 File Offset: 0x00166583
		public Identifier Collation
		{
			get
			{
				return this._collation;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._collation = value;
			}
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x00168393 File Offset: 0x00166593
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x0016839F File Offset: 0x0016659F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Collation != null)
			{
				this.Collation.Accept(visitor);
			}
		}

		// Token: 0x04001C71 RID: 7281
		private Identifier _collation;
	}
}
