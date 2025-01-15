using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200008B RID: 139
	internal class CsdlSemanticsIfExpression : CsdlSemanticsExpression, IEdmIfExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00005FA1 File Offset: 0x000041A1
		public CsdlSemanticsIfExpression(CsdlIfExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00005FDA File Offset: 0x000041DA
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00005FE2 File Offset: 0x000041E2
		public IEdmEntityType BindingContext
		{
			get
			{
				return this.bindingContext;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00005FEA File Offset: 0x000041EA
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00005FEE File Offset: 0x000041EE
		public IEdmExpression TestExpression
		{
			get
			{
				return this.testCache.GetValue(this, CsdlSemanticsIfExpression.ComputeTestFunc, null);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00006002 File Offset: 0x00004202
		public IEdmExpression TrueExpression
		{
			get
			{
				return this.ifTrueCache.GetValue(this, CsdlSemanticsIfExpression.ComputeIfTrueFunc, null);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00006016 File Offset: 0x00004216
		public IEdmExpression FalseExpression
		{
			get
			{
				return this.ifFalseCache.GetValue(this, CsdlSemanticsIfExpression.ComputeIfFalseFunc, null);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000602A File Offset: 0x0000422A
		private IEdmExpression ComputeTest()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.Test, this.BindingContext, base.Schema);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00006048 File Offset: 0x00004248
		private IEdmExpression ComputeIfTrue()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.IfTrue, this.BindingContext, base.Schema);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00006066 File Offset: 0x00004266
		private IEdmExpression ComputeIfFalse()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.IfFalse, this.BindingContext, base.Schema);
		}

		// Token: 0x040000DA RID: 218
		private readonly CsdlIfExpression expression;

		// Token: 0x040000DB RID: 219
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040000DC RID: 220
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> testCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x040000DD RID: 221
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeTestFunc = (CsdlSemanticsIfExpression me) => me.ComputeTest();

		// Token: 0x040000DE RID: 222
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> ifTrueCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x040000DF RID: 223
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeIfTrueFunc = (CsdlSemanticsIfExpression me) => me.ComputeIfTrue();

		// Token: 0x040000E0 RID: 224
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> ifFalseCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x040000E1 RID: 225
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeIfFalseFunc = (CsdlSemanticsIfExpression me) => me.ComputeIfFalse();
	}
}
