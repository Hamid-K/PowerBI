using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000395 RID: 917
	[Serializable]
	internal abstract class KeyOption : TSqlFragment
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x0016B8CC File Offset: 0x00169ACC
		// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x0016B8D4 File Offset: 0x00169AD4
		public KeyOptionKind OptionKind
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

		// Token: 0x06002DCA RID: 11722 RVA: 0x0016B8DD File Offset: 0x00169ADD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D6F RID: 7535
		private KeyOptionKind _optionKind;
	}
}
