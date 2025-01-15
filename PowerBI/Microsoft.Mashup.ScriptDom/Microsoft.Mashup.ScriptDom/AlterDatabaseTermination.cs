using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000324 RID: 804
	[Serializable]
	internal class AlterDatabaseTermination : TSqlFragment
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x00168700 File Offset: 0x00166900
		// (set) Token: 0x06002ACA RID: 10954 RVA: 0x00168708 File Offset: 0x00166908
		public bool ImmediateRollback
		{
			get
			{
				return this._immediateRollback;
			}
			set
			{
				this._immediateRollback = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x00168711 File Offset: 0x00166911
		// (set) Token: 0x06002ACC RID: 10956 RVA: 0x00168719 File Offset: 0x00166919
		public Literal RollbackAfter
		{
			get
			{
				return this._rollbackAfter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._rollbackAfter = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x00168729 File Offset: 0x00166929
		// (set) Token: 0x06002ACE RID: 10958 RVA: 0x00168731 File Offset: 0x00166931
		public bool NoWait
		{
			get
			{
				return this._noWait;
			}
			set
			{
				this._noWait = value;
			}
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x0016873A File Offset: 0x0016693A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x00168746 File Offset: 0x00166946
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.RollbackAfter != null)
			{
				this.RollbackAfter.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C81 RID: 7297
		private bool _immediateRollback;

		// Token: 0x04001C82 RID: 7298
		private Literal _rollbackAfter;

		// Token: 0x04001C83 RID: 7299
		private bool _noWait;
	}
}
