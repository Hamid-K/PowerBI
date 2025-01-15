using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200024D RID: 589
	[Serializable]
	internal class SecurityTargetObjectName : TSqlFragment
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x0016391D File Offset: 0x00161B1D
		// (set) Token: 0x060025F7 RID: 9719 RVA: 0x00163925 File Offset: 0x00161B25
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

		// Token: 0x060025F8 RID: 9720 RVA: 0x00163935 File Offset: 0x00161B35
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00163941 File Offset: 0x00161B41
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.MultiPartIdentifier != null)
			{
				this.MultiPartIdentifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B33 RID: 6963
		private MultiPartIdentifier _multiPartIdentifier;
	}
}
