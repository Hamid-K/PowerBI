using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001B1 RID: 433
	[Serializable]
	internal class ProcedureReferenceName : TSqlFragment
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x0015F1B1 File Offset: 0x0015D3B1
		// (set) Token: 0x0600222F RID: 8751 RVA: 0x0015F1B9 File Offset: 0x0015D3B9
		public ProcedureReference ProcedureReference
		{
			get
			{
				return this._procedureReference;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._procedureReference = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x0015F1C9 File Offset: 0x0015D3C9
		// (set) Token: 0x06002231 RID: 8753 RVA: 0x0015F1D1 File Offset: 0x0015D3D1
		public VariableReference ProcedureVariable
		{
			get
			{
				return this._procedureVariable;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._procedureVariable = value;
			}
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0015F1E1 File Offset: 0x0015D3E1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0015F1ED File Offset: 0x0015D3ED
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ProcedureReference != null)
			{
				this.ProcedureReference.Accept(visitor);
			}
			if (this.ProcedureVariable != null)
			{
				this.ProcedureVariable.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A14 RID: 6676
		private ProcedureReference _procedureReference;

		// Token: 0x04001A15 RID: 6677
		private VariableReference _procedureVariable;
	}
}
