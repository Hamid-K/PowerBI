using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000243 RID: 579
	[Serializable]
	internal class UpdateTextStatement : TextModificationStatement
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x001632D8 File Offset: 0x001614D8
		// (set) Token: 0x060025B3 RID: 9651 RVA: 0x001632E0 File Offset: 0x001614E0
		public ScalarExpression InsertOffset
		{
			get
			{
				return this._insertOffset;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._insertOffset = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x001632F0 File Offset: 0x001614F0
		// (set) Token: 0x060025B5 RID: 9653 RVA: 0x001632F8 File Offset: 0x001614F8
		public ScalarExpression DeleteLength
		{
			get
			{
				return this._deleteLength;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._deleteLength = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x00163308 File Offset: 0x00161508
		// (set) Token: 0x060025B7 RID: 9655 RVA: 0x00163310 File Offset: 0x00161510
		public ColumnReferenceExpression SourceColumn
		{
			get
			{
				return this._sourceColumn;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sourceColumn = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x00163320 File Offset: 0x00161520
		// (set) Token: 0x060025B9 RID: 9657 RVA: 0x00163328 File Offset: 0x00161528
		public ValueExpression SourceParameter
		{
			get
			{
				return this._sourceParameter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sourceParameter = value;
			}
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x00163338 File Offset: 0x00161538
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x00163344 File Offset: 0x00161544
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.InsertOffset != null)
			{
				this.InsertOffset.Accept(visitor);
			}
			if (this.DeleteLength != null)
			{
				this.DeleteLength.Accept(visitor);
			}
			if (this.SourceColumn != null)
			{
				this.SourceColumn.Accept(visitor);
			}
			if (this.SourceParameter != null)
			{
				this.SourceParameter.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B1D RID: 6941
		private ScalarExpression _insertOffset;

		// Token: 0x04001B1E RID: 6942
		private ScalarExpression _deleteLength;

		// Token: 0x04001B1F RID: 6943
		private ColumnReferenceExpression _sourceColumn;

		// Token: 0x04001B20 RID: 6944
		private ValueExpression _sourceParameter;
	}
}
