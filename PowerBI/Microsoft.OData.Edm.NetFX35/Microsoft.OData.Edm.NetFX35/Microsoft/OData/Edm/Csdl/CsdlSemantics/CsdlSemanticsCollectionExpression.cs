using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000070 RID: 112
	internal class CsdlSemanticsCollectionExpression : CsdlSemanticsExpression, IEdmCollectionExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x000047CD File Offset: 0x000029CD
		public CsdlSemanticsCollectionExpression(CsdlCollectionExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000047FB File Offset: 0x000029FB
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00004803 File Offset: 0x00002A03
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Collection;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004807 File Offset: 0x00002A07
		public IEdmTypeReference DeclaredType
		{
			get
			{
				return this.declaredTypeCache.GetValue(this, CsdlSemanticsCollectionExpression.ComputeDeclaredTypeFunc, null);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000481B File Offset: 0x00002A1B
		public IEnumerable<IEdmExpression> Elements
		{
			get
			{
				return this.elementsCache.GetValue(this, CsdlSemanticsCollectionExpression.ComputeElementsFunc, null);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00004830 File Offset: 0x00002A30
		private IEnumerable<IEdmExpression> ComputeElements()
		{
			List<IEdmExpression> list = new List<IEdmExpression>();
			foreach (CsdlExpressionBase csdlExpressionBase in this.expression.ElementValues)
			{
				list.Add(CsdlSemanticsModel.WrapExpression(csdlExpressionBase, this.bindingContext, base.Schema));
			}
			return list;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000489C File Offset: 0x00002A9C
		private IEdmTypeReference ComputeDeclaredType()
		{
			if (this.expression.Type == null)
			{
				return null;
			}
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type);
		}

		// Token: 0x0400009A RID: 154
		private readonly CsdlCollectionExpression expression;

		// Token: 0x0400009B RID: 155
		private readonly IEdmEntityType bindingContext;

		// Token: 0x0400009C RID: 156
		private readonly Cache<CsdlSemanticsCollectionExpression, IEdmTypeReference> declaredTypeCache = new Cache<CsdlSemanticsCollectionExpression, IEdmTypeReference>();

		// Token: 0x0400009D RID: 157
		private static readonly Func<CsdlSemanticsCollectionExpression, IEdmTypeReference> ComputeDeclaredTypeFunc = (CsdlSemanticsCollectionExpression me) => me.ComputeDeclaredType();

		// Token: 0x0400009E RID: 158
		private readonly Cache<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>> elementsCache = new Cache<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>>();

		// Token: 0x0400009F RID: 159
		private static readonly Func<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>> ComputeElementsFunc = (CsdlSemanticsCollectionExpression me) => me.ComputeElements();
	}
}
