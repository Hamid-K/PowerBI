using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000178 RID: 376
	internal class CsdlSemanticsIfExpression : CsdlSemanticsExpression, IEdmIfExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0001C514 File Offset: 0x0001A714
		public CsdlSemanticsIfExpression(CsdlIfExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0001C54D File Offset: 0x0001A74D
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0001C555 File Offset: 0x0001A755
		public IEdmEntityType BindingContext
		{
			get
			{
				return this.bindingContext;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00011F9C File Offset: 0x0001019C
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0001C55D File Offset: 0x0001A75D
		public IEdmExpression TestExpression
		{
			get
			{
				return this.testCache.GetValue(this, CsdlSemanticsIfExpression.ComputeTestFunc, null);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0001C571 File Offset: 0x0001A771
		public IEdmExpression TrueExpression
		{
			get
			{
				return this.ifTrueCache.GetValue(this, CsdlSemanticsIfExpression.ComputeIfTrueFunc, null);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0001C585 File Offset: 0x0001A785
		public IEdmExpression FalseExpression
		{
			get
			{
				return this.ifFalseCache.GetValue(this, CsdlSemanticsIfExpression.ComputeIfFalseFunc, null);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001C599 File Offset: 0x0001A799
		private IEdmExpression ComputeTest()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.Test, this.BindingContext, base.Schema);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001C5B7 File Offset: 0x0001A7B7
		private IEdmExpression ComputeIfTrue()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.IfTrue, this.BindingContext, base.Schema);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001C5D5 File Offset: 0x0001A7D5
		private IEdmExpression ComputeIfFalse()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.IfFalse, this.BindingContext, base.Schema);
		}

		// Token: 0x04000624 RID: 1572
		private readonly CsdlIfExpression expression;

		// Token: 0x04000625 RID: 1573
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000626 RID: 1574
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> testCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x04000627 RID: 1575
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeTestFunc = (CsdlSemanticsIfExpression me) => me.ComputeTest();

		// Token: 0x04000628 RID: 1576
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> ifTrueCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x04000629 RID: 1577
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeIfTrueFunc = (CsdlSemanticsIfExpression me) => me.ComputeIfTrue();

		// Token: 0x0400062A RID: 1578
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> ifFalseCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x0400062B RID: 1579
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeIfFalseFunc = (CsdlSemanticsIfExpression me) => me.ComputeIfFalse();
	}
}
