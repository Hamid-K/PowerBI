using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002F6 RID: 758
	[Serializable]
	internal class DropTriggerStatement : DropObjectsStatement
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x0016760E File Offset: 0x0016580E
		// (set) Token: 0x060029A7 RID: 10663 RVA: 0x00167616 File Offset: 0x00165816
		public TriggerScope TriggerScope
		{
			get
			{
				return this._triggerScope;
			}
			set
			{
				this._triggerScope = value;
			}
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x0016761F File Offset: 0x0016581F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x0016762B File Offset: 0x0016582B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C2F RID: 7215
		private TriggerScope _triggerScope;
	}
}
