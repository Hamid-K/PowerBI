using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001B8 RID: 440
	public class EdmEnumMemberReferenceExpression : EdmElement, IEdmEnumMemberReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x0001934E File Offset: 0x0001754E
		public EdmEnumMemberReferenceExpression(IEdmEnumMember referencedEnumMember)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember>(referencedEnumMember, "referencedEnumMember");
			this.referencedEnumMember = referencedEnumMember;
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00019369 File Offset: 0x00017569
		public IEdmEnumMember ReferencedEnumMember
		{
			get
			{
				return this.referencedEnumMember;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00019371 File Offset: 0x00017571
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMemberReference;
			}
		}

		// Token: 0x04000495 RID: 1173
		private readonly IEdmEnumMember referencedEnumMember;
	}
}
