using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000057 RID: 87
	internal class BadElement : IEdmElement, IEdmCheckable, IEdmVocabularyAnnotatable
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00004F4A File Offset: 0x0000314A
		public BadElement(IEnumerable<EdmError> errors)
		{
			this.errors = errors;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004F59 File Offset: 0x00003159
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x040000A4 RID: 164
		private readonly IEnumerable<EdmError> errors;
	}
}
