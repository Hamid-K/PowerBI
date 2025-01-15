using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000250 RID: 592
	[Serializable]
	internal class GrantStatement80 : SecurityStatementBody80
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x00163A29 File Offset: 0x00161C29
		// (set) Token: 0x06002609 RID: 9737 RVA: 0x00163A31 File Offset: 0x00161C31
		public bool WithGrantOption
		{
			get
			{
				return this._withGrantOption;
			}
			set
			{
				this._withGrantOption = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x00163A3A File Offset: 0x00161C3A
		// (set) Token: 0x0600260B RID: 9739 RVA: 0x00163A42 File Offset: 0x00161C42
		public Identifier AsClause
		{
			get
			{
				return this._asClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._asClause = value;
			}
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x00163A52 File Offset: 0x00161C52
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x00163A5E File Offset: 0x00161C5E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.AsClause != null)
			{
				this.AsClause.Accept(visitor);
			}
		}

		// Token: 0x04001B38 RID: 6968
		private bool _withGrantOption;

		// Token: 0x04001B39 RID: 6969
		private Identifier _asClause;
	}
}
