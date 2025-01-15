using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001B9 RID: 441
	public class EdmLabeledExpressionReferenceExpression : EdmElement, IEdmLabeledExpressionReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x00019375 File Offset: 0x00017575
		public EdmLabeledExpressionReferenceExpression()
		{
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001937D File Offset: 0x0001757D
		public EdmLabeledExpressionReferenceExpression(IEdmLabeledExpression referencedLabeledExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmLabeledExpression>(referencedLabeledExpression, "referencedLabeledExpression");
			this.referencedLabeledExpression = referencedLabeledExpression;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00019398 File Offset: 0x00017598
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x000193A0 File Offset: 0x000175A0
		public IEdmLabeledExpression ReferencedLabeledExpression
		{
			get
			{
				return this.referencedLabeledExpression;
			}
			set
			{
				EdmUtil.CheckArgumentNull<IEdmLabeledExpression>(value, "value");
				if (this.referencedLabeledExpression != null)
				{
					throw new InvalidOperationException(Strings.ValueHasAlreadyBeenSet);
				}
				this.referencedLabeledExpression = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x000193C8 File Offset: 0x000175C8
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.LabeledExpressionReference;
			}
		}

		// Token: 0x04000496 RID: 1174
		private IEdmLabeledExpression referencedLabeledExpression;
	}
}
