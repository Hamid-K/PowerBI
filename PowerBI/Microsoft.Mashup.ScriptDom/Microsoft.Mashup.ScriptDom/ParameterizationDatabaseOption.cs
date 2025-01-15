using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000331 RID: 817
	[Serializable]
	internal class ParameterizationDatabaseOption : DatabaseOption
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x00168AC2 File Offset: 0x00166CC2
		// (set) Token: 0x06002B18 RID: 11032 RVA: 0x00168ACA File Offset: 0x00166CCA
		public bool IsSimple
		{
			get
			{
				return this._isSimple;
			}
			set
			{
				this._isSimple = value;
			}
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x00168AD3 File Offset: 0x00166CD3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x00168ADF File Offset: 0x00166CDF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C95 RID: 7317
		private bool _isSimple;
	}
}
