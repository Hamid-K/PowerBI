using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017A RID: 378
	internal class CsdlSemanticsLabeledExpression : CsdlSemanticsElement, IEdmLabeledExpression, IEdmNamedElement, IEdmElement, IEdmExpression
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x0001C700 File Offset: 0x0001A900
		public CsdlSemanticsLabeledExpression(string name, CsdlExpressionBase element, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(element)
		{
			this.name = name;
			this.sourceElement = element;
			this.bindingContext = bindingContext;
			this.schema = schema;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0001C731 File Offset: 0x0001A931
		public override CsdlElement Element
		{
			get
			{
				return this.sourceElement;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0001C739 File Offset: 0x0001A939
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0001C746 File Offset: 0x0001A946
		public IEdmEntityType BindingContext
		{
			get
			{
				return this.bindingContext;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001C74E File Offset: 0x0001A94E
		public IEdmExpression Expression
		{
			get
			{
				return this.expressionCache.GetValue(this, CsdlSemanticsLabeledExpression.ComputeExpressionFunc, null);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00004C41 File Offset: 0x00002E41
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0001C762 File Offset: 0x0001A962
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001C76A File Offset: 0x0001A96A
		private IEdmExpression ComputeExpression()
		{
			return CsdlSemanticsModel.WrapExpression(this.sourceElement, this.BindingContext, this.schema);
		}

		// Token: 0x04000632 RID: 1586
		private readonly string name;

		// Token: 0x04000633 RID: 1587
		private readonly CsdlExpressionBase sourceElement;

		// Token: 0x04000634 RID: 1588
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000635 RID: 1589
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000636 RID: 1590
		private readonly Cache<CsdlSemanticsLabeledExpression, IEdmExpression> expressionCache = new Cache<CsdlSemanticsLabeledExpression, IEdmExpression>();

		// Token: 0x04000637 RID: 1591
		private static readonly Func<CsdlSemanticsLabeledExpression, IEdmExpression> ComputeExpressionFunc = (CsdlSemanticsLabeledExpression me) => me.ComputeExpression();
	}
}
