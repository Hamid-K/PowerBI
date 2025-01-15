using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001BB RID: 443
	public class EdmParameterReferenceExpression : EdmElement, IEdmParameterReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000958 RID: 2392 RVA: 0x000193E9 File Offset: 0x000175E9
		public EdmParameterReferenceExpression(IEdmOperationParameter referencedParameter)
		{
			EdmUtil.CheckArgumentNull<IEdmOperationParameter>(referencedParameter, "referencedParameter");
			this.referencedParameter = referencedParameter;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00019404 File Offset: 0x00017604
		public IEdmOperationParameter ReferencedParameter
		{
			get
			{
				return this.referencedParameter;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001940C File Offset: 0x0001760C
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.ParameterReference;
			}
		}

		// Token: 0x04000498 RID: 1176
		private readonly IEdmOperationParameter referencedParameter;
	}
}
