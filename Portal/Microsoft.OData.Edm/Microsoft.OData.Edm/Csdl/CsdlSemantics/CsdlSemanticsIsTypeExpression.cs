using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000179 RID: 377
	internal class CsdlSemanticsIsTypeExpression : CsdlSemanticsExpression, IEdmIsTypeExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x0001C640 File Offset: 0x0001A840
		public CsdlSemanticsIsTypeExpression(CsdlIsTypeExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0001C66E File Offset: 0x0001A86E
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00011FDE File Offset: 0x000101DE
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IsType;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0001C676 File Offset: 0x0001A876
		public IEdmExpression Operand
		{
			get
			{
				return this.operandCache.GetValue(this, CsdlSemanticsIsTypeExpression.ComputeOperandFunc, null);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0001C68A File Offset: 0x0001A88A
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsIsTypeExpression.ComputeTypeFunc, null);
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001C69E File Offset: 0x0001A89E
		private IEdmExpression ComputeOperand()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.Operand, this.bindingContext, base.Schema);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001C6BC File Offset: 0x0001A8BC
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type);
		}

		// Token: 0x0400062C RID: 1580
		private readonly CsdlIsTypeExpression expression;

		// Token: 0x0400062D RID: 1581
		private readonly IEdmEntityType bindingContext;

		// Token: 0x0400062E RID: 1582
		private readonly Cache<CsdlSemanticsIsTypeExpression, IEdmExpression> operandCache = new Cache<CsdlSemanticsIsTypeExpression, IEdmExpression>();

		// Token: 0x0400062F RID: 1583
		private static readonly Func<CsdlSemanticsIsTypeExpression, IEdmExpression> ComputeOperandFunc = (CsdlSemanticsIsTypeExpression me) => me.ComputeOperand();

		// Token: 0x04000630 RID: 1584
		private readonly Cache<CsdlSemanticsIsTypeExpression, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsIsTypeExpression, IEdmTypeReference>();

		// Token: 0x04000631 RID: 1585
		private static readonly Func<CsdlSemanticsIsTypeExpression, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsIsTypeExpression me) => me.ComputeType();
	}
}
