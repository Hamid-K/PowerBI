using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B87 RID: 7047
	public sealed class InliningVisitor : LogicalAstVisitor2<IExpression>
	{
		// Token: 0x0600B07D RID: 45181 RVA: 0x00242B42 File Offset: 0x00240D42
		public static void RegisterSimpleFunction(IFunctionValue function)
		{
			InliningVisitor.simpleFunctions.Add(function);
		}

		// Token: 0x0600B07E RID: 45182 RVA: 0x00242B50 File Offset: 0x00240D50
		public static IExpression Inline(IEngine engine, IExpression node, int complexityLimit)
		{
			return new InliningVisitor(engine, complexityLimit).VisitExpression(node);
		}

		// Token: 0x0600B07F RID: 45183 RVA: 0x00242B5F File Offset: 0x00240D5F
		private InliningVisitor(IEngine engine, int complexityLimit)
		{
			this.engine = engine;
			this.identifierMap = new Dictionary<Identifier, Identifier>();
			this.complexityLimit = complexityLimit;
		}

		// Token: 0x0600B080 RID: 45184 RVA: 0x00242B80 File Offset: 0x00240D80
		private static bool IsSimpleExpression(IExpression expression)
		{
			ExpressionKind kind = expression.Kind;
			if (kind != ExpressionKind.Constant)
			{
				switch (kind)
				{
				case ExpressionKind.FieldAccess:
					return InliningVisitor.IsSimpleExpression(((IFieldAccessExpression)expression).Expression);
				case ExpressionKind.Function:
				case ExpressionKind.If:
				case ExpressionKind.Let:
					break;
				case ExpressionKind.Identifier:
					return true;
				case ExpressionKind.Invocation:
					return InliningVisitor.IsSimpleInvocationExpression((IInvocationExpression)expression);
				case ExpressionKind.List:
					return InliningVisitor.IsSimpleListExpression((IListExpression)expression);
				default:
					if (kind == ExpressionKind.Record)
					{
						return InliningVisitor.IsSimpleRecordExpression((IRecordExpression)expression);
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600B081 RID: 45185 RVA: 0x00242BFD File Offset: 0x00240DFD
		private static bool IsSimpleListExpression(IListExpression list)
		{
			return list.Members.Count == 0;
		}

		// Token: 0x0600B082 RID: 45186 RVA: 0x00242C0D File Offset: 0x00240E0D
		private static bool IsSimpleRecordExpression(IRecordExpression record)
		{
			return record.Members.Count == 0;
		}

		// Token: 0x0600B083 RID: 45187 RVA: 0x00242C20 File Offset: 0x00240E20
		private static bool IsSimpleInvocationExpression(IInvocationExpression invocation)
		{
			bool flag = false;
			IConstantExpression2 constantExpression = invocation.Function as IConstantExpression2;
			if (constantExpression != null && constantExpression.Value.IsFunction)
			{
				flag = InliningVisitor.IsSimpleFunction(constantExpression.Value.AsFunction);
			}
			int num = 0;
			while (flag && num < invocation.Arguments.Count)
			{
				flag &= InliningVisitor.IsSimpleExpression(invocation.Arguments[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x0600B084 RID: 45188 RVA: 0x00242C8A File Offset: 0x00240E8A
		private static bool IsSimpleFunction(IFunctionValue function)
		{
			return InliningVisitor.simpleFunctions.Contains(function);
		}

		// Token: 0x0600B085 RID: 45189 RVA: 0x00242C98 File Offset: 0x00240E98
		protected override IExpression VisitInvocation(IInvocationExpression invocation)
		{
			IList<IExpression> list = base.VisitListElements(invocation.Arguments);
			bool flag = true;
			int num = 0;
			while (flag && num < list.Count)
			{
				flag &= InliningVisitor.IsSimpleExpression(list[num]);
				num++;
			}
			if (flag)
			{
				IFunctionExpression functionExpression = invocation.Function as IFunctionExpression;
				IValue value;
				if (functionExpression == null && invocation.Function.TryGetConstant(out value) && !(value is IOpaqueFunctionValue))
				{
					functionExpression = this.engine.GetExpression(value) as IFunctionExpression;
				}
				if (functionExpression != null && functionExpression.FunctionType.Min <= list.Count && list.Count <= functionExpression.FunctionType.Parameters.Count)
				{
					int num2 = ComplexityVisitor.AnalyzeComplexity(functionExpression);
					if (this.complexity + num2 <= this.complexityLimit)
					{
						this.complexity += num2;
						list = this.EnsureBindings(list, functionExpression.FunctionType.Parameters.Count);
						functionExpression = base.VisitFunction(functionExpression, list);
						return functionExpression.Expression;
					}
				}
			}
			return base.CreateInvocation(invocation, this.VisitExpression(invocation.Function), list);
		}

		// Token: 0x0600B086 RID: 45190 RVA: 0x00242DAC File Offset: 0x00240FAC
		protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			IExpression expression;
			if (base.Environment.TryGetValue(identifier.Name, identifier.IsInclusive, out expression) && expression != null)
			{
				return expression;
			}
			return identifier;
		}

		// Token: 0x0600B087 RID: 45191 RVA: 0x00242DDC File Offset: 0x00240FDC
		protected override IExpression VisitFunction(IFunctionExpression function)
		{
			IParameter[] array = null;
			IExpression[] array2 = new IExpression[function.FunctionType.Parameters.Count];
			for (int i = 0; i < array2.Length; i++)
			{
				IExpression expression;
				if (base.Environment.TryGetValue(function.FunctionType.Parameters[i].Identifier, false, out expression))
				{
					if (array == null)
					{
						array = new IParameter[function.FunctionType.Parameters.Count];
						for (int j = 0; j < array.Length; j++)
						{
							array[j] = function.FunctionType.Parameters[j];
						}
					}
					Identifier identifier = Identifier.New();
					array[i] = new ParameterSyntaxNode(identifier, array[i].Type);
					array2[i] = new InclusiveIdentifierExpressionSyntaxNode(identifier);
				}
			}
			IFunctionExpression functionExpression = base.VisitFunction(function, array2);
			if (functionExpression != function && array != null)
			{
				functionExpression = new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(functionExpression.FunctionType.ReturnType, array, functionExpression.FunctionType.Min), functionExpression.Expression);
			}
			return functionExpression;
		}

		// Token: 0x0600B088 RID: 45192 RVA: 0x001CD516 File Offset: 0x001CB716
		protected override IExpression VisitLet(ILetExpression let)
		{
			return base.VisitLet(let, new IExpression[let.Variables.Count]);
		}

		// Token: 0x0600B089 RID: 45193 RVA: 0x001CD548 File Offset: 0x001CB748
		protected override IExpression VisitRecord(IRecordExpression record)
		{
			return base.VisitRecord(record, null, new IExpression[record.Members.Count]);
		}

		// Token: 0x0600B08A RID: 45194 RVA: 0x001CD562 File Offset: 0x001CB762
		protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
		{
			return base.VisitTryCatchExceptionCase(tryCatchExceptionCase, null);
		}

		// Token: 0x0600B08B RID: 45195 RVA: 0x001CD52F File Offset: 0x001CB72F
		protected override ISection VisitModule(ISection module)
		{
			return base.VisitModule(module, new IExpression[module.Members.Count]);
		}

		// Token: 0x0600B08C RID: 45196 RVA: 0x00242ED8 File Offset: 0x002410D8
		private IList<IExpression> EnsureBindings(IList<IExpression> bindings, int required)
		{
			if (bindings.Count < required)
			{
				IExpression[] array = new IExpression[required];
				for (int i = 0; i < bindings.Count; i++)
				{
					array[i] = bindings[i];
				}
				for (int j = bindings.Count; j < array.Length; j++)
				{
					array[j] = this.engine.ConstantExpression(this.engine.Null);
				}
				bindings = array;
			}
			return bindings;
		}

		// Token: 0x04005AC7 RID: 23239
		private static readonly HashSet<IFunctionValue> simpleFunctions = new HashSet<IFunctionValue>();

		// Token: 0x04005AC8 RID: 23240
		private readonly IEngine engine;

		// Token: 0x04005AC9 RID: 23241
		private readonly Dictionary<Identifier, Identifier> identifierMap;

		// Token: 0x04005ACA RID: 23242
		private readonly int complexityLimit;

		// Token: 0x04005ACB RID: 23243
		private int complexity;
	}
}
