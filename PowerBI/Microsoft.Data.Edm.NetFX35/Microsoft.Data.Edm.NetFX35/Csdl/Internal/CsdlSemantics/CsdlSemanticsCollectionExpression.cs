using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000042 RID: 66
	internal class CsdlSemanticsCollectionExpression : CsdlSemanticsExpression, IEdmCollectionExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00003681 File Offset: 0x00001881
		public CsdlSemanticsCollectionExpression(CsdlCollectionExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000036AF File Offset: 0x000018AF
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000036B7 File Offset: 0x000018B7
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Collection;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000036BB File Offset: 0x000018BB
		public IEdmTypeReference DeclaredType
		{
			get
			{
				return this.declaredTypeCache.GetValue(this, CsdlSemanticsCollectionExpression.ComputeDeclaredTypeFunc, null);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000036CF File Offset: 0x000018CF
		public IEnumerable<IEdmExpression> Elements
		{
			get
			{
				return this.elementsCache.GetValue(this, CsdlSemanticsCollectionExpression.ComputeElementsFunc, null);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000036E4 File Offset: 0x000018E4
		private IEnumerable<IEdmExpression> ComputeElements()
		{
			List<IEdmExpression> list = new List<IEdmExpression>();
			foreach (CsdlExpressionBase csdlExpressionBase in this.expression.ElementValues)
			{
				list.Add(CsdlSemanticsModel.WrapExpression(csdlExpressionBase, this.bindingContext, base.Schema));
			}
			return list;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003750 File Offset: 0x00001950
		private IEdmTypeReference ComputeDeclaredType()
		{
			if (this.expression.Type == null)
			{
				return null;
			}
			return CsdlSemanticsModel.WrapTypeReference(base.Schema, this.expression.Type);
		}

		// Token: 0x04000056 RID: 86
		private readonly CsdlCollectionExpression expression;

		// Token: 0x04000057 RID: 87
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000058 RID: 88
		private readonly Cache<CsdlSemanticsCollectionExpression, IEdmTypeReference> declaredTypeCache = new Cache<CsdlSemanticsCollectionExpression, IEdmTypeReference>();

		// Token: 0x04000059 RID: 89
		private static readonly Func<CsdlSemanticsCollectionExpression, IEdmTypeReference> ComputeDeclaredTypeFunc = (CsdlSemanticsCollectionExpression me) => me.ComputeDeclaredType();

		// Token: 0x0400005A RID: 90
		private readonly Cache<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>> elementsCache = new Cache<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>>();

		// Token: 0x0400005B RID: 91
		private static readonly Func<CsdlSemanticsCollectionExpression, IEnumerable<IEdmExpression>> ComputeElementsFunc = (CsdlSemanticsCollectionExpression me) => me.ComputeElements();
	}
}
