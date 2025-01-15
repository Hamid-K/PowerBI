using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001FF RID: 511
	[Serializable]
	internal abstract class CaseExpression : PrimaryExpression
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x00161126 File Offset: 0x0015F326
		// (set) Token: 0x060023E9 RID: 9193 RVA: 0x0016112E File Offset: 0x0015F32E
		public ScalarExpression ElseExpression
		{
			get
			{
				return this._elseExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._elseExpression = value;
			}
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x0016113E File Offset: 0x0015F33E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.ElseExpression != null)
			{
				this.ElseExpression.Accept(visitor);
			}
		}

		// Token: 0x04001A92 RID: 6802
		private ScalarExpression _elseExpression;
	}
}
