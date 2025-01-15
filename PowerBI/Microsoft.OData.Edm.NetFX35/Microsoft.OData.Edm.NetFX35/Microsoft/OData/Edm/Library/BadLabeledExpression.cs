using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200013B RID: 315
	internal class BadLabeledExpression : BadElement, IEdmLabeledExpression, IEdmNamedElement, IEdmExpression, IEdmElement
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public BadLabeledExpression(string name, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000E504 File Offset: 0x0000C704
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0000E50C File Offset: 0x0000C70C
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000E510 File Offset: 0x0000C710
		public IEdmExpression Expression
		{
			get
			{
				return this.expressionCache.GetValue(this, BadLabeledExpression.ComputeExpressionFunc, null);
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0000E524 File Offset: 0x0000C724
		private IEdmExpression ComputeExpression()
		{
			return EdmNullExpression.Instance;
		}

		// Token: 0x0400023E RID: 574
		private readonly string name;

		// Token: 0x0400023F RID: 575
		private readonly Cache<BadLabeledExpression, IEdmExpression> expressionCache = new Cache<BadLabeledExpression, IEdmExpression>();

		// Token: 0x04000240 RID: 576
		private static readonly Func<BadLabeledExpression, IEdmExpression> ComputeExpressionFunc = (BadLabeledExpression me) => me.ComputeExpression();
	}
}
