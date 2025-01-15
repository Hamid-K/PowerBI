using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200031C RID: 796
	[Serializable]
	internal class AlterDatabaseRebuildLogStatement : AlterDatabaseStatement
	{
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x001683C4 File Offset: 0x001665C4
		// (set) Token: 0x06002A95 RID: 10901 RVA: 0x001683CC File Offset: 0x001665CC
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

		// Token: 0x06002A96 RID: 10902 RVA: 0x001683DC File Offset: 0x001665DC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x001683E8 File Offset: 0x001665E8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FileDeclaration != null)
			{
				this.FileDeclaration.Accept(visitor);
			}
		}

		// Token: 0x04001C72 RID: 7282
		private FileDeclaration _fileDeclaration;
	}
}
