using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001D1 RID: 465
	public class EdmLabeledExpression : EdmElement, IEdmLabeledExpression, IEdmNamedElement, IEdmExpression, IEdmElement
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x00019989 File Offset: 0x00017B89
		public EdmLabeledExpression(string name, IEdmExpression expression)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			this.name = name;
			this.expression = expression;
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x000199B7 File Offset: 0x00017BB7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x000199BF File Offset: 0x00017BBF
		public IEdmExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x000199C7 File Offset: 0x00017BC7
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x040004BD RID: 1213
		private readonly string name;

		// Token: 0x040004BE RID: 1214
		private readonly IEdmExpression expression;
	}
}
