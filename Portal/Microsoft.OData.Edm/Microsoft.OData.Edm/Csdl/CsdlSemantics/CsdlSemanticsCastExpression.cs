using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016D RID: 365
	internal class CsdlSemanticsCastExpression : CsdlSemanticsExpression, IEdmCastExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x0001BA20 File Offset: 0x00019C20
		public CsdlSemanticsCastExpression(CsdlCastExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001BA4E File Offset: 0x00019C4E
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00011EB4 File Offset: 0x000100B4
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Cast;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0001BA56 File Offset: 0x00019C56
		public IEdmExpression Operand
		{
			get
			{
				return this.operandCache.GetValue(this, CsdlSemanticsCastExpression.ComputeOperandFunc, null);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0001BA6A File Offset: 0x00019C6A
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsCastExpression.ComputeTypeFunc, null);
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001BA7E File Offset: 0x00019C7E
		private IEdmExpression ComputeOperand()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.Operand, this.bindingContext, base.Schema);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001BA9C File Offset: 0x00019C9C
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type);
		}

		// Token: 0x040005F7 RID: 1527
		private readonly CsdlCastExpression expression;

		// Token: 0x040005F8 RID: 1528
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040005F9 RID: 1529
		private readonly Cache<CsdlSemanticsCastExpression, IEdmExpression> operandCache = new Cache<CsdlSemanticsCastExpression, IEdmExpression>();

		// Token: 0x040005FA RID: 1530
		private static readonly Func<CsdlSemanticsCastExpression, IEdmExpression> ComputeOperandFunc = (CsdlSemanticsCastExpression me) => me.ComputeOperand();

		// Token: 0x040005FB RID: 1531
		private readonly Cache<CsdlSemanticsCastExpression, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsCastExpression, IEdmTypeReference>();

		// Token: 0x040005FC RID: 1532
		private static readonly Func<CsdlSemanticsCastExpression, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsCastExpression me) => me.ComputeType();
	}
}
