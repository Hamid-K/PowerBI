using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001B2 RID: 434
	[Serializable]
	internal class ExecutableProcedureReference : ExecutableEntity
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x0015F226 File Offset: 0x0015D426
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x0015F22E File Offset: 0x0015D42E
		public ProcedureReferenceName ProcedureReference
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x0015F23E File Offset: 0x0015D43E
		// (set) Token: 0x06002238 RID: 8760 RVA: 0x0015F246 File Offset: 0x0015D446
		public AdHocDataSource AdHocDataSource
		{
			get
			{
				return this._adHocDataSource;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._adHocDataSource = value;
			}
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0015F256 File Offset: 0x0015D456
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0015F262 File Offset: 0x0015D462
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.ProcedureReference != null)
			{
				this.ProcedureReference.Accept(visitor);
			}
			if (this.AdHocDataSource != null)
			{
				this.AdHocDataSource.Accept(visitor);
			}
		}

		// Token: 0x04001A16 RID: 6678
		private ProcedureReferenceName _procedureReference;

		// Token: 0x04001A17 RID: 6679
		private AdHocDataSource _adHocDataSource;
	}
}
