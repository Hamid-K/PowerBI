using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200015E RID: 350
	internal class CsdlSemanticsCastExpression : CsdlSemanticsExpression, IEdmCastExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x0600091A RID: 2330 RVA: 0x00019920 File Offset: 0x00017B20
		public CsdlSemanticsCastExpression(CsdlCastExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001994E File Offset: 0x00017B4E
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x000139D0 File Offset: 0x00011BD0
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Cast;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00019956 File Offset: 0x00017B56
		public IEdmExpression Operand
		{
			get
			{
				return this.operandCache.GetValue(this, CsdlSemanticsCastExpression.ComputeOperandFunc, null);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0001996A File Offset: 0x00017B6A
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsCastExpression.ComputeTypeFunc, null);
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001997E File Offset: 0x00017B7E
		private IEdmExpression ComputeOperand()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.Operand, this.bindingContext, base.Schema);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001999C File Offset: 0x00017B9C
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type);
		}

		// Token: 0x0400057C RID: 1404
		private readonly CsdlCastExpression expression;

		// Token: 0x0400057D RID: 1405
		private readonly IEdmEntityType bindingContext;

		// Token: 0x0400057E RID: 1406
		private readonly Cache<CsdlSemanticsCastExpression, IEdmExpression> operandCache = new Cache<CsdlSemanticsCastExpression, IEdmExpression>();

		// Token: 0x0400057F RID: 1407
		private static readonly Func<CsdlSemanticsCastExpression, IEdmExpression> ComputeOperandFunc = (CsdlSemanticsCastExpression me) => me.ComputeOperand();

		// Token: 0x04000580 RID: 1408
		private readonly Cache<CsdlSemanticsCastExpression, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsCastExpression, IEdmTypeReference>();

		// Token: 0x04000581 RID: 1409
		private static readonly Func<CsdlSemanticsCastExpression, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsCastExpression me) => me.ComputeType();
	}
}
