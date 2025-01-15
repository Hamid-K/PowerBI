using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.DB2
{
	// Token: 0x02000CC5 RID: 3269
	internal sealed class DB2AstExpressionChecker : DbAstExpressionChecker
	{
		// Token: 0x06005856 RID: 22614 RVA: 0x00050416 File Offset: 0x0004E616
		private DB2AstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x06005857 RID: 22615 RVA: 0x00130C32 File Offset: 0x0012EE32
		public override int MaxCharacterStringLiteralLength
		{
			get
			{
				return 32672;
			}
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x00134934 File Offset: 0x00132B34
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			DB2AstExpressionChecker db2AstExpressionChecker = new DB2AstExpressionChecker(expression, cursor, externalEnvironment);
			db2AstExpressionChecker.Check(new DbAstExpressionChecker.SqlCheckerContext(db2AstExpressionChecker));
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x0013494C File Offset: 0x00132B4C
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

		// Token: 0x0600585A RID: 22618 RVA: 0x00046BD4 File Offset: 0x00044DD4
		protected override bool IsDateOrDateTimeCompatibleType(TypeValue type)
		{
			return type.TypeKind == ValueKind.Date || type.TypeKind == ValueKind.DateTime;
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x001349B0 File Offset: 0x00132BB0
		protected override void CheckTableJoin(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("DB2AstExpressionChecker.CheckTableJoin"))
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
