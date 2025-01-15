using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200031D RID: 797
	[Serializable]
	internal class AlterDatabaseAddFileStatement : AlterDatabaseStatement
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x0016840D File Offset: 0x0016660D
		public IList<FileDeclaration> FileDeclarations
		{
			get
			{
				return this._fileDeclarations;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x00168415 File Offset: 0x00166615
		// (set) Token: 0x06002A9B RID: 10907 RVA: 0x0016841D File Offset: 0x0016661D
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

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x0016842D File Offset: 0x0016662D
		// (set) Token: 0x06002A9D RID: 10909 RVA: 0x00168435 File Offset: 0x00166635
		public bool IsLog
		{
			get
			{
				return this._isLog;
			}
			set
			{
				this._isLog = value;
			}
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x0016843E File Offset: 0x0016663E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x0016844C File Offset: 0x0016664C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.FileDeclarations.Count;
			while (i < count)
			{
				this.FileDeclarations[i].Accept(visitor);
				i++;
			}
			if (this.FileGroup != null)
			{
				this.FileGroup.Accept(visitor);
			}
		}

		// Token: 0x04001C73 RID: 7283
		private List<FileDeclaration> _fileDeclarations = new List<FileDeclaration>();

		// Token: 0x04001C74 RID: 7284
		private Identifier _fileGroup;

		// Token: 0x04001C75 RID: 7285
		private bool _isLog;
	}
}
