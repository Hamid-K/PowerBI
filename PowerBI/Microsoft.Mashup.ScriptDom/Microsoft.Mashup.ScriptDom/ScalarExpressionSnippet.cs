using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000427 RID: 1063
	[Serializable]
	internal class ScalarExpressionSnippet : ScalarExpression
	{
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x0016F01A File Offset: 0x0016D21A
		// (set) Token: 0x0600312D RID: 12589 RVA: 0x0016F022 File Offset: 0x0016D222
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

		// Token: 0x0600312E RID: 12590 RVA: 0x0016F02B File Offset: 0x0016D22B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x0016F037 File Offset: 0x0016D237
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E5E RID: 7774
		private string _script;
	}
}
