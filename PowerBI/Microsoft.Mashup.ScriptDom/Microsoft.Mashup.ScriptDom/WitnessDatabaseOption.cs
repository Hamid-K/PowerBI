using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000330 RID: 816
	[Serializable]
	internal class WitnessDatabaseOption : DatabaseOption
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x00168A68 File Offset: 0x00166C68
		// (set) Token: 0x06002B11 RID: 11025 RVA: 0x00168A70 File Offset: 0x00166C70
		public Literal WitnessServer
		{
			get
			{
				return this._witnessServer;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._witnessServer = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x00168A80 File Offset: 0x00166C80
		// (set) Token: 0x06002B13 RID: 11027 RVA: 0x00168A88 File Offset: 0x00166C88
		public bool IsOff
		{
			get
			{
				return this._isOff;
			}
			set
			{
				this._isOff = value;
			}
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x00168A91 File Offset: 0x00166C91
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x00168A9D File Offset: 0x00166C9D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.WitnessServer != null)
			{
				this.WitnessServer.Accept(visitor);
			}
		}

		// Token: 0x04001C93 RID: 7315
		private Literal _witnessServer;

		// Token: 0x04001C94 RID: 7316
		private bool _isOff;
	}
}
