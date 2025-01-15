using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200036C RID: 876
	[Serializable]
	internal class AssemblyEncryptionSource : EncryptionSource
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x0016A6E5 File Offset: 0x001688E5
		// (set) Token: 0x06002CB3 RID: 11443 RVA: 0x0016A6ED File Offset: 0x001688ED
		public Identifier Assembly
		{
			get
			{
				return this._assembly;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._assembly = value;
			}
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x0016A6FD File Offset: 0x001688FD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x0016A709 File Offset: 0x00168909
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Assembly != null)
			{
				this.Assembly.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D1A RID: 7450
		private Identifier _assembly;
	}
}
