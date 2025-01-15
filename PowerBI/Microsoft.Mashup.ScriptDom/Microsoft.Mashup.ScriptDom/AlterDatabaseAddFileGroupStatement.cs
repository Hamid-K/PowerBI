using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200031E RID: 798
	[Serializable]
	internal class AlterDatabaseAddFileGroupStatement : AlterDatabaseStatement
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x001684B1 File Offset: 0x001666B1
		// (set) Token: 0x06002AA2 RID: 10914 RVA: 0x001684B9 File Offset: 0x001666B9
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

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x001684C9 File Offset: 0x001666C9
		// (set) Token: 0x06002AA4 RID: 10916 RVA: 0x001684D1 File Offset: 0x001666D1
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

		// Token: 0x06002AA5 RID: 10917 RVA: 0x001684DA File Offset: 0x001666DA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x001684E6 File Offset: 0x001666E6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FileGroup != null)
			{
				this.FileGroup.Accept(visitor);
			}
		}

		// Token: 0x04001C76 RID: 7286
		private Identifier _fileGroup;

		// Token: 0x04001C77 RID: 7287
		private bool _containsFileStream;
	}
}
