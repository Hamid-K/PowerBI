using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200002E RID: 46
	internal class BadElement : IEdmElement, IEdmCheckable, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000247 RID: 583 RVA: 0x000091A7 File Offset: 0x000073A7
		public BadElement(IEnumerable<EdmError> errors)
		{
			this.errors = errors;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000091B6 File Offset: 0x000073B6
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0400004A RID: 74
		private readonly IEnumerable<EdmError> errors;
	}
}
