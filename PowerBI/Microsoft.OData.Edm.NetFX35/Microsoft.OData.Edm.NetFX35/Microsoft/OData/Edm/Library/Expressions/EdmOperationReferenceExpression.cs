using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001CF RID: 463
	public class EdmOperationReferenceExpression : EdmElement, IEdmOperationReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x000198F8 File Offset: 0x00017AF8
		public EdmOperationReferenceExpression(IEdmOperation referencedOperation)
		{
			EdmUtil.CheckArgumentNull<IEdmOperation>(referencedOperation, "referencedFunction");
			this.referencedOperation = referencedOperation;
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00019913 File Offset: 0x00017B13
		public IEdmOperation ReferencedOperation
		{
			get
			{
				return this.referencedOperation;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001991B File Offset: 0x00017B1B
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.OperationReference;
			}
		}

		// Token: 0x040004BA RID: 1210
		private readonly IEdmOperation referencedOperation;
	}
}
