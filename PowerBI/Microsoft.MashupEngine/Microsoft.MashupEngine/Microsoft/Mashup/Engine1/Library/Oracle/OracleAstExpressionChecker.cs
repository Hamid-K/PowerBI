using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Oracle
{
	// Token: 0x0200055C RID: 1372
	internal sealed class OracleAstExpressionChecker : DbAstExpressionChecker
	{
		// Token: 0x06002BCB RID: 11211 RVA: 0x00050416 File Offset: 0x0004E616
		private OracleAstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x00084ECF File Offset: 0x000830CF
		public override int MaxCharacterStringLiteralLength
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x00084ED6 File Offset: 0x000830D6
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			OracleAstExpressionChecker oracleAstExpressionChecker = new OracleAstExpressionChecker(expression, cursor, externalEnvironment);
			oracleAstExpressionChecker.Check(new DbAstExpressionChecker.SqlCheckerContext(oracleAstExpressionChecker));
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x00084EEB File Offset: 0x000830EB
		public static void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			OracleAstExpressionChecker oracleAstExpressionChecker = new OracleAstExpressionChecker(expression, cursor, externalEnvironment);
			oracleAstExpressionChecker.CheckStatement(new DbAstExpressionChecker.SqlCheckerContext(oracleAstExpressionChecker));
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x00084F00 File Offset: 0x00083100
		protected override Dictionary<FunctionValue, Action<IInvocationExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Action<IInvocationExpression>> functions = base.GetFunctions();
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
					Library.Date.Month,
					new Action<IInvocationExpression>(base.CheckArgumentsAreValid)
				},
				{
					Library.Date.Year,
					new Action<IInvocationExpression>(base.CheckArgumentsAreValid)
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
				}
			});
			return functions;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x00084FD8 File Offset: 0x000831D8
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

		// Token: 0x06002BD1 RID: 11217 RVA: 0x0008167E File Offset: 0x0007F87E
		protected override void CheckArgumentForDateFromTypes(IInvocationExpression invocation)
		{
			base.IsFoldableDateFromTypes(invocation);
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x0008167E File Offset: 0x0007F87E
		protected override void CheckArgumentForDateTimeFromTypes(IInvocationExpression invocation)
		{
			base.IsFoldableDateFromTypes(invocation);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x00085048 File Offset: 0x00083248
		protected override void CheckArgumentForDateTimeZoneFromTypes(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("OracleAstExpressionChecker.CheckArgumentForDateTimeZoneFromTypes"))
			{
				if (invocation.Arguments.Count != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, int>>(FoldingWarnings.InvalidArgumentsCount("DateTimeZone.From", 1));
				}
				TypeValue type = base.GetType(invocation.Arguments[0]);
				ValueKind typeKind = type.TypeKind;
				if (typeKind - ValueKind.Date > 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string>>(FoldingWarnings.UnsupportedFunctionArgumentType("DateTimeZone.From", TypeValue.GetTypeKind(type)));
				}
				base.CheckArgumentsAreValid(invocation);
			}
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000850EC File Offset: 0x000832EC
		protected override bool AreScalarTypesCompatible(TypeValue targetType, TypeValue sourceType)
		{
			return (targetType.TypeKind == ValueKind.DateTime && (sourceType.TypeKind == ValueKind.Date || sourceType.TypeKind == ValueKind.DateTimeZone)) || (targetType.TypeKind == ValueKind.Number && sourceType.TypeKind == ValueKind.Logical) || base.AreScalarTypesCompatible(targetType, sourceType);
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x00085127 File Offset: 0x00083327
		protected override IExpression VisitConstant(IConstantExpression constant)
		{
			if (constant.Value.IsDuration)
			{
				return constant;
			}
			return base.VisitConstant(constant);
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x00046BD4 File Offset: 0x00044DD4
		protected override bool IsDateOrDateTimeCompatibleType(TypeValue type)
		{
			return type.TypeKind == ValueKind.Date || type.TypeKind == ValueKind.DateTime;
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x00085140 File Offset: 0x00083340
		protected override void CheckTableJoin(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("OracleAstExpressionChecker.CheckTableJoin"))
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
