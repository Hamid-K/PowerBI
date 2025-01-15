using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200048E RID: 1166
	[Serializable]
	internal class AlterAvailabilityGroupStatement : AvailabilityGroupStatement
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x0017119B File Offset: 0x0016F39B
		// (set) Token: 0x06003356 RID: 13142 RVA: 0x001711A3 File Offset: 0x0016F3A3
		public AlterAvailabilityGroupStatementType AlterAvailabilityGroupStatementType
		{
			get
			{
				return this._alterAvailabilityGroupStatementType;
			}
			set
			{
				this._alterAvailabilityGroupStatementType = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x001711AC File Offset: 0x0016F3AC
		// (set) Token: 0x06003358 RID: 13144 RVA: 0x001711B4 File Offset: 0x0016F3B4
		public AlterAvailabilityGroupAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._action = value;
			}
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x001711C4 File Offset: 0x0016F3C4
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x001711D0 File Offset: 0x0016F3D0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Action != null)
			{
				this.Action.Accept(visitor);
			}
		}

		// Token: 0x04001EED RID: 7917
		private AlterAvailabilityGroupStatementType _alterAvailabilityGroupStatementType;

		// Token: 0x04001EEE RID: 7918
		private AlterAvailabilityGroupAction _action;
	}
}
