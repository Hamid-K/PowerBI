using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200034E RID: 846
	[Serializable]
	internal class RestoreOption : TSqlFragment
	{
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x00169A2E File Offset: 0x00167C2E
		// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x00169A36 File Offset: 0x00167C36
		public RestoreOptionKind OptionKind
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

		// Token: 0x06002BF6 RID: 11254 RVA: 0x00169A3F File Offset: 0x00167C3F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x00169A4B File Offset: 0x00167C4B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CE1 RID: 7393
		private RestoreOptionKind _optionKind;
	}
}
