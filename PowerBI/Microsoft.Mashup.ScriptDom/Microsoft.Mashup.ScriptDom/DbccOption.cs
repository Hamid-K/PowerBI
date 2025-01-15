using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200035F RID: 863
	[Serializable]
	internal class DbccOption : TSqlFragment
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x0016A156 File Offset: 0x00168356
		// (set) Token: 0x06002C60 RID: 11360 RVA: 0x0016A15E File Offset: 0x0016835E
		public DbccOptionKind OptionKind
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

		// Token: 0x06002C61 RID: 11361 RVA: 0x0016A167 File Offset: 0x00168367
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x0016A173 File Offset: 0x00168373
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D01 RID: 7425
		private DbccOptionKind _optionKind;
	}
}
