using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000318 RID: 792
	[Serializable]
	internal class FileGrowthFileDeclarationOption : FileDeclarationOption
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06002A78 RID: 10872 RVA: 0x0016821F File Offset: 0x0016641F
		// (set) Token: 0x06002A79 RID: 10873 RVA: 0x00168227 File Offset: 0x00166427
		public Literal GrowthIncrement
		{
			get
			{
				return this._growthIncrement;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._growthIncrement = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x00168237 File Offset: 0x00166437
		// (set) Token: 0x06002A7B RID: 10875 RVA: 0x0016823F File Offset: 0x0016643F
		public MemoryUnit Units
		{
			get
			{
				return this._units;
			}
			set
			{
				this._units = value;
			}
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00168248 File Offset: 0x00166448
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x00168254 File Offset: 0x00166454
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.GrowthIncrement != null)
			{
				this.GrowthIncrement.Accept(visitor);
			}
		}

		// Token: 0x04001C69 RID: 7273
		private Literal _growthIncrement;

		// Token: 0x04001C6A RID: 7274
		private MemoryUnit _units;
	}
}
