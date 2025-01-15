using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Extensions
{
	// Token: 0x020019B7 RID: 6583
	public static class FormulaExpressionExtension
	{
		// Token: 0x0600D6E3 RID: 55011 RVA: 0x002DAD06 File Offset: 0x002D8F06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<FormulaExpressionDetail> Extract(this IEnumerable<FormulaExpression> expressions)
		{
			return FormulaExtractVisitor.Extract(expressions);
		}

		// Token: 0x0600D6E4 RID: 55012 RVA: 0x002DAD0E File Offset: 0x002D8F0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<FormulaExpressionDetail> Extract(this IEnumerable<FormulaExpression> expressions, Func<FormulaExpression, bool> predicate)
		{
			return FormulaExtractVisitor.Extract(expressions, predicate);
		}

		// Token: 0x0600D6E5 RID: 55013 RVA: 0x002DAD17 File Offset: 0x002D8F17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<FormulaExpression> ExtractNodes(this IEnumerable<FormulaExpression> expressions)
		{
			return FormulaExtractVisitor.ExtractNodes(expressions);
		}

		// Token: 0x0600D6E6 RID: 55014 RVA: 0x002DAD1F File Offset: 0x002D8F1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<FormulaExpression> ExtractNodes(this IEnumerable<FormulaExpression> expressions, Func<FormulaExpression, bool> predicate)
		{
			return FormulaExtractVisitor.ExtractNodes(expressions, predicate);
		}

		// Token: 0x0600D6E7 RID: 55015 RVA: 0x002DAD28 File Offset: 0x002D8F28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<FormulaExpression> Substitute(this IEnumerable<FormulaExpression> expressions, IReadOnlyDictionary<FormulaExpression, FormulaExpression> substitutions)
		{
			return FormulaSubstitutionVisitor.Substitute(expressions, substitutions);
		}
	}
}
