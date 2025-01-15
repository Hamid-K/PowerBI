using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Teradata
{
	// Token: 0x020002D5 RID: 725
	internal sealed class TeradataAstExpressionChecker : NoOutputClauseDbAstExpressionChecker
	{
		// Token: 0x06001CC4 RID: 7364 RVA: 0x00046885 File Offset: 0x00044A85
		private TeradataAstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x00046890 File Offset: 0x00044A90
		public override int MaxCharacterStringLiteralLength
		{
			get
			{
				return 31000;
			}
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00046897 File Offset: 0x00044A97
		public static void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			TeradataAstExpressionChecker teradataAstExpressionChecker = new TeradataAstExpressionChecker(expression, cursor, externalEnvironment);
			teradataAstExpressionChecker.CheckStatement(new DbAstExpressionChecker.SqlCheckerContext(teradataAstExpressionChecker));
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x000468AC File Offset: 0x00044AAC
		protected override void CheckListAverage(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("TeradataAstExpressionChecker.CheckListAverage"))
			{
				base.CheckListAverage(invocation);
				ValueKind typeKind = base.GetType(invocation).TypeKind;
				if (typeKind - ValueKind.Time <= 4)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00046910 File Offset: 0x00044B10
		protected override bool AreScalarTypesCompatible(TypeValue targetType, TypeValue sourceType)
		{
			return (targetType.TypeKind == ValueKind.DateTime && (sourceType.TypeKind == ValueKind.Date || sourceType.TypeKind == ValueKind.DateTimeZone)) || (targetType.TypeKind == ValueKind.Binary && sourceType.TypeKind == ValueKind.Number) || (targetType.TypeKind == ValueKind.Number && sourceType.TypeKind == ValueKind.Logical) || base.AreScalarTypesCompatible(targetType, sourceType);
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0004696C File Offset: 0x00044B6C
		protected override void CheckTableJoin(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("TeradataAstExpressionChecker.CheckTableJoin"))
			{
				switch (base.CheckTableJoinAndGetKind(invocation))
				{
				case TableTypeAlgebra.JoinKind.Inner:
					return;
				case TableTypeAlgebra.JoinKind.LeftOuter:
					base.CheckTableTypeHasAllComparableColumns(invocation.Arguments[0]);
					return;
				case TableTypeAlgebra.JoinKind.FullOuter:
					base.CheckTableTypeHasAllComparableColumns(invocation.Arguments[0]);
					base.CheckTableTypeHasAllComparableColumns(invocation.Arguments[2]);
					return;
				case TableTypeAlgebra.JoinKind.RightOuter:
					base.CheckTableTypeHasAllComparableColumns(invocation.Arguments[2]);
					return;
				}
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00046A34 File Offset: 0x00044C34
		protected override void CheckArgumentsForNumberRound(IInvocationExpression invocation)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("TeradataAstExpressionChecker.CheckArgumentsForNumberRound");
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

		// Token: 0x06001CCB RID: 7371 RVA: 0x00046A84 File Offset: 0x00044C84
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			TeradataAstExpressionChecker teradataAstExpressionChecker = new TeradataAstExpressionChecker(expression, cursor, externalEnvironment);
			teradataAstExpressionChecker.Check(new DbAstExpressionChecker.SqlCheckerContext(teradataAstExpressionChecker));
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00046A9C File Offset: 0x00044C9C
		protected override IExpression VisitBinary(IBinaryExpression binary)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("TeradataAstExpressionChecker.VisitBinary"))
			{
				BinaryOperator2 @operator = binary.Operator;
				if (@operator - BinaryOperator2.And <= 1)
				{
					if (binary.Left.Kind == ExpressionKind.Constant && ((IConstantExpression)binary.Left).Value.IsNull)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					if (binary.Right.Kind == ExpressionKind.Constant && ((IConstantExpression)binary.Right).Value.IsNull)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
				expression = base.VisitBinary(binary);
			}
			return expression;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00046B54 File Offset: 0x00044D54
		protected override IExpression VisitUnary(IUnaryExpression unary)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("TeradataAstExpressionChecker.VisitUnary"))
			{
				if (unary.Operator == UnaryOperator2.Not && unary.Expression.Kind == ExpressionKind.Constant && ((IConstantExpression)unary.Expression).Value.IsNull)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				expression = base.VisitUnary(unary);
			}
			return expression;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00046BD4 File Offset: 0x00044DD4
		protected override bool IsDateOrDateTimeCompatibleType(TypeValue type)
		{
			return type.TypeKind == ValueKind.Date || type.TypeKind == ValueKind.DateTime;
		}
	}
}
