using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library.Values;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200011D RID: 285
	internal class AmbiguousLabeledExpressionBinding : AmbiguousBinding<IEdmLabeledExpression>, IEdmLabeledExpression, IEdmNamedElement, IEdmExpression, IEdmElement
	{
		// Token: 0x060005A8 RID: 1448 RVA: 0x0000DECB File Offset: 0x0000C0CB
		public AmbiguousLabeledExpressionBinding(IEdmLabeledExpression first, IEdmLabeledExpression second)
			: base(first, second)
		{
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		public IEdmExpression Expression
		{
			get
			{
				return this.expressionCache.GetValue(this, AmbiguousLabeledExpressionBinding.ComputeExpressionFunc, null);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		private IEdmExpression ComputeExpression()
		{
			return EdmNullExpression.Instance;
		}

		// Token: 0x0400021E RID: 542
		private readonly Cache<AmbiguousLabeledExpressionBinding, IEdmExpression> expressionCache = new Cache<AmbiguousLabeledExpressionBinding, IEdmExpression>();

		// Token: 0x0400021F RID: 543
		private static readonly Func<AmbiguousLabeledExpressionBinding, IEdmExpression> ComputeExpressionFunc = (AmbiguousLabeledExpressionBinding me) => me.ComputeExpression();
	}
}
