using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006A RID: 106
	internal class BadTypeReference : EdmTypeReference, IEdmCheckable
	{
		// Token: 0x0600022B RID: 555 RVA: 0x0000554E File Offset: 0x0000374E
		public BadTypeReference(BadType definition, bool isNullable)
			: base(definition, isNullable)
		{
			this.errors = definition.Errors;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00005564 File Offset: 0x00003764
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000556C File Offset: 0x0000376C
		public override string ToString()
		{
			EdmError edmError = this.Errors.FirstOrDefault<EdmError>();
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040000C5 RID: 197
		private readonly IEnumerable<EdmError> errors;
	}
}
