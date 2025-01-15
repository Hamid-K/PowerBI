using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000032 RID: 50
	internal abstract class SqlFunctionExpression : SqlExpression
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00009317 File Offset: 0x00007517
		protected SqlFunctionExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments, SqlBatch sqlBatch)
			: this(qpInfo, functionContext, arguments, qpInfo.Nullable, sqlBatch)
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000932C File Offset: 0x0000752C
		protected SqlFunctionExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments, bool nullable, SqlBatch sqlBatch)
			: base(nullable)
		{
			if (qpInfo == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("qpInfo"));
			}
			if (arguments == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("arguments"));
			}
			if (sqlBatch == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("sqlBatch"));
			}
			this.m_functionNode = qpInfo.Expression.NodeAsFunction;
			if (this.m_functionNode == null || (this.m_functionNode.GetFunctionInfo().IsAggregate && this.m_functionNode.FunctionName != FunctionName.In))
			{
				throw SQEAssert.AssertFalseAndThrow("qpInfo must be a scalar function.", Array.Empty<object>());
			}
			this.m_arguments = arguments;
			this.m_sqlBatch = sqlBatch;
			this.m_isInNotContext = SqlFunctionExpression.IsInNotContext(this.m_functionNode.FunctionName, functionContext);
			this.m_parentFunctionExpectsLogicalValue = SqlFunctionExpression.ParentFunctionExpectsLogicalValue(functionContext);
			this.m_needToCastReturnValueAsDecimal = sqlBatch.NeedToCastReturnValueAsDecimal(this.m_functionNode, functionContext);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000940C File Offset: 0x0000760C
		internal override bool CanGroupBy
		{
			[DebuggerStepThrough]
			get
			{
				for (int i = 0; i < this.m_arguments.Length; i++)
				{
					if (this.m_arguments[i].CanGroupBy)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00009440 File Offset: 0x00007640
		internal override bool IsLogicalBooleanValue
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_functionNode.FunctionName != FunctionName.If && this.m_functionNode.GetFunctionInfo().ReturnType.DataType == DataType.Boolean;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000947C File Offset: 0x0000767C
		protected override void InitValues()
		{
			ISqlSnippet sqlSnippet = null;
			IEnumerable<ISqlSnippet> enumerable = null;
			switch (this.m_functionNode.FunctionName)
			{
			case FunctionName.Add:
				this.CheckArgsAndValues(2);
				sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.OpenParenSnippet,
					this.m_arguments[0],
					SqlExpression.AddSnippet,
					this.m_arguments[1],
					SqlExpression.CloseParenSnippet
				});
				goto IL_0831;
			case FunctionName.Subtract:
				this.CheckArgsAndValues(2);
				sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.OpenParenSnippet,
					this.m_arguments[0],
					SqlExpression.SubtractSnippet,
					this.m_arguments[1],
					SqlExpression.CloseParenSnippet
				});
				goto IL_0831;
			case FunctionName.Multiply:
				this.CheckArgsAndValues(2);
				sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.OpenParenSnippet,
					this.m_arguments[0],
					SqlExpression.MultiplySnippet,
					this.m_arguments[1],
					SqlExpression.CloseParenSnippet
				});
				goto IL_0831;
			case FunctionName.Divide:
				this.CheckArgsAndValues(2);
				sqlSnippet = this.CreateBasicSqlSnippetForDivide(this.m_arguments[0], this.m_functionNode.Arguments[0].GetResultType().DataType, this.m_arguments[1], this.m_functionNode.Arguments[1].GetResultType().DataType);
				goto IL_0831;
			case FunctionName.Negate:
				this.CheckArgsAndValues(1);
				sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.OpenParenSnippet,
					SqlExpression.ZeroSnippet,
					SqlExpression.SubtractSnippet,
					this.m_arguments[0],
					SqlExpression.CloseParenSnippet
				});
				goto IL_0831;
			case FunctionName.Mod:
				this.CheckArgsAndValues(2);
				sqlSnippet = this.CreateBasicSqlSnippetForMod(this.m_arguments[0], this.m_arguments[1]);
				goto IL_0831;
			case FunctionName.Power:
				this.CheckArgsAndValues(2);
				sqlSnippet = this.CreateBasicSqlSnippetForPower(this.m_arguments[0], this.m_functionNode.Arguments[0].GetResultType().DataType, this.m_arguments[1], this.m_functionNode.Arguments[1].GetResultType().DataType);
				goto IL_0831;
			case FunctionName.Equals:
			case FunctionName.NotEquals:
			case FunctionName.GreaterThan:
			case FunctionName.GreaterThanOrEquals:
			case FunctionName.LessThan:
			case FunctionName.LessThanOrEquals:
			case FunctionName.And:
			case FunctionName.Or:
			case FunctionName.Not:
			case FunctionName.In:
				sqlSnippet = this.CreateSqlSnippetForBooleanFunction();
				goto IL_0831;
			case FunctionName.Truncate:
				this.CheckArgs(2);
				sqlSnippet = this.CreateSqlSnippetForTruncate(this.m_arguments[0], this.m_arguments[1]);
				goto IL_0831;
			case FunctionName.Round:
				this.CheckArgs(2);
				sqlSnippet = this.CreateSqlSnippetForRound(this.m_arguments[0], this.m_arguments[1]);
				goto IL_0831;
			case FunctionName.Integer:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForInteger(this.m_arguments[0], this.m_functionNode.Arguments[0].GetResultType().DataType);
				goto IL_0831;
			case FunctionName.Decimal:
				this.CheckArgs(1);
				sqlSnippet = SqlFunctionExpression.CastAsDecimal(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.Float:
				this.CheckArgs(1);
				sqlSnippet = SqlFunctionExpression.CastAsFloat(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.String:
				this.CheckArgs(1);
				sqlSnippet = this.CreateSqlSnippetForString(this.m_arguments[0], this.m_functionNode.Arguments[0].GetResultType().DataType);
				goto IL_0831;
			case FunctionName.Length:
				sqlSnippet = this.CreateSqlSnippetForLength();
				goto IL_0831;
			case FunctionName.Find:
				sqlSnippet = this.CreateSqlSnippetForFind();
				goto IL_0831;
			case FunctionName.Substring:
				this.CheckArgsAndValues(3);
				sqlSnippet = this.CreateBasicSqlSnippetForSubstring(this.m_arguments[0], this.m_arguments[1], this.m_arguments[2]);
				goto IL_0831;
			case FunctionName.Left:
				this.CheckArgsAndValues(2);
				sqlSnippet = this.CreateBasicSqlSnippetForLeft(this.m_arguments[0], this.m_arguments[1]);
				goto IL_0831;
			case FunctionName.Right:
				this.CheckArgsAndValues(2);
				sqlSnippet = this.CreateBasicSqlSnippetForRight(this.m_arguments[0], this.m_arguments[1]);
				goto IL_0831;
			case FunctionName.Concat:
				sqlSnippet = this.CreateSqlSnippetForConcat();
				goto IL_0831;
			case FunctionName.Lower:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForLower(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.Upper:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForUpper(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.LTrim:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForLTrim(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.RTrim:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForRTrim(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.Replace:
				this.CheckArgsAndValues(3);
				sqlSnippet = this.CreateBasicSqlSnippetForReplace(this.m_arguments[0], this.m_arguments[1], this.m_arguments[2]);
				goto IL_0831;
			case FunctionName.Date:
				sqlSnippet = this.CreateSqlSnippetForDate();
				goto IL_0831;
			case FunctionName.DateTime:
				this.CheckArgsAndValues(6);
				sqlSnippet = this.CreateBasicSqlSnippetForDateTime(this.m_arguments[0], this.m_functionNode.Arguments[0].GetResultType().DataType, this.m_arguments[1], this.m_functionNode.Arguments[1].GetResultType().DataType, this.m_arguments[2], this.m_functionNode.Arguments[2].GetResultType().DataType, this.m_arguments[3], this.m_functionNode.Arguments[3].GetResultType().DataType, this.m_arguments[4], this.m_functionNode.Arguments[4].GetResultType().DataType, this.m_arguments[5], this.m_functionNode.Arguments[5].GetResultType().DataType);
				goto IL_0831;
			case FunctionName.Year:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForYear(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.Quarter:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForQuarter(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.Month:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForMonth(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.Day:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForDay(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.Hour:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForHour(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.Minute:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForMinute(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.Second:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForSecond(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.DayOfYear:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForDayOfYear(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.Week:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForWeek(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.DayOfWeek:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForDayOfWeek(this.m_arguments[0]);
				goto IL_0831;
			case FunctionName.DateDiff:
				this.CheckArgsAndValues(3);
				sqlSnippet = this.CreateBasicSqlSnippetForDateDiff(this.GetDatePart(this.m_functionNode.Arguments[0].NodeAsLiteral), this.m_arguments[1], this.m_arguments[2]);
				goto IL_0831;
			case FunctionName.DateAdd:
				this.CheckArgsAndValues(3);
				sqlSnippet = this.CreateBasicSqlSnippetForDateAdd(this.GetDatePart(this.m_functionNode.Arguments[0].NodeAsLiteral), this.m_arguments[1], this.m_arguments[2]);
				goto IL_0831;
			case FunctionName.If:
				enumerable = this.CreateSqlSnippetsForIf();
				goto IL_0831;
			case FunctionName.Switch:
				sqlSnippet = this.CreateBasicSqlSnippetForSwitch(this.PrepareSwitchArguments());
				goto IL_0831;
			case FunctionName.Time:
				this.CheckArgsAndValues(1);
				sqlSnippet = this.CreateBasicSqlSnippetForTime(this.m_arguments[0]);
				goto IL_0831;
			}
			throw SQEAssert.AssertFalseAndThrow("Unknown function: {0}.", new object[] { this.m_functionNode.FunctionName.ToString() });
			IL_0831:
			base.Values.AddRange(this.FinalizeSqlSnippetCreation(sqlSnippet, enumerable));
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009CCD File Offset: 0x00007ECD
		internal static ISqlSnippet CastAsDecimal(ISqlSnippet argument)
		{
			return SqlFunctionExpression.CastAsDecimal(argument, 8);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009CD8 File Offset: 0x00007ED8
		internal static ISqlSnippet CastAsDecimal(ISqlSnippet argument, int scale)
		{
			if (argument is SqlExpression)
			{
				SqlFunctionExpression.CheckArgValues(1, (SqlExpression)argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				SqlExpression.AsDecimalOpenParen28Comma,
				new SqlStringSnippet(scale.ToString(CultureInfo.InvariantCulture)),
				SqlExpression.CloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009D3C File Offset: 0x00007F3C
		internal static ISqlSnippet CastAsFloat(ISqlSnippet argument)
		{
			if (argument is SqlExpression)
			{
				SqlFunctionExpression.CheckArgValues(1, (SqlExpression)argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				SqlExpression.AsFloatCloseParenSnippet
			});
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009D71 File Offset: 0x00007F71
		internal static ISqlSnippet CastAsReal(ISqlSnippet argument)
		{
			if (argument is SqlExpression)
			{
				SqlFunctionExpression.CheckArgValues(1, (SqlExpression)argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				SqlExpression.AsRealCloseParenSnippet
			});
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009DA6 File Offset: 0x00007FA6
		internal static ISqlSnippet ConvertToBoolean(SqlExpression sqlBitExpression)
		{
			SqlFunctionExpression.CheckArgValues(1, sqlBitExpression);
			return SqlFunctionExpression.HandleNullsInComparisonExpression(new SqlCompositeSnippet(new ISqlSnippet[]
			{
				sqlBitExpression,
				SqlExpression.EqualsSnippet,
				SqlExpression.OneSnippet
			}), sqlBitExpression, sqlBitExpression.IsNullable, SqlExpression.OneSnippet, false, false, true);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00009DE2 File Offset: 0x00007FE2
		protected SqlBatch SqlBatch
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_sqlBatch;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009DEA File Offset: 0x00007FEA
		protected FunctionNode FunctionNode
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_functionNode;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009DF2 File Offset: 0x00007FF2
		protected bool NeedToHandleUnknown
		{
			get
			{
				return this.m_isInNotContext;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009DFA File Offset: 0x00007FFA
		protected ISqlSnippet CreateSqlSnippetForTruncate(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			if (arg1 is SqlExpression)
			{
				SqlFunctionExpression.CheckArgValues(1, (SqlExpression)arg1);
			}
			if (arg2 is SqlExpression)
			{
				SqlFunctionExpression.CheckArgValues(1, (SqlExpression)arg2);
			}
			return this.CreateBasicSqlSnippetForTruncate(arg1, arg2);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009E2C File Offset: 0x0000802C
		protected ISqlSnippet CreateSqlSnippetForRound(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			if (arg1 is SqlExpression)
			{
				SqlFunctionExpression.CheckArgValues(1, (SqlExpression)arg1);
			}
			if (arg2 is SqlExpression)
			{
				SqlFunctionExpression.CheckArgValues(1, (SqlExpression)arg2);
			}
			return this.CreateBasicSqlSnippetForRound(arg1, arg2);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009E5E File Offset: 0x0000805E
		protected ISqlSnippet CreateSqlSnippetForString(ISqlSnippet argument, DataType argDataType)
		{
			if (argument is SqlExpression)
			{
				SqlFunctionExpression.CheckArgValues(1, (SqlExpression)argument);
			}
			return this.CreateBasicSqlSnippetForString(argument, argDataType);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009E7C File Offset: 0x0000807C
		protected static bool FindSqlNullExpression<T>(ref T arg1, ref T arg2) where T : ISqlSnippet
		{
			bool flag = arg1 is SqlNullExpression;
			bool flag2 = arg2 is SqlNullExpression;
			if (!flag && !flag2)
			{
				return false;
			}
			if (flag)
			{
				T t = arg1;
				arg1 = arg2;
				arg2 = t;
			}
			return true;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009ED7 File Offset: 0x000080D7
		protected virtual ISqlSnippet CreateBasicSqlSnippetForDivide(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				arg1,
				SqlExpression.DivideSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009F04 File Offset: 0x00008104
		protected virtual ISqlSnippet CreateBasicSqlSnippetForMod(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				arg1,
				SqlExpression.ModSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009F31 File Offset: 0x00008131
		protected virtual ISqlSnippet CreateBasicSqlSnippetForPower(ISqlSnippet pBase, DataType pBaseDataType, ISqlSnippet pExp, DataType pExpDataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.PowerOpenParenSnippet,
				pBase,
				SqlExpression.CommaSnippet,
				pExp,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060001FD RID: 509
		protected abstract ISqlSnippet CreateBasicSqlSnippetForTruncate(ISqlSnippet arg1, ISqlSnippet arg2);

		// Token: 0x060001FE RID: 510
		protected abstract ISqlSnippet CreateBasicSqlSnippetForInteger(ISqlSnippet argument, DataType argDataType);

		// Token: 0x060001FF RID: 511 RVA: 0x00009F5E File Offset: 0x0000815E
		protected virtual ISqlSnippet CreateBasicSqlSnippetForString(ISqlSnippet argument, DataType argDataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				SqlExpression.AsVarChar255CloseParenSnippet
			});
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009F7F File Offset: 0x0000817F
		protected virtual ISqlSnippet CreateBasicSqlSnippetForLength(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.LenOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000201 RID: 513
		protected abstract ISqlSnippet CreateBasicSqlSnippetForFind(ISqlSnippet searchIn, ISqlSnippet searchFor);

		// Token: 0x06000202 RID: 514
		protected abstract ISqlSnippet CreateBasicSqlSnippetForSubstring(ISqlSnippet source, ISqlSnippet start, ISqlSnippet length);

		// Token: 0x06000203 RID: 515 RVA: 0x00009FA0 File Offset: 0x000081A0
		protected virtual ISqlSnippet CreateBasicSqlSnippetForLeft(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.LeftOpenParenSnippet,
				arg1,
				SqlExpression.CommaSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009FCD File Offset: 0x000081CD
		protected virtual ISqlSnippet CreateBasicSqlSnippetForRight(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.RightOpenParenSnippet,
				arg1,
				SqlExpression.CommaSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009FFA File Offset: 0x000081FA
		protected virtual ISqlSnippet CreateBasicSqlSnippetForConcat(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				arg1,
				SqlExpression.AddSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A027 File Offset: 0x00008227
		protected virtual ISqlSnippet CreateBasicSqlSnippetForLower(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.LowerOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A048 File Offset: 0x00008248
		protected virtual ISqlSnippet CreateBasicSqlSnippetForUpper(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.UpperOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A069 File Offset: 0x00008269
		protected virtual ISqlSnippet CreateBasicSqlSnippetForLTrim(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.LTrimOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A08A File Offset: 0x0000828A
		protected virtual ISqlSnippet CreateBasicSqlSnippetForRTrim(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.RTrimOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A0AB File Offset: 0x000082AB
		protected virtual ISqlSnippet CreateBasicSqlSnippetForReplace(ISqlSnippet searchIn, ISqlSnippet searchFor, ISqlSnippet replace)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.ReplaceOpenParenSnippet,
				searchIn,
				SqlExpression.CommaSnippet,
				searchFor,
				SqlExpression.CommaSnippet,
				replace,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000A0E4 File Offset: 0x000082E4
		protected virtual ISqlSnippet CreateBasicSqlSnippetForRound(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.RoundOpenParenSnippet,
				this.m_arguments[0],
				SqlExpression.CommaSnippet,
				this.m_arguments[1],
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A120 File Offset: 0x00008320
		protected virtual ISqlSnippet ConvertNullToEmptyString(Expression expression, SqlExpression stringExpression)
		{
			if (stringExpression.Values.Count != 1)
			{
				throw SQEAssert.AssertFalseAndThrow("stringExpression must have exactly one value.", Array.Empty<object>());
			}
			if (!stringExpression.IsNullable)
			{
				return stringExpression;
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CoalesceOpenParenSnippet,
				stringExpression,
				SqlExpression.CommaEmpStrCloseParenSnippet
			});
		}

		// Token: 0x0600020D RID: 525
		protected abstract ISqlSnippet CreateBasicSqlSnippetForDate(ISqlSnippet argument);

		// Token: 0x0600020E RID: 526
		protected abstract ISqlSnippet CreateBasicSqlSnippetForDate(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType, ISqlSnippet arg3, DataType arg3DataType);

		// Token: 0x0600020F RID: 527
		protected abstract ISqlSnippet CreateBasicSqlSnippetForDateTime(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType, ISqlSnippet arg3, DataType arg3DataType, ISqlSnippet arg4, DataType arg4DataType, ISqlSnippet arg5, DataType arg5DataType, ISqlSnippet arg6, DataType arg6DataType);

		// Token: 0x06000210 RID: 528
		protected abstract ISqlSnippet CreateBasicSqlSnippetForTime(ISqlSnippet argument);

		// Token: 0x06000211 RID: 529
		protected abstract ISqlSnippet CreateBasicSqlSnippetForYear(ISqlSnippet argument);

		// Token: 0x06000212 RID: 530
		protected abstract ISqlSnippet CreateBasicSqlSnippetForQuarter(ISqlSnippet argument);

		// Token: 0x06000213 RID: 531
		protected abstract ISqlSnippet CreateBasicSqlSnippetForMonth(ISqlSnippet argument);

		// Token: 0x06000214 RID: 532
		protected abstract ISqlSnippet CreateBasicSqlSnippetForDay(ISqlSnippet argument);

		// Token: 0x06000215 RID: 533
		protected abstract ISqlSnippet CreateBasicSqlSnippetForHour(ISqlSnippet argument);

		// Token: 0x06000216 RID: 534
		protected abstract ISqlSnippet CreateBasicSqlSnippetForMinute(ISqlSnippet argument);

		// Token: 0x06000217 RID: 535
		protected abstract ISqlSnippet CreateBasicSqlSnippetForSecond(ISqlSnippet argument);

		// Token: 0x06000218 RID: 536
		protected abstract ISqlSnippet CreateBasicSqlSnippetForDayOfYear(ISqlSnippet argument);

		// Token: 0x06000219 RID: 537
		protected abstract ISqlSnippet CreateBasicSqlSnippetForWeek(ISqlSnippet argument);

		// Token: 0x0600021A RID: 538
		protected abstract ISqlSnippet CreateBasicSqlSnippetForDayOfWeek(ISqlSnippet argument);

		// Token: 0x0600021B RID: 539
		protected abstract ISqlSnippet CreateBasicSqlSnippetForDateDiff(SqlFunctionExpression.DatePart datePart, ISqlSnippet startDate, ISqlSnippet endDate);

		// Token: 0x0600021C RID: 540
		protected abstract ISqlSnippet CreateBasicSqlSnippetForDateAdd(SqlFunctionExpression.DatePart datePart, ISqlSnippet delta, ISqlSnippet date);

		// Token: 0x0600021D RID: 541 RVA: 0x0000A178 File Offset: 0x00008378
		protected virtual ISqlSnippet CreateBasicSqlSnippetForIf(ISqlSnippet condition, ISqlSnippet thenSnippet, ISqlSnippet elseSnippet)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CaseWhenSnippet,
				condition,
				SqlExpression.ThenSnippet,
				thenSnippet,
				SqlExpression.ElseSnippet,
				elseSnippet,
				SqlExpression.EndSnippet
			});
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A1B4 File Offset: 0x000083B4
		protected virtual ISqlSnippet CreateBasicSqlSnippetForSwitch(IEnumerable<KeyValuePair<ISqlSnippet, ISqlSnippet>> arguments)
		{
			SqlCompositeSnippet sqlCompositeSnippet = new SqlCompositeSnippet(new ISqlSnippet[] { SqlExpression.CaseSnippet });
			foreach (KeyValuePair<ISqlSnippet, ISqlSnippet> keyValuePair in arguments)
			{
				sqlCompositeSnippet.Append(SqlExpression.WhenSnippet);
				sqlCompositeSnippet.Append(keyValuePair.Key);
				sqlCompositeSnippet.Append(SqlExpression.ThenSnippet);
				sqlCompositeSnippet.Append(keyValuePair.Value);
			}
			sqlCompositeSnippet.Append(SqlExpression.EndSnippet);
			return sqlCompositeSnippet;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A248 File Offset: 0x00008448
		protected internal virtual void CreateBasicSqlSnippetForSingleValueIn(SqlExpression item, SqlLiteralExpression.LiteralSet literalSet, SqlCompositeSnippet inClause, bool isReal)
		{
			if (item.Values.Count != 1)
			{
				SQEAssert.AssertFalseAndThrow("'item' must a singled-valued expression in In function", Array.Empty<object>());
			}
			inClause.Append(new SqlCompositeSnippet(new ISqlSnippet[]
			{
				item,
				SqlExpression.InOpenParenSnippet
			}));
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < literalSet.Tuples.Count; i++)
			{
				ISqlSnippet sqlSnippet = literalSet.Tuples[i];
				if (SqlFunctionExpression.FindSqlNullExpression<ISqlSnippet>(ref sqlSnippet, ref sqlSnippet))
				{
					flag = true;
				}
				else
				{
					if (flag2)
					{
						inClause.Append(SqlExpression.CommaSnippet);
					}
					if (isReal)
					{
						sqlSnippet = SqlFunctionExpression.CastAsReal(sqlSnippet);
					}
					inClause.Append(sqlSnippet);
					flag2 = true;
				}
			}
			inClause.Append(SqlExpression.CloseParenSnippet);
			if (flag)
			{
				inClause.Prepend(SqlExpression.OpenParenSnippet);
				inClause.Append(SqlExpression.OrSnippet);
				inClause.Append(item);
				inClause.Append(SqlExpression.IsNullSnippet);
				inClause.Append(SqlExpression.CloseParenSnippet);
				return;
			}
			if (item.IsNullable && this.NeedToHandleUnknown)
			{
				inClause.Append(SqlExpression.AndSnippet);
				inClause.Append(SqlExpression.NotOpenParenSnippet);
				inClause.Append(item);
				inClause.Append(SqlExpression.IsNullSnippet);
				inClause.Append(SqlExpression.CloseParenSnippet);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A370 File Offset: 0x00008570
		private ISqlSnippet CreateSqlSnippetForLength()
		{
			this.CheckArgsAndValues(1);
			SqlExpression sqlExpression = this.m_arguments[0];
			ISqlSnippet sqlSnippet = this.CreateBasicSqlSnippetForLength(sqlExpression);
			if (sqlExpression.IsNullable)
			{
				sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CoalesceOpenParenSnippet,
					sqlSnippet,
					SqlExpression.CommaZeroCloseParenSnippet
				});
			}
			return sqlSnippet;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A3C0 File Offset: 0x000085C0
		private ISqlSnippet CreateSqlSnippetForFind()
		{
			this.CheckArgsAndValues(2);
			SqlExpression sqlExpression = this.m_arguments[0];
			SqlExpression sqlExpression2 = this.m_arguments[1];
			ISqlSnippet sqlSnippet = this.CreateBasicSqlSnippetForFind(sqlExpression, sqlExpression2);
			if (sqlExpression.IsNullable || sqlExpression2.IsNullable)
			{
				sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CoalesceOpenParenSnippet,
					sqlSnippet,
					SqlExpression.CommaZeroCloseParenSnippet
				});
			}
			return sqlSnippet;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A420 File Offset: 0x00008620
		private ISqlSnippet CreateSqlSnippetForConcat()
		{
			this.CheckArgsAndValues(2);
			ISqlSnippet sqlSnippet = this.ConvertNullToEmptyString(this.m_functionNode.Arguments[0], this.m_arguments[0]);
			ISqlSnippet sqlSnippet2 = this.ConvertNullToEmptyString(this.m_functionNode.Arguments[0], this.m_arguments[1]);
			return this.CreateBasicSqlSnippetForConcat(sqlSnippet, sqlSnippet2);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A47C File Offset: 0x0000867C
		private ISqlSnippet CreateSqlSnippetForDate()
		{
			this.CheckArgsAndValues(this.m_arguments.Length);
			int num = this.m_arguments.Length;
			if (num == 1)
			{
				return this.CreateBasicSqlSnippetForDate(this.m_arguments[0]);
			}
			if (num != 3)
			{
				throw SQEAssert.AssertFalseAndThrow("Invalid number of function arguments.", Array.Empty<object>());
			}
			return this.CreateBasicSqlSnippetForDate(this.m_arguments[0], this.m_functionNode.Arguments[0].GetResultType().DataType, this.m_arguments[1], this.m_functionNode.Arguments[1].GetResultType().DataType, this.m_arguments[2], this.m_functionNode.Arguments[2].GetResultType().DataType);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A544 File Offset: 0x00008744
		private SqlFunctionExpression.DatePart GetDatePart(LiteralNode datePartLiteral)
		{
			if (datePartLiteral == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("datePartLiteral"));
			}
			SqlFunctionExpression.DatePart datePart;
			try
			{
				datePart = (SqlFunctionExpression.DatePart)Enum.Parse(typeof(SqlFunctionExpression.DatePart), datePartLiteral.ValueAsString);
			}
			catch (Exception)
			{
				throw SQEAssert.AssertFalseAndThrow("Failed to parse date part literal: {0}.", new object[] { datePartLiteral.ValueAsString });
			}
			return datePart;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A5B0 File Offset: 0x000087B0
		private IEnumerable<ISqlSnippet> CreateSqlSnippetsForIf()
		{
			this.CheckArgs(3);
			ISqlSnippet sqlSnippet;
			if (!this.m_arguments[0].IsLogicalBooleanValue)
			{
				sqlSnippet = SqlFunctionExpression.ConvertToBoolean(this.m_arguments[0]);
			}
			else
			{
				ISqlSnippet sqlSnippet2 = this.m_arguments[0];
				sqlSnippet = sqlSnippet2;
			}
			ISqlSnippet condition = sqlSnippet;
			SqlExpression thenExpression = this.m_arguments[1];
			SqlExpression elseExpression = this.m_arguments[2];
			if (thenExpression.Values.Count < 1 || thenExpression.Values.Count != elseExpression.Values.Count)
			{
				throw SQEAssert.AssertFalseAndThrow("Invalid tuple in TrueCase or FalseCase of the If function.", Array.Empty<object>());
			}
			int num;
			for (int i = 0; i < thenExpression.Values.Count; i = num)
			{
				yield return this.CreateBasicSqlSnippetForIf(condition, thenExpression.Values[i], elseExpression.Values[i]);
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A5C0 File Offset: 0x000087C0
		private IEnumerable<KeyValuePair<ISqlSnippet, ISqlSnippet>> PrepareSwitchArguments()
		{
			if (this.m_arguments.Length == 0 || this.m_arguments.Length % 2 != 0)
			{
				throw SQEAssert.AssertFalseAndThrow("Invalid number of function arguments.", Array.Empty<object>());
			}
			this.CheckArgsAndValues(this.m_arguments.Length);
			int i = 0;
			while (i < this.m_arguments.Length)
			{
				SqlExpression[] arguments = this.m_arguments;
				int num = i;
				i = num + 1;
				SqlExpression sqlExpression = arguments[num];
				ISqlSnippet sqlSnippet;
				if (!sqlExpression.IsLogicalBooleanValue)
				{
					sqlSnippet = SqlFunctionExpression.ConvertToBoolean(sqlExpression);
				}
				else
				{
					ISqlSnippet sqlSnippet2 = sqlExpression;
					sqlSnippet = sqlSnippet2;
				}
				SqlExpression[] arguments2 = this.m_arguments;
				num = i;
				i = num + 1;
				yield return new KeyValuePair<ISqlSnippet, ISqlSnippet>(sqlSnippet, arguments2[num]);
			}
			yield break;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A5D0 File Offset: 0x000087D0
		private ISqlSnippet CreateSqlSnippetForBooleanFunction()
		{
			FunctionName functionName = this.m_functionNode.FunctionName;
			ISqlSnippet sqlSnippet;
			switch (functionName)
			{
			case FunctionName.Equals:
			case FunctionName.NotEquals:
				sqlSnippet = this.CreateSqlSnippetForEquals();
				break;
			case FunctionName.GreaterThan:
			case FunctionName.GreaterThanOrEquals:
			case FunctionName.LessThan:
			case FunctionName.LessThanOrEquals:
				sqlSnippet = this.CreateSqlSnippetForGreaterLessFunction();
				break;
			case FunctionName.And:
			case FunctionName.Or:
			{
				this.CheckArgsAndValues(2);
				ISqlSnippet sqlSnippet2;
				if (!this.m_arguments[0].IsLogicalBooleanValue)
				{
					sqlSnippet2 = SqlFunctionExpression.ConvertToBoolean(this.m_arguments[0]);
				}
				else
				{
					ISqlSnippet sqlSnippet3 = this.m_arguments[0];
					sqlSnippet2 = sqlSnippet3;
				}
				ISqlSnippet sqlSnippet4 = sqlSnippet2;
				ISqlSnippet sqlSnippet5;
				if (!this.m_arguments[1].IsLogicalBooleanValue)
				{
					sqlSnippet5 = SqlFunctionExpression.ConvertToBoolean(this.m_arguments[1]);
				}
				else
				{
					ISqlSnippet sqlSnippet3 = this.m_arguments[1];
					sqlSnippet5 = sqlSnippet3;
				}
				ISqlSnippet sqlSnippet6 = sqlSnippet5;
				if (this.m_functionNode.FunctionName == FunctionName.And)
				{
					sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
					{
						sqlSnippet4,
						SqlExpression.AndSnippet,
						sqlSnippet6
					});
				}
				else
				{
					sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
					{
						SqlExpression.OpenParenSnippet,
						sqlSnippet4,
						SqlExpression.OrSnippet,
						sqlSnippet6,
						SqlExpression.CloseParenSnippet
					});
				}
				break;
			}
			case FunctionName.Not:
			{
				this.CheckArgsAndValues(1);
				ISqlSnippet[] array = new ISqlSnippet[3];
				array[0] = SqlExpression.NotOpenParenSnippet;
				int num = 1;
				ISqlSnippet sqlSnippet7;
				if (!this.m_arguments[0].IsLogicalBooleanValue)
				{
					sqlSnippet7 = SqlFunctionExpression.ConvertToBoolean(this.m_arguments[0]);
				}
				else
				{
					ISqlSnippet sqlSnippet3 = this.m_arguments[0];
					sqlSnippet7 = sqlSnippet3;
				}
				array[num] = sqlSnippet7;
				array[2] = SqlExpression.CloseParenSnippet;
				sqlSnippet = new SqlCompositeSnippet(array);
				break;
			}
			default:
				if (functionName != FunctionName.In)
				{
					throw SQEAssert.AssertFalseAndThrow("Unknown boolean function: {0}.", new object[] { this.m_functionNode.FunctionName });
				}
				sqlSnippet = this.CreateSqlSnippetForIn();
				break;
			}
			if (!this.m_parentFunctionExpectsLogicalValue)
			{
				sqlSnippet = this.ConvertToBit(sqlSnippet);
			}
			return sqlSnippet;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A788 File Offset: 0x00008988
		private SqlCompositeSnippet CreateSqlSnippetForEquals()
		{
			this.CheckArgs(2);
			SqlExpression sqlExpression = this.m_arguments[0];
			SqlExpression sqlExpression2 = this.m_arguments[1];
			SqlFunctionExpression.CheckArgValues(sqlExpression.Values.Count, sqlExpression2);
			bool flag = SqlFunctionExpression.FindSqlNullExpression<SqlExpression>(ref sqlExpression, ref sqlExpression2);
			bool flag2 = SqlFunctionExpression.IsRealTraverser.IsReal(this.m_functionNode.Arguments[0]);
			bool flag3 = SqlFunctionExpression.IsRealTraverser.IsReal(this.m_functionNode.Arguments[1]);
			bool flag4 = this.m_functionNode.FunctionName == FunctionName.Equals;
			SqlCompositeSnippet sqlCompositeSnippet = (flag4 ? new SqlCompositeSnippet(Array.Empty<ISqlSnippet>()) : new SqlCompositeSnippet(new ISqlSnippet[] { SqlExpression.NotOpenParenSnippet }));
			for (int i = 0; i < sqlExpression.Values.Count; i++)
			{
				if (i > 0)
				{
					sqlCompositeSnippet.Append(SqlExpression.AndSnippet);
				}
				ISqlSnippet sqlSnippet = sqlExpression.Values[i];
				ISqlSnippet sqlSnippet2 = sqlExpression2.Values[i];
				bool flag5 = flag;
				if (!flag5)
				{
					flag5 = SqlFunctionExpression.FindSqlNullExpression<ISqlSnippet>(ref sqlSnippet, ref sqlSnippet2);
				}
				if (flag5)
				{
					sqlCompositeSnippet.Append(SqlFunctionExpression.HandleComparisonToSqlNull(FunctionName.Equals, sqlSnippet));
				}
				else
				{
					ISqlSnippet sqlSnippet3 = SqlFunctionExpression.CreateComparisonExpression(sqlSnippet, flag2, SqlExpression.EqualsSnippet, sqlSnippet2, flag3);
					sqlCompositeSnippet.Append(SqlFunctionExpression.HandleNullsInComparisonExpression(sqlSnippet3, sqlSnippet, sqlExpression.IsNullable, sqlSnippet2, sqlExpression2.IsNullable, true, this.NeedToHandleUnknown));
				}
			}
			if (!flag4)
			{
				sqlCompositeSnippet.Append(SqlExpression.CloseParenSnippet);
			}
			return sqlCompositeSnippet;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		private ISqlSnippet CreateSqlSnippetForGreaterLessFunction()
		{
			this.CheckArgsAndValues(2);
			SqlExpression sqlExpression = this.m_arguments[0];
			SqlExpression sqlExpression2 = this.m_arguments[1];
			if (SqlFunctionExpression.FindSqlNullExpression<SqlExpression>(ref sqlExpression, ref sqlExpression2))
			{
				return SqlFunctionExpression.HandleComparisonToSqlNull(this.m_functionNode.FunctionName, sqlExpression);
			}
			bool flag = SqlFunctionExpression.IsRealTraverser.IsReal(this.m_functionNode.Arguments[0]);
			bool flag2 = SqlFunctionExpression.IsRealTraverser.IsReal(this.m_functionNode.Arguments[1]);
			ISqlSnippet sqlSnippet;
			bool flag3;
			switch (this.m_functionNode.FunctionName)
			{
			case FunctionName.GreaterThan:
				sqlSnippet = SqlExpression.GreaterThanSnippet;
				flag3 = false;
				break;
			case FunctionName.GreaterThanOrEquals:
				sqlSnippet = SqlExpression.GreaterThanOrEqualsSnippet;
				flag3 = true;
				break;
			case FunctionName.LessThan:
				sqlSnippet = SqlExpression.LessThanSnippet;
				flag3 = false;
				break;
			case FunctionName.LessThanOrEquals:
				sqlSnippet = SqlExpression.LessThanOrEqualsSnippet;
				flag3 = true;
				break;
			default:
				throw SQEAssert.AssertFalseAndThrow("Unknown 'greater-less' function: {0}.", new object[] { this.m_functionNode.FunctionName });
			}
			return SqlFunctionExpression.HandleNullsInComparisonExpression(SqlFunctionExpression.CreateComparisonExpression(sqlExpression, flag, sqlSnippet, sqlExpression2, flag2), sqlExpression, sqlExpression.IsNullable, sqlExpression2, sqlExpression2.IsNullable, flag3, this.NeedToHandleUnknown);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000AA04 File Offset: 0x00008C04
		private ISqlSnippet CreateSqlSnippetForIn()
		{
			this.CheckArgs(2);
			SqlExpression sqlExpression = this.m_arguments[0];
			SqlLiteralExpression.LiteralSet literalSet = this.GetLiteralSet(this.m_arguments[1]);
			SqlCompositeSnippet sqlCompositeSnippet = new SqlCompositeSnippet(Array.Empty<ISqlSnippet>());
			if (sqlExpression.Values.Count == 0 || literalSet.Tuples.Count == 0)
			{
				throw SQEAssert.AssertFalseAndThrow("Invalid 'item' or 'set' argument in the In function.", Array.Empty<object>());
			}
			if (sqlExpression.Values.Count == 1)
			{
				this.CreateBasicSqlSnippetForSingleValueIn(sqlExpression, literalSet, sqlCompositeSnippet, SqlFunctionExpression.IsRealTraverser.IsReal(this.FunctionNode.Arguments[0]));
			}
			else
			{
				sqlCompositeSnippet = new SqlCompositeSnippet(new ISqlSnippet[] { SqlExpression.OpenParenSnippet });
				bool flag = SqlFunctionExpression.IsRealTraverser.IsReal(this.m_functionNode.Arguments[0]);
				for (int i = 0; i < literalSet.Tuples.Count; i++)
				{
					if (i > 0)
					{
						sqlCompositeSnippet.Append(SqlExpression.OrSnippet);
					}
					SqlTupleExpression sqlTupleExpression = literalSet.Tuples[i] as SqlTupleExpression;
					if (sqlTupleExpression == null || sqlExpression.Values.Count != sqlTupleExpression.Values.Count)
					{
						throw SQEAssert.AssertFalseAndThrow("Invalid tuple in literal set.", Array.Empty<object>());
					}
					for (int j = 0; j < sqlExpression.Values.Count; j++)
					{
						if (j > 0)
						{
							sqlCompositeSnippet.Append(SqlExpression.AndSnippet);
						}
						ISqlSnippet sqlSnippet = sqlExpression.Values[j];
						ISqlSnippet sqlSnippet2 = sqlTupleExpression.Values[j];
						if (SqlFunctionExpression.FindSqlNullExpression<ISqlSnippet>(ref sqlSnippet2, ref sqlSnippet2))
						{
							sqlCompositeSnippet.Append(SqlFunctionExpression.HandleComparisonToSqlNull(FunctionName.Equals, sqlSnippet));
						}
						else
						{
							ISqlSnippet sqlSnippet3 = SqlFunctionExpression.CreateComparisonExpression(sqlSnippet, flag, SqlExpression.EqualsSnippet, sqlSnippet2, false);
							sqlCompositeSnippet.Append(SqlFunctionExpression.HandleNullsInComparisonExpression(sqlSnippet3, sqlSnippet, sqlExpression.IsNullable, sqlSnippet2, sqlTupleExpression.IsNullable, true, this.NeedToHandleUnknown));
						}
					}
				}
				sqlCompositeSnippet.Append(SqlExpression.CloseParenSnippet);
			}
			return sqlCompositeSnippet;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		private SqlLiteralExpression.LiteralSet GetLiteralSet(SqlExpression inSetArgument)
		{
			SqlLiteralExpression sqlLiteralExpression = inSetArgument as SqlLiteralExpression;
			if (sqlLiteralExpression == null)
			{
				throw SQEAssert.AssertFalseAndThrow("inSetArgument must be SqlLiteralExpression.", Array.Empty<object>());
			}
			SqlFunctionExpression.CheckArgValues(1, inSetArgument);
			SqlLiteralExpression.LiteralSet literalSet = inSetArgument.Values[0] as SqlLiteralExpression.LiteralSet;
			if (literalSet == null)
			{
				literalSet = sqlLiteralExpression.ToSet();
			}
			return literalSet;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000AC2C File Offset: 0x00008E2C
		private static ISqlSnippet HandleComparisonToSqlNull(FunctionName comparisonFunction, ISqlSnippet arg1)
		{
			if (arg1 is SqlExpression)
			{
				SqlFunctionExpression.CheckArgValues(1, (SqlExpression)arg1);
			}
			switch (comparisonFunction)
			{
			case FunctionName.Equals:
			case FunctionName.GreaterThanOrEquals:
			case FunctionName.LessThanOrEquals:
				return new SqlCompositeSnippet(new ISqlSnippet[]
				{
					arg1,
					SqlExpression.IsNullSnippet
				});
			case FunctionName.GreaterThan:
			case FunctionName.LessThan:
				return SqlExpression.BoolFalseSnippet;
			}
			throw SQEAssert.AssertFalseAndThrow("Unknown comparison function: {0}.", new object[] { comparisonFunction });
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		private static ISqlSnippet HandleNullsInComparisonExpression(ISqlSnippet comparisonExpression, ISqlSnippet arg1, bool arg1Nullable, ISqlSnippet arg2, bool arg2Nullable, bool nullEqualsNull, bool handleUnknown)
		{
			SqlCompositeSnippet sqlCompositeSnippet = new SqlCompositeSnippet(Array.Empty<ISqlSnippet>());
			nullEqualsNull &= arg1Nullable && arg2Nullable;
			handleUnknown &= arg1Nullable || arg2Nullable;
			if (nullEqualsNull)
			{
				sqlCompositeSnippet.Append(SqlExpression.OpenParenSnippet);
			}
			sqlCompositeSnippet.Append(comparisonExpression);
			if (handleUnknown)
			{
				sqlCompositeSnippet.Append(SqlExpression.AndSnippet);
				sqlCompositeSnippet.Append(SqlExpression.NotOpenParenSnippet);
				if (arg1Nullable)
				{
					sqlCompositeSnippet.Append(arg1);
					sqlCompositeSnippet.Append(SqlExpression.IsNullSnippet);
				}
				if (arg2Nullable)
				{
					if (arg1Nullable)
					{
						sqlCompositeSnippet.Append(SqlExpression.OrSnippet);
					}
					sqlCompositeSnippet.Append(arg2);
					sqlCompositeSnippet.Append(SqlExpression.IsNullSnippet);
				}
				sqlCompositeSnippet.Append(SqlExpression.CloseParenSnippet);
			}
			if (nullEqualsNull)
			{
				sqlCompositeSnippet.Append(SqlExpression.OrSnippet);
				sqlCompositeSnippet.Append(arg1);
				sqlCompositeSnippet.Append(SqlExpression.IsNullSnippet);
				sqlCompositeSnippet.Append(SqlExpression.AndSnippet);
				sqlCompositeSnippet.Append(arg2);
				sqlCompositeSnippet.Append(SqlExpression.IsNullSnippet);
			}
			if (nullEqualsNull)
			{
				sqlCompositeSnippet.Append(SqlExpression.CloseParenSnippet);
			}
			return sqlCompositeSnippet;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000AD94 File Offset: 0x00008F94
		private static bool IsInNotContext(FunctionName currentFunctionName, FunctionContext functionContext)
		{
			if (currentFunctionName == FunctionName.NotEquals)
			{
				return true;
			}
			foreach (FunctionContext.Frame frame in functionContext)
			{
				if (frame.FunctionNode.FunctionName == FunctionName.Not || frame.FunctionNode.FunctionName == FunctionName.NotEquals)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000AE08 File Offset: 0x00009008
		private static bool ParentFunctionExpectsLogicalValue(FunctionContext functionContext)
		{
			return functionContext.Current != null && functionContext.Current.IsInBooleanArgument;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000AE20 File Offset: 0x00009020
		private ISqlSnippet ConvertToBit(ISqlSnippet sqlBooleanExpression)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CaseWhenSnippet,
				sqlBooleanExpression,
				SqlExpression.ThenSnippet,
				this.m_sqlBatch.SqlBitTrueSnippet,
				SqlExpression.ElseSnippet,
				this.m_sqlBatch.SqlBitFalseSnippet,
				SqlExpression.EndSnippet
			});
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000AE78 File Offset: 0x00009078
		private static ISqlSnippet CreateComparisonExpression(ISqlSnippet operand1, bool operand1IsReal, ISqlSnippet comparisonOpSnippet, ISqlSnippet operand2, bool operand2IsReal)
		{
			if (operand1IsReal && !operand2IsReal)
			{
				operand2 = SqlFunctionExpression.CastAsReal(operand2);
			}
			else if (!operand1IsReal && operand2IsReal)
			{
				operand1 = SqlFunctionExpression.CastAsReal(operand1);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[] { operand1, comparisonOpSnippet, operand2 });
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000AEB4 File Offset: 0x000090B4
		private void CheckArgsAndValues(int argNum)
		{
			this.CheckArgs(argNum);
			for (int i = 0; i < this.m_arguments.Length; i++)
			{
				SqlFunctionExpression.CheckArgValues(1, this.m_arguments[i]);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000AEE9 File Offset: 0x000090E9
		private void CheckArgs(int argNum)
		{
			if (this.m_arguments.Length != argNum)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("argNum", "Invalid number of function arguments."));
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000AF0B File Offset: 0x0000910B
		private static void CheckArgValues(int argValNum, SqlExpression argument)
		{
			if (argument.Values.Count != argValNum)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("argValNum", "Invalid number of function arguments."));
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000AF30 File Offset: 0x00009130
		private static IEnumerable<ISqlSnippet> ConvertSqlSnippetToEnumerable(ISqlSnippet value)
		{
			yield return value;
			yield break;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000AF40 File Offset: 0x00009140
		private IEnumerable<ISqlSnippet> FinalizeSqlSnippetCreation(ISqlSnippet value, IEnumerable<ISqlSnippet> values)
		{
			if ((value != null && values != null) || (value == null && values == null))
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			if (value != null)
			{
				yield return this.m_needToCastReturnValueAsDecimal ? SqlFunctionExpression.CastAsDecimal(value) : value;
			}
			else
			{
				foreach (ISqlSnippet sqlSnippet in values)
				{
					yield return this.m_needToCastReturnValueAsDecimal ? SqlFunctionExpression.CastAsDecimal(sqlSnippet) : sqlSnippet;
				}
				IEnumerator<ISqlSnippet> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x040000CF RID: 207
		private readonly SqlBatch m_sqlBatch;

		// Token: 0x040000D0 RID: 208
		private readonly FunctionNode m_functionNode;

		// Token: 0x040000D1 RID: 209
		private readonly SqlExpression[] m_arguments;

		// Token: 0x040000D2 RID: 210
		private readonly bool m_isInNotContext;

		// Token: 0x040000D3 RID: 211
		private readonly bool m_parentFunctionExpectsLogicalValue;

		// Token: 0x040000D4 RID: 212
		private readonly bool m_needToCastReturnValueAsDecimal;

		// Token: 0x020000BC RID: 188
		protected enum DatePart
		{
			// Token: 0x0400035D RID: 861
			Year,
			// Token: 0x0400035E RID: 862
			Quarter,
			// Token: 0x0400035F RID: 863
			Month,
			// Token: 0x04000360 RID: 864
			Day,
			// Token: 0x04000361 RID: 865
			Hour,
			// Token: 0x04000362 RID: 866
			Minute,
			// Token: 0x04000363 RID: 867
			Second,
			// Token: 0x04000364 RID: 868
			Week
		}

		// Token: 0x020000BD RID: 189
		private sealed class IsRealTraverser : TraverseExpressionAlgorithm<bool>
		{
			// Token: 0x060006D6 RID: 1750 RVA: 0x0001B1BC File Offset: 0x000193BC
			internal static bool IsReal(Expression expression)
			{
				return expression.GetResultType().DataType == DataType.Float && SqlFunctionExpression.IsRealTraverser.Instance.Traverse(expression);
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x00004555 File Offset: 0x00002755
			protected override bool LiteralVisitor(LiteralNode literalNode)
			{
				return false;
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x0001B1E7 File Offset: 0x000193E7
			protected override bool AttributeRefVisitor(DsvColumn dsvColumn)
			{
				return dsvColumn.DataType == typeof(float);
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x0001B1FE File Offset: 0x000193FE
			protected override bool EntityRefVisitor(Type[] keyPartTypes)
			{
				throw SQEAssert.AssertFalseAndThrow("EntityRefVisitor must not be called during Real detection.", Array.Empty<object>());
			}

			// Token: 0x060006DA RID: 1754 RVA: 0x0001B210 File Offset: 0x00019410
			protected override bool FunctionVisitor(FunctionNode functionNode)
			{
				if (functionNode.GetFunctionInfo().IsAggregate && functionNode.FunctionName != FunctionName.Min && functionNode.FunctionName != FunctionName.Max)
				{
					return false;
				}
				FunctionName functionName = functionNode.FunctionName;
				if (functionName == FunctionName.Power || functionName == FunctionName.Float)
				{
					return false;
				}
				foreach (Expression expression in functionNode.Arguments)
				{
					if (expression.GetResultType().DataType == DataType.Float && !base.Traverse(expression))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060006DB RID: 1755 RVA: 0x0001B2B4 File Offset: 0x000194B4
			private IsRealTraverser()
			{
			}

			// Token: 0x04000365 RID: 869
			private static readonly SqlFunctionExpression.IsRealTraverser Instance = new SqlFunctionExpression.IsRealTraverser();
		}
	}
}
