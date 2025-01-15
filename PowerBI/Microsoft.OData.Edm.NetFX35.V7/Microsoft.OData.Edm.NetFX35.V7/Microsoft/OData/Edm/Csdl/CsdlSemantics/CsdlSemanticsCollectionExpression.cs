using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200015F RID: 351
	internal class CsdlSemanticsCollectionExpression : CsdlSemanticsExpression, IEdmCollectionExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000922 RID: 2338 RVA: 0x000199E0 File Offset: 0x00017BE0
		public CsdlSemanticsCollectionExpression(CsdlCollectionExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00019A0E File Offset: 0x00017C0E
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00013A23 File Offset: 0x00011C23
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Collection;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00019A16 File Offset: 0x00017C16
		public IEdmTypeReference DeclaredType
		{
			get
			{
				return this.declaredTypeCache.GetValue(this, CsdlSemanticsCollectionExpression.ComputeDeclaredTypeFunc, null);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00019A2A File Offset: 0x00017C2A
		public IEnumerable<IEdmExpression> Elements
		{
			get
			{
				return this.elementsCache.GetValue(this, CsdlSemanticsCollectionExpression.ComputeElementsFunc, null);
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00019A40 File Offset: 0x00017C40
		private IEnumerable<IEdmExpression> ComputeElements()
		{
			List<IEdmExpression> list = new List<IEdmExpression>();
			foreach (CsdlExpressionBase csdlExpressionBase in this.expression.ElementValues)
			{
				list.Add(CsdlSemanticsModel.WrapExpression(csdlExpressionBase, this.bindingContext, base.Schema));
			}
			return list;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00019AAC File Offset: 0x00017CAC
		private IEdmTypeReference ComputeDeclaredType()
		{
			if (this.expression.Type == null)
			{
				return null;
			}
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type);
		}

		// Token: 0x04000582 RID: 1410
		private readonly CsdlCollectionExpression expression;

		// Token: 0x04000583 RID: 1411
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000584 RID: 1412
		private readonly Cache<CsdlSemanticsCollectionExpression, IEdmTypeReference> declaredTypeCache = new Cache<CsdlSemanticsCollectionExpression, IEdmTypeReference>();

		// Token: 0x04000585 RID: 1413
		private static readonly Func<CsdlSemanticsCollectionExpression, IEdmTypeReference> ComputeDeclaredTypeFunc = (CsdlSemanticsCollectionExpression me) => me.ComputeDeclaredType();

		// Token: 0x04000586 RID: 1414
		private readonly Cache<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>> elementsCache = new Cache<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>>();

		// Token: 0x04000587 RID: 1415
		private static readonly Func<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>> ComputeElementsFunc = (CsdlSemanticsCollectionExpression me) => me.ComputeElements();
	}
}
