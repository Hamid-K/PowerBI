using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001E4 RID: 484
	[Serializable]
	internal class LiteralTableHint : TableHint
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x0016060D File Offset: 0x0015E80D
		// (set) Token: 0x06002348 RID: 9032 RVA: 0x00160615 File Offset: 0x0015E815
		public Literal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x00160625 File Offset: 0x0015E825
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x00160631 File Offset: 0x0015E831
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001A63 RID: 6755
		private Literal _value;
	}
}
