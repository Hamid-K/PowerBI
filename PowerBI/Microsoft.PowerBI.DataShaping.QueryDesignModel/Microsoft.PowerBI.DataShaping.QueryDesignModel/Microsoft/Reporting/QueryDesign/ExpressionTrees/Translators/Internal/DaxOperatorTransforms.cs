using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000140 RID: 320
	internal static class DaxOperatorTransforms
	{
		// Token: 0x0200038A RID: 906
		internal sealed class NaryOperatorTransform : DaxOperatorTransform
		{
			// Token: 0x06001FB5 RID: 8117 RVA: 0x0005731C File Offset: 0x0005551C
			internal NaryOperatorTransform(Func<DaxExpression[], DaxExpression> transform)
			{
				this._transform = transform;
			}

			// Token: 0x06001FB6 RID: 8118 RVA: 0x0005732C File Offset: 0x0005552C
			internal override DaxExpression Translate()
			{
				DaxValidation.CheckCondition(base.Arguments.Count > 1, "Unexpected arguments were specified for a N-ary operator.");
				IEnumerable<DaxExpression> enumerable = base.Arguments.Select((QueryExpression arg) => arg.Accept<DaxExpression>(base.DaxTransform));
				return this._transform(enumerable.ToArray<DaxExpression>());
			}

			// Token: 0x040012D3 RID: 4819
			private readonly Func<DaxExpression[], DaxExpression> _transform;
		}

		// Token: 0x0200038B RID: 907
		internal sealed class BinaryEquivalentFunctionTransform : DaxOperatorTransform
		{
			// Token: 0x06001FB8 RID: 8120 RVA: 0x00057388 File Offset: 0x00055588
			internal BinaryEquivalentFunctionTransform(Func<DaxExpression, DaxExpression, DaxExpression> daxFunction)
			{
				this._daxFunction = daxFunction;
			}

			// Token: 0x06001FB9 RID: 8121 RVA: 0x00057398 File Offset: 0x00055598
			internal override DaxExpression Translate()
			{
				DaxValidation.CheckCondition(base.Arguments.Count > 1, "Unexpected arguments were specified for a N-ary operator.");
				DaxExpression daxExpression = base.Arguments[0].Accept<DaxExpression>(base.DaxTransform);
				for (int i = 1; i < base.Arguments.Count; i++)
				{
					DaxExpression daxExpression2 = base.Arguments[i].Accept<DaxExpression>(base.DaxTransform);
					daxExpression = this._daxFunction(daxExpression, daxExpression2);
				}
				return daxExpression;
			}

			// Token: 0x040012D4 RID: 4820
			private readonly Func<DaxExpression, DaxExpression, DaxExpression> _daxFunction;
		}
	}
}
