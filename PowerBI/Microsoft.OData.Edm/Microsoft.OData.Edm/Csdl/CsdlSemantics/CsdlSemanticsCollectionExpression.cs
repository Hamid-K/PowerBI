using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016E RID: 366
	internal class CsdlSemanticsCollectionExpression : CsdlSemanticsExpression, IEdmCollectionExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x0001BAE0 File Offset: 0x00019CE0
		public CsdlSemanticsCollectionExpression(CsdlCollectionExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0001BB0E File Offset: 0x00019D0E
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00011F07 File Offset: 0x00010107
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Collection;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0001BB16 File Offset: 0x00019D16
		public IEdmTypeReference DeclaredType
		{
			get
			{
				return this.declaredTypeCache.GetValue(this, CsdlSemanticsCollectionExpression.ComputeDeclaredTypeFunc, null);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0001BB2A File Offset: 0x00019D2A
		public IEnumerable<IEdmExpression> Elements
		{
			get
			{
				return this.elementsCache.GetValue(this, CsdlSemanticsCollectionExpression.ComputeElementsFunc, null);
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001BB40 File Offset: 0x00019D40
		private IEnumerable<IEdmExpression> ComputeElements()
		{
			List<IEdmExpression> list = new List<IEdmExpression>();
			foreach (CsdlExpressionBase csdlExpressionBase in this.expression.ElementValues)
			{
				list.Add(CsdlSemanticsModel.WrapExpression(csdlExpressionBase, this.bindingContext, base.Schema));
			}
			return list;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001BBAC File Offset: 0x00019DAC
		private IEdmTypeReference ComputeDeclaredType()
		{
			if (this.expression.Type == null)
			{
				return null;
			}
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type);
		}

		// Token: 0x040005FD RID: 1533
		private readonly CsdlCollectionExpression expression;

		// Token: 0x040005FE RID: 1534
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040005FF RID: 1535
		private readonly Cache<CsdlSemanticsCollectionExpression, IEdmTypeReference> declaredTypeCache = new Cache<CsdlSemanticsCollectionExpression, IEdmTypeReference>();

		// Token: 0x04000600 RID: 1536
		private static readonly Func<CsdlSemanticsCollectionExpression, IEdmTypeReference> ComputeDeclaredTypeFunc = (CsdlSemanticsCollectionExpression me) => me.ComputeDeclaredType();

		// Token: 0x04000601 RID: 1537
		private readonly Cache<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>> elementsCache = new Cache<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>>();

		// Token: 0x04000602 RID: 1538
		private static readonly Func<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>> ComputeElementsFunc = (CsdlSemanticsCollectionExpression me) => me.ComputeElements();
	}
}
