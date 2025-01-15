using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005C RID: 92
	internal class BadEntityTypeReference : EdmEntityTypeReference, IEdmCheckable
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00004FFE File Offset: 0x000031FE
		public BadEntityTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadEntityType(qualifiedName, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00005015 File Offset: 0x00003215
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005020 File Offset: 0x00003220
		public override string ToString()
		{
			EdmError edmError = this.Errors.FirstOrDefault<EdmError>();
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040000AB RID: 171
		private readonly IEnumerable<EdmError> errors;
	}
}
