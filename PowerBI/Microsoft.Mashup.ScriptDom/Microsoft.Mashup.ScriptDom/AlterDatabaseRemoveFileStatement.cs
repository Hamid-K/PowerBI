using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000320 RID: 800
	[Serializable]
	internal class AlterDatabaseRemoveFileStatement : AlterDatabaseStatement
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06002AAD RID: 10925 RVA: 0x00168554 File Offset: 0x00166754
		// (set) Token: 0x06002AAE RID: 10926 RVA: 0x0016855C File Offset: 0x0016675C
		public Identifier File
		{
			get
			{
				return this._file;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._file = value;
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x0016856C File Offset: 0x0016676C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x00168578 File Offset: 0x00166778
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.File != null)
			{
				this.File.Accept(visitor);
			}
		}

		// Token: 0x04001C79 RID: 7289
		private Identifier _file;
	}
}
