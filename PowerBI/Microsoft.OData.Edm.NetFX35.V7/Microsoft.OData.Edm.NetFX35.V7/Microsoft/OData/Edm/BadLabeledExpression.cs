using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000035 RID: 53
	internal class BadLabeledExpression : BadElement, IEdmLabeledExpression, IEdmNamedElement, IEdmElement, IEdmExpression
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000930E File Offset: 0x0000750E
		public BadLabeledExpression(string name, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00009332 File Offset: 0x00007532
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00008DED File Offset: 0x00006FED
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000933A File Offset: 0x0000753A
		public IEdmExpression Expression
		{
			get
			{
				return this.expressionCache.GetValue(this, BadLabeledExpression.ComputeExpressionFunc, null);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008DF1 File Offset: 0x00006FF1
		private static IEdmExpression ComputeExpression()
		{
			return EdmNullExpression.Instance;
		}

		// Token: 0x04000053 RID: 83
		private readonly string name;

		// Token: 0x04000054 RID: 84
		private readonly Cache<BadLabeledExpression, IEdmExpression> expressionCache = new Cache<BadLabeledExpression, IEdmExpression>();

		// Token: 0x04000055 RID: 85
		private static readonly Func<BadLabeledExpression, IEdmExpression> ComputeExpressionFunc = (BadLabeledExpression me) => BadLabeledExpression.ComputeExpression();
	}
}
