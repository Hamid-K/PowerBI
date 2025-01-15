using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000301 RID: 769
	[Serializable]
	internal class ShutdownStatement : TSqlStatement
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x00167A71 File Offset: 0x00165C71
		// (set) Token: 0x060029F1 RID: 10737 RVA: 0x00167A79 File Offset: 0x00165C79
		public bool WithNoWait
		{
			get
			{
				return this._withNoWait;
			}
			set
			{
				this._withNoWait = value;
			}
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00167A82 File Offset: 0x00165C82
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x00167A8E File Offset: 0x00165C8E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C44 RID: 7236
		private bool _withNoWait;
	}
}
