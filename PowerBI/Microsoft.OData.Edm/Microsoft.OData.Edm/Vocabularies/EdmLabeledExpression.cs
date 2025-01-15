using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000EE RID: 238
	public class EdmLabeledExpression : EdmElement, IEdmLabeledExpression, IEdmNamedElement, IEdmElement, IEdmExpression
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x00011FE2 File Offset: 0x000101E2
		public EdmLabeledExpression(string name, IEdmExpression expression)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			this.name = name;
			this.expression = expression;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00012010 File Offset: 0x00010210
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00012018 File Offset: 0x00010218
		public IEdmExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00004C41 File Offset: 0x00002E41
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x0400030F RID: 783
		private readonly string name;

		// Token: 0x04000310 RID: 784
		private readonly IEdmExpression expression;
	}
}
