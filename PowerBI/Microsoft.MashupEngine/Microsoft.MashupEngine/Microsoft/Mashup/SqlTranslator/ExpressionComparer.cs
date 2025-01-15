using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x02002008 RID: 8200
	internal sealed class ExpressionComparer : IEqualityComparer<IExpression>
	{
		// Token: 0x0600C7C8 RID: 51144 RVA: 0x0027BF33 File Offset: 0x0027A133
		public ExpressionComparer(bool ordinalFunctionParameters)
		{
			this.comparingVisitor = new ExpressionComparer.ExpressionComparingVisitor(ordinalFunctionParameters);
			this.hashingVisitor = new ExpressionComparer.ExpressionHashingVisitor();
		}

		// Token: 0x0600C7C9 RID: 51145 RVA: 0x0027BF52 File Offset: 0x0027A152
		public bool Equals(IExpression x, IExpression y)
		{
			return this.comparingVisitor.Equals(x, y);
		}

		// Token: 0x0600C7CA RID: 51146 RVA: 0x0027BF61 File Offset: 0x0027A161
		public int GetHashCode(IExpression obj)
		{
			return this.hashingVisitor.GetHashCode(obj);
		}

		// Token: 0x040065FC RID: 26108
		private const int complexityLimit = 50;

		// Token: 0x040065FD RID: 26109
		private readonly ExpressionComparer.ExpressionComparingVisitor comparingVisitor;

		// Token: 0x040065FE RID: 26110
		private readonly ExpressionComparer.ExpressionHashingVisitor hashingVisitor;

		// Token: 0x02002009 RID: 8201
		private sealed class ExpressionHashingVisitor : AstVisitor2
		{
			// Token: 0x0600C7CB RID: 51147 RVA: 0x0027BF6F File Offset: 0x0027A16F
			public int GetHashCode(IExpression expression)
			{
				this.hashCode = 0;
				this.VisitExpression(expression);
				return this.hashCode;
			}

			// Token: 0x0600C7CC RID: 51148 RVA: 0x0027BF88 File Offset: 0x0027A188
			protected override IExpression VisitExpression(IExpression expression)
			{
				if (this.complexity < 50)
				{
					this.hashCode = (int)(this.hashCode * 37 + expression.Kind);
					this.complexity++;
					base.VisitExpression(expression);
					this.complexity--;
				}
				return expression;
			}

			// Token: 0x0600C7CD RID: 51149 RVA: 0x0027BFDA File Offset: 0x0027A1DA
			protected override IExpression VisitConstant(IConstantExpression2 constant)
			{
				this.hashCode = this.hashCode * 29 + constant.Value.GetHashCode();
				return constant;
			}

			// Token: 0x0600C7CE RID: 51150 RVA: 0x0027BFF8 File Offset: 0x0027A1F8
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				this.hashCode = this.hashCode * 83 + identifier.Name.GetHashCode();
				return identifier;
			}

			// Token: 0x040065FF RID: 26111
			private int hashCode;

			// Token: 0x04006600 RID: 26112
			private int complexity;
		}

		// Token: 0x0200200A RID: 8202
		private sealed class ExpressionComparingVisitor : AstVisitor2
		{
			// Token: 0x0600C7D0 RID: 51152 RVA: 0x0027C016 File Offset: 0x0027A216
			public ExpressionComparingVisitor(bool ordinalFunctionParameters)
			{
				this.ordinalFunctionParameters = ordinalFunctionParameters;
			}

			// Token: 0x0600C7D1 RID: 51153 RVA: 0x0027C025 File Offset: 0x0027A225
			public bool Equals(IExpression x, IExpression y)
			{
				this.remap = null;
				this.pattern = x;
				this.complexity = 0;
				this.VisitExpression(x, y);
				return this.ContinueVisitation;
			}

			// Token: 0x1700306C RID: 12396
			// (get) Token: 0x0600C7D2 RID: 51154 RVA: 0x0027C04B File Offset: 0x0027A24B
			private bool ContinueVisitation
			{
				get
				{
					return this.pattern != null;
				}
			}

			// Token: 0x0600C7D3 RID: 51155 RVA: 0x0027C056 File Offset: 0x0027A256
			private void TerminateVisitation()
			{
				this.pattern = null;
			}

			// Token: 0x0600C7D4 RID: 51156 RVA: 0x0027C060 File Offset: 0x0027A260
			private IExpression VisitExpression(IExpression pattern, IExpression expression)
			{
				if (this.complexity >= 50)
				{
					this.TerminateVisitation();
				}
				if (!this.ContinueVisitation)
				{
					return expression;
				}
				if (expression == null || pattern.Kind != expression.Kind)
				{
					this.TerminateVisitation();
					return expression;
				}
				IExpression expression2 = this.pattern;
				this.pattern = pattern;
				this.complexity++;
				base.VisitExpression(expression);
				this.complexity--;
				if (this.ContinueVisitation)
				{
					this.pattern = expression2;
				}
				return expression;
			}

			// Token: 0x0600C7D5 RID: 51157 RVA: 0x0027C0E4 File Offset: 0x0027A2E4
			protected override IExpression VisitBinary(IBinaryExpression binary)
			{
				IBinaryExpression binaryExpression = (IBinaryExpression)this.pattern;
				if (binaryExpression.Operator != binary.Operator)
				{
					this.TerminateVisitation();
				}
				this.VisitExpression(binaryExpression.Left, binary.Left);
				this.VisitExpression(binaryExpression.Right, binary.Right);
				return binary;
			}

			// Token: 0x0600C7D6 RID: 51158 RVA: 0x0027C138 File Offset: 0x0027A338
			protected override IExpression VisitConstant(IConstantExpression2 constant)
			{
				IValue value;
				if (!ExpressionComparer.ExpressionComparingVisitor.TryGetConstant(this.pattern, out value) || !value.Equals(constant.Value))
				{
					this.TerminateVisitation();
				}
				return constant;
			}

			// Token: 0x0600C7D7 RID: 51159 RVA: 0x0027C16C File Offset: 0x0027A36C
			protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
			{
				IFieldAccessExpression fieldAccessExpression = (IFieldAccessExpression)this.pattern;
				if (fieldAccessExpression.IsOptional != fieldAccess.IsOptional || !fieldAccessExpression.MemberName.Equals(fieldAccess.MemberName))
				{
					this.TerminateVisitation();
				}
				this.VisitExpression(fieldAccessExpression.Expression, fieldAccess.Expression);
				return fieldAccess;
			}

			// Token: 0x0600C7D8 RID: 51160 RVA: 0x0027C1C0 File Offset: 0x0027A3C0
			protected override IExpression VisitFunction(IFunctionExpression function)
			{
				IFunctionExpression functionExpression = (IFunctionExpression)this.pattern;
				if (function.FunctionType.Parameters.Count != functionExpression.FunctionType.Parameters.Count)
				{
					this.TerminateVisitation();
					return function;
				}
				Dictionary<Identifier, Identifier> dictionary = this.remap;
				if (this.ordinalFunctionParameters)
				{
					this.remap = new Dictionary<Identifier, Identifier>();
					if (dictionary != null)
					{
						foreach (KeyValuePair<Identifier, Identifier> keyValuePair in dictionary)
						{
							this.remap.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					for (int i = 0; i < function.FunctionType.Parameters.Count; i++)
					{
						this.remap[function.FunctionType.Parameters[i].Identifier] = functionExpression.FunctionType.Parameters[i].Identifier;
					}
				}
				this.VisitExpression(functionExpression.Expression, function.Expression);
				this.remap = dictionary;
				return function;
			}

			// Token: 0x0600C7D9 RID: 51161 RVA: 0x0027C2EC File Offset: 0x0027A4EC
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				Identifier name;
				if (this.remap == null || !this.remap.TryGetValue(identifier.Name, out name))
				{
					name = identifier.Name;
				}
				IIdentifierExpression identifierExpression = (IIdentifierExpression)this.pattern;
				if (identifier.IsInclusive != identifierExpression.IsInclusive || !name.Equals(identifierExpression.Name))
				{
					this.TerminateVisitation();
				}
				return identifier;
			}

			// Token: 0x0600C7DA RID: 51162 RVA: 0x0027C34C File Offset: 0x0027A54C
			protected override IExpression VisitIf(IIfExpression @if)
			{
				IIfExpression ifExpression = (IIfExpression)this.pattern;
				this.VisitExpression(ifExpression.Condition, @if.Condition);
				this.VisitExpression(ifExpression.TrueCase, @if.TrueCase);
				this.VisitExpression(ifExpression.FalseCase, @if.FalseCase);
				return @if;
			}

			// Token: 0x0600C7DB RID: 51163 RVA: 0x0027C3A0 File Offset: 0x0027A5A0
			protected override IExpression VisitElementAccess(IElementAccessExpression elementAccess)
			{
				IElementAccessExpression elementAccessExpression = (IElementAccessExpression)this.pattern;
				if (elementAccessExpression.IsOptional != elementAccess.IsOptional)
				{
					this.TerminateVisitation();
				}
				this.VisitExpression(elementAccessExpression.Collection, elementAccess.Collection);
				this.VisitExpression(elementAccessExpression.Key, elementAccess.Key);
				return elementAccess;
			}

			// Token: 0x0600C7DC RID: 51164 RVA: 0x0027C3F4 File Offset: 0x0027A5F4
			protected override IExpression VisitInvocation(IInvocationExpression invocation)
			{
				IInvocationExpression invocationExpression = (IInvocationExpression)this.pattern;
				this.VisitExpression(invocationExpression.Function, invocation.Function);
				this.VisitExpressions(invocationExpression.Arguments, invocation.Arguments);
				return invocation;
			}

			// Token: 0x0600C7DD RID: 51165 RVA: 0x0027C434 File Offset: 0x0027A634
			protected override IExpression VisitLet(ILetExpression let)
			{
				ILetExpression letExpression = (ILetExpression)this.pattern;
				this.VisitInitializers(letExpression.Variables, let.Variables);
				this.VisitExpression(letExpression.Expression, let.Expression);
				return let;
			}

			// Token: 0x0600C7DE RID: 51166 RVA: 0x0027C474 File Offset: 0x0027A674
			protected override IExpression VisitList(IListExpression list)
			{
				IListExpression listExpression = (IListExpression)this.pattern;
				this.VisitExpressions(listExpression.Members, list.Members);
				return list;
			}

			// Token: 0x0600C7DF RID: 51167 RVA: 0x0027C4A0 File Offset: 0x0027A6A0
			protected override IExpression VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
			{
				IMultiFieldRecordProjectionExpression multiFieldRecordProjectionExpression = (IMultiFieldRecordProjectionExpression)this.pattern;
				if (multiFieldRecordProjectionExpression.IsOptional != multiFieldRecordProjection.IsOptional)
				{
					this.TerminateVisitation();
				}
				this.VisitIdentifiers(multiFieldRecordProjectionExpression.MemberNames, multiFieldRecordProjection.MemberNames);
				this.VisitExpression(multiFieldRecordProjectionExpression.Expression, multiFieldRecordProjection.Expression);
				return multiFieldRecordProjection;
			}

			// Token: 0x0600C7E0 RID: 51168 RVA: 0x0027C4F4 File Offset: 0x0027A6F4
			protected override IExpression VisitParentheses(IParenthesesExpression parentheses)
			{
				IParenthesesExpression parenthesesExpression = (IParenthesesExpression)this.pattern;
				this.VisitExpression(parenthesesExpression.Expression, parentheses.Expression);
				return parentheses;
			}

			// Token: 0x0600C7E1 RID: 51169 RVA: 0x0027C524 File Offset: 0x0027A724
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				IRecordExpression recordExpression = (IRecordExpression)this.pattern;
				this.VisitInitializers(recordExpression.Members, record.Members);
				return record;
			}

			// Token: 0x0600C7E2 RID: 51170 RVA: 0x0027C550 File Offset: 0x0027A750
			protected override IExpression VisitUnary(IUnaryExpression unary)
			{
				IUnaryExpression unaryExpression = (IUnaryExpression)this.pattern;
				if (unaryExpression.Operator != unary.Operator)
				{
					this.TerminateVisitation();
				}
				this.VisitExpression(unaryExpression.Expression, unary.Expression);
				return unary;
			}

			// Token: 0x0600C7E3 RID: 51171 RVA: 0x0027C594 File Offset: 0x0027A794
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

			// Token: 0x0600C7E4 RID: 51172 RVA: 0x0027C688 File Offset: 0x0027A888
			private void VisitExpressions(IList<IExpression> patternList, IList<IExpression> list)
			{
				if (patternList.Count != list.Count)
				{
					this.TerminateVisitation();
					return;
				}
				for (int i = 0; i < list.Count; i++)
				{
					this.VisitExpression(patternList[i], list[i]);
				}
			}

			// Token: 0x0600C7E5 RID: 51173 RVA: 0x0027C6D0 File Offset: 0x0027A8D0
			private void VisitIdentifiers(IList<Identifier> patternList, IList<Identifier> list)
			{
				if (patternList.Count != list.Count)
				{
					this.TerminateVisitation();
					return;
				}
				for (int i = 0; i < list.Count; i++)
				{
					if (patternList[i] != list[i])
					{
						this.TerminateVisitation();
						return;
					}
				}
			}

			// Token: 0x0600C7E6 RID: 51174 RVA: 0x0027C720 File Offset: 0x0027A920
			private static bool TryGetConstant(IExpression expression, out IValue value)
			{
				IConstantExpression2 constantExpression = expression as IConstantExpression2;
				if (constantExpression != null)
				{
					value = constantExpression.Value;
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x04006601 RID: 26113
			private readonly bool ordinalFunctionParameters;

			// Token: 0x04006602 RID: 26114
			private Dictionary<Identifier, Identifier> remap;

			// Token: 0x04006603 RID: 26115
			private IExpression pattern;

			// Token: 0x04006604 RID: 26116
			private int complexity;
		}
	}
}
