using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000280 RID: 640
	[Serializable]
	internal class AlterTableChangeTrackingModificationStatement : AlterTableStatement
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x00164AFE File Offset: 0x00162CFE
		// (set) Token: 0x0600270F RID: 9999 RVA: 0x00164B06 File Offset: 0x00162D06
		public bool IsEnable
		{
			get
			{
				return this._isEnable;
			}
			set
			{
				this._isEnable = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06002710 RID: 10000 RVA: 0x00164B0F File Offset: 0x00162D0F
		// (set) Token: 0x06002711 RID: 10001 RVA: 0x00164B17 File Offset: 0x00162D17
		public OptionState TrackColumnsUpdated
		{
			get
			{
				return this._trackColumnsUpdated;
			}
			set
			{
				this._trackColumnsUpdated = value;
			}
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x00164B20 File Offset: 0x00162D20
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x00164B2C File Offset: 0x00162D2C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.SchemaObjectName != null)
			{
				base.SchemaObjectName.Accept(visitor);
			}
		}

		// Token: 0x04001B7D RID: 7037
		private bool _isEnable;

		// Token: 0x04001B7E RID: 7038
		private OptionState _trackColumnsUpdated;
	}
}
