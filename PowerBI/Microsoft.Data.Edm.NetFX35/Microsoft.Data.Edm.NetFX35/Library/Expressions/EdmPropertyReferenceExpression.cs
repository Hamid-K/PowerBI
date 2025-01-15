using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Expressions
{
	// Token: 0x02000199 RID: 409
	public class EdmPropertyReferenceExpression : EdmElement, IEdmPropertyReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x0001858C File Offset: 0x0001678C
		public EdmPropertyReferenceExpression(IEdmExpression baseExpression, IEdmProperty referencedProperty)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(baseExpression, "baseExpression");
			EdmUtil.CheckArgumentNull<IEdmProperty>(referencedProperty, "referencedPropert");
			this.baseExpression = baseExpression;
			this.referencedProperty = referencedProperty;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x000185BA File Offset: 0x000167BA
		public IEdmExpression Base
		{
			get
			{
				return this.baseExpression;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x000185C2 File Offset: 0x000167C2
		public IEdmProperty ReferencedProperty
		{
			get
			{
				return this.referencedProperty;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x000185CA File Offset: 0x000167CA
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyReference;
			}
		}

		// Token: 0x04000462 RID: 1122
		private readonly IEdmExpression baseExpression;

		// Token: 0x04000463 RID: 1123
		private readonly IEdmProperty referencedProperty;
	}
}
