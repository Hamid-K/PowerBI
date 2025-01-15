using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017FF RID: 6143
	internal class FormulaSubstitutionVisitor : IFormulaVisitor<FormulaExpression>
	{
		// Token: 0x0600CA0C RID: 51724 RVA: 0x002B358F File Offset: 0x002B178F
		private FormulaSubstitutionVisitor(IReadOnlyDictionary<FormulaExpression, FormulaExpression> substitutions)
		{
			this._substitutions = substitutions;
		}

		// Token: 0x0600CA0D RID: 51725 RVA: 0x002B35A0 File Offset: 0x002B17A0
		public static FormulaExpression Substitute(FormulaExpression expression, Func<FormulaExpression, bool> extractSelector, Func<IEnumerable<FormulaExpression>, Dictionary<FormulaExpression, FormulaExpression>> substituteSelector)
		{
			IEnumerable<FormulaExpression> enumerable = expression.Nodes.Where(extractSelector);
			return new FormulaSubstitutionVisitor(substituteSelector(enumerable)).Visit(expression);
		}

		// Token: 0x0600CA0E RID: 51726 RVA: 0x002B35CC File Offset: 0x002B17CC
		public static FormulaExpression Substitute(FormulaExpression expression, IReadOnlyDictionary<FormulaExpression, FormulaExpression> substitutions)
		{
			return new FormulaSubstitutionVisitor(substitutions).Visit(expression);
		}

		// Token: 0x0600CA0F RID: 51727 RVA: 0x002B35DC File Offset: 0x002B17DC
		public static IEnumerable<FormulaExpression> Substitute(IEnumerable<FormulaExpression> expressions, IReadOnlyDictionary<FormulaExpression, FormulaExpression> substitutions)
		{
			FormulaSubstitutionVisitor visitor = new FormulaSubstitutionVisitor(substitutions);
			return expressions.Select((FormulaExpression e) => visitor.Visit(e));
		}

		// Token: 0x0600CA10 RID: 51728 RVA: 0x002B3610 File Offset: 0x002B1810
		public FormulaExpression Visit(FormulaExpression expression)
		{
			FormulaExpression formulaExpression;
			if (!this._substitutions.TryGetValue(expression, out formulaExpression))
			{
				return expression.AcceptClone(this);
			}
			return formulaExpression;
		}

		// Token: 0x04004F55 RID: 20309
		private readonly IReadOnlyDictionary<FormulaExpression, FormulaExpression> _substitutions;
	}
}
