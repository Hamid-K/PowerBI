using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x0200010E RID: 270
	internal class BadTypeReference : EdmTypeReference, IEdmCheckable
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0000CC72 File Offset: 0x0000AE72
		public BadTypeReference(BadType definition, bool isNullable)
			: base(definition, isNullable)
		{
			this.errors = definition.Errors;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000CC88 File Offset: 0x0000AE88
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000CC90 File Offset: 0x0000AE90
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040001E7 RID: 487
		private readonly IEnumerable<EdmError> errors;
	}
}
