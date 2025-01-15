using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001B9 RID: 441
	[Serializable]
	internal class TriggerObject : TSqlFragment
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x0015F4E0 File Offset: 0x0015D6E0
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x0015F4E8 File Offset: 0x0015D6E8
		public SchemaObjectName Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x0015F4F8 File Offset: 0x0015D6F8
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x0015F500 File Offset: 0x0015D700
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

		// Token: 0x06002260 RID: 8800 RVA: 0x0015F509 File Offset: 0x0015D709
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0015F515 File Offset: 0x0015D715
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A21 RID: 6689
		private SchemaObjectName _name;

		// Token: 0x04001A22 RID: 6690
		private TriggerScope _triggerScope;
	}
}
