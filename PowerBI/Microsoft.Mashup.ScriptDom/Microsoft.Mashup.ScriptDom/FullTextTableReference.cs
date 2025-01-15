using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000205 RID: 517
	[Serializable]
	internal class FullTextTableReference : TableReferenceWithAlias
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x001613E8 File Offset: 0x0015F5E8
		// (set) Token: 0x0600240B RID: 9227 RVA: 0x001613F0 File Offset: 0x0015F5F0
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x001613F9 File Offset: 0x0015F5F9
		// (set) Token: 0x0600240D RID: 9229 RVA: 0x00161401 File Offset: 0x0015F601
		public SchemaObjectName TableName
		{
			get
			{
				return this._tableName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._tableName = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x00161411 File Offset: 0x0015F611
		public IList<ColumnReferenceExpression> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x00161419 File Offset: 0x0015F619
		// (set) Token: 0x06002410 RID: 9232 RVA: 0x00161421 File Offset: 0x0015F621
		public ValueExpression SearchCondition
		{
			get
			{
				return this._searchCondition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._searchCondition = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x00161431 File Offset: 0x0015F631
		// (set) Token: 0x06002412 RID: 9234 RVA: 0x00161439 File Offset: 0x0015F639
		public ValueExpression TopN
		{
			get
			{
				return this._topN;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._topN = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x00161449 File Offset: 0x0015F649
		// (set) Token: 0x06002414 RID: 9236 RVA: 0x00161451 File Offset: 0x0015F651
		public ValueExpression Language
		{
			get
			{
				return this._language;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._language = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x00161461 File Offset: 0x0015F661
		// (set) Token: 0x06002416 RID: 9238 RVA: 0x00161469 File Offset: 0x0015F669
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

		// Token: 0x06002417 RID: 9239 RVA: 0x00161479 File Offset: 0x0015F679
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x00161488 File Offset: 0x0015F688
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.TableName != null)
			{
				this.TableName.Accept(visitor);
			}
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			if (this.SearchCondition != null)
			{
				this.SearchCondition.Accept(visitor);
			}
			if (this.TopN != null)
			{
				this.TopN.Accept(visitor);
			}
			if (this.Language != null)
			{
				this.Language.Accept(visitor);
			}
			if (this.PropertyName != null)
			{
				this.PropertyName.Accept(visitor);
			}
		}

		// Token: 0x04001A9C RID: 6812
		private FullTextFunctionType _fullTextFunctionType;

		// Token: 0x04001A9D RID: 6813
		private SchemaObjectName _tableName;

		// Token: 0x04001A9E RID: 6814
		private List<ColumnReferenceExpression> _columns = new List<ColumnReferenceExpression>();

		// Token: 0x04001A9F RID: 6815
		private ValueExpression _searchCondition;

		// Token: 0x04001AA0 RID: 6816
		private ValueExpression _topN;

		// Token: 0x04001AA1 RID: 6817
		private ValueExpression _language;

		// Token: 0x04001AA2 RID: 6818
		private StringLiteral _propertyName;
	}
}
