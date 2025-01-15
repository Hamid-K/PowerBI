using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x02001807 RID: 6151
	internal static class FormulaVisitorExtensions
	{
		// Token: 0x0600CA36 RID: 51766 RVA: 0x002B3938 File Offset: 0x002B1B38
		public static IReadOnlyList<TSource> Accept<TSource>(this IEnumerable<TSource> source, IFormulaVisitor<FormulaExpression> visitor) where TSource : FormulaExpression
		{
			return source.Select((TSource i) => i.Accept<FormulaExpression>(visitor)).Cast<TSource>().ToList<TSource>();
		}

		// Token: 0x0600CA37 RID: 51767 RVA: 0x002B3970 File Offset: 0x002B1B70
		public static IReadOnlyList<TVisitor> AcceptChildren<TVisitor>(this FormulaExpression source, IFormulaVisitor<TVisitor> visitor)
		{
			IReadOnlyList<FormulaExpression> children = source.Children;
			if (children == null)
			{
				return null;
			}
			return children.Select((FormulaExpression i) => i.Accept<TVisitor>(visitor)).ToList<TVisitor>();
		}

		// Token: 0x0600CA38 RID: 51768 RVA: 0x002B39AC File Offset: 0x002B1BAC
		public static void AcceptChildren(this FormulaExpression source, IFormulaVisitor visitor)
		{
			foreach (FormulaExpression formulaExpression in source.Children)
			{
				formulaExpression.Accept(visitor);
			}
		}

		// Token: 0x0600CA39 RID: 51769 RVA: 0x002B39F8 File Offset: 0x002B1BF8
		public static TSource AcceptSingle<TSource>(this TSource source, IFormulaVisitor<FormulaExpression> visitor) where TSource : FormulaExpression
		{
			return (TSource)((object)source.Accept<FormulaExpression>(visitor));
		}
	}
}
