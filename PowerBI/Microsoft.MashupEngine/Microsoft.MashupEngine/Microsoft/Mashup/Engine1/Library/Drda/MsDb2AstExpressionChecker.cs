using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000CAF RID: 3247
	internal class MsDb2AstExpressionChecker : DbAstExpressionChecker
	{
		// Token: 0x060057DE RID: 22494 RVA: 0x00050416 File Offset: 0x0004E616
		private MsDb2AstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x17001A69 RID: 6761
		// (get) Token: 0x060057DF RID: 22495 RVA: 0x00130C32 File Offset: 0x0012EE32
		public override int MaxCharacterStringLiteralLength
		{
			get
			{
				return 32672;
			}
		}

		// Token: 0x17001A6A RID: 6762
		// (get) Token: 0x060057E0 RID: 22496 RVA: 0x00002139 File Offset: 0x00000339
		protected override bool CanCastNumericPrecision
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x001325C3 File Offset: 0x001307C3
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			MsDb2AstExpressionChecker msDb2AstExpressionChecker = new MsDb2AstExpressionChecker(expression, cursor, externalEnvironment);
			msDb2AstExpressionChecker.Check(new DbAstExpressionChecker.SqlCheckerContext(msDb2AstExpressionChecker));
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x001325D8 File Offset: 0x001307D8
		public static void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			MsDb2AstExpressionChecker msDb2AstExpressionChecker = new MsDb2AstExpressionChecker(expression, cursor, externalEnvironment);
			msDb2AstExpressionChecker.CheckStatement(new DbAstExpressionChecker.SqlCheckerContext(msDb2AstExpressionChecker));
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x001325F0 File Offset: 0x001307F0
		protected override Dictionary<FunctionValue, Action<IInvocationExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Action<IInvocationExpression>> functions = base.GetFunctions();
			functions.AddRange(new Dictionary<FunctionValue, Action<IInvocationExpression>>
			{
				{
					Library.Binary.Length,
					base.CheckArgumentCount(1)
				},
				{
					BinaryOperator.BitwiseAnd,
					base.CheckArgumentCount(2)
				},
				{
					BinaryOperator.BitwiseOr,
					base.CheckArgumentCount(2)
				},
				{
					BinaryOperator.BitwiseXor,
					base.CheckArgumentCount(2)
				},
				{
					Library.Text.PositionOf,
					base.CheckArgumentCount(2)
				},
				{
					Library.Text.Start,
					base.CheckArgumentCount(2)
				},
				{
					Library.Text.End,
					base.CheckArgumentCount(2)
				},
				{
					Library.Text.Middle,
					base.CheckArgumentCount(2, 3)
				},
				{
					Library.Text.Replace,
					new Action<IInvocationExpression>(base.CheckTextReplace)
				},
				{
					TypeSpecificFunction.NumberRandom,
					base.CheckArgumentCount(0)
				},
				{
					TypeSpecificFunction.NumberRandomBetween,
					base.CheckArgumentCount(2)
				},
				{
					Library.Number.RoundDown,
					base.CheckArgumentCount(1, 2)
				},
				{
					Library.Number.RoundUp,
					base.CheckArgumentCount(1, 2)
				},
				{
					Library.Number.Cosh,
					base.CheckArgumentCount(1)
				},
				{
					Library.Number.Sinh,
					base.CheckArgumentCount(1)
				},
				{
					Library.Number.Tanh,
					base.CheckArgumentCount(1)
				},
				{
					Library.Number.Ln,
					base.CheckArgumentCount(1)
				},
				{
					Library.Number.IsEven,
					base.CheckArgumentCount(1)
				},
				{
					Library.Number.IsOdd,
					base.CheckArgumentCount(1)
				},
				{
					Library.Date.AddDays,
					base.CheckArgumentCount(2)
				},
				{
					Library.Date.AddWeeks,
					base.CheckArgumentCount(2)
				},
				{
					Library.Date.AddQuarters,
					base.CheckArgumentCount(2)
				},
				{
					Library.Date.Day,
					base.CheckArgumentCount(1)
				},
				{
					Library.Date.Month,
					base.CheckArgumentCount(1)
				},
				{
					Library.Date.Year,
					base.CheckArgumentCount(1)
				},
				{
					Library.Date.DayOfYear,
					base.CheckArgumentCount(1)
				},
				{
					Library.Date.QuarterOfYear,
					base.CheckArgumentCount(1)
				},
				{
					Library.Time.Hour,
					base.CheckArgumentCount(1)
				},
				{
					Library.Time.Minute,
					base.CheckArgumentCount(1)
				},
				{
					Library.Time.Second,
					base.CheckArgumentCount(1)
				},
				{
					CultureSpecificFunction.DateDayOfWeek,
					base.CheckArgumentCount(1, 2)
				},
				{
					CultureSpecificFunction.DateWeekOfYear,
					base.CheckArgumentCount(1)
				}
			});
			return functions;
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x00132858 File Offset: 0x00130A58
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

		// Token: 0x060057E5 RID: 22501 RVA: 0x001328C8 File Offset: 0x00130AC8
		protected override void CheckArgumentForDateFromTypes(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("MsDb2AstExpressionChecker.CheckArgumentForDateFromTypes"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("Date.From", 1));
				}
				switch (base.GetType(invocation.Arguments[0]).TypeKind)
				{
				case ValueKind.Null:
				case ValueKind.Date:
				case ValueKind.DateTime:
				case ValueKind.Number:
					base.CheckArgumentsAreValid(invocation);
					break;
				default:
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x00132978 File Offset: 0x00130B78
		protected override void CheckArgumentForDateTimeFromTypes(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("MsDb2AstExpressionChecker.CheckArgumentForDateTimeFromTypes"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("DateTime.From", 1));
				}
				ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
				if (typeKind > ValueKind.DateTime && typeKind != ValueKind.Number)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x00132A0C File Offset: 0x00130C0C
		protected override IExpression VisitConstant(IConstantExpression constant)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("MsDb2AstExpressionChecker.VisitConstant"))
			{
				if (base.Context.Milestone == ContextLabel.SortBody)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				IQueryResultValue queryResultValue = constant.Value as IQueryResultValue;
				if (queryResultValue != null)
				{
					base.CheckQueryResultValueHasConsistentEnvironment(queryResultValue, constant);
				}
				else
				{
					base.CheckRequiredScalarType(base.GetType(constant));
					if (constant.Value.IsNumber)
					{
						NumberValue asNumber = constant.Value.AsNumber;
						if (asNumber.IsNaN || asNumber.Equals(Library.Number.PositiveInfinity) || asNumber.Equals(Library.Number.NegativeInfinity))
						{
							return constant;
						}
					}
				}
				expression = base.VisitConstant(constant);
			}
			return expression;
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x00046BD4 File Offset: 0x00044DD4
		protected override bool IsDateOrDateTimeCompatibleType(TypeValue type)
		{
			return type.TypeKind == ValueKind.Date || type.TypeKind == ValueKind.DateTime;
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x00132AD0 File Offset: 0x00130CD0
		protected override void CheckTableJoin(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("MsDb2AstExpressionChecker.CheckTableJoin"))
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
