using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000EA RID: 234
	public class EdmCollectionExpression : EdmElement, IEdmCollectionExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x00011EB8 File Offset: 0x000100B8
		public EdmCollectionExpression(params IEdmExpression[] elements)
			: this(elements)
		{
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00011EC1 File Offset: 0x000100C1
		public EdmCollectionExpression(IEdmTypeReference declaredType, params IEdmExpression[] elements)
			: this(declaredType, elements)
		{
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00011ECB File Offset: 0x000100CB
		public EdmCollectionExpression(IEnumerable<IEdmExpression> elements)
			: this(null, elements)
		{
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00011ED5 File Offset: 0x000100D5
		public EdmCollectionExpression(IEdmTypeReference declaredType, IEnumerable<IEdmExpression> elements)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmExpression>>(elements, "elements");
			this.declaredType = declaredType;
			this.elements = elements;
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00011EF7 File Offset: 0x000100F7
		public IEdmTypeReference DeclaredType
		{
			get
			{
				return this.declaredType;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00011EFF File Offset: 0x000100FF
		public IEnumerable<IEdmExpression> Elements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00011F07 File Offset: 0x00010107
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Collection;
			}
		}

		// Token: 0x04000307 RID: 775
		private readonly IEdmTypeReference declaredType;

		// Token: 0x04000308 RID: 776
		private readonly IEnumerable<IEdmExpression> elements;
	}
}
