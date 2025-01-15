using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F6 RID: 246
	public class EdmLabeledExpressionReferenceExpression : EdmElement, IEdmLabeledExpressionReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		public EdmLabeledExpressionReferenceExpression()
		{
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00013B3C File Offset: 0x00011D3C
		public EdmLabeledExpressionReferenceExpression(IEdmLabeledExpression referencedLabeledExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmLabeledExpression>(referencedLabeledExpression, "referencedLabeledExpression");
			this.referencedLabeledExpression = referencedLabeledExpression;
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00013B57 File Offset: 0x00011D57
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x00013B5F File Offset: 0x00011D5F
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

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00013B87 File Offset: 0x00011D87
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.LabeledExpressionReference;
			}
		}

		// Token: 0x0400041D RID: 1053
		private IEdmLabeledExpression referencedLabeledExpression;
	}
}
