using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017B8 RID: 6072
	internal sealed class ExpressionPattern
	{
		// Token: 0x06009982 RID: 39298 RVA: 0x001FC0AE File Offset: 0x001FA2AE
		public ExpressionPattern(params string[] patterns)
			: this(patterns, null)
		{
		}

		// Token: 0x06009983 RID: 39299 RVA: 0x001FC0B8 File Offset: 0x001FA2B8
		private ExpressionPattern(string[] patterns, IExpression[] compiled)
		{
			this.patterns = patterns;
			this.compiled = compiled;
		}

		// Token: 0x06009984 RID: 39300 RVA: 0x001FC0D0 File Offset: 0x001FA2D0
		public ExpressionPattern Bind(RecordValue bindings)
		{
			this.EnsureCompiled();
			ExpressionPattern.ReplaceBindingsVisitor replaceBindingsVisitor = new ExpressionPattern.ReplaceBindingsVisitor(bindings);
			IExpression[] array = this.compiled.Select(new Func<IExpression, IExpression>(replaceBindingsVisitor.ReplaceBindings)).ToArray<IExpression>();
			return new ExpressionPattern(this.patterns, array);
		}

		// Token: 0x06009985 RID: 39301 RVA: 0x001FC114 File Offset: 0x001FA314
		public bool TryMatch(IExpression expression, out Dictionary<string, IExpression> captures)
		{
			int num;
			return this.TryMatch(expression, out num, out captures);
		}

		// Token: 0x06009986 RID: 39302 RVA: 0x001FC12B File Offset: 0x001FA32B
		public bool TryMatch(IExpression expression, out int index, out Dictionary<string, IExpression> captures)
		{
			this.EnsureCompiled();
			for (index = 0; index < this.compiled.Length; index++)
			{
				if (new ExpressionPattern.ExpressionExtractingVisitor(this.compiled[index]).TryMatch(expression, out captures))
				{
					return true;
				}
			}
			index = -1;
			captures = null;
			return false;
		}

		// Token: 0x06009987 RID: 39303 RVA: 0x001FC16A File Offset: 0x001FA36A
		private void EnsureCompiled()
		{
			if (this.compiled == null)
			{
				this.compiled = this.patterns.Select(new Func<string, IExpression>(ExpressionPattern.Compile)).ToArray<IExpression>();
			}
		}

		// Token: 0x06009988 RID: 39304 RVA: 0x001FC196 File Offset: 0x001FA396
		private static IExpression Compile(string m)
		{
			return ((IExpressionDocument)Engine.Instance.Parse(m, delegate(IError entry)
			{
				throw new InvalidOperationException(entry.Message);
			})).Expression;
		}

		// Token: 0x06009989 RID: 39305 RVA: 0x001FC1CC File Offset: 0x001FA3CC
		private static bool IsOptional(string name)
		{
			return name.StartsWith("_o_", StringComparison.Ordinal);
		}

		// Token: 0x0600998A RID: 39306 RVA: 0x001FC1DA File Offset: 0x001FA3DA
		private static bool IsCapture(string name)
		{
			return name.StartsWith("__", StringComparison.Ordinal) || ExpressionPattern.IsOptional(name);
		}

		// Token: 0x0400514B RID: 20811
		private const string requiredPrefix = "__";

		// Token: 0x0400514C RID: 20812
		private const string optionalPrefix = "_o_";

		// Token: 0x0400514D RID: 20813
		private readonly string[] patterns;

		// Token: 0x0400514E RID: 20814
		private IExpression[] compiled;

		// Token: 0x020017B9 RID: 6073
		private sealed class ExpressionExtractingVisitor : AstVisitor
		{
			// Token: 0x0600998B RID: 39307 RVA: 0x001FC1F2 File Offset: 0x001FA3F2
			public ExpressionExtractingVisitor(IExpression pattern)
			{
				this.pattern = pattern;
				this.captured = new Dictionary<string, IExpression>();
			}

			// Token: 0x0600998C RID: 39308 RVA: 0x001FC20C File Offset: 0x001FA40C
			public bool TryMatch(IExpression expression, out Dictionary<string, IExpression> captures)
			{
				this.VisitExpression(this.pattern, expression);
				if (this.ContinueVisitation)
				{
					captures = this.captured;
				}
				else
				{
					captures = null;
				}
				return captures != null;
			}

			// Token: 0x0600998D RID: 39309 RVA: 0x001FC238 File Offset: 0x001FA438
			private void Capture(string name, IExpression expression)
			{
				string text = (ExpressionPattern.IsOptional(name) ? "_o_" : "__");
				name = name.Substring(text.Length);
				IExpression expression2;
				if (this.captured.TryGetValue(name, out expression2))
				{
					this.VisitExpression(expression2, expression);
					return;
				}
				this.captured.Add(name, expression);
			}

			// Token: 0x170027B0 RID: 10160
			// (get) Token: 0x0600998E RID: 39310 RVA: 0x001FC28F File Offset: 0x001FA48F
			private bool ContinueVisitation
			{
				get
				{
					return this.pattern != null;
				}
			}

			// Token: 0x0600998F RID: 39311 RVA: 0x001FC29A File Offset: 0x001FA49A
			private void TerminateVisitation()
			{
				this.pattern = null;
			}

			// Token: 0x06009990 RID: 39312 RVA: 0x001FC2A4 File Offset: 0x001FA4A4
			private IExpression VisitExpression(IExpression pattern, IExpression expression)
			{
				if (!this.ContinueVisitation)
				{
					return expression;
				}
				if (pattern.Kind == ExpressionKind.Identifier)
				{
					IIdentifierExpression identifierExpression = (IIdentifierExpression)pattern;
					if (expression == null)
					{
						if (!ExpressionPattern.IsOptional(identifierExpression.Name.Name))
						{
							this.TerminateVisitation();
						}
						return expression;
					}
					if (ExpressionPattern.IsCapture(identifierExpression.Name.Name))
					{
						this.Capture(identifierExpression.Name.Name, expression);
						return expression;
					}
				}
				Value value;
				if ((pattern.Kind == ExpressionKind.Identifier || pattern.Kind == ExpressionKind.Function) && expression != null && expression.TryGetConstant(out value))
				{
					expression = value.Expression;
				}
				if (expression == null || pattern.Kind != expression.Kind)
				{
					this.TerminateVisitation();
					return expression;
				}
				IExpression expression2 = this.pattern;
				this.pattern = pattern;
				base.VisitExpression(expression);
				if (this.ContinueVisitation)
				{
					this.pattern = expression2;
				}
				return expression;
			}

			// Token: 0x06009991 RID: 39313 RVA: 0x001FC374 File Offset: 0x001FA574
			private IExpression VisitOptionalExpression(IExpression pattern, IExpression expression)
			{
				if (pattern == null)
				{
					return expression;
				}
				return this.VisitExpression(pattern, expression);
			}

			// Token: 0x06009992 RID: 39314 RVA: 0x001FC384 File Offset: 0x001FA584
			private IParameter VisitParameter(IParameter pattern, IParameter parameter)
			{
				this.VisitOptionalExpression(pattern.Type, parameter.Type);
				if (this.ContinueVisitation)
				{
					if (ExpressionPattern.IsCapture(pattern.Identifier.Name))
					{
						this.Capture(pattern.Identifier.Name, new ExclusiveIdentifierExpressionSyntaxNode(parameter.Identifier));
					}
					else if (!pattern.Identifier.Equals(parameter.Identifier))
					{
						this.TerminateVisitation();
					}
				}
				return parameter;
			}

			// Token: 0x06009993 RID: 39315 RVA: 0x001FC3F8 File Offset: 0x001FA5F8
			protected override IExpression VisitBinary(IBinaryExpression binary)
			{
				IBinaryExpression binaryExpression = (IBinaryExpression)this.pattern;
				this.VisitExpression(binaryExpression.Left, binary.Left);
				this.VisitExpression(binaryExpression.Right, binary.Right);
				if (binaryExpression.Operator != binary.Operator)
				{
					this.TerminateVisitation();
				}
				return binary;
			}

			// Token: 0x06009994 RID: 39316 RVA: 0x001FC44C File Offset: 0x001FA64C
			protected override IExpression VisitConstant(IConstantExpression constant)
			{
				Value value;
				if (!this.pattern.TryGetConstant(out value) || !value.Equals(constant.Value))
				{
					this.TerminateVisitation();
				}
				return constant;
			}

			// Token: 0x06009995 RID: 39317 RVA: 0x001FC480 File Offset: 0x001FA680
			protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
			{
				IFieldAccessExpression fieldAccessExpression = (IFieldAccessExpression)this.pattern;
				this.VisitExpression(fieldAccessExpression.Expression, fieldAccess.Expression);
				if (!fieldAccessExpression.MemberName.Equals(fieldAccess.MemberName))
				{
					this.TerminateVisitation();
				}
				return fieldAccess;
			}

			// Token: 0x06009996 RID: 39318 RVA: 0x001FC4C8 File Offset: 0x001FA6C8
			protected override IExpression VisitFunctionType(IFunctionTypeExpression functionType)
			{
				IFunctionTypeExpression functionTypeExpression = (IFunctionTypeExpression)this.pattern;
				this.VisitParameters(functionTypeExpression.Parameters, functionType.Parameters);
				this.VisitOptionalExpression(functionTypeExpression.ReturnType, functionType.ReturnType);
				return functionType;
			}

			// Token: 0x06009997 RID: 39319 RVA: 0x001FC508 File Offset: 0x001FA708
			protected override IExpression VisitFunction(IFunctionExpression function)
			{
				IFunctionExpression functionExpression = (IFunctionExpression)this.pattern;
				this.VisitExpression(functionExpression.FunctionType, function.FunctionType);
				this.VisitExpression(functionExpression.Expression, function.Expression);
				return function;
			}

			// Token: 0x06009998 RID: 39320 RVA: 0x001FC548 File Offset: 0x001FA748
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				IIdentifierExpression identifierExpression = (IIdentifierExpression)this.pattern;
				if (!identifier.Name.Equals(identifierExpression.Name))
				{
					this.TerminateVisitation();
				}
				return identifier;
			}

			// Token: 0x06009999 RID: 39321 RVA: 0x001FC57C File Offset: 0x001FA77C
			protected override IExpression VisitIf(IIfExpression @if)
			{
				IIfExpression ifExpression = (IIfExpression)this.pattern;
				this.VisitExpression(ifExpression.Condition, @if.Condition);
				this.VisitExpression(ifExpression.TrueCase, @if.TrueCase);
				this.VisitExpression(ifExpression.FalseCase, @if.FalseCase);
				return @if;
			}

			// Token: 0x0600999A RID: 39322 RVA: 0x001FC5D0 File Offset: 0x001FA7D0
			protected override IExpression VisitElementAccess(IElementAccessExpression elementAccess)
			{
				IElementAccessExpression elementAccessExpression = (IElementAccessExpression)this.pattern;
				this.VisitExpression(elementAccessExpression.Collection, elementAccess.Collection);
				this.VisitExpression(elementAccessExpression.Key, elementAccess.Key);
				return elementAccess;
			}

			// Token: 0x0600999B RID: 39323 RVA: 0x001FC610 File Offset: 0x001FA810
			protected override IExpression VisitInvocation(IInvocationExpression invocation)
			{
				IInvocationExpression invocationExpression = (IInvocationExpression)this.pattern;
				this.VisitExpression(invocationExpression.Function, invocation.Function);
				this.VisitExpressions(invocationExpression.Arguments, invocation.Arguments);
				return invocation;
			}

			// Token: 0x0600999C RID: 39324 RVA: 0x001FC650 File Offset: 0x001FA850
			protected override IExpression VisitLet(ILetExpression let)
			{
				ILetExpression letExpression = (ILetExpression)this.pattern;
				this.VisitInitializers(letExpression.Variables, let.Variables);
				this.VisitExpression(letExpression.Expression, let.Expression);
				return let;
			}

			// Token: 0x0600999D RID: 39325 RVA: 0x001FC690 File Offset: 0x001FA890
			protected override IExpression VisitList(IListExpression list)
			{
				IListExpression listExpression = (IListExpression)this.pattern;
				this.VisitExpressions(listExpression.Members, list.Members);
				return list;
			}

			// Token: 0x0600999E RID: 39326 RVA: 0x001FC6BC File Offset: 0x001FA8BC
			protected override IExpression VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
			{
				IMultiFieldRecordProjectionExpression multiFieldRecordProjectionExpression = (IMultiFieldRecordProjectionExpression)this.pattern;
				this.VisitExpression(multiFieldRecordProjectionExpression.Expression, multiFieldRecordProjection.Expression);
				return multiFieldRecordProjection;
			}

			// Token: 0x0600999F RID: 39327 RVA: 0x001FC6EC File Offset: 0x001FA8EC
			protected override IExpression VisitParentheses(IParenthesesExpression parentheses)
			{
				IParenthesesExpression parenthesesExpression = (IParenthesesExpression)this.pattern;
				this.VisitExpression(parenthesesExpression.Expression, parentheses.Expression);
				return parentheses;
			}

			// Token: 0x060099A0 RID: 39328 RVA: 0x001FC71C File Offset: 0x001FA91C
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				IRecordExpression recordExpression = (IRecordExpression)this.pattern;
				this.VisitInitializers(recordExpression.Members, record.Members);
				return record;
			}

			// Token: 0x060099A1 RID: 39329 RVA: 0x001FC748 File Offset: 0x001FA948
			protected override IExpression VisitUnary(IUnaryExpression unary)
			{
				IUnaryExpression unaryExpression = (IUnaryExpression)this.pattern;
				this.VisitExpression(unaryExpression.Expression, unary.Expression);
				return unary;
			}

			// Token: 0x060099A2 RID: 39330 RVA: 0x001FC778 File Offset: 0x001FA978
			private void VisitInitializers(IList<VariableInitializer> patternVariables, IList<VariableInitializer> variables)
			{
				Dictionary<Identifier, VariableInitializer> dictionary = patternVariables.ToDictionary((VariableInitializer init) => init.Name);
				foreach (VariableInitializer variableInitializer in variables)
				{
					VariableInitializer variableInitializer2;
					if (!dictionary.TryGetValue(variableInitializer.Name, out variableInitializer2))
					{
						this.TerminateVisitation();
						return;
					}
					this.VisitExpression(variableInitializer2.Value, variableInitializer.Value);
					dictionary.Remove(variableInitializer.Name);
				}
				foreach (VariableInitializer variableInitializer3 in dictionary.Values)
				{
					this.VisitExpression(variableInitializer3.Value, null);
				}
			}

			// Token: 0x060099A3 RID: 39331 RVA: 0x001FC86C File Offset: 0x001FAA6C
			private void VisitExpressions(IList<IExpression> patternList, IList<IExpression> list)
			{
				if (patternList.Count < list.Count)
				{
					this.TerminateVisitation();
					return;
				}
				int i;
				for (i = 0; i < list.Count; i++)
				{
					this.VisitExpression(patternList[i], list[i]);
				}
				while (i < patternList.Count)
				{
					this.VisitExpression(patternList[i], null);
					i++;
				}
			}

			// Token: 0x060099A4 RID: 39332 RVA: 0x001FC8D4 File Offset: 0x001FAAD4
			private void VisitParameters(IList<IParameter> patternList, IList<IParameter> list)
			{
				if (patternList.Count < list.Count)
				{
					this.TerminateVisitation();
					return;
				}
				int i;
				for (i = 0; i < list.Count; i++)
				{
					this.VisitParameter(patternList[i], list[i]);
				}
				while (i < patternList.Count)
				{
					if (!ExpressionPattern.IsOptional(patternList[i].Identifier.Name))
					{
						this.TerminateVisitation();
					}
					i++;
				}
			}

			// Token: 0x0400514F RID: 20815
			private IExpression pattern;

			// Token: 0x04005150 RID: 20816
			private readonly Dictionary<string, IExpression> captured;
		}

		// Token: 0x020017BB RID: 6075
		private sealed class ReplaceBindingsVisitor : LogicalAstVisitor<Value>
		{
			// Token: 0x060099A8 RID: 39336 RVA: 0x001FC960 File Offset: 0x001FAB60
			public ReplaceBindingsVisitor(RecordValue bindings)
			{
				base.Environment.EnterScope();
				for (int i = 0; i < bindings.Keys.Length; i++)
				{
					base.Environment.Add(bindings.Keys[i], bindings[i]);
				}
			}

			// Token: 0x060099A9 RID: 39337 RVA: 0x00146DFF File Offset: 0x00144FFF
			public IExpression ReplaceBindings(IExpression expression)
			{
				return this.VisitExpression(expression);
			}

			// Token: 0x060099AA RID: 39338 RVA: 0x001FC9B8 File Offset: 0x001FABB8
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				Value value;
				if (base.Environment.TryGetValue(identifier.Name, identifier.IsInclusive, out value) && value != null)
				{
					return new ConstantExpressionSyntaxNode(value);
				}
				return base.VisitIdentifier(identifier);
			}

			// Token: 0x060099AB RID: 39339 RVA: 0x001FC9F1 File Offset: 0x001FABF1
			protected override IExpression VisitFunction(IFunctionExpression function)
			{
				return base.VisitFunction(function, new Value[function.FunctionType.Parameters.Count]);
			}

			// Token: 0x060099AC RID: 39340 RVA: 0x001FCA0F File Offset: 0x001FAC0F
			protected override IExpression VisitLet(ILetExpression let)
			{
				return base.VisitLet(let, new Value[let.Variables.Count]);
			}

			// Token: 0x060099AD RID: 39341 RVA: 0x001FCA28 File Offset: 0x001FAC28
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				return base.VisitRecord(record, null, new Value[record.Members.Count]);
			}

			// Token: 0x060099AE RID: 39342 RVA: 0x001FCA42 File Offset: 0x001FAC42
			protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
			{
				return base.VisitTryCatchExceptionCase(tryCatchExceptionCase, null);
			}

			// Token: 0x060099AF RID: 39343 RVA: 0x001FCA4C File Offset: 0x001FAC4C
			protected override ISection VisitModule(ISection module)
			{
				return base.VisitModule(module, null);
			}
		}
	}
}
