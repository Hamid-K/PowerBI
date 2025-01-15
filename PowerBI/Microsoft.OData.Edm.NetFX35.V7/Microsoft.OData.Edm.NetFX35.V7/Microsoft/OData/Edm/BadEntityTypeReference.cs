using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000033 RID: 51
	internal class BadEntityTypeReference : EdmEntityTypeReference, IEdmCheckable
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00009250 File Offset: 0x00007450
		public BadEntityTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadEntityType(qualifiedName, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00009267 File Offset: 0x00007467
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009270 File Offset: 0x00007470
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000050 RID: 80
		private readonly IEnumerable<EdmError> errors;
	}
}
