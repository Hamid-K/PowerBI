using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000288 RID: 648
	[Serializable]
	internal class FileTableConstraintNameTableOption : TableOption
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x00164D25 File Offset: 0x00162F25
		// (set) Token: 0x06002737 RID: 10039 RVA: 0x00164D2D File Offset: 0x00162F2D
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

		// Token: 0x06002738 RID: 10040 RVA: 0x00164D3D File Offset: 0x00162F3D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x00164D49 File Offset: 0x00162F49
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001B86 RID: 7046
		private Identifier _value;
	}
}
