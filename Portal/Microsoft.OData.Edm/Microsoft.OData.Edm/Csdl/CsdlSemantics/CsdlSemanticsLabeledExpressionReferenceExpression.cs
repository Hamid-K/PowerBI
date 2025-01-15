using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017B RID: 379
	internal class CsdlSemanticsLabeledExpressionReferenceExpression : CsdlSemanticsExpression, IEdmLabeledExpressionReferenceExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000A4A RID: 2634 RVA: 0x0001C79A File Offset: 0x0001A99A
		public CsdlSemanticsLabeledExpressionReferenceExpression(CsdlLabeledExpressionReferenceExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0001C7BD File Offset: 0x0001A9BD
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0001206B File Offset: 0x0001026B
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.LabeledExpressionReference;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0001C7C5 File Offset: 0x0001A9C5
		public IEdmLabeledExpression ReferencedLabeledExpression
		{
			get
			{
				return this.elementCache.GetValue(this, CsdlSemanticsLabeledExpressionReferenceExpression.ComputeElementFunc, null);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0001C7D9 File Offset: 0x0001A9D9
		public IEnumerable<EdmError> Errors
		{
			get
			{
				if (this.ReferencedLabeledExpression is IUnresolvedElement)
				{
					return this.ReferencedLabeledExpression.Errors();
				}
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001C7FC File Offset: 0x0001A9FC
		private IEdmLabeledExpression ComputeElement()
		{
			IEdmLabeledExpression edmLabeledExpression = base.Schema.FindLabeledElement(this.expression.Label, this.bindingContext);
			if (edmLabeledExpression != null)
			{
				return edmLabeledExpression;
			}
			return new UnresolvedLabeledElement(this.expression.Label, base.Location);
		}

		// Token: 0x04000638 RID: 1592
		private readonly CsdlLabeledExpressionReferenceExpression expression;

		// Token: 0x04000639 RID: 1593
		private readonly IEdmEntityType bindingContext;

		// Token: 0x0400063A RID: 1594
		private readonly Cache<CsdlSemanticsLabeledExpressionReferenceExpression, IEdmLabeledExpression> elementCache = new Cache<CsdlSemanticsLabeledExpressionReferenceExpression, IEdmLabeledExpression>();

		// Token: 0x0400063B RID: 1595
		private static readonly Func<CsdlSemanticsLabeledExpressionReferenceExpression, IEdmLabeledExpression> ComputeElementFunc = (CsdlSemanticsLabeledExpressionReferenceExpression me) => me.ComputeElement();
	}
}
