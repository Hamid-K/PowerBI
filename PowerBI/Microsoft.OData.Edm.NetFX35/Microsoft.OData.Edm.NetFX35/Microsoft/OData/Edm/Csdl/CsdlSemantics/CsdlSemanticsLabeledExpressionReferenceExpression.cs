using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000091 RID: 145
	internal class CsdlSemanticsLabeledExpressionReferenceExpression : CsdlSemanticsExpression, IEdmLabeledExpressionReferenceExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600026A RID: 618 RVA: 0x000062BC File Offset: 0x000044BC
		public CsdlSemanticsLabeledExpressionReferenceExpression(CsdlLabeledExpressionReferenceExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000062DF File Offset: 0x000044DF
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000062E7 File Offset: 0x000044E7
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.LabeledExpressionReference;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000062EB File Offset: 0x000044EB
		public IEdmLabeledExpression ReferencedLabeledExpression
		{
			get
			{
				return this.elementCache.GetValue(this, CsdlSemanticsLabeledExpressionReferenceExpression.ComputeElementFunc, null);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000062FF File Offset: 0x000044FF
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

		// Token: 0x0600026F RID: 623 RVA: 0x00006320 File Offset: 0x00004520
		private IEdmLabeledExpression ComputeElement()
		{
			IEdmLabeledExpression edmLabeledExpression = base.Schema.FindLabeledElement(this.expression.Label, this.bindingContext);
			if (edmLabeledExpression != null)
			{
				return edmLabeledExpression;
			}
			return new UnresolvedLabeledElement(this.expression.Label, base.Location);
		}

		// Token: 0x040000F4 RID: 244
		private readonly CsdlLabeledExpressionReferenceExpression expression;

		// Token: 0x040000F5 RID: 245
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040000F6 RID: 246
		private readonly Cache<CsdlSemanticsLabeledExpressionReferenceExpression, IEdmLabeledExpression> elementCache = new Cache<CsdlSemanticsLabeledExpressionReferenceExpression, IEdmLabeledExpression>();

		// Token: 0x040000F7 RID: 247
		private static readonly Func<CsdlSemanticsLabeledExpressionReferenceExpression, IEdmLabeledExpression> ComputeElementFunc = (CsdlSemanticsLabeledExpressionReferenceExpression me) => me.ComputeElement();
	}
}
