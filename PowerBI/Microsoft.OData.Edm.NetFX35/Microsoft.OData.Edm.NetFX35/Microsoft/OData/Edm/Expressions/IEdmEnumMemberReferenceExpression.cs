using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x0200016D RID: 365
	public interface IEdmEnumMemberReferenceExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000701 RID: 1793
		IEdmEnumMember ReferencedEnumMember { get; }
	}
}
