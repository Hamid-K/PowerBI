using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200029C RID: 668
	[Serializable]
	internal class QueueProcedureOption : QueueOption
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x0016552A File Offset: 0x0016372A
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x00165532 File Offset: 0x00163732
		public SchemaObjectName OptionValue
		{
			get
			{
				return this._optionValue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._optionValue = value;
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00165542 File Offset: 0x00163742
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x0016554E File Offset: 0x0016374E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.OptionValue != null)
			{
				this.OptionValue.Accept(visitor);
			}
		}

		// Token: 0x04001BAB RID: 7083
		private SchemaObjectName _optionValue;
	}
}
