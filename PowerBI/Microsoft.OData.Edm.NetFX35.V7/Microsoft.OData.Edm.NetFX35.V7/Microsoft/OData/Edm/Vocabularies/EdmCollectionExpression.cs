using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F1 RID: 241
	public class EdmCollectionExpression : EdmElement, IEdmCollectionExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x000139D4 File Offset: 0x00011BD4
		public EdmCollectionExpression(params IEdmExpression[] elements)
			: this(elements)
		{
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000139DD File Offset: 0x00011BDD
		public EdmCollectionExpression(IEdmTypeReference declaredType, params IEdmExpression[] elements)
			: this(declaredType, elements)
		{
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000139E7 File Offset: 0x00011BE7
		public EdmCollectionExpression(IEnumerable<IEdmExpression> elements)
			: this(null, elements)
		{
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000139F1 File Offset: 0x00011BF1
		public EdmCollectionExpression(IEdmTypeReference declaredType, IEnumerable<IEdmExpression> elements)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmExpression>>(elements, "elements");
			this.declaredType = declaredType;
			this.elements = elements;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00013A13 File Offset: 0x00011C13
		public IEdmTypeReference DeclaredType
		{
			get
			{
				return this.declaredType;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00013A1B File Offset: 0x00011C1B
		public IEnumerable<IEdmExpression> Elements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00013A23 File Offset: 0x00011C23
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Collection;
			}
		}

		// Token: 0x04000413 RID: 1043
		private readonly IEdmTypeReference declaredType;

		// Token: 0x04000414 RID: 1044
		private readonly IEnumerable<IEdmExpression> elements;
	}
}
