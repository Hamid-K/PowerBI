using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000308 RID: 776
	[Serializable]
	internal class SetOffsetsStatement : SetOnOffStatement
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x00167C09 File Offset: 0x00165E09
		// (set) Token: 0x06002A15 RID: 10773 RVA: 0x00167C11 File Offset: 0x00165E11
		public SetOffsets Options
		{
			get
			{
				return this._options;
			}
			set
			{
				this._options = value;
			}
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x00167C1A File Offset: 0x00165E1A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x00167C26 File Offset: 0x00165E26
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C4C RID: 7244
		private SetOffsets _options;
	}
}
