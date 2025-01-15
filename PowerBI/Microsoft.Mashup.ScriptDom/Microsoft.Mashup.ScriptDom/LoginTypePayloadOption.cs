using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200038B RID: 907
	[Serializable]
	internal class LoginTypePayloadOption : PayloadOption
	{
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x0016B538 File Offset: 0x00169738
		// (set) Token: 0x06002D8A RID: 11658 RVA: 0x0016B540 File Offset: 0x00169740
		public bool IsWindows
		{
			get
			{
				return this._isWindows;
			}
			set
			{
				this._isWindows = value;
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x0016B549 File Offset: 0x00169749
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x0016B555 File Offset: 0x00169755
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D5D RID: 7517
		private bool _isWindows;
	}
}
