using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200033E RID: 830
	[Serializable]
	internal class ColumnStorageOptions : TSqlFragment
	{
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x001690AB File Offset: 0x001672AB
		// (set) Token: 0x06002B78 RID: 11128 RVA: 0x001690B3 File Offset: 0x001672B3
		public bool IsFileStream
		{
			get
			{
				return this._isFileStream;
			}
			set
			{
				this._isFileStream = value;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x001690BC File Offset: 0x001672BC
		// (set) Token: 0x06002B7A RID: 11130 RVA: 0x001690C4 File Offset: 0x001672C4
		public SparseColumnOption SparseOption
		{
			get
			{
				return this._sparseOption;
			}
			set
			{
				this._sparseOption = value;
			}
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x001690CD File Offset: 0x001672CD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x001690D9 File Offset: 0x001672D9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CB3 RID: 7347
		private bool _isFileStream;

		// Token: 0x04001CB4 RID: 7348
		private SparseColumnOption _sparseOption;
	}
}
