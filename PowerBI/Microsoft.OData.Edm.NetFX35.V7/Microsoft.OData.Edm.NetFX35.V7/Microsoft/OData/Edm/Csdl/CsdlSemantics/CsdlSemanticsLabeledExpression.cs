using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016B RID: 363
	internal class CsdlSemanticsLabeledExpression : CsdlSemanticsElement, IEdmLabeledExpression, IEdmNamedElement, IEdmElement, IEdmExpression
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x0001A5F8 File Offset: 0x000187F8
		public CsdlSemanticsLabeledExpression(string name, CsdlExpressionBase element, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(element)
		{
			this.name = name;
			this.sourceElement = element;
			this.bindingContext = bindingContext;
			this.schema = schema;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0001A629 File Offset: 0x00018829
		public override CsdlElement Element
		{
			get
			{
				return this.sourceElement;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001A631 File Offset: 0x00018831
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001A63E File Offset: 0x0001883E
		public IEdmEntityType BindingContext
		{
			get
			{
				return this.bindingContext;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001A646 File Offset: 0x00018846
		public IEdmExpression Expression
		{
			get
			{
				return this.expressionCache.GetValue(this, CsdlSemanticsLabeledExpression.ComputeExpressionFunc, null);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00008DED File Offset: 0x00006FED
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0001A65A File Offset: 0x0001885A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001A662 File Offset: 0x00018862
		private IEdmExpression ComputeExpression()
		{
			return CsdlSemanticsModel.WrapExpression(this.sourceElement, this.BindingContext, this.schema);
		}

		// Token: 0x040005B7 RID: 1463
		private readonly string name;

		// Token: 0x040005B8 RID: 1464
		private readonly CsdlExpressionBase sourceElement;

		// Token: 0x040005B9 RID: 1465
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x040005BA RID: 1466
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040005BB RID: 1467
		private readonly Cache<CsdlSemanticsLabeledExpression, IEdmExpression> expressionCache = new Cache<CsdlSemanticsLabeledExpression, IEdmExpression>();

		// Token: 0x040005BC RID: 1468
		private static readonly Func<CsdlSemanticsLabeledExpression, IEdmExpression> ComputeExpressionFunc = (CsdlSemanticsLabeledExpression me) => me.ComputeExpression();
	}
}
