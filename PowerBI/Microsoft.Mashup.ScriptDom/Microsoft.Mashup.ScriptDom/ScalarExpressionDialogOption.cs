using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200041F RID: 1055
	[Serializable]
	internal class ScalarExpressionDialogOption : DialogOption
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x0016EE2F File Offset: 0x0016D02F
		// (set) Token: 0x06003106 RID: 12550 RVA: 0x0016EE37 File Offset: 0x0016D037
		public ScalarExpression Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x0016EE47 File Offset: 0x0016D047
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x0016EE53 File Offset: 0x0016D053
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001E56 RID: 7766
		private ScalarExpression _value;
	}
}
