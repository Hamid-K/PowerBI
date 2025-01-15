using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.MySQL
{
	// Token: 0x02000911 RID: 2321
	internal sealed class MySQLAstExpressionChecker : NoOutputClauseDbAstExpressionChecker
	{
		// Token: 0x0600421E RID: 16926 RVA: 0x00046885 File Offset: 0x00044A85
		private MySQLAstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000DEE8A File Offset: 0x000DD08A
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			MySQLAstExpressionChecker mySQLAstExpressionChecker = new MySQLAstExpressionChecker(expression, cursor, externalEnvironment);
			mySQLAstExpressionChecker.Check(new DbAstExpressionChecker.SqlCheckerContext(mySQLAstExpressionChecker));
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x000DEEA0 File Offset: 0x000DD0A0
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
					Library.Text.Replace,
					new Action<IInvocationExpression>(base.CheckTextReplace)
				}
			});
			return functions;
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x000DEF1A File Offset: 0x000DD11A
		public static void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			MySQLAstExpressionChecker mySQLAstExpressionChecker = new MySQLAstExpressionChecker(expression, cursor, externalEnvironment);
			mySQLAstExpressionChecker.CheckStatement(new DbAstExpressionChecker.SqlCheckerContext(mySQLAstExpressionChecker));
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x0008167E File Offset: 0x0007F87E
		protected override void CheckArgumentForDateFromTypes(IInvocationExpression invocation)
		{
			base.IsFoldableDateFromTypes(invocation);
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x0008167E File Offset: 0x0007F87E
		protected override void CheckArgumentForDateTimeFromTypes(IInvocationExpression invocation)
		{
			base.IsFoldableDateFromTypes(invocation);
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x00046BD4 File Offset: 0x00044DD4
		protected override bool IsDateOrDateTimeCompatibleType(TypeValue type)
		{
			return type.TypeKind == ValueKind.Date || type.TypeKind == ValueKind.DateTime;
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x000DEF30 File Offset: 0x000DD130
		protected override void CheckTableJoin(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("MySQLAstExpressionChecker.CheckTableJoin"))
			{
				switch (base.CheckTableJoinAndGetKind(invocation))
				{
				case TableTypeAlgebra.JoinKind.Inner:
				case TableTypeAlgebra.JoinKind.LeftOuter:
				case TableTypeAlgebra.JoinKind.RightOuter:
					break;
				default:
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}
	}
}
