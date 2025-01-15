using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000336 RID: 822
	[Serializable]
	internal class AutoCleanupChangeTrackingOptionDetail : ChangeTrackingOptionDetail
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x00168C0A File Offset: 0x00166E0A
		// (set) Token: 0x06002B2F RID: 11055 RVA: 0x00168C12 File Offset: 0x00166E12
		public bool IsOn
		{
			get
			{
				return this._isOn;
			}
			set
			{
				this._isOn = value;
			}
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x00168C1B File Offset: 0x00166E1B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x00168C27 File Offset: 0x00166E27
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C9A RID: 7322
		private bool _isOn;
	}
}
