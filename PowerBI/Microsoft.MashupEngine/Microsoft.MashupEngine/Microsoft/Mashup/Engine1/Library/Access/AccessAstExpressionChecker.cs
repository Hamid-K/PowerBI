using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Access
{
	// Token: 0x0200122B RID: 4651
	internal sealed class AccessAstExpressionChecker : DbAstExpressionChecker
	{
		// Token: 0x06007AF4 RID: 31476 RVA: 0x00050416 File Offset: 0x0004E616
		private AccessAstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x06007AF5 RID: 31477 RVA: 0x001A85A3 File Offset: 0x001A67A3
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			AccessAstExpressionChecker accessAstExpressionChecker = new AccessAstExpressionChecker(expression, cursor, externalEnvironment);
			accessAstExpressionChecker.Check(new DbAstExpressionChecker.SqlCheckerContext(accessAstExpressionChecker));
		}

		// Token: 0x06007AF6 RID: 31478 RVA: 0x001A85B8 File Offset: 0x001A67B8
		protected override Dictionary<FunctionValue, Action<IInvocationExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Action<IInvocationExpression>> functions = base.GetFunctions();
			functions.Remove(Library.Number.Acos);
			functions.Remove(Library.Number.Asin);
			functions.Remove(Library.Number.Atan2);
			functions.Remove(Library.List.CountOfDistinct);
			functions.Remove(Library.List.CountOfDistinctNull);
			functions.Remove(Library.List.CountOfDistinctNotNull);
			functions.AddRange(new Dictionary<FunctionValue, Action<IInvocationExpression>>
			{
				{
					TimeSpecificFunction.DateTimeZoneLocalNow,
					new Action<IInvocationExpression>(base.CheckArgumentsAreValid)
				},
				{
					TimeSpecificFunction.DateTimeZoneUtcNow,
					new Action<IInvocationExpression>(base.CheckArgumentsAreValid)
				},
				{
					Library.Number.RoundDown,
					new Action<IInvocationExpression>(base.CheckArgumentsForRoundUpAndRoundDown)
				}
			});
			return functions;
		}

		// Token: 0x06007AF7 RID: 31479 RVA: 0x001A8663 File Offset: 0x001A6863
		protected override void CheckInitializerType(TypeValue type)
		{
			base.CheckInitializerType(type);
			if (type.TypeKind == ValueKind.Duration)
			{
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
		}

		// Token: 0x06007AF8 RID: 31480 RVA: 0x001A8684 File Offset: 0x001A6884
		protected override void CheckDecimalFromHasFoldableArguments(IInvocationExpression invocation)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AccessAstExpressionChecker.CheckDecimalFromHasFoldableArguments");
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

		// Token: 0x06007AF9 RID: 31481 RVA: 0x001A86D4 File Offset: 0x001A68D4
		protected override bool IsDateOrDateTimeCompatibleType(TypeValue type)
		{
			return type.TypeKind == ValueKind.DateTime;
		}
	}
}
