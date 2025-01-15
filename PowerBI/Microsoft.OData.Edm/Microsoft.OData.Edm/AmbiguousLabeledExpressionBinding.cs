using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004E RID: 78
	internal class AmbiguousLabeledExpressionBinding : AmbiguousBinding<IEdmLabeledExpression>, IEdmLabeledExpression, IEdmNamedElement, IEdmElement, IEdmExpression
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00004C18 File Offset: 0x00002E18
		public AmbiguousLabeledExpressionBinding(IEdmLabeledExpression first, IEdmLabeledExpression second)
			: base(first, second)
		{
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00004C2D File Offset: 0x00002E2D
		public IEdmExpression Expression
		{
			get
			{
				return this.expressionCache.GetValue(this, AmbiguousLabeledExpressionBinding.ComputeExpressionFunc, null);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00004C41 File Offset: 0x00002E41
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004C45 File Offset: 0x00002E45
		private static IEdmExpression ComputeExpression()
		{
			return EdmNullExpression.Instance;
		}

		// Token: 0x04000093 RID: 147
		private readonly Cache<AmbiguousLabeledExpressionBinding, IEdmExpression> expressionCache = new Cache<AmbiguousLabeledExpressionBinding, IEdmExpression>();

		// Token: 0x04000094 RID: 148
		private static readonly Func<AmbiguousLabeledExpressionBinding, IEdmExpression> ComputeExpressionFunc = (AmbiguousLabeledExpressionBinding me) => AmbiguousLabeledExpressionBinding.ComputeExpression();
	}
}
