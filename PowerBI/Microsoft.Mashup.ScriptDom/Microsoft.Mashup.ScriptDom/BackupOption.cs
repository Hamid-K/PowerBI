using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000353 RID: 851
	[Serializable]
	internal class BackupOption : TSqlFragment
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x00169BE9 File Offset: 0x00167DE9
		// (set) Token: 0x06002C14 RID: 11284 RVA: 0x00169BF1 File Offset: 0x00167DF1
		public BackupOptionKind OptionKind
		{
			get
			{
				return this._optionKind;
			}
			set
			{
				this._optionKind = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x00169BFA File Offset: 0x00167DFA
		// (set) Token: 0x06002C16 RID: 11286 RVA: 0x00169C02 File Offset: 0x00167E02
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

		// Token: 0x06002C17 RID: 11287 RVA: 0x00169C12 File Offset: 0x00167E12
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x00169C1E File Offset: 0x00167E1E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CE9 RID: 7401
		private BackupOptionKind _optionKind;

		// Token: 0x04001CEA RID: 7402
		private ScalarExpression _value;
	}
}
