using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000169 RID: 361
	internal class CsdlSemanticsIfExpression : CsdlSemanticsExpression, IEdmIfExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0001A40C File Offset: 0x0001860C
		public CsdlSemanticsIfExpression(CsdlIfExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0001A445 File Offset: 0x00018645
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0001A44D File Offset: 0x0001864D
		public IEdmEntityType BindingContext
		{
			get
			{
				return this.bindingContext;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00013AB8 File Offset: 0x00011CB8
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0001A455 File Offset: 0x00018655
		public IEdmExpression TestExpression
		{
			get
			{
				return this.testCache.GetValue(this, CsdlSemanticsIfExpression.ComputeTestFunc, null);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0001A469 File Offset: 0x00018669
		public IEdmExpression TrueExpression
		{
			get
			{
				return this.ifTrueCache.GetValue(this, CsdlSemanticsIfExpression.ComputeIfTrueFunc, null);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001A47D File Offset: 0x0001867D
		public IEdmExpression FalseExpression
		{
			get
			{
				return this.ifFalseCache.GetValue(this, CsdlSemanticsIfExpression.ComputeIfFalseFunc, null);
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001A491 File Offset: 0x00018691
		private IEdmExpression ComputeTest()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.Test, this.BindingContext, base.Schema);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001A4AF File Offset: 0x000186AF
		private IEdmExpression ComputeIfTrue()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.IfTrue, this.BindingContext, base.Schema);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001A4CD File Offset: 0x000186CD
		private IEdmExpression ComputeIfFalse()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.IfFalse, this.BindingContext, base.Schema);
		}

		// Token: 0x040005A9 RID: 1449
		private readonly CsdlIfExpression expression;

		// Token: 0x040005AA RID: 1450
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040005AB RID: 1451
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> testCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x040005AC RID: 1452
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeTestFunc = (CsdlSemanticsIfExpression me) => me.ComputeTest();

		// Token: 0x040005AD RID: 1453
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> ifTrueCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x040005AE RID: 1454
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeIfTrueFunc = (CsdlSemanticsIfExpression me) => me.ComputeIfTrue();

		// Token: 0x040005AF RID: 1455
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> ifFalseCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x040005B0 RID: 1456
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeIfFalseFunc = (CsdlSemanticsIfExpression me) => me.ComputeIfFalse();
	}
}
