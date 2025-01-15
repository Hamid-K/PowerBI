using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002D8 RID: 728
	[Serializable]
	internal class CursorOption : TSqlFragment
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600290F RID: 10511 RVA: 0x00166CED File Offset: 0x00164EED
		// (set) Token: 0x06002910 RID: 10512 RVA: 0x00166CF5 File Offset: 0x00164EF5
		public CursorOptionKind OptionKind
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

		// Token: 0x06002911 RID: 10513 RVA: 0x00166CFE File Offset: 0x00164EFE
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x00166D0A File Offset: 0x00164F0A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C0C RID: 7180
		private CursorOptionKind _optionKind;
	}
}
