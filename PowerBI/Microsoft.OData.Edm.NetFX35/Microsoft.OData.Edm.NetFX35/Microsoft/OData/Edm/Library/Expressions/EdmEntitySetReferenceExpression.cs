using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x02000066 RID: 102
	public class EdmEntitySetReferenceExpression : EdmElement, IEdmEntitySetReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000429A File Offset: 0x0000249A
		public EdmEntitySetReferenceExpression(IEdmEntitySet referencedEntitySet)
		{
			EdmUtil.CheckArgumentNull<IEdmEntitySet>(referencedEntitySet, "referencedEntitySet");
			this.referencedEntitySet = referencedEntitySet;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000042B5 File Offset: 0x000024B5
		public IEdmEntitySet ReferencedEntitySet
		{
			get
			{
				return this.referencedEntitySet;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000042BD File Offset: 0x000024BD
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EntitySetReference;
			}
		}

		// Token: 0x04000086 RID: 134
		private readonly IEdmEntitySet referencedEntitySet;
	}
}
