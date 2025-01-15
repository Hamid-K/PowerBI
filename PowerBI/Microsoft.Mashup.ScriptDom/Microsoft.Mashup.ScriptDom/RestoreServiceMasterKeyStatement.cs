using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000424 RID: 1060
	[Serializable]
	internal class RestoreServiceMasterKeyStatement : BackupRestoreMasterKeyStatementBase
	{
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x0016EF75 File Offset: 0x0016D175
		// (set) Token: 0x0600311E RID: 12574 RVA: 0x0016EF7D File Offset: 0x0016D17D
		public bool IsForce
		{
			get
			{
				return this._isForce;
			}
			set
			{
				this._isForce = value;
			}
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x0016EF86 File Offset: 0x0016D186
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x0016EF92 File Offset: 0x0016D192
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E5B RID: 7771
		private bool _isForce;
	}
}
