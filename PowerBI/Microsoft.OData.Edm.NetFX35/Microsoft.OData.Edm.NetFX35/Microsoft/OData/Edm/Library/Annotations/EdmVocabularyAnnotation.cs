using System;
using Microsoft.OData.Edm.Annotations;

namespace Microsoft.OData.Edm.Library.Annotations
{
	// Token: 0x020001B4 RID: 436
	public abstract class EdmVocabularyAnnotation : EdmElement, IEdmVocabularyAnnotation, IEdmElement
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x000191A5 File Offset: 0x000173A5
		protected EdmVocabularyAnnotation(IEdmVocabularyAnnotatable target, IEdmValueTerm term, string qualifier)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			this.target = target;
			this.term = term;
			this.qualifier = qualifier;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x000191DA File Offset: 0x000173DA
		public IEdmVocabularyAnnotatable Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000191E2 File Offset: 0x000173E2
		public IEdmTerm Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x000191EA File Offset: 0x000173EA
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x0400048A RID: 1162
		private readonly IEdmVocabularyAnnotatable target;

		// Token: 0x0400048B RID: 1163
		private readonly IEdmValueTerm term;

		// Token: 0x0400048C RID: 1164
		private readonly string qualifier;
	}
}
