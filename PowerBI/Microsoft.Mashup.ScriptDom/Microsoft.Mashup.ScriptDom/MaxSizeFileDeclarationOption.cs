using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000317 RID: 791
	[Serializable]
	internal class MaxSizeFileDeclarationOption : FileDeclarationOption
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06002A6F RID: 10863 RVA: 0x001681B4 File Offset: 0x001663B4
		// (set) Token: 0x06002A70 RID: 10864 RVA: 0x001681BC File Offset: 0x001663BC
		public Literal MaxSize
		{
			get
			{
				return this._maxSize;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._maxSize = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06002A71 RID: 10865 RVA: 0x001681CC File Offset: 0x001663CC
		// (set) Token: 0x06002A72 RID: 10866 RVA: 0x001681D4 File Offset: 0x001663D4
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

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x001681DD File Offset: 0x001663DD
		// (set) Token: 0x06002A74 RID: 10868 RVA: 0x001681E5 File Offset: 0x001663E5
		public bool Unlimited
		{
			get
			{
				return this._unlimited;
			}
			set
			{
				this._unlimited = value;
			}
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x001681EE File Offset: 0x001663EE
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x001681FA File Offset: 0x001663FA
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.MaxSize != null)
			{
				this.MaxSize.Accept(visitor);
			}
		}

		// Token: 0x04001C66 RID: 7270
		private Literal _maxSize;

		// Token: 0x04001C67 RID: 7271
		private MemoryUnit _units;

		// Token: 0x04001C68 RID: 7272
		private bool _unlimited;
	}
}
