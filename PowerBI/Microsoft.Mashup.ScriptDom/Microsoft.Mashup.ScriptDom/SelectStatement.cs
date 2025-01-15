using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001EF RID: 495
	[Serializable]
	internal class SelectStatement : StatementWithCtesAndXmlNamespaces
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x00160B8D File Offset: 0x0015ED8D
		// (set) Token: 0x06002397 RID: 9111 RVA: 0x00160B95 File Offset: 0x0015ED95
		public QueryExpression QueryExpression
		{
			get
			{
				return this._queryExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._queryExpression = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x00160BA5 File Offset: 0x0015EDA5
		// (set) Token: 0x06002399 RID: 9113 RVA: 0x00160BAD File Offset: 0x0015EDAD
		public SchemaObjectName Into
		{
			get
			{
				return this._into;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._into = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x00160BBD File Offset: 0x0015EDBD
		public IList<ComputeClause> ComputeClauses
		{
			get
			{
				return this._computeClauses;
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x00160BC5 File Offset: 0x0015EDC5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x00160BD4 File Offset: 0x0015EDD4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.QueryExpression != null)
			{
				this.QueryExpression.Accept(visitor);
			}
			if (this.Into != null)
			{
				this.Into.Accept(visitor);
			}
			int i = 0;
			int count = this.ComputeClauses.Count;
			while (i < count)
			{
				this.ComputeClauses[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A7D RID: 6781
		private QueryExpression _queryExpression;

		// Token: 0x04001A7E RID: 6782
		private SchemaObjectName _into;

		// Token: 0x04001A7F RID: 6783
		private List<ComputeClause> _computeClauses = new List<ComputeClause>();
	}
}
