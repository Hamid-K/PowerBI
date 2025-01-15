using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200006E RID: 110
	internal class CsdlSemanticsCastExpression : CsdlSemanticsExpression, IEdmCastExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060001AC RID: 428 RVA: 0x000046D1 File Offset: 0x000028D1
		public CsdlSemanticsCastExpression(CsdlCastExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000046FF File Offset: 0x000028FF
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00004707 File Offset: 0x00002907
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Cast;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000470B File Offset: 0x0000290B
		public IEdmExpression Operand
		{
			get
			{
				return this.operandCache.GetValue(this, CsdlSemanticsCastExpression.ComputeOperandFunc, null);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000471F File Offset: 0x0000291F
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsCastExpression.ComputeTypeFunc, null);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00004733 File Offset: 0x00002933
		private IEdmExpression ComputeOperand()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.Operand, this.bindingContext, base.Schema);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00004751 File Offset: 0x00002951
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type);
		}

		// Token: 0x04000092 RID: 146
		private readonly CsdlCastExpression expression;

		// Token: 0x04000093 RID: 147
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000094 RID: 148
		private readonly Cache<CsdlSemanticsCastExpression, IEdmExpression> operandCache = new Cache<CsdlSemanticsCastExpression, IEdmExpression>();

		// Token: 0x04000095 RID: 149
		private static readonly Func<CsdlSemanticsCastExpression, IEdmExpression> ComputeOperandFunc = (CsdlSemanticsCastExpression me) => me.ComputeOperand();

		// Token: 0x04000096 RID: 150
		private readonly Cache<CsdlSemanticsCastExpression, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsCastExpression, IEdmTypeReference>();

		// Token: 0x04000097 RID: 151
		private static readonly Func<CsdlSemanticsCastExpression, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsCastExpression me) => me.ComputeType();
	}
}
