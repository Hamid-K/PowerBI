using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Sybase
{
	// Token: 0x0200036F RID: 879
	internal sealed class SybaseAstExpressionChecker : DbAstExpressionChecker
	{
		// Token: 0x06001F1E RID: 7966 RVA: 0x00050416 File Offset: 0x0004E616
		private SybaseAstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x00050421 File Offset: 0x0004E621
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			SybaseAstExpressionChecker sybaseAstExpressionChecker = new SybaseAstExpressionChecker(expression, cursor, externalEnvironment);
			sybaseAstExpressionChecker.Check(new DbAstExpressionChecker.SqlCheckerContext(sybaseAstExpressionChecker));
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x00050438 File Offset: 0x0004E638
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

		// Token: 0x06001F21 RID: 7969 RVA: 0x0005049C File Offset: 0x0004E69C
		protected override void CheckSingleFromHasFoldableArguments(IInvocationExpression invocation)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("SybaseAstExpressionChecker.CheckSingleFromHasFoldableArguments");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.FunctionNotImplemented("Single.From"));
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_0030;
				}
				goto IL_0030;
				IL_0030:;
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000504F8 File Offset: 0x0004E6F8
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
