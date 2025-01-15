using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200008D RID: 141
	internal class CsdlSemanticsIsTypeExpression : CsdlSemanticsExpression, IEdmIsTypeExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000610F File Offset: 0x0000430F
		public CsdlSemanticsIsTypeExpression(CsdlIsTypeExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000613D File Offset: 0x0000433D
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00006145 File Offset: 0x00004345
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IsType;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00006149 File Offset: 0x00004349
		public IEdmExpression Operand
		{
			get
			{
				return this.operandCache.GetValue(this, CsdlSemanticsIsTypeExpression.ComputeOperandFunc, null);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000615D File Offset: 0x0000435D
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsIsTypeExpression.ComputeTypeFunc, null);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006171 File Offset: 0x00004371
		private IEdmExpression ComputeOperand()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.Operand, this.bindingContext, base.Schema);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000618F File Offset: 0x0000438F
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type);
		}

		// Token: 0x040000E5 RID: 229
		private readonly CsdlIsTypeExpression expression;

		// Token: 0x040000E6 RID: 230
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040000E7 RID: 231
		private readonly Cache<CsdlSemanticsIsTypeExpression, IEdmExpression> operandCache = new Cache<CsdlSemanticsIsTypeExpression, IEdmExpression>();

		// Token: 0x040000E8 RID: 232
		private static readonly Func<CsdlSemanticsIsTypeExpression, IEdmExpression> ComputeOperandFunc = (CsdlSemanticsIsTypeExpression me) => me.ComputeOperand();

		// Token: 0x040000E9 RID: 233
		private readonly Cache<CsdlSemanticsIsTypeExpression, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsIsTypeExpression, IEdmTypeReference>();

		// Token: 0x040000EA RID: 234
		private static readonly Func<CsdlSemanticsIsTypeExpression, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsIsTypeExpression me) => me.ComputeType();
	}
}
