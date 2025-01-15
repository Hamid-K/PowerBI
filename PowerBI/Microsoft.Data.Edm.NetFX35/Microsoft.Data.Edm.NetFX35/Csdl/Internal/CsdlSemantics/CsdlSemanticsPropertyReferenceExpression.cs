using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000089 RID: 137
	internal class CsdlSemanticsPropertyReferenceExpression : CsdlSemanticsExpression, IEdmPropertyReferenceExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00005D67 File Offset: 0x00003F67
		public CsdlSemanticsPropertyReferenceExpression(CsdlPropertyReferenceExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00005D95 File Offset: 0x00003F95
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00005D9D File Offset: 0x00003F9D
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyReference;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00005DA1 File Offset: 0x00003FA1
		public IEdmExpression Base
		{
			get
			{
				return this.baseCache.GetValue(this, CsdlSemanticsPropertyReferenceExpression.ComputeBaseFunc, null);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00005DB5 File Offset: 0x00003FB5
		public IEdmProperty ReferencedProperty
		{
			get
			{
				return this.elementCache.GetValue(this, CsdlSemanticsPropertyReferenceExpression.ComputeReferencedFunc, null);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00005DC9 File Offset: 0x00003FC9
		public IEnumerable<EdmError> Errors
		{
			get
			{
				if (this.ReferencedProperty is IUnresolvedElement)
				{
					return this.ReferencedProperty.Errors();
				}
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00005DE9 File Offset: 0x00003FE9
		private IEdmExpression ComputeBase()
		{
			if (this.expression.BaseExpression == null)
			{
				return null;
			}
			return CsdlSemanticsModel.WrapExpression(this.expression.BaseExpression, this.bindingContext, base.Schema);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00005E16 File Offset: 0x00004016
		private IEdmProperty ComputeReferenced()
		{
			return new UnresolvedProperty(this.bindingContext ?? new BadEntityType("", new EdmError[0]), this.expression.Property, base.Location);
		}

		// Token: 0x040000FB RID: 251
		private readonly CsdlPropertyReferenceExpression expression;

		// Token: 0x040000FC RID: 252
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040000FD RID: 253
		private readonly Cache<CsdlSemanticsPropertyReferenceExpression, IEdmExpression> baseCache = new Cache<CsdlSemanticsPropertyReferenceExpression, IEdmExpression>();

		// Token: 0x040000FE RID: 254
		private static readonly Func<CsdlSemanticsPropertyReferenceExpression, IEdmExpression> ComputeBaseFunc = (CsdlSemanticsPropertyReferenceExpression me) => me.ComputeBase();

		// Token: 0x040000FF RID: 255
		private readonly Cache<CsdlSemanticsPropertyReferenceExpression, IEdmProperty> elementCache = new Cache<CsdlSemanticsPropertyReferenceExpression, IEdmProperty>();

		// Token: 0x04000100 RID: 256
		private static readonly Func<CsdlSemanticsPropertyReferenceExpression, IEdmProperty> ComputeReferencedFunc = (CsdlSemanticsPropertyReferenceExpression me) => me.ComputeReferenced();
	}
}
