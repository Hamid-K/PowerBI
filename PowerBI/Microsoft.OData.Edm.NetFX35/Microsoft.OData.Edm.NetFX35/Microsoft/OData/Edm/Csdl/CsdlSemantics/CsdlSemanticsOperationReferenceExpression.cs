using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000083 RID: 131
	internal class CsdlSemanticsOperationReferenceExpression : CsdlSemanticsExpression, IEdmOperationReferenceExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00005C4D File Offset: 0x00003E4D
		public CsdlSemanticsOperationReferenceExpression(CsdlOperationReferenceExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00005C70 File Offset: 0x00003E70
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00005C78 File Offset: 0x00003E78
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.OperationReference;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00005C7C File Offset: 0x00003E7C
		public IEdmOperation ReferencedOperation
		{
			get
			{
				return this.referencedCache.GetValue(this, CsdlSemanticsOperationReferenceExpression.ComputeReferencedFunc, null);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00005C90 File Offset: 0x00003E90
		public IEnumerable<EdmError> Errors
		{
			get
			{
				if (this.ReferencedOperation is IUnresolvedElement)
				{
					return this.ReferencedOperation.Errors();
				}
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00005CB0 File Offset: 0x00003EB0
		private IEdmOperation ComputeReferenced()
		{
			return new UnresolvedOperation(this.expression.Operation, Strings.Bad_UnresolvedOperation(this.expression.Operation), base.Location);
		}

		// Token: 0x040000C7 RID: 199
		private readonly CsdlOperationReferenceExpression expression;

		// Token: 0x040000C8 RID: 200
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040000C9 RID: 201
		private readonly Cache<CsdlSemanticsOperationReferenceExpression, IEdmOperation> referencedCache = new Cache<CsdlSemanticsOperationReferenceExpression, IEdmOperation>();

		// Token: 0x040000CA RID: 202
		private static readonly Func<CsdlSemanticsOperationReferenceExpression, IEdmOperation> ComputeReferencedFunc = (CsdlSemanticsOperationReferenceExpression me) => me.ComputeReferenced();
	}
}
