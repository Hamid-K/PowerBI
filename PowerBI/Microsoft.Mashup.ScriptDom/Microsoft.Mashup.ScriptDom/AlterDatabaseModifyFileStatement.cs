using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000322 RID: 802
	[Serializable]
	internal class AlterDatabaseModifyFileStatement : AlterDatabaseStatement
	{
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x001685E6 File Offset: 0x001667E6
		// (set) Token: 0x06002AB8 RID: 10936 RVA: 0x001685EE File Offset: 0x001667EE
		public FileDeclaration FileDeclaration
		{
			get
			{
				return this._fileDeclaration;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileDeclaration = value;
			}
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x001685FE File Offset: 0x001667FE
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x0016860A File Offset: 0x0016680A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FileDeclaration != null)
			{
				this.FileDeclaration.Accept(visitor);
			}
		}

		// Token: 0x04001C7B RID: 7291
		private FileDeclaration _fileDeclaration;
	}
}
