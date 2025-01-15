using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000339 RID: 825
	[Serializable]
	internal class MaxSizeDatabaseOption : DatabaseOption
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x00168CEC File Offset: 0x00166EEC
		// (set) Token: 0x06002B42 RID: 11074 RVA: 0x00168CF4 File Offset: 0x00166EF4
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

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x00168D04 File Offset: 0x00166F04
		// (set) Token: 0x06002B44 RID: 11076 RVA: 0x00168D0C File Offset: 0x00166F0C
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

		// Token: 0x06002B45 RID: 11077 RVA: 0x00168D15 File Offset: 0x00166F15
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x00168D21 File Offset: 0x00166F21
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.MaxSize != null)
			{
				this.MaxSize.Accept(visitor);
			}
		}

		// Token: 0x04001C9F RID: 7327
		private Literal _maxSize;

		// Token: 0x04001CA0 RID: 7328
		private MemoryUnit _units;
	}
}
