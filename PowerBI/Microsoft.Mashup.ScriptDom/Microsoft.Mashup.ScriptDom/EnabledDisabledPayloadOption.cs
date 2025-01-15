using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000389 RID: 905
	[Serializable]
	internal class EnabledDisabledPayloadOption : PayloadOption
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x0016B4B0 File Offset: 0x001696B0
		// (set) Token: 0x06002D7E RID: 11646 RVA: 0x0016B4B8 File Offset: 0x001696B8
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				this._isEnabled = value;
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x0016B4C1 File Offset: 0x001696C1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x0016B4CD File Offset: 0x001696CD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D5A RID: 7514
		private bool _isEnabled;
	}
}
