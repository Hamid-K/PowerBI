using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002ED RID: 749
	[Serializable]
	internal class MoveToDropIndexOption : IndexOption
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x001674B1 File Offset: 0x001656B1
		// (set) Token: 0x06002988 RID: 10632 RVA: 0x001674B9 File Offset: 0x001656B9
		public FileGroupOrPartitionScheme MoveTo
		{
			get
			{
				return this._moveTo;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._moveTo = value;
			}
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x001674C9 File Offset: 0x001656C9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x001674D5 File Offset: 0x001656D5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.MoveTo != null)
			{
				this.MoveTo.Accept(visitor);
			}
		}

		// Token: 0x04001C2D RID: 7213
		private FileGroupOrPartitionScheme _moveTo;
	}
}
