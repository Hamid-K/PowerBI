using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200002B RID: 43
	internal class BadComplexTypeReference : EdmComplexTypeReference, IEdmCheckable
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000909A File Offset: 0x0000729A
		public BadComplexTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadComplexType(qualifiedName, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000090B1 File Offset: 0x000072B1
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000090BC File Offset: 0x000072BC
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000048 RID: 72
		private readonly IEnumerable<EdmError> errors;
	}
}
