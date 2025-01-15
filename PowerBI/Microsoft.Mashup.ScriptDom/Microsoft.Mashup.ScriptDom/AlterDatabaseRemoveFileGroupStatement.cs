using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200031F RID: 799
	[Serializable]
	internal class AlterDatabaseRemoveFileGroupStatement : AlterDatabaseStatement
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x0016850B File Offset: 0x0016670B
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x00168513 File Offset: 0x00166713
		public Identifier FileGroup
		{
			get
			{
				return this._fileGroup;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileGroup = value;
			}
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x00168523 File Offset: 0x00166723
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0016852F File Offset: 0x0016672F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FileGroup != null)
			{
				this.FileGroup.Accept(visitor);
			}
		}

		// Token: 0x04001C78 RID: 7288
		private Identifier _fileGroup;
	}
}
