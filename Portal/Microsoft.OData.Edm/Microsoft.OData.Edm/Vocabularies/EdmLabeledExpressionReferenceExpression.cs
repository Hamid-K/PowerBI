using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000EF RID: 239
	public class EdmLabeledExpressionReferenceExpression : EdmElement, IEdmLabeledExpressionReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x0000BE2B File Offset: 0x0000A02B
		public EdmLabeledExpressionReferenceExpression()
		{
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00012020 File Offset: 0x00010220
		public EdmLabeledExpressionReferenceExpression(IEdmLabeledExpression referencedLabeledExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmLabeledExpression>(referencedLabeledExpression, "referencedLabeledExpression");
			this.referencedLabeledExpression = referencedLabeledExpression;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001203B File Offset: 0x0001023B
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x00012043 File Offset: 0x00010243
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

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001206B File Offset: 0x0001026B
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.LabeledExpressionReference;
			}
		}

		// Token: 0x04000311 RID: 785
		private IEdmLabeledExpression referencedLabeledExpression;
	}
}
