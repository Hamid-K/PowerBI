using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200032D RID: 813
	[Serializable]
	internal class TargetRecoveryTimeDatabaseOption : DatabaseOption
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x0016895A File Offset: 0x00166B5A
		// (set) Token: 0x06002AFC RID: 11004 RVA: 0x00168962 File Offset: 0x00166B62
		public Literal RecoveryTime
		{
			get
			{
				return this._recoveryTime;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._recoveryTime = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x00168972 File Offset: 0x00166B72
		// (set) Token: 0x06002AFE RID: 11006 RVA: 0x0016897A File Offset: 0x00166B7A
		public TimeUnit Unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				this._unit = value;
			}
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x00168983 File Offset: 0x00166B83
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x0016898F File Offset: 0x00166B8F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.RecoveryTime != null)
			{
				this.RecoveryTime.Accept(visitor);
			}
		}

		// Token: 0x04001C8D RID: 7309
		private Literal _recoveryTime;

		// Token: 0x04001C8E RID: 7310
		private TimeUnit _unit;
	}
}
