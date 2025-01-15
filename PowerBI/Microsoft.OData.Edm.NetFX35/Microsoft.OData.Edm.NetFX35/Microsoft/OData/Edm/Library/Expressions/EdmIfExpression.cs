using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001CE RID: 462
	public class EdmIfExpression : EdmElement, IEdmIfExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x00019890 File Offset: 0x00017A90
		public EdmIfExpression(IEdmExpression testExpression, IEdmExpression trueExpression, IEdmExpression falseExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(testExpression, "testExpression");
			EdmUtil.CheckArgumentNull<IEdmExpression>(trueExpression, "trueExpression");
			EdmUtil.CheckArgumentNull<IEdmExpression>(falseExpression, "falseExpression");
			this.testExpression = testExpression;
			this.trueExpression = trueExpression;
			this.falseExpression = falseExpression;
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x000198DC File Offset: 0x00017ADC
		public IEdmExpression TestExpression
		{
			get
			{
				return this.testExpression;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x000198E4 File Offset: 0x00017AE4
		public IEdmExpression TrueExpression
		{
			get
			{
				return this.trueExpression;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x000198EC File Offset: 0x00017AEC
		public IEdmExpression FalseExpression
		{
			get
			{
				return this.falseExpression;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x000198F4 File Offset: 0x00017AF4
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x040004B7 RID: 1207
		private readonly IEdmExpression testExpression;

		// Token: 0x040004B8 RID: 1208
		private readonly IEdmExpression trueExpression;

		// Token: 0x040004B9 RID: 1209
		private readonly IEdmExpression falseExpression;
	}
}
