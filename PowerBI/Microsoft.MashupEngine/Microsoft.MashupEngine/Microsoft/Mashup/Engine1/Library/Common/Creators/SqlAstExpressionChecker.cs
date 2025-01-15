using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Sql;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x020011B2 RID: 4530
	internal sealed class SqlAstExpressionChecker : DbAstExpressionChecker
	{
		// Token: 0x060077AA RID: 30634 RVA: 0x0019ECB4 File Offset: 0x0019CEB4
		public SqlAstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
			this.dbEnvironment = externalEnvironment;
			this.supportsDateTime2 = externalEnvironment.SqlSettings.GetSetting<bool>("SupportsDateTime2", true);
			this.supportsDateFromParts = externalEnvironment.SqlSettings.GetSetting<bool>("SupportsDateFromParts", true);
		}

		// Token: 0x170020BB RID: 8379
		// (get) Token: 0x060077AB RID: 30635 RVA: 0x00002139 File Offset: 0x00000339
		protected override bool CanCastNumericPrecision
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060077AC RID: 30636 RVA: 0x0019ECF4 File Offset: 0x0019CEF4
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			SqlAstExpressionChecker sqlAstExpressionChecker = new SqlAstExpressionChecker(expression, cursor, externalEnvironment);
			sqlAstExpressionChecker.Check(new DbAstExpressionChecker.SqlCheckerContext(sqlAstExpressionChecker));
		}

		// Token: 0x060077AD RID: 30637 RVA: 0x0019ED09 File Offset: 0x0019CF09
		public static void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			SqlAstExpressionChecker sqlAstExpressionChecker = new SqlAstExpressionChecker(expression, cursor, externalEnvironment);
			sqlAstExpressionChecker.CheckStatement(new DbAstExpressionChecker.SqlCheckerContext(sqlAstExpressionChecker));
		}

		// Token: 0x060077AE RID: 30638 RVA: 0x0019ED20 File Offset: 0x0019CF20
		protected override Dictionary<FunctionValue, Action<IInvocationExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Action<IInvocationExpression>> functions = base.GetFunctions();
			functions.AddRange(new Dictionary<FunctionValue, Action<IInvocationExpression>>
			{
				{
					TimeSpecificFunction.DateTimeFixedLocalNow,
					base.CheckArgumentCount(0)
				},
				{
					TimeSpecificFunction.DateTimeLocalNow,
					base.CheckArgumentCount(0)
				},
				{
					TimeSpecificFunction.DateTimeZoneFixedLocalNow,
					base.CheckArgumentCount(0)
				},
				{
					TimeSpecificFunction.DateTimeZoneLocalNow,
					base.CheckArgumentCount(0)
				},
				{
					TimeSpecificFunction.DateTimeZoneFixedUtcNow,
					base.CheckArgumentCount(0)
				},
				{
					TimeSpecificFunction.DateTimeZoneUtcNow,
					base.CheckArgumentCount(0)
				},
				{
					TableModule.Table.Pivot,
					new Action<IInvocationExpression>(base.CheckTablePivot)
				},
				{
					TableModule.Table.Unpivot,
					new Action<IInvocationExpression>(base.CheckTableUnpivot)
				},
				{
					Library.Number.RoundDown,
					new Action<IInvocationExpression>(base.CheckArgumentsForRoundUpAndRoundDown)
				},
				{
					Library.Number.RoundUp,
					new Action<IInvocationExpression>(base.CheckArgumentsForRoundUpAndRoundDown)
				},
				{
					Library.Date.AddDays,
					base.CheckArgumentTypesForDateFunctions("Date.AddDays", TypeValue.Number.Nullable)
				},
				{
					Library.Date.AddWeeks,
					base.CheckArgumentTypesForDateFunctions("Date.AddWeeks", TypeValue.Number.Nullable)
				},
				{
					Library.Date.AddQuarters,
					base.CheckArgumentTypesForDateFunctions("Date.AddQuarters", TypeValue.Number.Nullable)
				},
				{
					Library.Date.Year,
					new Action<IInvocationExpression>(this.CheckInputArgumentHasDateComponent)
				},
				{
					Library.Date.QuarterOfYear,
					new Action<IInvocationExpression>(this.CheckInputArgumentHasDateComponent)
				},
				{
					Library.Date.Month,
					new Action<IInvocationExpression>(this.CheckInputArgumentHasDateComponent)
				},
				{
					Library.Date.DayOfYear,
					new Action<IInvocationExpression>(this.CheckInputArgumentHasDateComponent)
				},
				{
					Library.Date.EndOfDay,
					new Action<IInvocationExpression>(this.CheckDateEndOfDay)
				},
				{
					Library.Date.StartOfMonth,
					new Action<IInvocationExpression>(this.CheckDateStartOrEndOfMQY)
				},
				{
					Library.Date.EndOfMonth,
					new Action<IInvocationExpression>(this.CheckDateStartOrEndOfMQY)
				},
				{
					Library.Date.StartOfQuarter,
					new Action<IInvocationExpression>(this.CheckDateStartOrEndOfMQY)
				},
				{
					Library.Date.EndOfQuarter,
					new Action<IInvocationExpression>(this.CheckDateStartOrEndOfMQY)
				},
				{
					Library.Date.StartOfYear,
					new Action<IInvocationExpression>(this.CheckDateStartOrEndOfMQY)
				},
				{
					Library.Date.EndOfYear,
					new Action<IInvocationExpression>(this.CheckDateStartOrEndOfMQY)
				},
				{
					CultureSpecificFunction.DateWeekOfYear,
					new Action<IInvocationExpression>(this.CheckInputArgumentHasDateComponent)
				},
				{
					CultureSpecificFunction.DateDayOfWeek,
					new Action<IInvocationExpression>(this.CheckArgumentsForWeekFunctions)
				},
				{
					CultureSpecificFunction.DateStartOfWeek,
					new Action<IInvocationExpression>(this.CheckDateStartOfWeek)
				},
				{
					CultureSpecificFunction.DateEndOfWeek,
					new Action<IInvocationExpression>(this.CheckDateEndOfWeek)
				},
				{
					Library.Date.Day,
					new Action<IInvocationExpression>(this.CheckInputArgumentHasDateComponent)
				},
				{
					Library.Time.Hour,
					new Action<IInvocationExpression>(this.CheckInputArgumentHasTimeComponent)
				},
				{
					Library.Time.Minute,
					new Action<IInvocationExpression>(this.CheckInputArgumentHasTimeComponent)
				},
				{
					Library.Time.Second,
					new Action<IInvocationExpression>(this.CheckInputArgumentHasTimeComponent)
				},
				{
					Library.Time.StartOfHour,
					new Action<IInvocationExpression>(this.CheckTimeStartOrEndOfHour)
				},
				{
					Library.Time.EndOfHour,
					new Action<IInvocationExpression>(this.CheckTimeStartOrEndOfHour)
				},
				{
					Library.Duration.TotalHours,
					new Action<IInvocationExpression>(this.CheckArgumentForDurationTotalHours)
				},
				{
					Library.Duration.TotalMinutes,
					new Action<IInvocationExpression>(this.CheckArgumentForDurationTotalMinutes)
				},
				{
					Library.Duration.TotalSeconds,
					new Action<IInvocationExpression>(this.CheckArgumentForDurationTotalSeconds)
				},
				{
					Library.Duration.duration,
					new Action<IInvocationExpression>(this.CheckDurationLiteral)
				},
				{
					CultureSpecificFunction.ByteFrom,
					new Action<IInvocationExpression>(this.CheckInt64FromHasFoldableArguments)
				},
				{
					CultureSpecificFunction.Int8From,
					new Action<IInvocationExpression>(this.CheckInt64FromHasFoldableArguments)
				},
				{
					CultureSpecificFunction.Int16From,
					new Action<IInvocationExpression>(this.CheckInt64FromHasFoldableArguments)
				},
				{
					CultureSpecificFunction.Int32From,
					new Action<IInvocationExpression>(this.CheckInt64FromHasFoldableArguments)
				},
				{
					CultureSpecificFunction.Int64From,
					new Action<IInvocationExpression>(this.CheckInt64FromHasFoldableArguments)
				},
				{
					CultureSpecificFunction.CurrencyFrom,
					new Action<IInvocationExpression>(this.CheckCurrencyFromHasFoldableArguments)
				},
				{
					Library.Logical.From,
					new Action<IInvocationExpression>(this.CheckLogicalFromHasFoldableArguments)
				},
				{
					Library.Text.Start,
					base.CheckArgumentTypes("Text.Start", new TypeValue[]
					{
						NullableTypeValue.Text,
						TypeValue.Int32
					})
				},
				{
					Library.Text.End,
					base.CheckArgumentTypes("Text.End", new TypeValue[]
					{
						NullableTypeValue.Text,
						TypeValue.Int32
					})
				},
				{
					Library.Text.Middle,
					base.CheckArgumentTypes("Text.Middle", new TypeValue[]
					{
						NullableTypeValue.Text,
						TypeValue.Int32,
						TypeValue.Int32
					})
				},
				{
					Library.Text.Replace,
					base.CheckArgumentTypes("Text.Replace", new TypeValue[]
					{
						NullableTypeValue.Text,
						TypeValue.Text,
						TypeValue.Text
					})
				},
				{
					Library.Text.PositionOf,
					base.CheckArgumentTypes("Text.PositionOf", new TypeValue[]
					{
						TypeValue.Text,
						TypeValue.Text
					})
				},
				{
					TypeSpecificFunction.TextNewGuid,
					base.CheckArgumentCount(0)
				}
			});
			return functions;
		}

		// Token: 0x060077AF RID: 30639 RVA: 0x0019F248 File Offset: 0x0019D448
		protected override Dictionary<FunctionValue, Action<IInvocationExpression>> GetStatementFunctions()
		{
			return new Dictionary<FunctionValue, Action<IInvocationExpression>>
			{
				{
					ActionModule.Action.Bind,
					new Action<IInvocationExpression>(base.CheckBind)
				},
				{
					ActionModule.TableAction.InsertRows,
					new Action<IInvocationExpression>(base.CheckInsertRows)
				},
				{
					ActionModule.TableAction.UpdateRows,
					new Action<IInvocationExpression>(base.CheckUpdateRows)
				},
				{
					ActionModule.TableAction.DeleteRows,
					new Action<IInvocationExpression>(base.CheckDeleteRows)
				}
			};
		}

		// Token: 0x060077B0 RID: 30640 RVA: 0x0019F2B8 File Offset: 0x0019D4B8
		protected override void CheckTableJoin(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckTableJoin"))
			{
				TableTypeAlgebra.JoinKind joinKind = base.CheckTableJoinAndGetKind(invocation);
				if (joinKind > TableTypeAlgebra.JoinKind.RightSemi)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060077B1 RID: 30641 RVA: 0x0019F30C File Offset: 0x0019D50C
		private void CheckNumberFromHasFoldableArguments(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckNumberFromHasFoldableArguments"))
			{
				switch (base.GetType(invocation.Arguments[0]).TypeKind)
				{
				case ValueKind.Null:
				case ValueKind.Time:
				case ValueKind.Date:
				case ValueKind.DateTime:
				case ValueKind.DateTimeZone:
				case ValueKind.Number:
				case ValueKind.Logical:
					goto IL_007C;
				case ValueKind.Text:
					if (!base.ExternalEnvironment.UnsafeTypeConversions)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					goto IL_007C;
				}
				throw base.FoldingTracingService.NewFoldingFailureException(null);
				IL_007C:
				base.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x060077B2 RID: 30642 RVA: 0x0019F3B8 File Offset: 0x0019D5B8
		protected override void CheckArgumentForNumberFrom(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckArgumentForNumberFrom"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Number.From", 1));
				}
				this.CheckNumberFromHasFoldableArguments(invocation);
			}
		}

		// Token: 0x060077B3 RID: 30643 RVA: 0x0019F420 File Offset: 0x0019D620
		private void CheckArgumentForDurationTotalHours(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForDurationTotalHours"))
			{
				if (expression.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Duration.TotalHours", 1));
				}
				base.InternalCheckArgumentForDurationFromTypes(expression);
			}
		}

		// Token: 0x060077B4 RID: 30644 RVA: 0x0019F488 File Offset: 0x0019D688
		private void CheckArgumentForDurationTotalMinutes(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForDurationTotalMinutes"))
			{
				if (expression.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Duration.TotalMinutes", 1));
				}
				base.InternalCheckArgumentForDurationFromTypes(expression);
			}
		}

		// Token: 0x060077B5 RID: 30645 RVA: 0x0019F4F0 File Offset: 0x0019D6F0
		private void CheckArgumentForDurationTotalSeconds(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckArgumentForDurationTotalSeconds"))
			{
				if (expression.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Duration.TotalSeconds", 1));
				}
				base.InternalCheckArgumentForDurationFromTypes(expression);
			}
		}

		// Token: 0x060077B6 RID: 30646 RVA: 0x0019F558 File Offset: 0x0019D758
		protected override void CheckArgumentForDateStartOfDay(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckArgumentForDateStartOfDay"))
			{
				if (!this.supportsDateFromParts && base.GetType(expression.Arguments[0]).Kind == ValueKind.DateTimeZone)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckArgumentForDateStartOfDay(expression);
			}
		}

		// Token: 0x060077B7 RID: 30647 RVA: 0x0019F5C8 File Offset: 0x0019D7C8
		private void CheckDateStartOfWeek(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckDateStartOfWeek"))
			{
				ValueKind kind = base.GetType(expression.Arguments[0]).Kind;
				if (!this.supportsDateFromParts && kind == ValueKind.DateTimeZone)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckArgumentsForWeekFunctions(expression);
			}
		}

		// Token: 0x060077B8 RID: 30648 RVA: 0x0019F63C File Offset: 0x0019D83C
		private void CheckDateEndOfWeek(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckDateEndOfWeek"))
			{
				ValueKind kind = base.GetType(expression.Arguments[0]).Kind;
				if (!this.supportsDateFromParts && (kind == ValueKind.DateTime || kind == ValueKind.DateTimeZone))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckArgumentsForWeekFunctions(expression);
			}
		}

		// Token: 0x060077B9 RID: 30649 RVA: 0x0019F6B4 File Offset: 0x0019D8B4
		private void CheckArgumentsForWeekFunctions(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckArgumentsForWeekFunctions"))
			{
				int count = expression.Arguments.Count;
				ValueKind valueKind;
				if (count != 1)
				{
					if (count != 2)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					IExpression expression2 = expression.Arguments[1];
					if (!(expression2 is IConstantExpression))
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					valueKind = base.GetType(expression2).TypeKind;
					if (valueKind != ValueKind.Null && valueKind != ValueKind.Number)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
				valueKind = base.GetType(expression.Arguments[0]).TypeKind;
				if (valueKind - ValueKind.Date > 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckArgumentsAreValid(expression);
			}
		}

		// Token: 0x060077BA RID: 30650 RVA: 0x0019F784 File Offset: 0x0019D984
		private void CheckDateEndOfDay(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckDateEndOfDay"))
			{
				if (expression.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Date.EndOfDay", 1));
				}
				base.CheckArgumentsAreValid(expression);
				ValueKind typeKind = base.GetType(expression.Arguments[0]).TypeKind;
				if (typeKind != ValueKind.Date)
				{
					if (typeKind - ValueKind.DateTime > 1)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					if (!this.supportsDateFromParts)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
			}
		}

		// Token: 0x060077BB RID: 30651 RVA: 0x0019F830 File Offset: 0x0019DA30
		private void CheckDateStartOrEndOfMQY(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckDateStartOrEndOfMQY"))
			{
				if (expression.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckArgumentsAreValid(expression);
				ValueKind typeKind = base.GetType(expression.Arguments[0]).TypeKind;
				if (typeKind - ValueKind.Date > 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (!this.supportsDateFromParts)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060077BC RID: 30652 RVA: 0x0019F8CC File Offset: 0x0019DACC
		private void CheckInputArgumentHasDateComponent(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckInputArgumentHasDateComponent"))
			{
				if (expression.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckArgumentsAreValid(expression);
				IExpression expression2 = expression.Arguments[0];
				TypeValue type = base.GetType(expression2);
				if (type.TypeKind != ValueKind.Date && type.TypeKind != ValueKind.DateTime && type.TypeKind != ValueKind.DateTimeZone)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060077BD RID: 30653 RVA: 0x0019F968 File Offset: 0x0019DB68
		private void CheckTimeStartOrEndOfHour(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckTimeStartOrEndOfHour"))
			{
				if (expression.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckArgumentsAreValid(expression);
				TypeValue type = base.GetType(expression.Arguments[0]);
				if (type.TypeKind != ValueKind.DateTime && type.TypeKind != ValueKind.DateTimeZone && type.TypeKind != ValueKind.Time)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (!this.supportsDateFromParts)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060077BE RID: 30654 RVA: 0x0019FA14 File Offset: 0x0019DC14
		private void CheckInputArgumentHasTimeComponent(IInvocationExpression expression)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckInputArgumentHasTimeComponent"))
			{
				base.CheckArgumentsAreValid(expression);
				IExpression expression2 = expression.Arguments[0];
				TypeValue type = base.GetType(expression2);
				if (type.TypeKind != ValueKind.DateTime && type.TypeKind != ValueKind.DateTimeZone && type.TypeKind != ValueKind.Time)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060077BF RID: 30655 RVA: 0x0019FA94 File Offset: 0x0019DC94
		private void CheckDurationLiteral(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DbAstExpressionChecker.CheckDurationLiteral"))
			{
				if (invocation.Arguments.Count != 4)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("#duration", 4));
				}
				foreach (IExpression expression in invocation.Arguments)
				{
					if (base.GetType(expression).TypeKind != ValueKind.Number)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
				base.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x060077C0 RID: 30656 RVA: 0x0019FB4C File Offset: 0x0019DD4C
		protected override void CheckSubtractOperation(IBinaryExpression binary)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckSubtractOperation"))
			{
				SqlDataType sqlScalarType = this.dbEnvironment.GetSqlScalarType(base.GetType(binary.Left));
				SqlDataType sqlScalarType2 = this.dbEnvironment.GetSqlScalarType(base.GetType(binary.Right));
				if ((sqlScalarType == SqlDataType.DateTimeOffset && SqlEnvironment.IsMDateTimeCompatibleType(sqlScalarType2)) || (sqlScalarType2 == SqlDataType.DateTimeOffset && SqlEnvironment.IsMDateTimeCompatibleType(sqlScalarType)))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if ((sqlScalarType == SqlDataType.Time && SqlEnvironment.IsMDateTimeCompatibleType(sqlScalarType2)) || (sqlScalarType2 == SqlDataType.Time && SqlEnvironment.IsMDateTimeCompatibleType(sqlScalarType)))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckRequiredScalarType(binary);
			}
		}

		// Token: 0x060077C1 RID: 30657 RVA: 0x0019FC18 File Offset: 0x0019DE18
		protected override void CheckArgumentForDateFromTypes(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckArgumentForDateFromTypes"))
			{
				base.IsFoldableDateFromTypes(invocation);
				if ((base.GetType(invocation).TypeKind == ValueKind.DateTimeZone || base.GetType(invocation).TypeKind == ValueKind.Date) && !this.supportsDateTime2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060077C2 RID: 30658 RVA: 0x0019FC8C File Offset: 0x0019DE8C
		private void CheckDateTimeFromHasFoldableArguments(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckDateTimeFromHasFoldableArguments"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("DateTime.From", 1));
				}
				switch (base.GetType(invocation.Arguments[0]).TypeKind)
				{
				case ValueKind.Null:
				case ValueKind.Time:
				case ValueKind.Date:
				case ValueKind.DateTime:
				case ValueKind.DateTimeZone:
					goto IL_00A1;
				case ValueKind.Number:
				case ValueKind.Text:
					if (!base.ExternalEnvironment.UnsafeTypeConversions)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					goto IL_00A1;
				}
				throw base.FoldingTracingService.NewFoldingFailureException(null);
				IL_00A1:
				base.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x060077C3 RID: 30659 RVA: 0x0019FD60 File Offset: 0x0019DF60
		protected override void CheckArgumentForDateTimeFromTypes(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckArgumentForDateTimeFromTypes"))
			{
				this.CheckDateTimeFromHasFoldableArguments(invocation);
				if ((base.GetType(invocation).TypeKind == ValueKind.DateTimeZone || base.GetType(invocation).TypeKind == ValueKind.Date) && !this.supportsDateTime2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060077C4 RID: 30660 RVA: 0x0019FDD4 File Offset: 0x0019DFD4
		protected override void CheckArgumentForDateTimeZoneFromTypes(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckArgumentForDateTimeZoneFromTypes"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("DateTimeZone.From", 1));
				}
				ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
				if (typeKind - ValueKind.Date > 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckArgumentsAreValid(invocation);
				if ((base.GetType(invocation).TypeKind == ValueKind.DateTimeZone || base.GetType(invocation).TypeKind == ValueKind.Date) && !this.supportsDateTime2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060077C5 RID: 30661 RVA: 0x0019FE98 File Offset: 0x0019E098
		private void CheckInt64FromHasFoldableArguments(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckInt64FromHasFoldableArguments"))
			{
				if (invocation.Arguments.Count < 1 || invocation.Arguments.Count > 3)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				Value value;
				if (!invocation.Function.TryGetConstant(out value))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (!value.AsFunction.Type.AsFunctionType.ReturnType.Equals(TypeValue.Int64.Nullable))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				Value value2;
				Value value3;
				if ((invocation.Arguments.Count > 1 && (!invocation.Arguments[1].TryGetConstant(out value2) || !value2.IsNull)) || (invocation.Arguments.Count > 2 && (!invocation.Arguments[2].TryGetConstant(out value3) || (!value3.IsNull && !value3.Equals(Library.RoundingMode.AwayFromZero)))))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckNumberFromHasFoldableArguments(invocation);
				if (base.GetType(invocation.Arguments[0]).TypeKind == ValueKind.Number && !base.ExternalEnvironment.UnsafeTypeConversions)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060077C6 RID: 30662 RVA: 0x001A0004 File Offset: 0x0019E204
		protected override void CheckSingleFromHasFoldableArguments(IInvocationExpression invocation)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckSingleFromHasFoldableArguments");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_0027;
				}
				goto IL_0027;
				IL_0027:;
			}
		}

		// Token: 0x060077C7 RID: 30663 RVA: 0x001A0054 File Offset: 0x0019E254
		protected override void CheckDecimalFromHasFoldableArguments(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckDecimalFromHasFoldableArguments"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Decimal.From", 1));
				}
				this.CheckNonIntTypeArguments(invocation);
				TypeValue type = base.GetType(invocation.Arguments[0]);
				SqlDataType sqlScalarType = base.ExternalEnvironment.GetSqlScalarType(type.NonNullable);
				if (sqlScalarType == null || (sqlScalarType.Equals(SqlDataType.Float) && !base.ExternalEnvironment.UnsafeTypeConversions))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060077C8 RID: 30664 RVA: 0x001A010C File Offset: 0x0019E30C
		protected override void CheckNonIntTypeArguments(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckNonIntTypeArguments"))
			{
				this.CheckNumberFromHasFoldableArguments(invocation);
			}
		}

		// Token: 0x060077C9 RID: 30665 RVA: 0x001A0150 File Offset: 0x0019E350
		private void CheckCurrencyFromHasFoldableArguments(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckCurrencyFromHasFoldableArguments"))
			{
				if (invocation.Arguments.Count != 3)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				Value value;
				Value value2;
				if (!invocation.Arguments[1].TryGetConstant(out value) || !invocation.Arguments[2].TryGetConstant(out value2) || !value.IsNull || !value2.Equals(Library.RoundingMode.AwayFromZero))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				TypeValue type = base.GetType(invocation.Arguments[0]);
				ValueKind typeKind = type.TypeKind;
				if (typeKind > ValueKind.DateTimeZone && typeKind - ValueKind.Number > 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if ((type.TypeKind == ValueKind.Text || type.TypeKind == ValueKind.Number) && !base.ExternalEnvironment.UnsafeTypeConversions)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x060077CA RID: 30666 RVA: 0x001A0258 File Offset: 0x0019E458
		private void CheckLogicalFromHasFoldableArguments(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SqlAstExpressionChecker.CheckLogicalFromHasFoldableArguments"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Logical.From", 1));
				}
				TypeValue type = base.GetType(invocation.Arguments[0]);
				ValueKind typeKind = type.TypeKind;
				if (typeKind != ValueKind.Null && typeKind - ValueKind.Number > 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (type.TypeKind == ValueKind.Text && !base.ExternalEnvironment.UnsafeTypeConversions)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x04004116 RID: 16662
		private readonly DbEnvironment dbEnvironment;

		// Token: 0x04004117 RID: 16663
		private readonly bool supportsDateTime2;

		// Token: 0x04004118 RID: 16664
		private readonly bool supportsDateFromParts;
	}
}
