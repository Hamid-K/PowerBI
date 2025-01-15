using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200033F RID: 831
	[Serializable]
	internal abstract class ConstraintDefinition : TSqlFragment
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06002B7E RID: 11134 RVA: 0x001690EA File Offset: 0x001672EA
		// (set) Token: 0x06002B7F RID: 11135 RVA: 0x001690F2 File Offset: 0x001672F2
		public Identifier ConstraintIdentifier
		{
			get
			{
				return this._constraintIdentifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._constraintIdentifier = value;
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00169102 File Offset: 0x00167302
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ConstraintIdentifier != null)
			{
				this.ConstraintIdentifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CB5 RID: 7349
		private Identifier _constraintIdentifier;
	}
}
