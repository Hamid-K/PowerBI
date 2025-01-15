using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000EC RID: 236
	public class EdmIfExpression : EdmElement, IEdmIfExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x00011F38 File Offset: 0x00010138
		public EdmIfExpression(IEdmExpression testExpression, IEdmExpression trueExpression, IEdmExpression falseExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(testExpression, "testExpression");
			EdmUtil.CheckArgumentNull<IEdmExpression>(trueExpression, "trueExpression");
			EdmUtil.CheckArgumentNull<IEdmExpression>(falseExpression, "falseExpression");
			this.testExpression = testExpression;
			this.trueExpression = trueExpression;
			this.falseExpression = falseExpression;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00011F84 File Offset: 0x00010184
		public IEdmExpression TestExpression
		{
			get
			{
				return this.testExpression;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00011F8C File Offset: 0x0001018C
		public IEdmExpression TrueExpression
		{
			get
			{
				return this.trueExpression;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00011F94 File Offset: 0x00010194
		public IEdmExpression FalseExpression
		{
			get
			{
				return this.falseExpression;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00011F9C File Offset: 0x0001019C
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x0400030A RID: 778
		private readonly IEdmExpression testExpression;

		// Token: 0x0400030B RID: 779
		private readonly IEdmExpression trueExpression;

		// Token: 0x0400030C RID: 780
		private readonly IEdmExpression falseExpression;
	}
}
