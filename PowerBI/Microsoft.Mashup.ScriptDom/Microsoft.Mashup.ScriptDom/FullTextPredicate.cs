using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001EC RID: 492
	[Serializable]
	internal class FullTextPredicate : BooleanExpression
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x00160995 File Offset: 0x0015EB95
		// (set) Token: 0x0600237F RID: 9087 RVA: 0x0016099D File Offset: 0x0015EB9D
		public FullTextFunctionType FullTextFunctionType
		{
			get
			{
				return this._fullTextFunctionType;
			}
			set
			{
				this._fullTextFunctionType = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x001609A6 File Offset: 0x0015EBA6
		public IList<ColumnReferenceExpression> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x001609AE File Offset: 0x0015EBAE
		// (set) Token: 0x06002382 RID: 9090 RVA: 0x001609B6 File Offset: 0x0015EBB6
		public ValueExpression Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x001609C6 File Offset: 0x0015EBC6
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x001609CE File Offset: 0x0015EBCE
		public ValueExpression LanguageTerm
		{
			get
			{
				return this._languageTerm;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._languageTerm = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x001609DE File Offset: 0x0015EBDE
		// (set) Token: 0x06002386 RID: 9094 RVA: 0x001609E6 File Offset: 0x0015EBE6
		public StringLiteral PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._propertyName = value;
			}
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x001609F6 File Offset: 0x0015EBF6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00160A04 File Offset: 0x0015EC04
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			if (this.LanguageTerm != null)
			{
				this.LanguageTerm.Accept(visitor);
			}
			if (this.PropertyName != null)
			{
				this.PropertyName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A74 RID: 6772
		private FullTextFunctionType _fullTextFunctionType;

		// Token: 0x04001A75 RID: 6773
		private List<ColumnReferenceExpression> _columns = new List<ColumnReferenceExpression>();

		// Token: 0x04001A76 RID: 6774
		private ValueExpression _value;

		// Token: 0x04001A77 RID: 6775
		private ValueExpression _languageTerm;

		// Token: 0x04001A78 RID: 6776
		private StringLiteral _propertyName;
	}
}
