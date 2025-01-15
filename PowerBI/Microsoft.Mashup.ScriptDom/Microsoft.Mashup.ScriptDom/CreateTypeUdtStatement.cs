using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000296 RID: 662
	[Serializable]
	internal class CreateTypeUdtStatement : CreateTypeStatement
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x00165327 File Offset: 0x00163527
		// (set) Token: 0x06002792 RID: 10130 RVA: 0x0016532F File Offset: 0x0016352F
		public AssemblyName AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._assemblyName = value;
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x0016533F File Offset: 0x0016353F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x0016534B File Offset: 0x0016354B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (this.AssemblyName != null)
			{
				this.AssemblyName.Accept(visitor);
			}
		}

		// Token: 0x04001BA2 RID: 7074
		private AssemblyName _assemblyName;
	}
}
