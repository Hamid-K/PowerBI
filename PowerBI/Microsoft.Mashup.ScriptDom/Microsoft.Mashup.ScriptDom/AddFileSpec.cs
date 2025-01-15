using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000279 RID: 633
	[Serializable]
	internal class AddFileSpec : TSqlFragment
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x00164804 File Offset: 0x00162A04
		// (set) Token: 0x060026E4 RID: 9956 RVA: 0x0016480C File Offset: 0x00162A0C
		public ScalarExpression File
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

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x0016481C File Offset: 0x00162A1C
		// (set) Token: 0x060026E6 RID: 9958 RVA: 0x00164824 File Offset: 0x00162A24
		public Literal FileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileName = value;
			}
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x00164834 File Offset: 0x00162A34
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x00164840 File Offset: 0x00162A40
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.File != null)
			{
				this.File.Accept(visitor);
			}
			if (this.FileName != null)
			{
				this.FileName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B71 RID: 7025
		private ScalarExpression _file;

		// Token: 0x04001B72 RID: 7026
		private Literal _fileName;
	}
}
