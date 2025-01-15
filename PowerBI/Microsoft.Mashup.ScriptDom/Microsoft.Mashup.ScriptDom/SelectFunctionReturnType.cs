using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001D9 RID: 473
	[Serializable]
	internal class SelectFunctionReturnType : FunctionReturnType
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x001601C3 File Offset: 0x0015E3C3
		// (set) Token: 0x06002310 RID: 8976 RVA: 0x001601CB File Offset: 0x0015E3CB
		public SelectStatement SelectStatement
		{
			get
			{
				return this._selectStatement;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._selectStatement = value;
			}
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x001601DB File Offset: 0x0015E3DB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x001601E7 File Offset: 0x0015E3E7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SelectStatement != null)
			{
				this.SelectStatement.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A53 RID: 6739
		private SelectStatement _selectStatement;
	}
}
