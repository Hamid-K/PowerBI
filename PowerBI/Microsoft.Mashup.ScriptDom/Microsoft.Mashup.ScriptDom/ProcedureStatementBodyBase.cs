using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001C0 RID: 448
	[Serializable]
	internal abstract class ProcedureStatementBodyBase : TSqlStatement
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x0015F7C0 File Offset: 0x0015D9C0
		public IList<ProcedureParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x0015F7C8 File Offset: 0x0015D9C8
		// (set) Token: 0x0600228E RID: 8846 RVA: 0x0015F7D0 File Offset: 0x0015D9D0
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x0015F7E0 File Offset: 0x0015D9E0
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x0015F7E8 File Offset: 0x0015D9E8
		public MethodSpecifier MethodSpecifier
		{
			get
			{
				return this._methodSpecifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._methodSpecifier = value;
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0015F7F8 File Offset: 0x0015D9F8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
			if (this.StatementList != null)
			{
				this.StatementList.Accept(visitor);
			}
			if (this.MethodSpecifier != null)
			{
				this.MethodSpecifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A30 RID: 6704
		private List<ProcedureParameter> _parameters = new List<ProcedureParameter>();

		// Token: 0x04001A31 RID: 6705
		private StatementList _statementList;

		// Token: 0x04001A32 RID: 6706
		private MethodSpecifier _methodSpecifier;
	}
}
