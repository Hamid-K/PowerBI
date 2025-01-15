using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000450 RID: 1104
	[Serializable]
	internal class MaxSizeAuditTargetOption : AuditTargetOption
	{
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x0016FBA3 File Offset: 0x0016DDA3
		// (set) Token: 0x060031F6 RID: 12790 RVA: 0x0016FBAB File Offset: 0x0016DDAB
		public bool IsUnlimited
		{
			get
			{
				return this._isUnlimited;
			}
			set
			{
				this._isUnlimited = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x0016FBB4 File Offset: 0x0016DDB4
		// (set) Token: 0x060031F8 RID: 12792 RVA: 0x0016FBBC File Offset: 0x0016DDBC
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

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x0016FBCC File Offset: 0x0016DDCC
		// (set) Token: 0x060031FA RID: 12794 RVA: 0x0016FBD4 File Offset: 0x0016DDD4
		public MemoryUnit Unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				this._unit = value;
			}
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x0016FBDD File Offset: 0x0016DDDD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x0016FBE9 File Offset: 0x0016DDE9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Size != null)
			{
				this.Size.Accept(visitor);
			}
		}

		// Token: 0x04001E8D RID: 7821
		private bool _isUnlimited;

		// Token: 0x04001E8E RID: 7822
		private Literal _size;

		// Token: 0x04001E8F RID: 7823
		private MemoryUnit _unit;
	}
}
