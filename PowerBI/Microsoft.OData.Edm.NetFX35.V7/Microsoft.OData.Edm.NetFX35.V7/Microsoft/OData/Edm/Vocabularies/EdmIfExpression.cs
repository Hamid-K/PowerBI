using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F3 RID: 243
	public class EdmIfExpression : EdmElement, IEdmIfExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x00013A54 File Offset: 0x00011C54
		public EdmIfExpression(IEdmExpression testExpression, IEdmExpression trueExpression, IEdmExpression falseExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(testExpression, "testExpression");
			EdmUtil.CheckArgumentNull<IEdmExpression>(trueExpression, "trueExpression");
			EdmUtil.CheckArgumentNull<IEdmExpression>(falseExpression, "falseExpression");
			this.testExpression = testExpression;
			this.trueExpression = trueExpression;
			this.falseExpression = falseExpression;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00013AA0 File Offset: 0x00011CA0
		public IEdmExpression TestExpression
		{
			get
			{
				return this.testExpression;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00013AA8 File Offset: 0x00011CA8
		public IEdmExpression TrueExpression
		{
			get
			{
				return this.trueExpression;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00013AB0 File Offset: 0x00011CB0
		public IEdmExpression FalseExpression
		{
			get
			{
				return this.falseExpression;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x00013AB8 File Offset: 0x00011CB8
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x04000416 RID: 1046
		private readonly IEdmExpression testExpression;

		// Token: 0x04000417 RID: 1047
		private readonly IEdmExpression trueExpression;

		// Token: 0x04000418 RID: 1048
		private readonly IEdmExpression falseExpression;
	}
}
