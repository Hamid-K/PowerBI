using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016A RID: 362
	internal class CsdlSemanticsIsTypeExpression : CsdlSemanticsExpression, IEdmIsTypeExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x0001A538 File Offset: 0x00018738
		public CsdlSemanticsIsTypeExpression(CsdlIsTypeExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0001A566 File Offset: 0x00018766
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00013AFA File Offset: 0x00011CFA
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IsType;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001A56E File Offset: 0x0001876E
		public IEdmExpression Operand
		{
			get
			{
				return this.operandCache.GetValue(this, CsdlSemanticsIsTypeExpression.ComputeOperandFunc, null);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0001A582 File Offset: 0x00018782
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsIsTypeExpression.ComputeTypeFunc, null);
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001A596 File Offset: 0x00018796
		private IEdmExpression ComputeOperand()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.Operand, this.bindingContext, base.Schema);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001A5B4 File Offset: 0x000187B4
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type);
		}

		// Token: 0x040005B1 RID: 1457
		private readonly CsdlIsTypeExpression expression;

		// Token: 0x040005B2 RID: 1458
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040005B3 RID: 1459
		private readonly Cache<CsdlSemanticsIsTypeExpression, IEdmExpression> operandCache = new Cache<CsdlSemanticsIsTypeExpression, IEdmExpression>();

		// Token: 0x040005B4 RID: 1460
		private static readonly Func<CsdlSemanticsIsTypeExpression, IEdmExpression> ComputeOperandFunc = (CsdlSemanticsIsTypeExpression me) => me.ComputeOperand();

		// Token: 0x040005B5 RID: 1461
		private readonly Cache<CsdlSemanticsIsTypeExpression, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsIsTypeExpression, IEdmTypeReference>();

		// Token: 0x040005B6 RID: 1462
		private static readonly Func<CsdlSemanticsIsTypeExpression, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsIsTypeExpression me) => me.ComputeType();
	}
}
