using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.OData.V3.Compiler
{
	// Token: 0x020008F3 RID: 2291
	internal sealed class ODataExpressionChecker : AstExpressionChecker<object>
	{
		// Token: 0x06004150 RID: 16720 RVA: 0x000DAC58 File Offset: 0x000D8E58
		private ODataExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x000DAC63 File Offset: 0x000D8E63
		public static void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			ODataExpressionChecker odataExpressionChecker = new ODataExpressionChecker(expression, cursor, externalEnvironment);
			odataExpressionChecker.Check(new ODataExpressionChecker.ODataCheckerContext(odataExpressionChecker));
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x000DAC78 File Offset: 0x000D8E78
		private void CheckCompatibleOperands(IBinaryExpression binary)
		{
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.CheckCompatibleOperands"))
			{
				TypeValue type = base.GetType(binary.Left);
				TypeValue type2 = base.GetType(binary.Right);
				if (!TypeServices.IsCompatibleType(type, type2))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x000DACE0 File Offset: 0x000D8EE0
		private void CheckInlineInvocationExpression(IInvocationExpression invocation)
		{
			using (base.Context.Enter(ContextLabel.Inline, base.FoldingTracingService))
			{
				IExpression expression = invocation.Arguments[0];
				this.VisitExpression(expression);
				base.VisitListFunctionArgumentAsLambda(invocation, 1, new Func<IExpression, IExpression>(this.VisitExpression));
			}
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x000DAD48 File Offset: 0x000D8F48
		private void CheckSelectInvocationExpression(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.CheckSelectInvocationExpression"))
			{
				IConstantExpression constantExpression = (IConstantExpression)invocation.Function;
				if (!constantExpression.Value.IsFunction)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				FunctionConversions.Conversion conversion;
				if (!FunctionConversions.TryGetConversion(constantExpression.Value.AsFunction, out conversion))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (conversion.RequiresTypes)
				{
					TypeValue[] array = new TypeValue[invocation.Arguments.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = base.GetType(invocation.Arguments[i]);
					}
					if (!conversion.ValidateWithTypes(invocation.Arguments, array, base.FoldingTracingService))
					{
						base.VisitListElements(invocation.Arguments);
					}
				}
				else if (!conversion.Validate(invocation.Arguments, base.FoldingTracingService))
				{
					base.VisitListElements(invocation.Arguments);
				}
			}
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x000DAE48 File Offset: 0x000D9048
		protected override object[] CreateDefaultBindingsFromParameterTypes(FunctionTypeValue functionType)
		{
			object[] array = new object[functionType.ParameterCount];
			for (int i = 0; i < functionType.ParameterCount; i++)
			{
				array[i] = functionType.Parameters.KeyValue(i);
			}
			return array;
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x000DAE84 File Offset: 0x000D9084
		private int ValidateFieldAccessToRangeVariable(IFieldAccessExpression fieldAccess)
		{
			int num2;
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.ValidateFieldAccessToRangeVariable"))
			{
				base.ValidateExpressionTypeIsValid(fieldAccess);
				int num = 0;
				IFieldAccessExpression fieldAccessExpression = fieldAccess;
				IExpression expression;
				do
				{
					expression = fieldAccessExpression.Expression;
					if (expression.Kind == ExpressionKind.FieldAccess)
					{
						fieldAccessExpression = (IFieldAccessExpression)expression;
					}
					num++;
				}
				while (expression.Kind == ExpressionKind.FieldAccess);
				if (expression.Kind != ExpressionKind.Identifier)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				this.VisitIdentifier((IIdentifierExpression)expression);
				num2 = num;
			}
			return num2;
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x000DAF18 File Offset: 0x000D9118
		private void ValidateFieldTypeIsReturnable(TypeValue type, IExpression errorTerm, string fieldName)
		{
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.ValidateFieldTypeIsReturnable"))
			{
				Value value;
				if (type.TryGetMetaField("Returned", out value) && !value.AsLogical.Boolean)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x000DAF7C File Offset: 0x000D917C
		private void ValidateFieldTypeIsQueryable(TypeValue type, string fieldName)
		{
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.ValidateFieldTypeIsQueryable"))
			{
				Value value;
				if (type.TryGetMetaField("Queryable", out value) && !value.AsLogical.Boolean)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x000DAFE0 File Offset: 0x000D91E0
		private void ValidateServiceOperationTypeIsQueryable(IConstantExpression constant, Identifier functionName)
		{
			TypeValue returnType = base.GetType(constant).AsFunctionType.ReturnType;
			this.ValidateFieldTypeIsQueryable(returnType, functionName);
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x000DB00C File Offset: 0x000D920C
		protected override IExpression VisitBinary(IBinaryExpression binary)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitBinary"))
			{
				if (base.Context.Milestone != ContextLabel.SelectBody)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				binary = (IBinaryExpression)base.VisitBinary(binary);
				switch (binary.Operator)
				{
				case BinaryOperator2.Add:
				case BinaryOperator2.Subtract:
				case BinaryOperator2.Multiply:
				case BinaryOperator2.Divide:
				case BinaryOperator2.GreaterThan:
				case BinaryOperator2.LessThan:
				case BinaryOperator2.GreaterThanOrEquals:
				case BinaryOperator2.LessThanOrEquals:
				case BinaryOperator2.And:
				case BinaryOperator2.Or:
				case BinaryOperator2.Concatenate:
					this.CheckCompatibleOperands(binary);
					break;
				case BinaryOperator2.Equals:
				case BinaryOperator2.NotEquals:
					break;
				case BinaryOperator2.MetadataAdd:
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("'meta'"));
				case BinaryOperator2.Range:
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("range"));
				case BinaryOperator2.As:
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("'as'"));
				case BinaryOperator2.Is:
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("'is'"));
				case BinaryOperator2.Coalesce:
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("coalesce"));
				default:
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnknownOperator(binary.Operator.ToString()));
				}
				expression = binary;
			}
			return expression;
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x000DB178 File Offset: 0x000D9378
		protected override IExpression VisitConstant(IConstantExpression constant)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitConstant"))
			{
				if (base.Context.Milestone == ContextLabel.SortBody)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (base.Context.Milestone == ContextLabel.TransformBody)
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
					TypeValue type = base.GetType(constant);
					if (type.TypeKind != ValueKind.Record && type.TypeKind != ValueKind.List && !LiteralConverter.CanConvert(type))
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
				expression = base.VisitConstant(constant);
			}
			return expression;
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x000DB240 File Offset: 0x000D9440
		protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitFieldAccess"))
			{
				if (base.Context.Milestone == ContextLabel.SortBody || base.Context.Milestone == ContextLabel.SelectBody || base.Context.Milestone == ContextLabel.Inline)
				{
					using (base.Context.Enter(ContextLabel.FieldAccess, base.FoldingTracingService))
					{
						TypeValue type = base.GetType(fieldAccess);
						this.ValidateFieldAccessToRangeVariable(fieldAccess);
						if (((ODataExpressionChecker.ODataCheckerContext)base.Context).RequiresQueryable)
						{
							this.ValidateFieldTypeIsQueryable(type, fieldAccess.MemberName);
						}
						if (((ODataExpressionChecker.ODataCheckerContext)base.Context).RequiresReturnable)
						{
							this.ValidateFieldTypeIsReturnable(type, fieldAccess, fieldAccess.MemberName);
						}
						return base.VisitFieldAccess(fieldAccess);
					}
				}
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
			IExpression expression;
			return expression;
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x000DB340 File Offset: 0x000D9540
		protected override IExpression VisitIf(IIfExpression @if)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitIf");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x000DB394 File Offset: 0x000D9594
		protected override VariableInitializer VisitInitializer(VariableInitializer member)
		{
			VariableInitializer variableInitializer;
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitInitializer"))
			{
				if (base.Context.CurrentRecord == null || member.Value.Kind != ExpressionKind.FieldAccess)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.Cursor.Push(base.Context.CurrentRecord, member.Name);
				IFieldAccessExpression fieldAccessExpression = (IFieldAccessExpression)member.Value;
				int num = this.ValidateFieldAccessToRangeVariable(fieldAccessExpression);
				if (member.Name != fieldAccessExpression.MemberName)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				if (num != 1)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				base.Cursor.Pop();
				variableInitializer = member;
			}
			return variableInitializer;
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x000DB468 File Offset: 0x000D9668
		protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitIdentifier"))
			{
				if (base.Context.Milestone != ContextLabel.FieldAccess && base.Context.Milestone != ContextLabel.MultifieldRecordProjection && base.Context.Milestone != ContextLabel.TransformBody)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				expression = base.VisitIdentifier(identifier);
			}
			return expression;
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x000DB4E4 File Offset: 0x000D96E4
		protected override IExpression VisitInvocation(IInvocationExpression invocation)
		{
			IExpression expression3;
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitInvocation"))
			{
				IExpression expression = invocation.Function.Simplify();
				if (expression.Kind != ExpressionKind.Constant)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				IConstantExpression constantExpression = (IConstantExpression)expression;
				if (constantExpression.Value.Equals(TableModule.Table.SelectRows))
				{
					base.CheckListSelectInvocation(invocation);
				}
				else if (constantExpression.Value.Equals(Library.ListRuntime.Transform))
				{
					base.CheckListTransformInvocation(invocation);
				}
				else if (constantExpression.Value.Equals(TableModule.Table.Sort))
				{
					base.CheckListSortInvocation(invocation);
				}
				else if (constantExpression.Value.Equals(Library._Value.As) && invocation.Arguments.Count == 2 && invocation.Arguments[1].Kind == ExpressionKind.Constant)
				{
					IExpression expression2 = this.VisitExpression(invocation.Arguments[0]);
					TypeValue type = base.GetType(expression2);
					IConstantExpression constantExpression2 = (IConstantExpression)invocation.Arguments[1];
					if (!constantExpression2.Value.IsType || !type.IsCompatibleWith(constantExpression2.Value.AsType))
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
				}
				else if (base.Context.Milestone == ContextLabel.SelectBody)
				{
					this.CheckSelectInvocationExpression(invocation);
				}
				else if (constantExpression.Value.Equals(TableModule.Table.ForceColumns))
				{
					this.CheckInlineInvocationExpression(invocation);
				}
				else
				{
					QueryResultFunctionValue queryResultFunctionValue = constantExpression.Value as QueryResultFunctionValue;
					if (queryResultFunctionValue != null)
					{
						if (((ODataExpressionChecker.ODataCheckerContext)base.Context).RequiresQueryable)
						{
							this.ValidateServiceOperationTypeIsQueryable(constantExpression, queryResultFunctionValue.Identifier);
						}
						return base.VisitInvocation(invocation);
					}
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				expression3 = invocation;
			}
			return expression3;
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x000DB6CC File Offset: 0x000D98CC
		protected override IExpression VisitList(IListExpression list)
		{
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitList"))
			{
				if (base.Context.Milestone == ContextLabel.SelectBody)
				{
					if (ODataTypeServices.GetEdmTypeKind(base.GetType(list)) == EdmPrimitiveTypeKind.Binary)
					{
						return base.VisitList(list);
					}
				}
				else if (base.Context.Milestone == ContextLabel.Inline)
				{
					return list;
				}
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
			IExpression expression;
			return expression;
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x000DB74C File Offset: 0x000D994C
		protected override IExpression VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
		{
			IExpression expression2;
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitMultiFieldRecordProjection"))
			{
				using (base.Context.Enter(ContextLabel.MultifieldRecordProjection, base.FoldingTracingService))
				{
					IExpression expression = base.VisitMultiFieldRecordProjection(multiFieldRecordProjection);
					if (base.MultifieldProjectionIsNoop(multiFieldRecordProjection))
					{
						expression2 = expression;
					}
					else
					{
						RecordTypeValue itemType = TypeServices.StripNullableAndMetadata(base.GetType(multiFieldRecordProjection)).AsTableType.ItemType;
						foreach (string text in itemType.Fields.Keys)
						{
							TypeValue typeValue = RecordTypeAlgebra.Field(itemType, text);
							this.ValidateFieldTypeIsReturnable(typeValue, multiFieldRecordProjection, text);
						}
						expression2 = expression;
					}
				}
			}
			return expression2;
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x000DB838 File Offset: 0x000D9A38
		protected override IExpression VisitRecord(IRecordExpression record)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitRecord"))
			{
				if (base.Context.Milestone != ContextLabel.TransformBody)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				List<object> list = new List<object>(record.Members.Count);
				foreach (VariableInitializer variableInitializer in record.Members)
				{
					list.Add(variableInitializer);
				}
				expression = base.VisitRecord(record, null, list);
			}
			return expression;
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x000DB8F0 File Offset: 0x000D9AF0
		protected override IExpression VisitUnary(IUnaryExpression unary)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("ODataExpressionChecker.VisitUnary"))
			{
				if (base.Context.Milestone != ContextLabel.SelectBody)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				unary = (IUnaryExpression)base.VisitUnary(unary);
				UnaryOperator2 @operator = unary.Operator;
				if (@operator > UnaryOperator2.Negative)
				{
					if (@operator == UnaryOperator2.Positive)
					{
						throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnsupportedOperator("unary '+'"));
					}
					throw base.FoldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.UnknownOperator(unary.Operator.ToString()));
				}
				else
				{
					expression = unary;
				}
			}
			return expression;
		}

		// Token: 0x020008F4 RID: 2292
		private class ODataCheckerContext : CheckerContext
		{
			// Token: 0x06004165 RID: 16741 RVA: 0x000DB9A0 File Offset: 0x000D9BA0
			public ODataCheckerContext(ODataExpressionChecker checker)
				: base(ContextLabel.None, new Action<CheckerContext>(checker.SetCurrentContext))
			{
			}

			// Token: 0x06004166 RID: 16742 RVA: 0x000DB9B5 File Offset: 0x000D9BB5
			private ODataCheckerContext(ContextLabel milestone, CheckerContext parentContext)
				: base(milestone, parentContext)
			{
			}

			// Token: 0x170014F8 RID: 5368
			// (get) Token: 0x06004167 RID: 16743 RVA: 0x000DB9C0 File Offset: 0x000D9BC0
			protected override bool IsValid
			{
				get
				{
					ContextLabel milestone = base.Milestone;
					if (milestone != ContextLabel.Inline)
					{
						switch (milestone)
						{
						case ContextLabel.Select:
						case ContextLabel.Transform:
						case ContextLabel.Sort:
							return true;
						}
						return base.IsValid;
					}
					return true;
				}
			}

			// Token: 0x170014F9 RID: 5369
			// (get) Token: 0x06004168 RID: 16744 RVA: 0x000DBA00 File Offset: 0x000D9C00
			internal bool RequiresQueryable
			{
				get
				{
					ContextLabel milestone = base.Milestone;
					return milestone - ContextLabel.FieldAccess <= 2 || milestone - ContextLabel.Select <= 5;
				}
			}

			// Token: 0x170014FA RID: 5370
			// (get) Token: 0x06004169 RID: 16745 RVA: 0x000DBA24 File Offset: 0x000D9C24
			internal bool RequiresReturnable
			{
				get
				{
					ContextLabel milestone = base.Milestone;
					if (milestone != ContextLabel.FieldAccess)
					{
						return milestone == ContextLabel.MultifieldRecordProjection || milestone - ContextLabel.Transform <= 1;
					}
					return base.Parent.Milestone == ContextLabel.Transform || base.Parent.Milestone == ContextLabel.TransformBody || base.Parent.Milestone == ContextLabel.MultifieldRecordProjection;
				}
			}

			// Token: 0x0600416A RID: 16746 RVA: 0x000DBA77 File Offset: 0x000D9C77
			protected override CheckerContext EnterHelper(ContextLabel milestone)
			{
				return new ODataExpressionChecker.ODataCheckerContext(milestone, this);
			}
		}
	}
}
