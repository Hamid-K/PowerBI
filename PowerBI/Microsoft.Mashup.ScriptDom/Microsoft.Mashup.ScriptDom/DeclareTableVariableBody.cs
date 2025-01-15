using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001DB RID: 475
	[Serializable]
	internal class DeclareTableVariableBody : TSqlFragment
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x001602AE File Offset: 0x0015E4AE
		// (set) Token: 0x0600231A RID: 8986 RVA: 0x001602B6 File Offset: 0x0015E4B6
		public Identifier VariableName
		{
			get
			{
				return this._variableName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._variableName = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x001602C6 File Offset: 0x0015E4C6
		// (set) Token: 0x0600231C RID: 8988 RVA: 0x001602CE File Offset: 0x0015E4CE
		public bool AsDefined
		{
			get
			{
				return this._asDefined;
			}
			set
			{
				this._asDefined = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x001602D7 File Offset: 0x0015E4D7
		// (set) Token: 0x0600231E RID: 8990 RVA: 0x001602DF File Offset: 0x0015E4DF
		public TableDefinition Definition
		{
			get
			{
				return this._definition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._definition = value;
			}
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x001602EF File Offset: 0x0015E4EF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x001602FB File Offset: 0x0015E4FB
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.VariableName != null)
			{
				this.VariableName.Accept(visitor);
			}
			if (this.Definition != null)
			{
				this.Definition.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A56 RID: 6742
		private Identifier _variableName;

		// Token: 0x04001A57 RID: 6743
		private bool _asDefined;

		// Token: 0x04001A58 RID: 6744
		private TableDefinition _definition;
	}
}
