using System;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011EB RID: 4587
	internal abstract class PagingStrategy
	{
		// Token: 0x060078F3 RID: 30963 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void WritePrologue(PagingQuerySpecification specification, ScriptWriter writer)
		{
		}

		// Token: 0x060078F4 RID: 30964 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void WriteAdditionalSelectColumnList(PagingQuerySpecification specification, ScriptWriter writer)
		{
		}

		// Token: 0x060078F5 RID: 30965 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void WriteEpilogue(PagingQuerySpecification specification, ScriptWriter writer)
		{
		}

		// Token: 0x060078F6 RID: 30966 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void WriteAdditionalSelectModifiers(PagingQuerySpecification specification, ScriptWriter writer)
		{
		}

		// Token: 0x060078F7 RID: 30967 RVA: 0x001A25CA File Offset: 0x001A07CA
		public virtual bool ShouldWriteBaseOrderByClause(PagingQuerySpecification specification)
		{
			return specification.PagingClause == null || specification.PagingClause.OffsetExpression <= 0L;
		}

		// Token: 0x060078F8 RID: 30968 RVA: 0x001A25E8 File Offset: 0x001A07E8
		private Condition CreatePageCondition(PagingClause pagingClause)
		{
			long offsetExpression = pagingClause.OffsetExpression;
			long? fetchExpression = pagingClause.FetchExpression;
			SqlExpression sqlExpression = new ColumnReference(Alias.PagedSource, Alias.PagedRowNumberName);
			Condition condition;
			if (offsetExpression != 0L && fetchExpression != null)
			{
				condition = new BetweenOperation(sqlExpression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(offsetExpression + 1L), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(offsetExpression + fetchExpression.Value));
			}
			else if (fetchExpression != null)
			{
				condition = new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.LessThanOrEqual, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(fetchExpression.Value));
			}
			else
			{
				condition = new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.GreaterThan, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(offsetExpression));
			}
			return condition;
		}

		// Token: 0x060078F9 RID: 30969 RVA: 0x001A2670 File Offset: 0x001A0870
		private Condition CreateRowCountCondition(PagingClause pagingClause)
		{
			long? fetchExpression = pagingClause.FetchExpression;
			Condition condition = null;
			SqlExpression sqlExpression = new LiteralExpression(SqlLanguageStrings.RowNumSqlString);
			if (fetchExpression != null)
			{
				condition = new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.LessThanOrEqual, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(fetchExpression.Value));
			}
			return condition;
		}

		// Token: 0x040041DB RID: 16859
		public static readonly PagingStrategy TopAndRowCount = new PagingStrategy.TopAndRowCountStrategy();

		// Token: 0x040041DC RID: 16860
		public static readonly PagingStrategy RowCountWithoutOrder = new PagingStrategy.RowCountWithoutOrderStrategy();

		// Token: 0x040041DD RID: 16861
		public static readonly PagingStrategy RowCountOnly = new PagingStrategy.RowCountOnlyStrategy();

		// Token: 0x040041DE RID: 16862
		public static readonly PagingStrategy Limit = new PagingStrategy.LimitStrategy();

		// Token: 0x040041DF RID: 16863
		public static readonly PagingStrategy AnsiSql2008 = new PagingStrategy.AnsiSql2008Strategy();

		// Token: 0x020011EC RID: 4588
		private sealed class TopAndRowCountStrategy : PagingStrategy
		{
			// Token: 0x060078FC RID: 30972 RVA: 0x001A26E4 File Offset: 0x001A08E4
			public override void WritePrologue(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				if (pagingClause != null && pagingClause.OffsetExpression > 0L)
				{
					writer.WriteSpaceAfter(SqlLanguageStrings.SelectSqlString);
					if (pagingClause.FetchExpression != null)
					{
						writer.WriteSpaceAfter(SqlLanguageStrings.TopSqlString);
						writer.WriteLong(pagingClause.FetchExpression.Value);
					}
					writer.WriteSpace();
					bool flag = false;
					foreach (SelectItem selectItem in specification.SelectItems)
					{
						flag = writer.WriteLineCommaIfNeeded(flag);
						writer.WriteIdentifier(selectItem.Name);
					}
					writer.WriteLine();
					writer.WriteSpaceAfter(SqlLanguageStrings.FromSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				}
			}

			// Token: 0x060078FD RID: 30973 RVA: 0x001A27B8 File Offset: 0x001A09B8
			public override void WriteAdditionalSelectColumnList(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				OrderByClause orderByClause = specification.OrderByClause;
				if (pagingClause == null || pagingClause.OffsetExpression <= 0L)
				{
					return;
				}
				if (orderByClause != null)
				{
					writer.WriteLineCommaIfNeeded(specification.SelectItems.Count > 0);
					writer.Write(SqlLanguageStrings.RowNumberSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageStrings.OverSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					orderByClause.WriteCreateScript(writer);
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageStrings.AsSqlString);
					writer.WriteIdentifier(Alias.PagedRowNumberName);
					return;
				}
				throw ValueException.NewDataSourceError<Message0>(Strings.SqlUnsortedPagingNotSupported, Value.Null, null);
			}

			// Token: 0x060078FE RID: 30974 RVA: 0x001A2870 File Offset: 0x001A0A70
			public override void WriteAdditionalSelectModifiers(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				if (pagingClause != null && pagingClause.FetchExpression != null && pagingClause.OffsetExpression <= 0L)
				{
					long value = pagingClause.FetchExpression.Value;
					writer.WriteSpaceAfter(SqlLanguageStrings.TopSqlString);
					SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(value).WriteCreateScript(writer);
					writer.WriteLine();
				}
			}

			// Token: 0x060078FF RID: 30975 RVA: 0x001A28CC File Offset: 0x001A0ACC
			public override void WriteEpilogue(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				OrderByClause orderByClause = specification.OrderByClause;
				if (pagingClause == null)
				{
					return;
				}
				if (pagingClause.OffsetExpression > 0L)
				{
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					if (writer.Settings.RequiresAsForFromAlias)
					{
						writer.WriteSpaceAfter(SqlLanguageStrings.AsSqlString);
					}
					writer.WriteIdentifier(Alias.PagedSource);
					writer.WriteLine();
					writer.WriteSpaceAfter(SqlLanguageStrings.WhereSqlString);
					base.CreatePageCondition(pagingClause).WriteCreateScript(writer);
					if (orderByClause != null && !pagingClause.SuppressOrderBy)
					{
						writer.WriteLine();
						writer.WriteSpaceAfter(SqlLanguageStrings.OrderBySqlString);
						writer.WriteIdentifier(Alias.PagedSource);
						writer.Write(SqlLanguageSymbols.DotSqlString);
						writer.WriteIdentifier(Alias.PagedRowNumberName);
					}
				}
			}
		}

		// Token: 0x020011ED RID: 4589
		private sealed class RowCountWithoutOrderStrategy : PagingStrategy
		{
			// Token: 0x06007901 RID: 30977 RVA: 0x001A298C File Offset: 0x001A0B8C
			public override void WritePrologue(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				if (pagingClause == null)
				{
					return;
				}
				if (pagingClause.OffsetExpression > 0L || pagingClause.FetchExpression != null)
				{
					writer.WriteSpaceAfter(SqlLanguageStrings.SelectSqlString);
					writer.WriteSpace();
					bool flag = false;
					foreach (SelectItem selectItem in specification.SelectItems)
					{
						flag = writer.WriteLineCommaIfNeeded(flag);
						writer.WriteIdentifier(selectItem.Name);
					}
					writer.WriteLine();
					writer.WriteSpaceAfter(SqlLanguageStrings.FromSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				}
			}

			// Token: 0x06007902 RID: 30978 RVA: 0x001A2A3C File Offset: 0x001A0C3C
			public override void WriteAdditionalSelectColumnList(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				OrderByClause orderByClause = specification.OrderByClause;
				if (pagingClause == null)
				{
					return;
				}
				if ((pagingClause.OffsetExpression > 0L || pagingClause.FetchExpression != null) && orderByClause != null)
				{
					writer.WriteLineCommaIfNeeded(specification.SelectItems.Count > 0);
					writer.Write(SqlLanguageStrings.RowNumberSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageStrings.OverSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					orderByClause.WriteCreateScript(writer);
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageStrings.AsSqlString);
					writer.WriteIdentifier(Alias.PagedRowNumberName);
				}
			}

			// Token: 0x06007903 RID: 30979 RVA: 0x001A2AEC File Offset: 0x001A0CEC
			public override void WriteEpilogue(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				OrderByClause orderByClause = specification.OrderByClause;
				if (pagingClause == null)
				{
					return;
				}
				if (pagingClause.FetchExpression != null || pagingClause.OffsetExpression > 0L)
				{
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					if (writer.Settings.RequiresAsForFromAlias)
					{
						writer.WriteSpaceAfter(SqlLanguageStrings.AsSqlString);
					}
					writer.WriteIdentifier(Alias.PagedSource);
					writer.WriteLine();
					writer.WriteSpaceAfter(SqlLanguageStrings.WhereSqlString);
					((orderByClause == null) ? base.CreateRowCountCondition(pagingClause) : base.CreatePageCondition(pagingClause)).WriteCreateScript(writer);
					if (orderByClause != null && !pagingClause.SuppressOrderBy)
					{
						writer.WriteLine();
						writer.WriteSpaceAfter(SqlLanguageStrings.OrderBySqlString);
						writer.WriteIdentifier(Alias.PagedSource);
						writer.Write(SqlLanguageSymbols.DotSqlString);
						writer.WriteIdentifier(Alias.PagedRowNumberName);
					}
				}
			}
		}

		// Token: 0x020011EE RID: 4590
		private sealed class RowCountOnlyStrategy : PagingStrategy
		{
			// Token: 0x06007905 RID: 30981 RVA: 0x001A2BC0 File Offset: 0x001A0DC0
			public override void WritePrologue(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				if (pagingClause == null)
				{
					return;
				}
				if (pagingClause.OffsetExpression > 0L || pagingClause.FetchExpression != null)
				{
					writer.WriteSpaceAfter(SqlLanguageStrings.SelectSqlString);
					writer.WriteSpace();
					bool flag = false;
					foreach (SelectItem selectItem in specification.SelectItems)
					{
						flag = writer.WriteLineCommaIfNeeded(flag);
						writer.WriteIdentifier(selectItem.Name);
					}
					writer.WriteLine();
					writer.WriteSpaceAfter(SqlLanguageStrings.FromSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				}
			}

			// Token: 0x06007906 RID: 30982 RVA: 0x001A2C70 File Offset: 0x001A0E70
			public override void WriteAdditionalSelectColumnList(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				OrderByClause orderByClause = specification.OrderByClause;
				if (pagingClause == null)
				{
					return;
				}
				if (pagingClause.OffsetExpression <= 0L && pagingClause.FetchExpression == null)
				{
					return;
				}
				if (orderByClause != null)
				{
					writer.WriteLineCommaIfNeeded(specification.SelectItems.Count > 0);
					writer.Write(SqlLanguageStrings.RowNumberSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageStrings.OverSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					orderByClause.WriteCreateScript(writer);
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageStrings.AsSqlString);
					writer.WriteIdentifier(Alias.PagedRowNumberName);
					return;
				}
				if (pagingClause.FetchExpression != null)
				{
					writer.WriteLineCommaIfNeeded(specification.SelectItems.Count > 0);
					writer.Write(SqlLanguageStrings.RowNumberSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageStrings.OverSqlString);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					writer.WriteSpaceAfter(SqlLanguageStrings.AsSqlString);
					writer.WriteIdentifier(Alias.PagedRowNumberName);
					return;
				}
				throw ValueException.NewDataSourceError<Message0>(Strings.SqlUnsortedPagingNotSupported, Value.Null, null);
			}

			// Token: 0x06007907 RID: 30983 RVA: 0x001A2DB4 File Offset: 0x001A0FB4
			public override void WriteEpilogue(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				OrderByClause orderByClause = specification.OrderByClause;
				if (pagingClause == null)
				{
					return;
				}
				if (pagingClause.FetchExpression != null || pagingClause.OffsetExpression > 0L)
				{
					writer.WriteSpaceAfter(SqlLanguageSymbols.CloseParenthesisSqlString);
					if (writer.Settings.RequiresAsForFromAlias)
					{
						writer.WriteSpaceAfter(SqlLanguageStrings.AsSqlString);
					}
					writer.WriteIdentifier(Alias.PagedSource);
					writer.WriteLine();
					writer.WriteSpaceAfter(SqlLanguageStrings.WhereSqlString);
					base.CreatePageCondition(pagingClause).WriteCreateScript(writer);
					if (orderByClause != null && !pagingClause.SuppressOrderBy)
					{
						writer.WriteLine();
						writer.WriteSpaceAfter(SqlLanguageStrings.OrderBySqlString);
						writer.WriteIdentifier(Alias.PagedSource);
						writer.Write(SqlLanguageSymbols.DotSqlString);
						writer.WriteIdentifier(Alias.PagedRowNumberName);
					}
				}
			}
		}

		// Token: 0x020011EF RID: 4591
		private sealed class LimitStrategy : PagingStrategy
		{
			// Token: 0x06007909 RID: 30985 RVA: 0x001A2E7C File Offset: 0x001A107C
			public override void WriteEpilogue(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				if (pagingClause == null)
				{
					return;
				}
				if (pagingClause.FetchExpression != null || pagingClause.OffsetExpression > 0L)
				{
					writer.WriteLine();
					writer.WriteSpaceAfter(SqlLanguageStrings.LimitSqlString);
					writer.WriteLong(pagingClause.FetchExpression.GetValueOrDefault(long.MaxValue));
				}
				if (pagingClause.OffsetExpression > 0L)
				{
					writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.OffsetSqlString);
					writer.WriteLong(pagingClause.OffsetExpression);
				}
			}
		}

		// Token: 0x020011F0 RID: 4592
		private sealed class AnsiSql2008Strategy : PagingStrategy
		{
			// Token: 0x0600790B RID: 30987 RVA: 0x001A2F00 File Offset: 0x001A1100
			public override void WriteEpilogue(PagingQuerySpecification specification, ScriptWriter writer)
			{
				PagingClause pagingClause = specification.PagingClause;
				OrderByClause orderByClause = specification.OrderByClause;
				if (pagingClause == null)
				{
					return;
				}
				if (pagingClause.OffsetExpression > 0L)
				{
					if (orderByClause != null && !pagingClause.SuppressOrderBy)
					{
						writer.WriteLine();
						orderByClause.WriteCreateScript(writer);
					}
					writer.WriteLine();
					writer.WriteSpaceAfter(SqlLanguageStrings.OffsetSqlString);
					writer.WriteLong(pagingClause.OffsetExpression);
					writer.WriteSpaceBefore(SqlLanguageStrings.RowsSqlString);
				}
				if (pagingClause.FetchExpression != null)
				{
					writer.WriteLine();
					writer.WriteSpaceAfter(SqlLanguageStrings.FetchSqlString);
					writer.WriteSpaceAfter(SqlLanguageStrings.NextSqlString);
					writer.WriteLong(pagingClause.FetchExpression.Value);
					writer.WriteSpaceBefore(SqlLanguageStrings.RowsSqlString);
					writer.WriteSpaceBefore(SqlLanguageStrings.OnlySqlString);
				}
			}
		}
	}
}
