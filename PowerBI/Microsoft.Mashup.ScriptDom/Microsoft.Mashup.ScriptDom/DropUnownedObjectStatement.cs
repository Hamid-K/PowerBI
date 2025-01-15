using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002CB RID: 715
	[Serializable]
	internal abstract class DropUnownedObjectStatement : TSqlStatement
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x0016674C File Offset: 0x0016494C
		// (set) Token: 0x060028C7 RID: 10439 RVA: 0x00166754 File Offset: 0x00164954
		public Identifier Name
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

		// Token: 0x060028C8 RID: 10440 RVA: 0x00166764 File Offset: 0x00164964
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BF7 RID: 7159
		private Identifier _name;
	}
}
