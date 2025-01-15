using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200046C RID: 1132
	[Serializable]
	internal class FullTextStopListAction : TSqlFragment
	{
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x00170405 File Offset: 0x0016E605
		// (set) Token: 0x0600328C RID: 12940 RVA: 0x0017040D File Offset: 0x0016E60D
		public bool IsAdd
		{
			get
			{
				return this._isAdd;
			}
			set
			{
				this._isAdd = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x00170416 File Offset: 0x0016E616
		// (set) Token: 0x0600328E RID: 12942 RVA: 0x0017041E File Offset: 0x0016E61E
		public bool IsAll
		{
			get
			{
				return this._isAll;
			}
			set
			{
				this._isAll = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x00170427 File Offset: 0x0016E627
		// (set) Token: 0x06003290 RID: 12944 RVA: 0x0017042F File Offset: 0x0016E62F
		public Literal StopWord
		{
			get
			{
				return this._stopWord;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._stopWord = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x0017043F File Offset: 0x0016E63F
		// (set) Token: 0x06003292 RID: 12946 RVA: 0x00170447 File Offset: 0x0016E647
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

		// Token: 0x06003293 RID: 12947 RVA: 0x00170457 File Offset: 0x0016E657
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x00170463 File Offset: 0x0016E663
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.StopWord != null)
			{
				this.StopWord.Accept(visitor);
			}
			if (this.LanguageTerm != null)
			{
				this.LanguageTerm.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EB2 RID: 7858
		private bool _isAdd;

		// Token: 0x04001EB3 RID: 7859
		private bool _isAll;

		// Token: 0x04001EB4 RID: 7860
		private Literal _stopWord;

		// Token: 0x04001EB5 RID: 7861
		private IdentifierOrValueExpression _languageTerm;
	}
}
