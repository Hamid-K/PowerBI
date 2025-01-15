using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000223 RID: 547
	[Serializable]
	internal class BeginEndBlockStatement : TSqlStatement
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x001625E5 File Offset: 0x001607E5
		// (set) Token: 0x060024EF RID: 9455 RVA: 0x001625ED File Offset: 0x001607ED
		public StatementList StatementList
		{
			get
			{
				return this._statementList;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._statementList = value;
			}
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x001625FD File Offset: 0x001607FD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00162609 File Offset: 0x00160809
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.StatementList != null)
			{
				this.StatementList.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AE7 RID: 6887
		private StatementList _statementList;
	}
}
