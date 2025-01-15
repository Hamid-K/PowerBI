using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000031 RID: 49
	internal abstract class SqlAggregateExpression : SqlExpression
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x000089FC File Offset: 0x00006BFC
		protected SqlAggregateExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression argument, SqlBatch sqlBatch)
			: base(SqlAggregateExpression.IsAggregateNullable(qpInfo, functionContext))
		{
			if (qpInfo == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("qpInfo"));
			}
			if (argument == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("argument"));
			}
			qpInfo.CheckAggregateExpression();
			this.m_qpInfo = qpInfo;
			if (qpInfo.IsInnerMost && qpInfo.AggregateArgument.GetResultType().DataType == DataType.EntityKey)
			{
				if (this.m_qpInfo.Expression.NodeAsFunction.FunctionName != FunctionName.Count)
				{
					throw SQEAssert.AssertFalseAndThrow("Only Count aggregate can aggregate entity keys.", Array.Empty<object>());
				}
				this.m_argument = this.GetEntityRefCountArgument();
			}
			else
			{
				this.m_argument = argument;
			}
			if (this.m_qpInfo.IsAggregateInvocationPoint)
			{
				this.m_needToCastReturnValueAsDecimal = sqlBatch.NeedToCastReturnValueAsDecimal(this.m_qpInfo.Expression.NodeAsFunction, functionContext);
				return;
			}
			this.m_needToCastReturnValueAsDecimal = false;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008AD8 File Offset: 0x00006CD8
		internal bool IsSqlAggregateGenerated
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_qpInfo.MustAggregate && !this.m_qpInfo.MustAggregateDegenerate;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00008AF7 File Offset: 0x00006CF7
		internal override bool CanGroupBy
		{
			get
			{
				return !this.IsSqlAggregateGenerated && this.m_argument.CanGroupBy;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008B10 File Offset: 0x00006D10
		protected override void InitValues()
		{
			FunctionNode nodeAsFunction = this.m_qpInfo.Expression.NodeAsFunction;
			if (nodeAsFunction == null)
			{
				throw SQEAssert.AssertFalseAndThrow("The current expression is not a function.", Array.Empty<object>());
			}
			ISqlSnippet sqlSnippet;
			if (this.m_qpInfo.MustAggregate)
			{
				switch (nodeAsFunction.FunctionName)
				{
				case FunctionName.Sum:
					this.CheckArgValues();
					if (!this.m_qpInfo.MustAggregateDegenerate)
					{
						sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
						{
							SqlExpression.SumOpenParenSnippet,
							this.m_argument,
							SqlExpression.CloseParenSnippet
						});
					}
					else
					{
						sqlSnippet = this.m_argument;
					}
					break;
				case FunctionName.Avg:
				{
					this.CheckArgValues();
					ISqlSnippet sqlSnippet2;
					if (this.m_qpInfo.IsInnerMostAggregation || this.m_qpInfo.MustAggregateDegenerate)
					{
						DataType dataType = this.m_qpInfo.AggregateArgument.GetResultType().DataType;
						switch (dataType)
						{
						case DataType.Integer:
							sqlSnippet2 = SqlFunctionExpression.CastAsDecimal(this.m_argument);
							break;
						case DataType.Decimal:
							sqlSnippet2 = this.m_argument;
							break;
						case DataType.Float:
							sqlSnippet2 = this.m_argument;
							break;
						default:
							throw SQEAssert.AssertFalseAndThrow("Invalid argument type for Avg aggregate: {0}.", new object[] { dataType });
						}
					}
					else
					{
						sqlSnippet2 = this.m_argument;
					}
					if (!this.m_qpInfo.MustAggregateDegenerate)
					{
						this.CheckNonTransitiveAggregation(nodeAsFunction.FunctionName);
						sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
						{
							SqlExpression.AvgOpenParenSnippet,
							sqlSnippet2,
							SqlExpression.CloseParenSnippet
						});
					}
					else
					{
						sqlSnippet = sqlSnippet2;
					}
					break;
				}
				case FunctionName.Max:
					this.CheckArgValues();
					sqlSnippet = this.CreateBasicSqlSnippetForMax(this.m_argument, this.m_qpInfo.MustAggregateDegenerate);
					break;
				case FunctionName.Min:
					this.CheckArgValues();
					sqlSnippet = this.CreateBasicSqlSnippetForMin(this.m_argument, this.m_qpInfo.MustAggregateDegenerate);
					break;
				case FunctionName.Count:
					this.CheckArgValues();
					if (!this.m_qpInfo.MustAggregateDegenerate)
					{
						if (this.m_qpInfo.IsInnerMostAggregation)
						{
							sqlSnippet = this.CreateBasicSqlSnippetForCount(this.m_argument, false);
						}
						else
						{
							sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
							{
								SqlExpression.SumOpenParenSnippet,
								this.m_argument,
								SqlExpression.CloseParenSnippet
							});
						}
					}
					else
					{
						sqlSnippet = this.CreateSqlSnippetForDegenerateCount();
					}
					break;
				case FunctionName.CountDistinct:
					this.CheckArgValues();
					if (!this.m_qpInfo.MustAggregateDegenerate)
					{
						this.CheckNonTransitiveAggregation(nodeAsFunction.FunctionName);
						sqlSnippet = this.CreateBasicSqlSnippetForCount(this.m_argument, true);
					}
					else
					{
						sqlSnippet = this.CreateSqlSnippetForDegenerateCount();
					}
					break;
				case FunctionName.StDev:
					this.CheckArgValues();
					if (!this.m_qpInfo.MustAggregateDegenerate)
					{
						this.CheckNonTransitiveAggregation(nodeAsFunction.FunctionName);
						sqlSnippet = this.CreateBasicSqlSnippetForStDev(this.m_argument, false);
					}
					else
					{
						sqlSnippet = this.CreateSqlSnippetForDegenerateStDevOrVar();
					}
					break;
				case FunctionName.StDevP:
					this.CheckArgValues();
					if (!this.m_qpInfo.MustAggregateDegenerate)
					{
						this.CheckNonTransitiveAggregation(nodeAsFunction.FunctionName);
						sqlSnippet = this.CreateBasicSqlSnippetForStDev(this.m_argument, true);
					}
					else
					{
						sqlSnippet = SqlExpression.NullSnippet;
					}
					break;
				case FunctionName.Var:
					this.CheckArgValues();
					if (!this.m_qpInfo.MustAggregateDegenerate)
					{
						this.CheckNonTransitiveAggregation(nodeAsFunction.FunctionName);
						sqlSnippet = this.CreateBasicSqlSnippetForVar(this.m_argument, false);
					}
					else
					{
						sqlSnippet = this.CreateSqlSnippetForDegenerateStDevOrVar();
					}
					break;
				case FunctionName.VarP:
					this.CheckArgValues();
					if (!this.m_qpInfo.MustAggregateDegenerate)
					{
						this.CheckNonTransitiveAggregation(nodeAsFunction.FunctionName);
						sqlSnippet = this.CreateBasicSqlSnippetForVar(this.m_argument, true);
					}
					else
					{
						sqlSnippet = SqlExpression.NullSnippet;
					}
					break;
				default:
					throw SQEAssert.AssertFalseAndThrow("Unknown aggregate function: {0}.", new object[] { nodeAsFunction.FunctionName });
				}
			}
			else if (!this.IsNullable && SqlAggregateExpression.NeedToCoalesceCount(this.m_qpInfo))
			{
				this.CheckArgValues();
				sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CoalesceOpenParenSnippet,
					this.m_argument,
					SqlExpression.CommaZeroCloseParenSnippet
				});
			}
			else
			{
				sqlSnippet = this.m_argument;
			}
			base.Values.Add(this.FinalizeSqlSnippetCreation(sqlSnippet));
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00008F07 File Offset: 0x00007107
		protected FunctionNode FunctionNode
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_qpInfo.Expression.NodeAsFunction;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008F19 File Offset: 0x00007119
		protected virtual SqlExpression GetEntityRefCountArgument()
		{
			return SqlExpression.SqlIntOneExpression;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008F20 File Offset: 0x00007120
		protected virtual ISqlSnippet CreateBasicSqlSnippetForMax(ISqlSnippet argument, bool degenerate)
		{
			if (degenerate)
			{
				return argument;
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.MaxOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008F46 File Offset: 0x00007146
		protected virtual ISqlSnippet CreateBasicSqlSnippetForMin(ISqlSnippet argument, bool degenerate)
		{
			if (degenerate)
			{
				return argument;
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.MinOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008F6C File Offset: 0x0000716C
		protected virtual ISqlSnippet CreateBasicSqlSnippetForCount(ISqlSnippet argument, bool distinct)
		{
			if (distinct)
			{
				return new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CountOpenParenDistinctSnippet,
					argument,
					SqlExpression.CloseParenSnippet
				});
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CountOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008FBB File Offset: 0x000071BB
		protected virtual ISqlSnippet CreateBasicSqlSnippetForStDev(ISqlSnippet argument, bool pop)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				pop ? SqlExpression.StDevPOpenParenSnippet : SqlExpression.StDevOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008FE6 File Offset: 0x000071E6
		protected virtual ISqlSnippet CreateBasicSqlSnippetForVar(ISqlSnippet argument, bool pop)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				pop ? SqlExpression.VarPOpenParenSnippet : SqlExpression.VarOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009014 File Offset: 0x00007214
		private static bool IsAggregateNullable(IQPExpressionInfo qpInfo, FunctionContext functionContext)
		{
			if (qpInfo.Nullable)
			{
				return true;
			}
			if (SqlAggregateExpression.NeedToCoalesceCount(qpInfo) && functionContext.Count > 0)
			{
				Predicate<long> predicate;
				switch (functionContext.Current.FunctionNode.FunctionName)
				{
				case FunctionName.Equals:
					predicate = (long x) => x != 0L;
					goto IL_00B4;
				case FunctionName.GreaterThan:
					predicate = (long x) => x >= 0L;
					goto IL_00B4;
				case FunctionName.GreaterThanOrEquals:
					predicate = (long x) => x > 0L;
					goto IL_00B4;
				}
				predicate = null;
				IL_00B4:
				if (predicate != null)
				{
					if (functionContext.Current.FunctionNode.Arguments.Count != 2)
					{
						throw SQEAssert.AssertFalseAndThrow("Wrong number of arguments in comparison function.", Array.Empty<object>());
					}
					Expression expression = functionContext.Current.FunctionNode.Arguments[0];
					Expression expression2 = functionContext.Current.FunctionNode.Arguments[1];
					Expression expression3;
					if (expression == qpInfo.Expression)
					{
						expression3 = expression2;
					}
					else
					{
						if (expression2 != qpInfo.Expression)
						{
							throw SQEAssert.AssertFalseAndThrow("Argument can not find itself in the function call.", Array.Empty<object>());
						}
						expression3 = expression;
					}
					if (expression3.Path.IsEmpty && expression3.NodeAsLiteral != null && expression3.NodeAsLiteral.DataType == DataType.Integer && expression3.NodeAsLiteral.Cardinality == Cardinality.One && predicate(expression3.NodeAsLiteral.ValueAsInteger))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000091A8 File Offset: 0x000073A8
		private static bool NeedToCoalesceCount(IQPExpressionInfo qpInfo)
		{
			return qpInfo.IsAggregateInvocationPoint && !qpInfo.MustAggregate && (qpInfo.Expression.NodeAsFunction.FunctionName == FunctionName.Count || qpInfo.Expression.NodeAsFunction.FunctionName == FunctionName.CountDistinct);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000091E5 File Offset: 0x000073E5
		private void CheckNonTransitiveAggregation(FunctionName aggregateFunction)
		{
			if (!this.m_qpInfo.IsInnerMostAggregation)
			{
				throw SQEAssert.AssertFalseAndThrow("Attempt to generate sql expression for {0} at a non innermost aggregation point. {0} is a non-transitive aggregate which should have only one aggregation point which is innermost by definition.", new object[] { aggregateFunction });
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009210 File Offset: 0x00007410
		private ISqlSnippet CreateSqlSnippetForDegenerateCount()
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CaseWhenSnippet,
				this.m_argument,
				SqlExpression.IsNullThenSnippet,
				SqlExpression.ZeroSnippet,
				SqlExpression.ElseSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.EndSnippet
			});
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009264 File Offset: 0x00007464
		private ISqlSnippet CreateSqlSnippetForDegenerateStDevOrVar()
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CaseWhenSnippet,
				this.m_argument,
				SqlExpression.IsNullThenSnippet,
				SqlExpression.NullSnippet,
				SqlExpression.ElseSnippet,
				SqlExpression.ZeroSnippet,
				SqlExpression.EndSnippet
			});
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000092B5 File Offset: 0x000074B5
		private void CheckArgValues()
		{
			if (this.m_argument.Values.Count != 1)
			{
				throw SQEAssert.AssertFalseAndThrow("Invalid number of aggregate arguments.", Array.Empty<object>());
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000092DA File Offset: 0x000074DA
		private ISqlSnippet FinalizeSqlSnippetCreation(ISqlSnippet value)
		{
			if (value is SqlExpression && ((SqlExpression)value).Values.Count != 1)
			{
				throw SQEAssert.AssertFalseAndThrow("Invalid number of aggregate arguments.", Array.Empty<object>());
			}
			if (!this.m_needToCastReturnValueAsDecimal)
			{
				return value;
			}
			return SqlFunctionExpression.CastAsDecimal(value);
		}

		// Token: 0x040000CC RID: 204
		private readonly IQPExpressionInfo m_qpInfo;

		// Token: 0x040000CD RID: 205
		private SqlExpression m_argument;

		// Token: 0x040000CE RID: 206
		private readonly bool m_needToCastReturnValueAsDecimal;
	}
}
