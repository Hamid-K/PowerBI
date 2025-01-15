using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001A9 RID: 425
	[Serializable]
	internal class ResultSetDefinition : TSqlFragment
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x0015EE4D File Offset: 0x0015D04D
		// (set) Token: 0x060021FC RID: 8700 RVA: 0x0015EE55 File Offset: 0x0015D055
		public ResultSetType ResultSetType
		{
			get
			{
				return this._resultSetType;
			}
			set
			{
				this._resultSetType = value;
			}
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0015EE5E File Offset: 0x0015D05E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0015EE6A File Offset: 0x0015D06A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A05 RID: 6661
		private ResultSetType _resultSetType;
	}
}
