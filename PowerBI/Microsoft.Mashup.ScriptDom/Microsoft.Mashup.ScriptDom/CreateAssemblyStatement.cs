using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000274 RID: 628
	[Serializable]
	internal class CreateAssemblyStatement : AssemblyStatement, IAuthorization
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x0016460E File Offset: 0x0016280E
		// (set) Token: 0x060026C9 RID: 9929 RVA: 0x00164616 File Offset: 0x00162816
		public Identifier Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._owner = value;
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x00164626 File Offset: 0x00162826
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x00164634 File Offset: 0x00162834
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			int i = 0;
			int count = base.Parameters.Count;
			while (i < count)
			{
				base.Parameters[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = base.Options.Count;
			while (j < count2)
			{
				base.Options[j].Accept(visitor);
				j++;
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
		}

		// Token: 0x04001B6A RID: 7018
		private Identifier _owner;
	}
}
