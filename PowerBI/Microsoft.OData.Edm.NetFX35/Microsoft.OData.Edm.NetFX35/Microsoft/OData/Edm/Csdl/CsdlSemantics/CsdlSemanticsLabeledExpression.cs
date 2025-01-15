using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200008F RID: 143
	internal class CsdlSemanticsLabeledExpression : CsdlSemanticsElement, IEdmLabeledExpression, IEdmNamedElement, IEdmExpression, IEdmElement
	{
		// Token: 0x0600025F RID: 607 RVA: 0x00006209 File Offset: 0x00004409
		public CsdlSemanticsLabeledExpression(string name, CsdlExpressionBase element, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(element)
		{
			this.name = name;
			this.sourceElement = element;
			this.bindingContext = bindingContext;
			this.schema = schema;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000623A File Offset: 0x0000443A
		public override CsdlElement Element
		{
			get
			{
				return this.sourceElement;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00006242 File Offset: 0x00004442
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000624F File Offset: 0x0000444F
		public IEdmEntityType BindingContext
		{
			get
			{
				return this.bindingContext;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00006257 File Offset: 0x00004457
		public IEdmExpression Expression
		{
			get
			{
				return this.expressionCache.GetValue(this, CsdlSemanticsLabeledExpression.ComputeExpressionFunc, null);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000626B File Offset: 0x0000446B
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000626F File Offset: 0x0000446F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00006277 File Offset: 0x00004477
		private IEdmExpression ComputeExpression()
		{
			return CsdlSemanticsModel.WrapExpression(this.sourceElement, this.BindingContext, this.schema);
		}

		// Token: 0x040000ED RID: 237
		private readonly string name;

		// Token: 0x040000EE RID: 238
		private readonly CsdlExpressionBase sourceElement;

		// Token: 0x040000EF RID: 239
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x040000F0 RID: 240
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040000F1 RID: 241
		private readonly Cache<CsdlSemanticsLabeledExpression, IEdmExpression> expressionCache = new Cache<CsdlSemanticsLabeledExpression, IEdmExpression>();

		// Token: 0x040000F2 RID: 242
		private static readonly Func<CsdlSemanticsLabeledExpression, IEdmExpression> ComputeExpressionFunc = (CsdlSemanticsLabeledExpression me) => me.ComputeExpression();
	}
}
