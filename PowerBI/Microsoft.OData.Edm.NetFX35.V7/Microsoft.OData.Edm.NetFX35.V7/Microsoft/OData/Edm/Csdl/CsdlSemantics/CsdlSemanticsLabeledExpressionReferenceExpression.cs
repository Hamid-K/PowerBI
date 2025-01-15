using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016C RID: 364
	internal class CsdlSemanticsLabeledExpressionReferenceExpression : CsdlSemanticsExpression, IEdmLabeledExpressionReferenceExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0001A692 File Offset: 0x00018892
		public CsdlSemanticsLabeledExpressionReferenceExpression(CsdlLabeledExpressionReferenceExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0001A6B5 File Offset: 0x000188B5
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00013B87 File Offset: 0x00011D87
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.LabeledExpressionReference;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0001A6BD File Offset: 0x000188BD
		public IEdmLabeledExpression ReferencedLabeledExpression
		{
			get
			{
				return this.elementCache.GetValue(this, CsdlSemanticsLabeledExpressionReferenceExpression.ComputeElementFunc, null);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0001A6D1 File Offset: 0x000188D1
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

		// Token: 0x06000994 RID: 2452 RVA: 0x0001A6F4 File Offset: 0x000188F4
		private IEdmLabeledExpression ComputeElement()
		{
			IEdmLabeledExpression edmLabeledExpression = base.Schema.FindLabeledElement(this.expression.Label, this.bindingContext);
			if (edmLabeledExpression != null)
			{
				return edmLabeledExpression;
			}
			return new UnresolvedLabeledElement(this.expression.Label, base.Location);
		}

		// Token: 0x040005BD RID: 1469
		private readonly CsdlLabeledExpressionReferenceExpression expression;

		// Token: 0x040005BE RID: 1470
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040005BF RID: 1471
		private readonly Cache<CsdlSemanticsLabeledExpressionReferenceExpression, IEdmLabeledExpression> elementCache = new Cache<CsdlSemanticsLabeledExpressionReferenceExpression, IEdmLabeledExpression>();

		// Token: 0x040005C0 RID: 1472
		private static readonly Func<CsdlSemanticsLabeledExpressionReferenceExpression, IEdmLabeledExpression> ComputeElementFunc = (CsdlSemanticsLabeledExpressionReferenceExpression me) => me.ComputeElement();
	}
}
