using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x0200200D RID: 8205
	internal sealed class LetSimplifier : AstVisitor2
	{
		// Token: 0x0600C7EB RID: 51179 RVA: 0x0027C7D4 File Offset: 0x0027A9D4
		public IExpression Simplify(IExpression expression)
		{
			this.substitutions = new Dictionary<Identifier, Identifier>();
			this.applySubstitutions = false;
			this.VisitExpression(expression);
			this.applySubstitutions = true;
			return this.VisitExpression(expression);
		}

		// Token: 0x0600C7EC RID: 51180 RVA: 0x0027C800 File Offset: 0x0027AA00
		protected override IExpression VisitLet(ILetExpression let)
		{
			List<VariableInitializer> list = new List<VariableInitializer>();
			for (int i = 0; i < let.Variables.Count; i++)
			{
				IIdentifierExpression identifierExpression = let.Variables[i].Value as IIdentifierExpression;
				if (identifierExpression != null && identifierExpression.Name.IsUnique)
				{
					if (!this.applySubstitutions)
					{
						this.substitutions.Add(let.Variables[i].Name, identifierExpression.Name);
					}
				}
				else
				{
					list.Add(new VariableInitializer(let.Variables[i].Name, this.VisitExpression(let.Variables[i].Value)));
				}
			}
			IExpression expression = this.VisitExpression(let.Expression);
			if (list.Count == 0)
			{
				return expression;
			}
			if (list.Count == 1)
			{
				IIdentifierExpression identifier2 = expression as IIdentifierExpression;
				if (identifier2 != null)
				{
					VariableInitializer variableInitializer = list.Where((VariableInitializer m) => m.Name.Equals(identifier2.Name)).SingleOrDefault<VariableInitializer>();
					if (variableInitializer.Value != null)
					{
						return variableInitializer.Value;
					}
					return identifier2;
				}
			}
			return new LetExpressionSyntaxNode(list, expression);
		}

		// Token: 0x0600C7ED RID: 51181 RVA: 0x0027C93C File Offset: 0x0027AB3C
		protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			Identifier identifier2 = identifier.Name;
			Identifier identifier3;
			while (this.TrySubstituteIdentifier(identifier2, out identifier3))
			{
				identifier2 = identifier3;
			}
			if (identifier2 != null && identifier2 != identifier.Name)
			{
				return new ExclusiveIdentifierExpressionSyntaxNode(identifier2);
			}
			return identifier;
		}

		// Token: 0x0600C7EE RID: 51182 RVA: 0x0027C97E File Offset: 0x0027AB7E
		private bool TrySubstituteIdentifier(Identifier identifier, out Identifier newIdentifier)
		{
			newIdentifier = null;
			return this.applySubstitutions && this.substitutions.TryGetValue(identifier, out newIdentifier);
		}

		// Token: 0x04006607 RID: 26119
		private bool applySubstitutions;

		// Token: 0x04006608 RID: 26120
		private Dictionary<Identifier, Identifier> substitutions;
	}
}
