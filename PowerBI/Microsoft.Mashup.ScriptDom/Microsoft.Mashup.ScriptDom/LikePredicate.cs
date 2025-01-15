using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001EA RID: 490
	[Serializable]
	internal class LikePredicate : BooleanExpression
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x001607F6 File Offset: 0x0015E9F6
		// (set) Token: 0x06002368 RID: 9064 RVA: 0x001607FE File Offset: 0x0015E9FE
		public ScalarExpression FirstExpression
		{
			get
			{
				return this._firstExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._firstExpression = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x0016080E File Offset: 0x0015EA0E
		// (set) Token: 0x0600236A RID: 9066 RVA: 0x00160816 File Offset: 0x0015EA16
		public ScalarExpression SecondExpression
		{
			get
			{
				return this._secondExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._secondExpression = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x00160826 File Offset: 0x0015EA26
		// (set) Token: 0x0600236C RID: 9068 RVA: 0x0016082E File Offset: 0x0015EA2E
		public bool NotDefined
		{
			get
			{
				return this._notDefined;
			}
			set
			{
				this._notDefined = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x00160837 File Offset: 0x0015EA37
		// (set) Token: 0x0600236E RID: 9070 RVA: 0x0016083F File Offset: 0x0015EA3F
		public bool OdbcEscape
		{
			get
			{
				return this._odbcEscape;
			}
			set
			{
				this._odbcEscape = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x00160848 File Offset: 0x0015EA48
		// (set) Token: 0x06002370 RID: 9072 RVA: 0x00160850 File Offset: 0x0015EA50
		public ScalarExpression EscapeExpression
		{
			get
			{
				return this._escapeExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._escapeExpression = value;
			}
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00160860 File Offset: 0x0015EA60
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0016086C File Offset: 0x0015EA6C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.FirstExpression != null)
			{
				this.FirstExpression.Accept(visitor);
			}
			if (this.SecondExpression != null)
			{
				this.SecondExpression.Accept(visitor);
			}
			if (this.EscapeExpression != null)
			{
				this.EscapeExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A6B RID: 6763
		private ScalarExpression _firstExpression;

		// Token: 0x04001A6C RID: 6764
		private ScalarExpression _secondExpression;

		// Token: 0x04001A6D RID: 6765
		private bool _notDefined;

		// Token: 0x04001A6E RID: 6766
		private bool _odbcEscape;

		// Token: 0x04001A6F RID: 6767
		private ScalarExpression _escapeExpression;
	}
}
