using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200023D RID: 573
	[Serializable]
	internal class UpdateStatement : DataModificationStatement
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x00162F09 File Offset: 0x00161109
		// (set) Token: 0x0600257D RID: 9597 RVA: 0x00162F11 File Offset: 0x00161111
		public UpdateSpecification UpdateSpecification
		{
			get
			{
				return this._updateSpecification;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._updateSpecification = value;
			}
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00162F21 File Offset: 0x00161121
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x00162F2D File Offset: 0x0016112D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.UpdateSpecification != null)
			{
				this.UpdateSpecification.Accept(visitor);
			}
		}

		// Token: 0x04001B0A RID: 6922
		private UpdateSpecification _updateSpecification;
	}
}
