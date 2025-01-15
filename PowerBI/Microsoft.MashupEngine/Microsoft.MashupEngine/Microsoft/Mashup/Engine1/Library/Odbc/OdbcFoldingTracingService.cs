using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005F6 RID: 1526
	internal class OdbcFoldingTracingService : FoldingTracingService
	{
		// Token: 0x0600301C RID: 12316 RVA: 0x00091B21 File Offset: 0x0008FD21
		public OdbcFoldingTracingService(OdbcDataSource dataSource)
			: base(dataSource.Host, "OdbcQuery/FoldingWarning")
		{
			this.dataSourceInfo = new OdbcFoldingTracingService.TracingOdbcDataSourceInfo(this, dataSource.Info);
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x00091B46 File Offset: 0x0008FD46
		public OdbcDataSourceInfo DataSourceInfo
		{
			get
			{
				return this.dataSourceInfo;
			}
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x00091B4E File Offset: 0x0008FD4E
		public bool ArgumentCountEquals(InvocationQueryExpression expression, int count)
		{
			if (expression.Arguments.Count == count)
			{
				return true;
			}
			base.Trace<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>, int>>(FoldingWarnings.InvalidArgumentsCount(expression, count));
			return false;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x00091B6E File Offset: 0x0008FD6E
		public bool ArgumentCountBetween(InvocationQueryExpression expression, int min, int max)
		{
			if (expression.Arguments.Count >= min && expression.Arguments.Count <= max)
			{
				return true;
			}
			base.Trace<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>, int, int>>(FoldingWarnings.InvalidArgumentsCount(expression, min, max));
			return false;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x00091B9D File Offset: 0x0008FD9D
		public bool ArgumentTypeKindIs(InvocationQueryExpression expression, int argumentIndex, OdbcScalarExpression argument, ValueKind kind)
		{
			if (argument.TypeInfo.TypeValue.TypeKind == kind)
			{
				return true;
			}
			base.Trace<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentType(expression, argumentIndex, kind.ToString()));
			return false;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x00091BD0 File Offset: 0x0008FDD0
		public bool ArgumentNotNullable(InvocationQueryExpression expression, int argumentIndex, OdbcScalarExpression argument)
		{
			if (!argument.TypeInfo.IsNullable)
			{
				return true;
			}
			base.Trace<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentType(expression, argumentIndex, "Non-Nullable"));
			return false;
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x00091BF4 File Offset: 0x0008FDF4
		public bool ArgumentDataSourceType(InvocationQueryExpression expression, int argumentIndex, OdbcScalarExpression argument, OdbcTypeInfo typeInfo)
		{
			if (argument.TypeInfo.DataSourceType == typeInfo)
			{
				return true;
			}
			base.Trace<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentType(expression, argumentIndex, typeInfo.ToString()));
			return false;
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x00091C1C File Offset: 0x0008FE1C
		public bool ArgumentSqlType(InvocationQueryExpression expression, int argumentIndex, OdbcScalarExpression argument, Odbc32.SQL_TYPE sqlType)
		{
			if (argument.TypeInfo.DataSourceType.SqlType == sqlType)
			{
				return true;
			}
			base.Trace<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentType(expression, argumentIndex, sqlType.ToString()));
			return false;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x00091C4F File Offset: 0x0008FE4F
		public bool ArgumentQueryExpressionKindIs(InvocationQueryExpression expression, int argumentIndex, QueryExpressionKind kind)
		{
			if (expression.Arguments[argumentIndex].Kind == kind)
			{
				return true;
			}
			base.Trace<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentType(expression, argumentIndex, kind.ToString()));
			return false;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x00091C82 File Offset: 0x0008FE82
		public bool ArgumentValueWhen(bool condition, InvocationQueryExpression expression, int argumentIndex, string reason)
		{
			if (condition)
			{
				return true;
			}
			base.Trace<FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>>(FoldingWarnings.InvalidArgumentValue(expression, argumentIndex, reason));
			return false;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x00091C99 File Offset: 0x0008FE99
		public bool RowRangeIsAll(RowRange rowRange)
		{
			if (rowRange.IsAll)
			{
				return true;
			}
			base.Trace<FoldingWarnings.FoldingWarning<RowCount, RowCount>>(FoldingWarnings.SkipTake(rowRange));
			return false;
		}

		// Token: 0x0400152E RID: 5422
		private const string foldingFailureEntryName = "OdbcQuery/FoldingWarning";

		// Token: 0x0400152F RID: 5423
		private readonly OdbcDataSourceInfo dataSourceInfo;

		// Token: 0x020005F7 RID: 1527
		private class TracingOdbcDataSourceInfo : DelegatingOdbcDataSourceInfo
		{
			// Token: 0x06003027 RID: 12327 RVA: 0x00091CB3 File Offset: 0x0008FEB3
			public TracingOdbcDataSourceInfo(OdbcFoldingTracingService scope, OdbcDataSourceInfo dataSourceInfo)
				: base(dataSourceInfo)
			{
				this.scope = scope;
				this.dataSourceInfo = dataSourceInfo;
			}

			// Token: 0x170011DB RID: 4571
			// (get) Token: 0x06003028 RID: 12328 RVA: 0x00091CCA File Offset: 0x0008FECA
			public override bool SupportsColumnAliases
			{
				get
				{
					if (!base.SupportsColumnAliases)
					{
						this.scope.Trace<FoldingWarnings.FoldingWarning<string, string, string>>(FoldingWarnings.InvalidSqlGetInfo<string, string, string>("Y", "N", "SQL_COLUMN_ALIAS"));
						return false;
					}
					return true;
				}
			}

			// Token: 0x06003029 RID: 12329 RVA: 0x00091CF6 File Offset: 0x0008FEF6
			public override bool Supports(Odbc32.SQL_FN_TD tdFunction)
			{
				if (base.Supports(tdFunction))
				{
					return true;
				}
				this.scope.Trace<FoldingWarnings.FoldingWarning<Odbc32.SQL_FN_TD, Odbc32.SQL_INFO>>(OdbcFoldingWarnings.SqlGetInfo<Odbc32.SQL_FN_TD>(Odbc32.SQL_INFO.SQL_TIMEDATE_FUNCTIONS, tdFunction));
				return false;
			}

			// Token: 0x0600302A RID: 12330 RVA: 0x00091D17 File Offset: 0x0008FF17
			public override bool Supports(Odbc32.SQL_FN_STR stringFunction)
			{
				if (this.dataSourceInfo.Supports(stringFunction))
				{
					return true;
				}
				this.scope.Trace<FoldingWarnings.FoldingWarning<Odbc32.SQL_FN_STR, Odbc32.SQL_INFO>>(OdbcFoldingWarnings.SqlGetInfo<Odbc32.SQL_FN_STR>(Odbc32.SQL_INFO.SQL_STRING_FUNCTIONS, stringFunction));
				return false;
			}

			// Token: 0x0600302B RID: 12331 RVA: 0x00091D3D File Offset: 0x0008FF3D
			public override bool Supports(Odbc32.SQL_FN_SYSTEM systemFunction)
			{
				if (this.dataSourceInfo.Supports(systemFunction))
				{
					return true;
				}
				this.scope.Trace<FoldingWarnings.FoldingWarning<Odbc32.SQL_FN_SYSTEM, Odbc32.SQL_INFO>>(OdbcFoldingWarnings.SqlGetInfo<Odbc32.SQL_FN_SYSTEM>(Odbc32.SQL_INFO.SQL_SYSTEM_FUNCTIONS, systemFunction));
				return false;
			}

			// Token: 0x0600302C RID: 12332 RVA: 0x00091D63 File Offset: 0x0008FF63
			public override bool Supports(Odbc32.SQL_FUN_NUM numberFunction)
			{
				if (this.dataSourceInfo.Supports(numberFunction))
				{
					return true;
				}
				this.scope.Trace<FoldingWarnings.FoldingWarning<Odbc32.SQL_FUN_NUM, Odbc32.SQL_INFO>>(OdbcFoldingWarnings.SqlGetInfo<Odbc32.SQL_FUN_NUM>(Odbc32.SQL_INFO.SQL_NUMERIC_FUNCTIONS, numberFunction));
				return false;
			}

			// Token: 0x0600302D RID: 12333 RVA: 0x00091D89 File Offset: 0x0008FF89
			public override bool Supports(Odbc32.SQL_SP predicate)
			{
				if (this.dataSourceInfo.Supports(predicate))
				{
					return true;
				}
				this.scope.Trace<FoldingWarnings.FoldingWarning<Odbc32.SQL_SP, Odbc32.SQL_INFO>>(OdbcFoldingWarnings.SqlGetInfo<Odbc32.SQL_SP>(Odbc32.SQL_INFO.SQL_SQL92_PREDICATES, predicate));
				return false;
			}

			// Token: 0x0600302E RID: 12334 RVA: 0x00091DB2 File Offset: 0x0008FFB2
			public override bool Supports(Odbc32.SQL_AF aggregateFunction)
			{
				if (this.dataSourceInfo.Supports(aggregateFunction))
				{
					return true;
				}
				this.scope.Trace<FoldingWarnings.FoldingWarning<Odbc32.SQL_AF, Odbc32.SQL_INFO>>(OdbcFoldingWarnings.SqlGetInfo<Odbc32.SQL_AF>(Odbc32.SQL_INFO.SQL_AGGREGATE_FUNCTIONS, aggregateFunction));
				return false;
			}

			// Token: 0x0600302F RID: 12335 RVA: 0x00091DDB File Offset: 0x0008FFDB
			public override bool Supports(Odbc32.SQL_SC level)
			{
				if (this.dataSourceInfo.Supports(level))
				{
					return true;
				}
				this.scope.Trace<FoldingWarnings.FoldingWarning<Odbc32.SQL_SC, Odbc32.SQL_INFO>>(OdbcFoldingWarnings.SqlGetInfo<Odbc32.SQL_SC>(Odbc32.SQL_INFO.SQL_SQL_CONFORMANCE, level));
				return false;
			}

			// Token: 0x04001530 RID: 5424
			private readonly OdbcFoldingTracingService scope;

			// Token: 0x04001531 RID: 5425
			private readonly OdbcDataSourceInfo dataSourceInfo;
		}
	}
}
