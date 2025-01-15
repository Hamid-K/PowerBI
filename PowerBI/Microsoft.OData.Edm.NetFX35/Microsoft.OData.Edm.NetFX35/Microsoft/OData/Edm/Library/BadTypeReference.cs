using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200014C RID: 332
	internal class BadTypeReference : EdmTypeReference, IEdmCheckable
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x0000EA8A File Offset: 0x0000CC8A
		public BadTypeReference(BadType definition, bool isNullable)
			: base(definition, isNullable)
		{
			this.errors = definition.Errors;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000259 RID: 601
		private readonly IEnumerable<EdmError> errors;
	}
}
