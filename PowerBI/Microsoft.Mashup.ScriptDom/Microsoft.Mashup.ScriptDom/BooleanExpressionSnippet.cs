using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000428 RID: 1064
	[Serializable]
	internal class BooleanExpressionSnippet : BooleanExpression
	{
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x0016F048 File Offset: 0x0016D248
		// (set) Token: 0x06003132 RID: 12594 RVA: 0x0016F050 File Offset: 0x0016D250
		public string Script
		{
			get
			{
				return this._script;
			}
			set
			{
				this._script = value;
			}
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x0016F059 File Offset: 0x0016D259
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x0016F065 File Offset: 0x0016D265
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E5F RID: 7775
		private string _script;
	}
}
