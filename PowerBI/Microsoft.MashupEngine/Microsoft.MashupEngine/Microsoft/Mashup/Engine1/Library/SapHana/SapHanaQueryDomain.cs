using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000476 RID: 1142
	internal sealed class SapHanaQueryDomain : OdbcQueryDomain
	{
		// Token: 0x06002600 RID: 9728 RVA: 0x0006DF96 File Offset: 0x0006C196
		public static OdbcQueryDomain NewCubeQueryDomain(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube, bool useSemanticSet)
		{
			return new SapHanaQueryDomain(dataSource, cube, useSemanticSet);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x0006DFA0 File Offset: 0x0006C1A0
		public static OdbcQueryDomain NewRelationalQueryDomain(SapHanaOdbcDataSource dataSource)
		{
			return new SapHanaQueryDomain(dataSource, null, false);
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x0006DFAC File Offset: 0x0006C1AC
		private SapHanaQueryDomain(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube, bool useSemanticSet)
			: base(dataSource, new UserOverrideOdbcSqlExpressionGenerator(dataSource, SapHanaOdbcOptions.Instance), new SapHanaQueryDomain.SapHanaOdbcQueryExpressionVisitorFactory(cube, useSemanticSet), false, false, dataSource.Host.QueryService<IFoldingFailureService>().ThrowOnFoldingFailure, false, null)
		{
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x0006DFE6 File Offset: 0x0006C1E6
		public override Query Optimize(Query query)
		{
			return base.OptimizingVisitor.VisitQuery(query);
		}

		// Token: 0x02000477 RID: 1143
		private class SapHanaOdbcQueryExpressionVisitorFactory : OdbcQueryExpressionVisitorFactory
		{
			// Token: 0x06002604 RID: 9732 RVA: 0x0006DFF4 File Offset: 0x0006C1F4
			public SapHanaOdbcQueryExpressionVisitorFactory(SapHanaCubeBase cube, bool useSemanticSet)
			{
				this.cube = cube;
				this.useSemanticSet = useSemanticSet;
			}

			// Token: 0x06002605 RID: 9733 RVA: 0x0006E00A File Offset: 0x0006C20A
			public override OdbcQueryExpressionVisitor New(OdbcDataSource dataSource, IList<SelectItem> selectItems, OdbcQueryColumnInfo[] columnInfos, bool allowAggregates, bool softNumbers, bool tolerateConcatOverflow, int[] groupKey)
			{
				return new SapHanaQueryExpressionVisitor(this.cube, dataSource, selectItems, columnInfos, allowAggregates, tolerateConcatOverflow, groupKey, this.useSemanticSet);
			}

			// Token: 0x04000FE6 RID: 4070
			private readonly SapHanaCubeBase cube;

			// Token: 0x04000FE7 RID: 4071
			private readonly bool useSemanticSet;
		}
	}
}
