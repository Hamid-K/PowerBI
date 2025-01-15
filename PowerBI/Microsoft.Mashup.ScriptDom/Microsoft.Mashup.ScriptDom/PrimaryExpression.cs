using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	internal abstract class PrimaryExpression : ScalarExpression, ICollationSetter
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000065C5 File Offset: 0x000047C5
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x000065CD File Offset: 0x000047CD
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

		// Token: 0x060001F6 RID: 502 RVA: 0x000065DD File Offset: 0x000047DD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Collation != null)
			{
				this.Collation.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x0400017A RID: 378
		private Identifier _collation;
	}
}
