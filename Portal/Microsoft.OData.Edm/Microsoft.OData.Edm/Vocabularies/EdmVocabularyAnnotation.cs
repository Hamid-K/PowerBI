using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000D7 RID: 215
	public class EdmVocabularyAnnotation : EdmElement, IEdmVocabularyAnnotation, IEdmElement
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x0000F7D7 File Offset: 0x0000D9D7
		public EdmVocabularyAnnotation(IEdmVocabularyAnnotatable target, IEdmTerm term, IEdmExpression value)
			: this(target, term, null, value)
		{
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
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

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0000F839 File Offset: 0x0000DA39
		public IEdmVocabularyAnnotatable Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0000F841 File Offset: 0x0000DA41
		public IEdmTerm Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0000F849 File Offset: 0x0000DA49
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0000F851 File Offset: 0x0000DA51
		public IEdmExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040002D7 RID: 727
		private readonly IEdmVocabularyAnnotatable target;

		// Token: 0x040002D8 RID: 728
		private readonly IEdmTerm term;

		// Token: 0x040002D9 RID: 729
		private readonly string qualifier;

		// Token: 0x040002DA RID: 730
		private readonly IEdmExpression value;
	}
}
