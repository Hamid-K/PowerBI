using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001FC RID: 508
	[Serializable]
	internal abstract class WhenClause : TSqlFragment
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x00161057 File Offset: 0x0015F257
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x0016105F File Offset: 0x0015F25F
		public ScalarExpression ThenExpression
		{
			get
			{
				return this._thenExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._thenExpression = value;
			}
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x0016106F File Offset: 0x0015F26F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ThenExpression != null)
			{
				this.ThenExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A8F RID: 6799
		private ScalarExpression _thenExpression;
	}
}
