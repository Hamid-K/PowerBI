using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000327 RID: 807
	[Serializable]
	internal class OnOffDatabaseOption : DatabaseOption
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x0016882B File Offset: 0x00166A2B
		// (set) Token: 0x06002ADE RID: 10974 RVA: 0x00168833 File Offset: 0x00166A33
		public OptionState OptionState
		{
			get
			{
				return this._optionState;
			}
			set
			{
				this._optionState = value;
			}
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0016883C File Offset: 0x00166A3C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x00168848 File Offset: 0x00166A48
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C87 RID: 7303
		private OptionState _optionState;
	}
}
