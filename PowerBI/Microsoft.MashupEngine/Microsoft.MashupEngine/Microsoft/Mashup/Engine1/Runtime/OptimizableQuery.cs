using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015A0 RID: 5536
	internal class OptimizableQuery : DataSourceQuery
	{
		// Token: 0x06008ADE RID: 35550 RVA: 0x001D3915 File Offset: 0x001D1B15
		public OptimizableQuery(Query query)
		{
			this.query = query;
		}

		// Token: 0x1700249B RID: 9371
		// (get) Token: 0x06008ADF RID: 35551 RVA: 0x001D3924 File Offset: 0x001D1B24
		public Query Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x1700249C RID: 9372
		// (get) Token: 0x06008AE0 RID: 35552 RVA: 0x001D392C File Offset: 0x001D1B2C
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.query.QueryDomain;
			}
		}

		// Token: 0x06008AE1 RID: 35553 RVA: 0x001D3939 File Offset: 0x001D1B39
		public override TypeValue GetColumnType(int column)
		{
			return this.query.GetColumnType(column);
		}

		// Token: 0x1700249D RID: 9373
		// (get) Token: 0x06008AE2 RID: 35554 RVA: 0x001D3947 File Offset: 0x001D1B47
		public override Keys Columns
		{
			get
			{
				return this.query.Columns;
			}
		}

		// Token: 0x06008AE3 RID: 35555 RVA: 0x001D3954 File Offset: 0x001D1B54
		public override IEnumerable<IValueReference> GetRows()
		{
			return this.query.GetRows();
		}

		// Token: 0x1700249E RID: 9374
		// (get) Token: 0x06008AE4 RID: 35556 RVA: 0x001D3961 File Offset: 0x001D1B61
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.query.TableKeys;
			}
		}

		// Token: 0x1700249F RID: 9375
		// (get) Token: 0x06008AE5 RID: 35557 RVA: 0x001D396E File Offset: 0x001D1B6E
		public override IEngineHost EngineHost
		{
			get
			{
				return this.query.GetEngineHost();
			}
		}

		// Token: 0x06008AE6 RID: 35558 RVA: 0x001D397B File Offset: 0x001D1B7B
		public override bool TryGetExpression(out IExpression expression)
		{
			expression = QueryToExpressionVisitor.ToExpression(this.QueryDomain.Optimize(this.Query));
			return true;
		}

		// Token: 0x04004C22 RID: 19490
		private readonly Query query;
	}
}
