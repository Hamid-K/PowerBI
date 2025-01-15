using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000021 RID: 33
	internal class AmbiguousLabeledExpressionBinding : AmbiguousBinding<IEdmLabeledExpression>, IEdmLabeledExpression, IEdmNamedElement, IEdmElement, IEdmExpression
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public AmbiguousLabeledExpressionBinding(IEdmLabeledExpression first, IEdmLabeledExpression second)
			: base(first, second)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00008DD9 File Offset: 0x00006FD9
		public IEdmExpression Expression
		{
			get
			{
				return this.expressionCache.GetValue(this, AmbiguousLabeledExpressionBinding.ComputeExpressionFunc, null);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00008DED File Offset: 0x00006FED
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008DF1 File Offset: 0x00006FF1
		private static IEdmExpression ComputeExpression()
		{
			return EdmNullExpression.Instance;
		}

		// Token: 0x04000039 RID: 57
		private readonly Cache<AmbiguousLabeledExpressionBinding, IEdmExpression> expressionCache = new Cache<AmbiguousLabeledExpressionBinding, IEdmExpression>();

		// Token: 0x0400003A RID: 58
		private static readonly Func<AmbiguousLabeledExpressionBinding, IEdmExpression> ComputeExpressionFunc = (AmbiguousLabeledExpressionBinding me) => AmbiguousLabeledExpressionBinding.ComputeExpression();
	}
}
