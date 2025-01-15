using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000316 RID: 790
	[Serializable]
	internal class SizeFileDeclarationOption : FileDeclarationOption
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x0016815A File Offset: 0x0016635A
		// (set) Token: 0x06002A69 RID: 10857 RVA: 0x00168162 File Offset: 0x00166362
		public Literal Size
		{
			get
			{
				return this._size;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._size = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x00168172 File Offset: 0x00166372
		// (set) Token: 0x06002A6B RID: 10859 RVA: 0x0016817A File Offset: 0x0016637A
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

		// Token: 0x06002A6C RID: 10860 RVA: 0x00168183 File Offset: 0x00166383
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x0016818F File Offset: 0x0016638F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Size != null)
			{
				this.Size.Accept(visitor);
			}
		}

		// Token: 0x04001C64 RID: 7268
		private Literal _size;

		// Token: 0x04001C65 RID: 7269
		private MemoryUnit _units;
	}
}
