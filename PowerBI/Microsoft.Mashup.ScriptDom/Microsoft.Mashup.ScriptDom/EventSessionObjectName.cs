using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000471 RID: 1137
	[Serializable]
	internal class EventSessionObjectName : TSqlFragment
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x001705D1 File Offset: 0x0016E7D1
		// (set) Token: 0x060032AD RID: 12973 RVA: 0x001705D9 File Offset: 0x0016E7D9
		public MultiPartIdentifier MultiPartIdentifier
		{
			get
			{
				return this._multiPartIdentifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._multiPartIdentifier = value;
			}
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x001705E9 File Offset: 0x0016E7E9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x001705F5 File Offset: 0x0016E7F5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.MultiPartIdentifier != null)
			{
				this.MultiPartIdentifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EBB RID: 7867
		private MultiPartIdentifier _multiPartIdentifier;
	}
}
