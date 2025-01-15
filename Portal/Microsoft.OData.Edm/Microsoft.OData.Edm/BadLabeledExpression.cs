using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005E RID: 94
	internal class BadLabeledExpression : BadElement, IEdmLabeledExpression, IEdmNamedElement, IEdmElement, IEdmExpression
	{
		// Token: 0x060001ED RID: 493 RVA: 0x000050BB File Offset: 0x000032BB
		public BadLabeledExpression(string name, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000050DF File Offset: 0x000032DF
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00004C41 File Offset: 0x00002E41
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000050E7 File Offset: 0x000032E7
		public IEdmExpression Expression
		{
			get
			{
				return this.expressionCache.GetValue(this, BadLabeledExpression.ComputeExpressionFunc, null);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00004C45 File Offset: 0x00002E45
		private static IEdmExpression ComputeExpression()
		{
			return EdmNullExpression.Instance;
		}

		// Token: 0x040000AF RID: 175
		private readonly string name;

		// Token: 0x040000B0 RID: 176
		private readonly Cache<BadLabeledExpression, IEdmExpression> expressionCache = new Cache<BadLabeledExpression, IEdmExpression>();

		// Token: 0x040000B1 RID: 177
		private static readonly Func<BadLabeledExpression, IEdmExpression> ComputeExpressionFunc = (BadLabeledExpression me) => BadLabeledExpression.ComputeExpression();
	}
}
