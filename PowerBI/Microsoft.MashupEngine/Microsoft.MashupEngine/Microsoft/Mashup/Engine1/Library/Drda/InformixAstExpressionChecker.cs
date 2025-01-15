using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000CAA RID: 3242
	internal class InformixAstExpressionChecker : DbAstExpressionChecker
	{
		// Token: 0x06005796 RID: 22422 RVA: 0x00050416 File Offset: 0x0004E616
		private InformixAstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x06005797 RID: 22423 RVA: 0x00130C32 File Offset: 0x0012EE32
		public override int MaxCharacterStringLiteralLength
		{
			get
			{
				return 32672;
			}
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x00130C39 File Offset: 0x0012EE39
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			InformixAstExpressionChecker informixAstExpressionChecker = new InformixAstExpressionChecker(expression, cursor, externalEnvironment);
			informixAstExpressionChecker.Check(new DbAstExpressionChecker.SqlCheckerContext(informixAstExpressionChecker));
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x00130C4E File Offset: 0x0012EE4E
		public static void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			InformixAstExpressionChecker informixAstExpressionChecker = new InformixAstExpressionChecker(expression, cursor, externalEnvironment);
			informixAstExpressionChecker.CheckStatement(new DbAstExpressionChecker.SqlCheckerContext(informixAstExpressionChecker));
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x00130C64 File Offset: 0x0012EE64
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
				}
			});
			return functions;
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x00130CC8 File Offset: 0x0012EEC8
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

		// Token: 0x0600579C RID: 22428 RVA: 0x00130D38 File Offset: 0x0012EF38
		protected override IExpression VisitConstant(IConstantExpression constant)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("InformixAstExpressionChecker.VisitConstant"))
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

		// Token: 0x0600579D RID: 22429 RVA: 0x00046BD4 File Offset: 0x00044DD4
		protected override bool IsDateOrDateTimeCompatibleType(TypeValue type)
		{
			return type.TypeKind == ValueKind.Date || type.TypeKind == ValueKind.DateTime;
		}
	}
}
