using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.PostgreSQL
{
	// Token: 0x02000538 RID: 1336
	internal sealed class PostgreSQLAstExpressionChecker : DbAstExpressionChecker
	{
		// Token: 0x06002AD8 RID: 10968 RVA: 0x00050416 File Offset: 0x0004E616
		private PostgreSQLAstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x00081368 File Offset: 0x0007F568
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			PostgreSQLAstExpressionChecker postgreSQLAstExpressionChecker = new PostgreSQLAstExpressionChecker(expression, cursor, externalEnvironment);
			postgreSQLAstExpressionChecker.Check(new DbAstExpressionChecker.SqlCheckerContext(postgreSQLAstExpressionChecker));
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x0008137D File Offset: 0x0007F57D
		public static void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			PostgreSQLAstExpressionChecker postgreSQLAstExpressionChecker = new PostgreSQLAstExpressionChecker(expression, cursor, externalEnvironment);
			postgreSQLAstExpressionChecker.CheckStatement(new DbAstExpressionChecker.SqlCheckerContext(postgreSQLAstExpressionChecker));
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x00002139 File Offset: 0x00000339
		protected override bool CanCastNumericPrecision
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x00081394 File Offset: 0x0007F594
		protected override Dictionary<FunctionValue, Action<IInvocationExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Action<IInvocationExpression>> functions = base.GetFunctions();
			functions.AddRange(new Dictionary<FunctionValue, Action<IInvocationExpression>>
			{
				{
					TypeSpecificFunction.NumberRandom,
					new Action<IInvocationExpression>(base.CheckArgumentsAreValid)
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
					Library.Date.Day,
					new Action<IInvocationExpression>(this.CheckArgumentForDateComponent)
				},
				{
					CultureSpecificFunction.DateDayOfWeek,
					new Action<IInvocationExpression>(this.CheckArgumentsForDayOfWeek)
				},
				{
					Library.Date.DayOfYear,
					new Action<IInvocationExpression>(this.CheckArgumentForDateComponent)
				},
				{
					Library.Date.Month,
					new Action<IInvocationExpression>(this.CheckArgumentForDateComponent)
				},
				{
					Library.Date.Year,
					new Action<IInvocationExpression>(this.CheckArgumentForDateComponent)
				},
				{
					Library.Date.QuarterOfYear,
					new Action<IInvocationExpression>(this.CheckArgumentForDateComponent)
				},
				{
					Library.Time.Hour,
					new Action<IInvocationExpression>(this.CheckArgumentForTimeFromTypes)
				},
				{
					Library.Time.Minute,
					new Action<IInvocationExpression>(this.CheckArgumentForTimeFromTypes)
				},
				{
					Library.Time.Second,
					new Action<IInvocationExpression>(this.CheckArgumentForTimeFromTypes)
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
					base.CheckArgumentTypes("Text.Middle", 2, new TypeValue[]
					{
						NullableTypeValue.Text,
						TypeValue.Int32,
						TypeValue.Int32
					})
				},
				{
					Library.Text.Replace,
					new Action<IInvocationExpression>(base.CheckTextReplace)
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
					CultureSpecificFunction.ByteFrom,
					new Action<IInvocationExpression>(this.CheckArgumentForIntegerFrom)
				},
				{
					CultureSpecificFunction.Int8From,
					new Action<IInvocationExpression>(this.CheckArgumentForIntegerFrom)
				},
				{
					CultureSpecificFunction.Int16From,
					new Action<IInvocationExpression>(this.CheckArgumentForIntegerFrom)
				},
				{
					CultureSpecificFunction.Int32From,
					new Action<IInvocationExpression>(this.CheckArgumentForIntegerFrom)
				},
				{
					CultureSpecificFunction.Int64From,
					new Action<IInvocationExpression>(this.CheckArgumentForIntegerFrom)
				}
			});
			return functions;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x00081610 File Offset: 0x0007F810
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

		// Token: 0x06002ADE RID: 10974 RVA: 0x0008167E File Offset: 0x0007F87E
		protected override void CheckArgumentForDateFromTypes(IInvocationExpression invocation)
		{
			base.IsFoldableDateFromTypes(invocation);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0008167E File Offset: 0x0007F87E
		protected override void CheckArgumentForDateTimeFromTypes(IInvocationExpression invocation)
		{
			base.IsFoldableDateFromTypes(invocation);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x00081688 File Offset: 0x0007F888
		private void CheckForCompatibleDateTimeTypes(IExpression expression)
		{
			using (base.FoldingTracingService.NewScope("PostgreSQLAstExpressionChecker.CheckForCompatibleDateTimeTypes"))
			{
				ValueKind typeKind = base.GetType(expression).TypeKind;
				if (typeKind - ValueKind.Date > 2)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000816E4 File Offset: 0x0007F8E4
		private void CheckArgumentForDateComponent(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("PostgreSQLAstExpressionChecker.CheckArgumentForDateComponent"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.CheckForCompatibleDateTimeTypes(invocation.Arguments[0]);
				base.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x00081754 File Offset: 0x0007F954
		private void CheckArgumentsForDayOfWeek(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("PostgreSQLAstExpressionChecker.CheckArgumentsForDayOfWeek"))
			{
				int count = invocation.Arguments.Count;
				if (count != 1)
				{
					if (count != 2)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					ValueKind typeKind = base.GetType(invocation.Arguments[1]).TypeKind;
					if (typeKind != ValueKind.Null && typeKind != ValueKind.Number)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
				this.CheckForCompatibleDateTimeTypes(invocation.Arguments[0]);
				base.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000817F8 File Offset: 0x0007F9F8
		private void CheckArgumentForTimeFromTypes(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("PostgreSQLAstExpressionChecker.CheckArgumentForTimeFromTypes"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
				if (typeKind != ValueKind.Time && typeKind - ValueKind.DateTime > 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x00081884 File Offset: 0x0007FA84
		protected override void CheckTextTrimFunction(IInvocationExpression invocation)
		{
			this.InternalCheckTextTrimFunctions("Text.Trim", invocation);
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x00081892 File Offset: 0x0007FA92
		protected override void CheckTextTrimEndFunction(IInvocationExpression invocation)
		{
			this.InternalCheckTextTrimFunctions("Text.TrimEnd", invocation);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000818A0 File Offset: 0x0007FAA0
		protected override void CheckTextTrimStartFunction(IInvocationExpression invocation)
		{
			this.InternalCheckTextTrimFunctions("Text.TrimStart", invocation);
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000818B0 File Offset: 0x0007FAB0
		private void InternalCheckTextTrimFunctions(string functionName, IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("PostgreSQLAstExpressionChecker.Check" + functionName + "Function"))
			{
				int count = invocation.Arguments.Count;
				if (count != 1)
				{
					if (count != 2)
					{
						throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int, int>>(FoldingWarnings.InvalidArgumentsCount(functionName, 1, 2));
					}
					string text;
					if (!invocation.Arguments[1].TryGetStringConstant(out text))
					{
						throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<int, string>>(FoldingWarnings.ConstantRequired(2, functionName));
					}
				}
				this.VisitExpression(invocation.Arguments[0]);
				if (base.GetType(invocation.Arguments[0]).TypeKind != ValueKind.Text)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<int, string, string>>(FoldingWarnings.InvalidArgumentType(1, functionName, "Text"));
				}
			}
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0008198C File Offset: 0x0007FB8C
		protected override void CheckSingleFromHasFoldableArguments(IInvocationExpression invocation)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("PostgreSQLAstExpressionChecker.CheckSingleFromHasFoldableArguments");
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

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000819DC File Offset: 0x0007FBDC
		protected override void CheckNonIntTypeArguments(IInvocationExpression invocation)
		{
			switch (base.GetType(invocation.Arguments[0]).TypeKind)
			{
			case ValueKind.Null:
			case ValueKind.Date:
			case ValueKind.DateTime:
			case ValueKind.Number:
				base.CheckArgumentsAreValid(invocation);
				return;
			default:
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x00081A38 File Offset: 0x0007FC38
		private void CheckArgumentForIntegerFrom(IInvocationExpression invocation)
		{
			if (invocation.Arguments.Count != 1)
			{
				throw new FoldingFailureException("Folding failed. More details are available in the trace.");
			}
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			if (typeKind != ValueKind.Null && typeKind != ValueKind.Duration && typeKind != ValueKind.Logical)
			{
				throw new FoldingFailureException("Folding failed. More details are available in the trace.");
			}
			base.CheckArgumentsAreValid(invocation);
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x00081A94 File Offset: 0x0007FC94
		protected override void CheckArgumentForNumberFrom(IInvocationExpression invocation)
		{
			if (invocation.Arguments.Count != 1)
			{
				throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Number.From", 1));
			}
			switch (base.GetType(invocation.Arguments[0]).TypeKind)
			{
			case ValueKind.Null:
			case ValueKind.Date:
			case ValueKind.DateTime:
			case ValueKind.Number:
			case ValueKind.Logical:
				base.CheckArgumentsAreValid(invocation);
				return;
			default:
				throw new FoldingFailureException("Folding failed. More details are available in the trace.");
			}
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x00081B16 File Offset: 0x0007FD16
		protected override bool AreScalarTypesCompatible(TypeValue targetType, TypeValue sourceType)
		{
			return (targetType.TypeKind == ValueKind.Text && sourceType.TypeKind == ValueKind.Binary) || (targetType.TypeKind == ValueKind.DateTime && sourceType.TypeKind == ValueKind.DateTimeZone) || base.AreScalarTypesCompatible(targetType, sourceType);
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x00046BD4 File Offset: 0x00044DD4
		protected override bool IsDateOrDateTimeCompatibleType(TypeValue type)
		{
			return type.TypeKind == ValueKind.Date || type.TypeKind == ValueKind.DateTime;
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x00081B4C File Offset: 0x0007FD4C
		protected override void CheckTableJoin(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("SybaseAstExpressionChecker.CheckTableJoin"))
			{
				TableTypeAlgebra.JoinKind joinKind = base.CheckTableJoinAndGetKind(invocation);
				if (joinKind > TableTypeAlgebra.JoinKind.RightSemi)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}
	}
}
