using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200007C RID: 124
	internal class CsdlSemanticsEntitySetReferenceExpression : CsdlSemanticsExpression, IEdmEntitySetReferenceExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x000057AF File Offset: 0x000039AF
		public CsdlSemanticsEntitySetReferenceExpression(CsdlEntitySetReferenceExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000057D2 File Offset: 0x000039D2
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000057DA File Offset: 0x000039DA
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EntitySetReference;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000057DE File Offset: 0x000039DE
		public IEdmEntitySet ReferencedEntitySet
		{
			get
			{
				return this.referencedCache.GetValue(this, CsdlSemanticsEntitySetReferenceExpression.ComputeReferencedFunc, null);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000057F2 File Offset: 0x000039F2
		public IEnumerable<EdmError> Errors
		{
			get
			{
				if (this.ReferencedEntitySet is IUnresolvedElement)
				{
					return this.ReferencedEntitySet.Errors();
				}
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005814 File Offset: 0x00003A14
		private IEdmEntitySet ComputeReferenced()
		{
			string[] array = this.expression.EntitySetPath.Split(new char[] { '/' });
			return new UnresolvedEntitySet(array[1], new UnresolvedEntityContainer(array[0], base.Location), base.Location);
		}

		// Token: 0x040000B4 RID: 180
		private readonly CsdlEntitySetReferenceExpression expression;

		// Token: 0x040000B5 RID: 181
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040000B6 RID: 182
		private readonly Cache<CsdlSemanticsEntitySetReferenceExpression, IEdmEntitySet> referencedCache = new Cache<CsdlSemanticsEntitySetReferenceExpression, IEdmEntitySet>();

		// Token: 0x040000B7 RID: 183
		private static readonly Func<CsdlSemanticsEntitySetReferenceExpression, IEdmEntitySet> ComputeReferencedFunc = (CsdlSemanticsEntitySetReferenceExpression me) => me.ComputeReferenced();
	}
}
