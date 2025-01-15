using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001D2 RID: 466
	public class EdmCollectionExpression : EdmElement, IEdmCollectionExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x000199CB File Offset: 0x00017BCB
		public EdmCollectionExpression(params IEdmExpression[] elements)
			: this((IEnumerable<IEdmExpression>)elements)
		{
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000199D9 File Offset: 0x00017BD9
		public EdmCollectionExpression(IEdmTypeReference declaredType, params IEdmExpression[] elements)
			: this(declaredType, (IEnumerable<IEdmExpression>)elements)
		{
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000199E8 File Offset: 0x00017BE8
		public EdmCollectionExpression(IEnumerable<IEdmExpression> elements)
			: this(null, elements)
		{
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000199F2 File Offset: 0x00017BF2
		public EdmCollectionExpression(IEdmTypeReference declaredType, IEnumerable<IEdmExpression> elements)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmExpression>>(elements, "elements");
			this.declaredType = declaredType;
			this.elements = elements;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00019A14 File Offset: 0x00017C14
		public IEdmTypeReference DeclaredType
		{
			get
			{
				return this.declaredType;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00019A1C File Offset: 0x00017C1C
		public IEnumerable<IEdmExpression> Elements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00019A24 File Offset: 0x00017C24
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Collection;
			}
		}

		// Token: 0x040004BF RID: 1215
		private readonly IEdmTypeReference declaredType;

		// Token: 0x040004C0 RID: 1216
		private readonly IEnumerable<IEdmExpression> elements;
	}
}
