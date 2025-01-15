using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000206 RID: 518
	[Serializable]
	internal class SemanticTableReference : TableReferenceWithAlias
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x0016153D File Offset: 0x0015F73D
		// (set) Token: 0x0600241B RID: 9243 RVA: 0x00161545 File Offset: 0x0015F745
		public SemanticFunctionType SemanticFunctionType
		{
			get
			{
				return this._semanticFunctionType;
			}
			set
			{
				this._semanticFunctionType = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x0016154E File Offset: 0x0015F74E
		// (set) Token: 0x0600241D RID: 9245 RVA: 0x00161556 File Offset: 0x0015F756
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x00161566 File Offset: 0x0015F766
		public IList<ColumnReferenceExpression> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x0016156E File Offset: 0x0015F76E
		// (set) Token: 0x06002420 RID: 9248 RVA: 0x00161576 File Offset: 0x0015F776
		public ScalarExpression SourceKey
		{
			get
			{
				return this._sourceKey;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sourceKey = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x00161586 File Offset: 0x0015F786
		// (set) Token: 0x06002422 RID: 9250 RVA: 0x0016158E File Offset: 0x0015F78E
		public ColumnReferenceExpression MatchedColumn
		{
			get
			{
				return this._matchedColumn;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._matchedColumn = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x0016159E File Offset: 0x0015F79E
		// (set) Token: 0x06002424 RID: 9252 RVA: 0x001615A6 File Offset: 0x0015F7A6
		public ScalarExpression MatchedKey
		{
			get
			{
				return this._matchedKey;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._matchedKey = value;
			}
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x001615B6 File Offset: 0x0015F7B6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x001615C4 File Offset: 0x0015F7C4
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
			if (this.SourceKey != null)
			{
				this.SourceKey.Accept(visitor);
			}
			if (this.MatchedColumn != null)
			{
				this.MatchedColumn.Accept(visitor);
			}
			if (this.MatchedKey != null)
			{
				this.MatchedKey.Accept(visitor);
			}
		}

		// Token: 0x04001AA3 RID: 6819
		private SemanticFunctionType _semanticFunctionType;

		// Token: 0x04001AA4 RID: 6820
		private SchemaObjectName _tableName;

		// Token: 0x04001AA5 RID: 6821
		private List<ColumnReferenceExpression> _columns = new List<ColumnReferenceExpression>();

		// Token: 0x04001AA6 RID: 6822
		private ScalarExpression _sourceKey;

		// Token: 0x04001AA7 RID: 6823
		private ColumnReferenceExpression _matchedColumn;

		// Token: 0x04001AA8 RID: 6824
		private ScalarExpression _matchedKey;
	}
}
