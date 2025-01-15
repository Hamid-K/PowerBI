using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000B5 RID: 181
	internal class CsdlSemanticsPropertyReferenceExpression : CsdlSemanticsExpression, IEdmPropertyReferenceExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600030D RID: 781 RVA: 0x0000712A File Offset: 0x0000532A
		public CsdlSemanticsPropertyReferenceExpression(CsdlPropertyReferenceExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00007158 File Offset: 0x00005358
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00007160 File Offset: 0x00005360
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyReference;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00007164 File Offset: 0x00005364
		public IEdmExpression Base
		{
			get
			{
				return this.baseCache.GetValue(this, CsdlSemanticsPropertyReferenceExpression.ComputeBaseFunc, null);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00007178 File Offset: 0x00005378
		public IEdmProperty ReferencedProperty
		{
			get
			{
				return this.elementCache.GetValue(this, CsdlSemanticsPropertyReferenceExpression.ComputeReferencedFunc, null);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000718C File Offset: 0x0000538C
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

		// Token: 0x06000313 RID: 787 RVA: 0x000071AC File Offset: 0x000053AC
		private IEdmExpression ComputeBase()
		{
			if (this.expression.BaseExpression == null)
			{
				return null;
			}
			return CsdlSemanticsModel.WrapExpression(this.expression.BaseExpression, this.bindingContext, base.Schema);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000071D9 File Offset: 0x000053D9
		private IEdmProperty ComputeReferenced()
		{
			return new UnresolvedProperty(this.bindingContext ?? new BadEntityType("", new EdmError[0]), this.expression.Property, base.Location);
		}

		// Token: 0x04000143 RID: 323
		private readonly CsdlPropertyReferenceExpression expression;

		// Token: 0x04000144 RID: 324
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000145 RID: 325
		private readonly Cache<CsdlSemanticsPropertyReferenceExpression, IEdmExpression> baseCache = new Cache<CsdlSemanticsPropertyReferenceExpression, IEdmExpression>();

		// Token: 0x04000146 RID: 326
		private static readonly Func<CsdlSemanticsPropertyReferenceExpression, IEdmExpression> ComputeBaseFunc = (CsdlSemanticsPropertyReferenceExpression me) => me.ComputeBase();

		// Token: 0x04000147 RID: 327
		private readonly Cache<CsdlSemanticsPropertyReferenceExpression, IEdmProperty> elementCache = new Cache<CsdlSemanticsPropertyReferenceExpression, IEdmProperty>();

		// Token: 0x04000148 RID: 328
		private static readonly Func<CsdlSemanticsPropertyReferenceExpression, IEdmProperty> ComputeReferencedFunc = (CsdlSemanticsPropertyReferenceExpression me) => me.ComputeReferenced();
	}
}
