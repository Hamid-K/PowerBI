using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000498 RID: 1176
	[Serializable]
	internal class AlterAvailabilityGroupAction : TSqlFragment
	{
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x00171417 File Offset: 0x0016F617
		// (set) Token: 0x06003389 RID: 13193 RVA: 0x0017141F File Offset: 0x0016F61F
		public AlterAvailabilityGroupActionType ActionType
		{
			get
			{
				return this._actionType;
			}
			set
			{
				this._actionType = value;
			}
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x00171428 File Offset: 0x0016F628
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x00171434 File Offset: 0x0016F634
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EF9 RID: 7929
		private AlterAvailabilityGroupActionType _actionType;
	}
}
