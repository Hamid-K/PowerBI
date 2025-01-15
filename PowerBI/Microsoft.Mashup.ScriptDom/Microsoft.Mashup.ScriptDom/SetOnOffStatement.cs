using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000304 RID: 772
	[Serializable]
	internal abstract class SetOnOffStatement : TSqlStatement
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x00167B42 File Offset: 0x00165D42
		// (set) Token: 0x06002A02 RID: 10754 RVA: 0x00167B4A File Offset: 0x00165D4A
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

		// Token: 0x06002A03 RID: 10755 RVA: 0x00167B53 File Offset: 0x00165D53
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C48 RID: 7240
		private bool _isOn;
	}
}
