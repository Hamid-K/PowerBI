using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000431 RID: 1073
	[Serializable]
	internal class MergeStatement : DataModificationStatement
	{
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x0016F259 File Offset: 0x0016D459
		// (set) Token: 0x0600315D RID: 12637 RVA: 0x0016F261 File Offset: 0x0016D461
		public MergeSpecification MergeSpecification
		{
			get
			{
				return this._mergeSpecification;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._mergeSpecification = value;
			}
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x0016F271 File Offset: 0x0016D471
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x0016F27D File Offset: 0x0016D47D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.MergeSpecification != null)
			{
				this.MergeSpecification.Accept(visitor);
			}
		}

		// Token: 0x04001E68 RID: 7784
		private MergeSpecification _mergeSpecification;
	}
}
