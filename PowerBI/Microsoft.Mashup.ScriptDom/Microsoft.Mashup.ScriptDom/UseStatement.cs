using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002FB RID: 763
	[Serializable]
	internal class UseStatement : TSqlStatement
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x001678B4 File Offset: 0x00165AB4
		// (set) Token: 0x060029CF RID: 10703 RVA: 0x001678BC File Offset: 0x00165ABC
		public Identifier DatabaseName
		{
			get
			{
				return this._databaseName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._databaseName = value;
			}
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x001678CC File Offset: 0x00165ACC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x001678D8 File Offset: 0x00165AD8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.DatabaseName != null)
			{
				this.DatabaseName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C3C RID: 7228
		private Identifier _databaseName;
	}
}
