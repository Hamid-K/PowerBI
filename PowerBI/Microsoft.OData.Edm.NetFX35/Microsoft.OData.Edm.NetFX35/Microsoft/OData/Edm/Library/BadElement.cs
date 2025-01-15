using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000043 RID: 67
	internal class BadElement : IEdmCheckable, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00003A0E File Offset: 0x00001C0E
		public BadElement(IEnumerable<EdmError> errors)
		{
			this.errors = errors;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003A1D File Offset: 0x00001C1D
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x04000061 RID: 97
		private readonly IEnumerable<EdmError> errors;
	}
}
