using System;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Annotations
{
	// Token: 0x020001B5 RID: 437
	public class EdmAnnotation : EdmVocabularyAnnotation, IEdmValueAnnotation, IEdmVocabularyAnnotation, IEdmElement
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x000191F2 File Offset: 0x000173F2
		public EdmAnnotation(IEdmVocabularyAnnotatable target, IEdmValueTerm term, IEdmExpression value)
			: this(target, term, null, value)
		{
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000191FE File Offset: 0x000173FE
		public EdmAnnotation(IEdmVocabularyAnnotatable target, IEdmValueTerm term, string qualifier, IEdmExpression value)
			: base(target, term, qualifier)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(value, "value");
			this.value = value;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001921E File Offset: 0x0001741E
		public IEdmExpression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400048D RID: 1165
		private readonly IEdmExpression value;
	}
}
