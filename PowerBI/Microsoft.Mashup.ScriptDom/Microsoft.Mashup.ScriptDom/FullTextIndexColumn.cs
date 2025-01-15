using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002AF RID: 687
	[Serializable]
	internal class FullTextIndexColumn : TSqlFragment
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x00165E55 File Offset: 0x00164055
		// (set) Token: 0x0600282D RID: 10285 RVA: 0x00165E5D File Offset: 0x0016405D
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

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x00165E6D File Offset: 0x0016406D
		// (set) Token: 0x0600282F RID: 10287 RVA: 0x00165E75 File Offset: 0x00164075
		public Identifier TypeColumn
		{
			get
			{
				return this._typeColumn;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._typeColumn = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x00165E85 File Offset: 0x00164085
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x00165E8D File Offset: 0x0016408D
		public IdentifierOrValueExpression LanguageTerm
		{
			get
			{
				return this._languageTerm;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._languageTerm = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x00165E9D File Offset: 0x0016409D
		// (set) Token: 0x06002833 RID: 10291 RVA: 0x00165EA5 File Offset: 0x001640A5
		public bool StatisticalSemantics
		{
			get
			{
				return this._statisticalSemantics;
			}
			set
			{
				this._statisticalSemantics = value;
			}
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x00165EAE File Offset: 0x001640AE
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x00165EBC File Offset: 0x001640BC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.TypeColumn != null)
			{
				this.TypeColumn.Accept(visitor);
			}
			if (this.LanguageTerm != null)
			{
				this.LanguageTerm.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BCF RID: 7119
		private Identifier _name;

		// Token: 0x04001BD0 RID: 7120
		private Identifier _typeColumn;

		// Token: 0x04001BD1 RID: 7121
		private IdentifierOrValueExpression _languageTerm;

		// Token: 0x04001BD2 RID: 7122
		private bool _statisticalSemantics;
	}
}
