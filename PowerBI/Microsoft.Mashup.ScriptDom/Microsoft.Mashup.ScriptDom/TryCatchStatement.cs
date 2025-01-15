using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000294 RID: 660
	[Serializable]
	internal class TryCatchStatement : TSqlStatement
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x00165275 File Offset: 0x00163475
		// (set) Token: 0x06002787 RID: 10119 RVA: 0x0016527D File Offset: 0x0016347D
		public StatementList TryStatements
		{
			get
			{
				return this._tryStatements;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._tryStatements = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06002788 RID: 10120 RVA: 0x0016528D File Offset: 0x0016348D
		// (set) Token: 0x06002789 RID: 10121 RVA: 0x00165295 File Offset: 0x00163495
		public StatementList CatchStatements
		{
			get
			{
				return this._catchStatements;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._catchStatements = value;
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x001652A5 File Offset: 0x001634A5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x001652B1 File Offset: 0x001634B1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.TryStatements != null)
			{
				this.TryStatements.Accept(visitor);
			}
			if (this.CatchStatements != null)
			{
				this.CatchStatements.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B9F RID: 7071
		private StatementList _tryStatements;

		// Token: 0x04001BA0 RID: 7072
		private StatementList _catchStatements;
	}
}
