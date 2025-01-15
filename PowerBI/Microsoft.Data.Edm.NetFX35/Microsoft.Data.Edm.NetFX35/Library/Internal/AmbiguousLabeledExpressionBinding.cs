using System;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Library.Values;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000E1 RID: 225
	internal class AmbiguousLabeledExpressionBinding : AmbiguousBinding<IEdmLabeledExpression>, IEdmLabeledExpression, IEdmNamedElement, IEdmExpression, IEdmElement
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0000C0DD File Offset: 0x0000A2DD
		public AmbiguousLabeledExpressionBinding(IEdmLabeledExpression first, IEdmLabeledExpression second)
			: base(first, second)
		{
			this.first = first;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000C0F9 File Offset: 0x0000A2F9
		public IEdmExpression Expression
		{
			get
			{
				return this.expressionCache.GetValue(this, AmbiguousLabeledExpressionBinding.ComputeExpressionFunc, null);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000C10D File Offset: 0x0000A30D
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000C111 File Offset: 0x0000A311
		private IEdmExpression ComputeExpression()
		{
			return EdmNullExpression.Instance;
		}

		// Token: 0x040001AD RID: 429
		private readonly IEdmLabeledExpression first;

		// Token: 0x040001AE RID: 430
		private readonly Cache<AmbiguousLabeledExpressionBinding, IEdmExpression> expressionCache = new Cache<AmbiguousLabeledExpressionBinding, IEdmExpression>();

		// Token: 0x040001AF RID: 431
		private static readonly Func<AmbiguousLabeledExpressionBinding, IEdmExpression> ComputeExpressionFunc = (AmbiguousLabeledExpressionBinding me) => me.ComputeExpression();
	}
}
