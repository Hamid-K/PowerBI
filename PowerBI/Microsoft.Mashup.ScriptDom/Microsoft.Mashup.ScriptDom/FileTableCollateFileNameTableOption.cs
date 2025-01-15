using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000287 RID: 647
	[Serializable]
	internal class FileTableCollateFileNameTableOption : TableOption
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x00164CDC File Offset: 0x00162EDC
		// (set) Token: 0x06002732 RID: 10034 RVA: 0x00164CE4 File Offset: 0x00162EE4
		public Identifier Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x00164CF4 File Offset: 0x00162EF4
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x00164D00 File Offset: 0x00162F00
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001B85 RID: 7045
		private Identifier _value;
	}
}
