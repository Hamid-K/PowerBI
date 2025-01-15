using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003AB RID: 939
	[Serializable]
	internal abstract class JoinTableReference : TableReference
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x0016C2C5 File Offset: 0x0016A4C5
		// (set) Token: 0x06002E4F RID: 11855 RVA: 0x0016C2CD File Offset: 0x0016A4CD
		public TableReference FirstTableReference
		{
			get
			{
				return this._firstTableReference;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._firstTableReference = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x0016C2DD File Offset: 0x0016A4DD
		// (set) Token: 0x06002E51 RID: 11857 RVA: 0x0016C2E5 File Offset: 0x0016A4E5
		public TableReference SecondTableReference
		{
			get
			{
				return this._secondTableReference;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._secondTableReference = value;
			}
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x0016C2F5 File Offset: 0x0016A4F5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.FirstTableReference != null)
			{
				this.FirstTableReference.Accept(visitor);
			}
			if (this.SecondTableReference != null)
			{
				this.SecondTableReference.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D97 RID: 7575
		private TableReference _firstTableReference;

		// Token: 0x04001D98 RID: 7576
		private TableReference _secondTableReference;
	}
}
