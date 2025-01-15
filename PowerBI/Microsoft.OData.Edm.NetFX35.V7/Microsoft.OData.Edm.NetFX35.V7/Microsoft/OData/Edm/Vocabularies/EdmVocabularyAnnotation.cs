using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E4 RID: 228
	public class EdmVocabularyAnnotation : EdmElement, IEdmVocabularyAnnotation, IEdmElement
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x00011E56 File Offset: 0x00010056
		public EdmVocabularyAnnotation(IEdmVocabularyAnnotatable target, IEdmTerm term, IEdmExpression value)
			: this(target, term, null, value)
		{
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00011E64 File Offset: 0x00010064
		public EdmVocabularyAnnotation(IEdmVocabularyAnnotatable target, IEdmTerm term, string qualifier, IEdmExpression value)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<IEdmExpression>(value, "value");
			this.target = target;
			this.term = term;
			this.qualifier = qualifier;
			this.value = value;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00011EB9 File Offset: 0x000100B9
		public IEdmVocabularyAnnotatable Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00011EC1 File Offset: 0x000100C1
		public IEdmTerm Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00011EC9 File Offset: 0x000100C9
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00011ED1 File Offset: 0x000100D1
		public IEdmExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040003F9 RID: 1017
		private readonly IEdmVocabularyAnnotatable target;

		// Token: 0x040003FA RID: 1018
		private readonly IEdmTerm term;

		// Token: 0x040003FB RID: 1019
		private readonly string qualifier;

		// Token: 0x040003FC RID: 1020
		private readonly IEdmExpression value;
	}
}
