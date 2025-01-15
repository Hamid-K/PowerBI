using System;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000058 RID: 88
	internal class CsdlSemanticsIfExpression : CsdlSemanticsExpression, IEdmIfExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00004A15 File Offset: 0x00002C15
		public CsdlSemanticsIfExpression(CsdlIfExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004A4E File Offset: 0x00002C4E
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004A56 File Offset: 0x00002C56
		public IEdmEntityType BindingContext
		{
			get
			{
				return this.bindingContext;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004A5E File Offset: 0x00002C5E
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004A62 File Offset: 0x00002C62
		public IEdmExpression TestExpression
		{
			get
			{
				return this.testCache.GetValue(this, CsdlSemanticsIfExpression.ComputeTestFunc, null);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00004A76 File Offset: 0x00002C76
		public IEdmExpression TrueExpression
		{
			get
			{
				return this.ifTrueCache.GetValue(this, CsdlSemanticsIfExpression.ComputeIfTrueFunc, null);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00004A8A File Offset: 0x00002C8A
		public IEdmExpression FalseExpression
		{
			get
			{
				return this.ifFalseCache.GetValue(this, CsdlSemanticsIfExpression.ComputeIfFalseFunc, null);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004A9E File Offset: 0x00002C9E
		private IEdmExpression ComputeTest()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.Test, this.BindingContext, base.Schema);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004ABC File Offset: 0x00002CBC
		private IEdmExpression ComputeIfTrue()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.IfTrue, this.BindingContext, base.Schema);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004ADA File Offset: 0x00002CDA
		private IEdmExpression ComputeIfFalse()
		{
			return CsdlSemanticsModel.WrapExpression(this.expression.IfFalse, this.BindingContext, base.Schema);
		}

		// Token: 0x04000086 RID: 134
		private readonly CsdlIfExpression expression;

		// Token: 0x04000087 RID: 135
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000088 RID: 136
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> testCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x04000089 RID: 137
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeTestFunc = (CsdlSemanticsIfExpression me) => me.ComputeTest();

		// Token: 0x0400008A RID: 138
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> ifTrueCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x0400008B RID: 139
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeIfTrueFunc = (CsdlSemanticsIfExpression me) => me.ComputeIfTrue();

		// Token: 0x0400008C RID: 140
		private readonly Cache<CsdlSemanticsIfExpression, IEdmExpression> ifFalseCache = new Cache<CsdlSemanticsIfExpression, IEdmExpression>();

		// Token: 0x0400008D RID: 141
		private static readonly Func<CsdlSemanticsIfExpression, IEdmExpression> ComputeIfFalseFunc = (CsdlSemanticsIfExpression me) => me.ComputeIfFalse();
	}
}
