using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200013F RID: 319
	internal abstract class DaxOperatorTransform
	{
		// Token: 0x06001176 RID: 4470 RVA: 0x00030C37 File Offset: 0x0002EE37
		internal static DaxOperatorTransform Create(QueryOperatorExpression expression, DaxTransform daxTransform)
		{
			DaxOperatorTransform daxOperatorTransform = DaxOperatorTransform.CreateCore(expression.Operator.Name, expression.UseBinaryEquivalent);
			daxOperatorTransform.Init(expression, daxTransform);
			return daxOperatorTransform;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00030C58 File Offset: 0x0002EE58
		private static DaxOperatorTransform CreateCore(string operatorName, bool useBinaryEquivalent)
		{
			if (!(operatorName == "Core.And"))
			{
				throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedOperator(operatorName));
			}
			if (useBinaryEquivalent)
			{
				Func<DaxExpression, DaxExpression, DaxExpression> func;
				if ((func = DaxOperatorTransform.<>O.<0>__And) == null)
				{
					func = (DaxOperatorTransform.<>O.<0>__And = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.And));
				}
				return new DaxOperatorTransforms.BinaryEquivalentFunctionTransform(func);
			}
			Func<DaxExpression[], DaxExpression> func2;
			if ((func2 = DaxOperatorTransform.<>O.<1>__And) == null)
			{
				func2 = (DaxOperatorTransform.<>O.<1>__And = new Func<DaxExpression[], DaxExpression>(DaxOperators.And));
			}
			return new DaxOperatorTransforms.NaryOperatorTransform(func2);
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00030CC2 File Offset: 0x0002EEC2
		protected IReadOnlyList<QueryExpression> Arguments
		{
			get
			{
				return this._expr.Arguments;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00030CCF File Offset: 0x0002EECF
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x00030CD7 File Offset: 0x0002EED7
		private protected DaxTransform DaxTransform { protected get; private set; }

		// Token: 0x0600117B RID: 4475
		internal abstract DaxExpression Translate();

		// Token: 0x0600117C RID: 4476 RVA: 0x00030CE0 File Offset: 0x0002EEE0
		private void Init(QueryOperatorExpression expression, DaxTransform daxTransform)
		{
			this._expr = expression;
			this.DaxTransform = daxTransform;
		}

		// Token: 0x04000AD5 RID: 2773
		private QueryOperatorExpression _expr;

		// Token: 0x02000389 RID: 905
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040012D1 RID: 4817
			public static Func<DaxExpression, DaxExpression, DaxExpression> <0>__And;

			// Token: 0x040012D2 RID: 4818
			public static Func<DaxExpression[], DaxExpression> <1>__And;
		}
	}
}
