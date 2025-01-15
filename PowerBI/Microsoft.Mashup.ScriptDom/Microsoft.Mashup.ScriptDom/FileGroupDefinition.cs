using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000319 RID: 793
	[Serializable]
	internal class FileGroupDefinition : TSqlFragment
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x00168279 File Offset: 0x00166479
		// (set) Token: 0x06002A80 RID: 10880 RVA: 0x00168281 File Offset: 0x00166481
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x00168291 File Offset: 0x00166491
		public IList<FileDeclaration> FileDeclarations
		{
			get
			{
				return this._fileDeclarations;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x00168299 File Offset: 0x00166499
		// (set) Token: 0x06002A83 RID: 10883 RVA: 0x001682A1 File Offset: 0x001664A1
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
			set
			{
				this._isDefault = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x001682AA File Offset: 0x001664AA
		// (set) Token: 0x06002A85 RID: 10885 RVA: 0x001682B2 File Offset: 0x001664B2
		public bool ContainsFileStream
		{
			get
			{
				return this._containsFileStream;
			}
			set
			{
				this._containsFileStream = value;
			}
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x001682BB File Offset: 0x001664BB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x001682C8 File Offset: 0x001664C8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.FileDeclarations.Count;
			while (i < count)
			{
				this.FileDeclarations[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C6B RID: 7275
		private Identifier _name;

		// Token: 0x04001C6C RID: 7276
		private List<FileDeclaration> _fileDeclarations = new List<FileDeclaration>();

		// Token: 0x04001C6D RID: 7277
		private bool _isDefault;

		// Token: 0x04001C6E RID: 7278
		private bool _containsFileStream;
	}
}
