using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000B0 RID: 176
	internal class CsdlSemanticsParameterReferenceExpression : CsdlSemanticsExpression, IEdmParameterReferenceExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x00006FAE File Offset: 0x000051AE
		public CsdlSemanticsParameterReferenceExpression(CsdlParameterReferenceExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00006FD1 File Offset: 0x000051D1
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00006FD9 File Offset: 0x000051D9
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.ParameterReference;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00006FDD File Offset: 0x000051DD
		public IEdmOperationParameter ReferencedParameter
		{
			get
			{
				return this.referencedCache.GetValue(this, CsdlSemanticsParameterReferenceExpression.ComputeReferencedFunc, null);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00006FF1 File Offset: 0x000051F1
		public IEnumerable<EdmError> Errors
		{
			get
			{
				if (this.ReferencedParameter is IUnresolvedElement)
				{
					return this.ReferencedParameter.Errors();
				}
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00007011 File Offset: 0x00005211
		private IEdmOperationParameter ComputeReferenced()
		{
			return new UnresolvedParameter(new UnresolvedOperation(string.Empty, Strings.Bad_UnresolvedOperation(string.Empty), base.Location), this.expression.Parameter, base.Location);
		}

		// Token: 0x04000139 RID: 313
		private readonly CsdlParameterReferenceExpression expression;

		// Token: 0x0400013A RID: 314
		private readonly IEdmEntityType bindingContext;

		// Token: 0x0400013B RID: 315
		private readonly Cache<CsdlSemanticsParameterReferenceExpression, IEdmOperationParameter> referencedCache = new Cache<CsdlSemanticsParameterReferenceExpression, IEdmOperationParameter>();

		// Token: 0x0400013C RID: 316
		private static readonly Func<CsdlSemanticsParameterReferenceExpression, IEdmOperationParameter> ComputeReferencedFunc = (CsdlSemanticsParameterReferenceExpression me) => me.ComputeReferenced();
	}
}
