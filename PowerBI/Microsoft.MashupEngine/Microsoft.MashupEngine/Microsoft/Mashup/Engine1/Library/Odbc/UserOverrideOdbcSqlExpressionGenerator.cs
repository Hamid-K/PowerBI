using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000686 RID: 1670
	internal class UserOverrideOdbcSqlExpressionGenerator : OdbcSqlExpressionGenerator
	{
		// Token: 0x06003453 RID: 13395 RVA: 0x000A8B5A File Offset: 0x000A6D5A
		public UserOverrideOdbcSqlExpressionGenerator(OdbcDataSource dataSource, OdbcOptions options)
		{
			this.dataSource = dataSource;
			this.options = options;
			this.trace = new OdbcFoldingTracingService(dataSource);
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000A8B7C File Offset: 0x000A6D7C
		public override bool TryGenerateConstant(OdbcTypeInfo typeInfo, Value value, out SqlExpression sqlExpression)
		{
			Value value2;
			if (this.options.AstVisitor.TryGetValue("Constant", out value2) && !value2.IsNull)
			{
				using (this.trace.NewScope("TryGenerateConstant"))
				{
					try
					{
						FunctionValue asFunction = value2.AsFunction;
						RecordValue recordValue = ExpressionToSimplifiedMAstVisitor.ToMAst(new ConstantExpressionSyntaxNode(value));
						RecordValue recordValue2 = UserOverrideOdbcSqlExpressionGenerator.ToRecord(typeInfo);
						Value value3 = asFunction.Invoke(recordValue2, recordValue);
						if (!value3.IsNull)
						{
							RecordValue asRecord = value3.AsRecord;
							sqlExpression = new UserOverrideOdbcSqlExpressionGenerator.UserLiteral(asRecord);
							return true;
						}
					}
					catch (ValueException ex)
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingException>(new FoldingWarnings.FoldingException(ex));
					}
				}
			}
			sqlExpression = null;
			return false;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000A8C44 File Offset: 0x000A6E44
		public override bool TryGetLimitClause(RowRange rowRange, out OdbcLimitClause limitClause, out RowRange localRowRange)
		{
			localRowRange = RowRange.All;
			Value value;
			if (this.options.AstVisitor.TryGetValue("LimitClause", out value))
			{
				try
				{
					NumberValue numberValue = NumberValue.New(rowRange.SkipCount.Value);
					Value value2 = (rowRange.TakeCount.IsInfinite ? Value.Null : NumberValue.New(rowRange.TakeCount.Value));
					RecordValue asRecord = value.AsFunction.Invoke(numberValue, value2).AsRecord;
					string asString = asRecord["Text"].AsString;
					if (asRecord.TryGetValue("Skip", out value))
					{
						localRowRange = localRowRange.Skip(new RowCount(value.AsNumber.AsInteger64));
					}
					if (asRecord.TryGetValue("Take", out value))
					{
						localRowRange = localRowRange.Take(new RowCount(value.AsNumber.AsInteger64));
					}
					OdbcLimitClauseLocation limitClauseLocation = UserOverrideOdbcSqlExpressionGenerator.GetLimitClauseLocation(asRecord["Location"].AsString);
					limitClause = new UserOverrideOdbcSqlExpressionGenerator.UserLimitClause(limitClauseLocation, asString);
					return true;
				}
				catch (ValueException)
				{
					goto IL_020A;
				}
			}
			if (this.options.SupportsTop && this.options.LimitClauseKind != LimitClauseKind.LimitClauseKindType.None)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Odbc_NoSupportTopAndLimitClauseKind, null, null);
			}
			if (this.options.LimitClauseKind != LimitClauseKind.LimitClauseKindType.None)
			{
				if (LimitClauseKind.TryGetLimitClause(this.options.LimitClauseKind, rowRange, out limitClause))
				{
					localRowRange = RowRange.All;
					return true;
				}
			}
			else if (this.options.SupportsTop && !rowRange.TakeCount.IsInfinite)
			{
				OdbcLimitClauseLocation odbcLimitClauseLocation = OdbcLimitClauseLocation.AfterSelect;
				if (this.options.LimitClauseLocation != null)
				{
					odbcLimitClauseLocation = UserOverrideOdbcSqlExpressionGenerator.GetLimitClauseLocation(this.options.LimitClauseLocation);
					if (odbcLimitClauseLocation != OdbcLimitClauseLocation.AfterSelect && odbcLimitClauseLocation != OdbcLimitClauseLocation.AfterSelectBeforeModifiers)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("LimitClauseLocation"), value, null);
					}
				}
				limitClause = new UserOverrideOdbcSqlExpressionGenerator.TopLimitClause(rowRange.TakeCount.Value + rowRange.SkipCount.Value, odbcLimitClauseLocation);
				localRowRange = localRowRange.Skip(rowRange.SkipCount);
				return true;
			}
			IL_020A:
			localRowRange = default(RowRange);
			limitClause = null;
			return false;
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000A8E7C File Offset: 0x000A707C
		public override bool TryGetAdditionalFunctions(out ListValue functions)
		{
			Value value;
			if (this.options.AstVisitor.TryGetValue("Functions", out value) && value.IsList)
			{
				functions = value.AsList;
				return true;
			}
			functions = null;
			return false;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000A8EB8 File Offset: 0x000A70B8
		public override bool TryGenerateInvocation(FunctionValue visitor, TypeValue rowType, Value groupKeys, Value invocation, out Value result)
		{
			Value value;
			if (this.options.AstVisitor.TryGetValue("Invocation", out value) && value.IsFunction)
			{
				using (this.trace.NewScope("Invocation"))
				{
					try
					{
						result = value.AsFunction.Invoke(visitor, rowType, groupKeys, invocation);
						return true;
					}
					catch (ValueException ex)
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingException>(new FoldingWarnings.FoldingException(ex));
					}
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000A8F50 File Offset: 0x000A7150
		private static RecordValue ToRecord(OdbcTypeInfo typeInfo)
		{
			return RecordValue.New(UserOverrideOdbcSqlExpressionGenerator.typeInfoKeys, new Value[]
			{
				NumberValue.New((int)typeInfo.SqlType),
				TextValue.NewOrNull(typeInfo.Name)
			});
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000A8F80 File Offset: 0x000A7180
		private static OdbcLimitClauseLocation GetLimitClauseLocation(string value)
		{
			if (value == "BeforeQuerySpecification")
			{
				return OdbcLimitClauseLocation.BeforeQuerySpecification;
			}
			if (value == "AfterQuerySpecification")
			{
				return OdbcLimitClauseLocation.AfterQuerySpecification;
			}
			if (value == "AfterSelect")
			{
				return OdbcLimitClauseLocation.AfterSelect;
			}
			if (!(value == "AfterSelectBeforeModifiers"))
			{
				throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("Location"), TextValue.New(value), null);
			}
			return OdbcLimitClauseLocation.AfterSelectBeforeModifiers;
		}

		// Token: 0x0400178E RID: 6030
		private static readonly Keys typeInfoKeys = Keys.New("DATA_TYPE", "TYPE_NAME");

		// Token: 0x0400178F RID: 6031
		private readonly OdbcDataSource dataSource;

		// Token: 0x04001790 RID: 6032
		private readonly OdbcOptions options;

		// Token: 0x04001791 RID: 6033
		private readonly OdbcFoldingTracingService trace;

		// Token: 0x02000687 RID: 1671
		private class UserLimitClause : OdbcLimitClause
		{
			// Token: 0x0600345B RID: 13403 RVA: 0x000A8FF7 File Offset: 0x000A71F7
			public UserLimitClause(OdbcLimitClauseLocation location, string expression)
			{
				this.location = location;
				this.expression = expression;
			}

			// Token: 0x1700129E RID: 4766
			// (get) Token: 0x0600345C RID: 13404 RVA: 0x000A900D File Offset: 0x000A720D
			public override OdbcLimitClauseLocation Location
			{
				get
				{
					return this.location;
				}
			}

			// Token: 0x0600345D RID: 13405 RVA: 0x000A9015 File Offset: 0x000A7215
			public override void WriteCreateScript(ScriptWriter scriptWriter)
			{
				scriptWriter.Write(new ConstantSqlString(this.expression));
			}

			// Token: 0x04001792 RID: 6034
			private readonly OdbcLimitClauseLocation location;

			// Token: 0x04001793 RID: 6035
			private readonly string expression;
		}

		// Token: 0x02000688 RID: 1672
		private class TopLimitClause : OdbcLimitClause
		{
			// Token: 0x0600345E RID: 13406 RVA: 0x000A9028 File Offset: 0x000A7228
			public TopLimitClause(long take, OdbcLimitClauseLocation location)
			{
				this.take = new SqlConstant(ConstantType.Integer, take.ToString(CultureInfo.InvariantCulture));
				this.location = location;
			}

			// Token: 0x1700129F RID: 4767
			// (get) Token: 0x0600345F RID: 13407 RVA: 0x000A904F File Offset: 0x000A724F
			public override OdbcLimitClauseLocation Location
			{
				get
				{
					return this.location;
				}
			}

			// Token: 0x06003460 RID: 13408 RVA: 0x000A9057 File Offset: 0x000A7257
			public override void WriteCreateScript(ScriptWriter scriptWriter)
			{
				scriptWriter.WriteSpaceAfter(SqlLanguageStrings.TopSqlString);
				this.take.WriteCreateScript(scriptWriter);
			}

			// Token: 0x04001794 RID: 6036
			private readonly SqlConstant take;

			// Token: 0x04001795 RID: 6037
			private readonly OdbcLimitClauseLocation location;
		}

		// Token: 0x02000689 RID: 1673
		private class UserLiteral : SqlExpression
		{
			// Token: 0x06003461 RID: 13409 RVA: 0x000A9070 File Offset: 0x000A7270
			public UserLiteral(RecordValue literal)
			{
				this.literal = literal;
			}

			// Token: 0x170012A0 RID: 4768
			// (get) Token: 0x06003462 RID: 13410 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06003463 RID: 13411 RVA: 0x000A907F File Offset: 0x000A727F
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.Write(new ConstantSqlString(this.literal["Text"].AsString));
			}

			// Token: 0x04001796 RID: 6038
			private readonly RecordValue literal;
		}
	}
}
