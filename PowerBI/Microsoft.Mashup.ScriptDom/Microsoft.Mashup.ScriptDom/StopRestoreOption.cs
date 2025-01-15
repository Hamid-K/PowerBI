using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000351 RID: 849
	[Serializable]
	internal class StopRestoreOption : RestoreOption
	{
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x00169B1A File Offset: 0x00167D1A
		// (set) Token: 0x06002C06 RID: 11270 RVA: 0x00169B22 File Offset: 0x00167D22
		public ValueExpression Mark
		{
			get
			{
				return this._mark;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._mark = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x00169B32 File Offset: 0x00167D32
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x00169B3A File Offset: 0x00167D3A
		public ValueExpression After
		{
			get
			{
				return this._after;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._after = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x00169B4A File Offset: 0x00167D4A
		// (set) Token: 0x06002C0A RID: 11274 RVA: 0x00169B52 File Offset: 0x00167D52
		public bool IsStopAt
		{
			get
			{
				return this._isStopAt;
			}
			set
			{
				this._isStopAt = value;
			}
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00169B5B File Offset: 0x00167D5B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x00169B67 File Offset: 0x00167D67
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Mark != null)
			{
				this.Mark.Accept(visitor);
			}
			if (this.After != null)
			{
				this.After.Accept(visitor);
			}
		}

		// Token: 0x04001CE5 RID: 7397
		private ValueExpression _mark;

		// Token: 0x04001CE6 RID: 7398
		private ValueExpression _after;

		// Token: 0x04001CE7 RID: 7399
		private bool _isStopAt;
	}
}
