using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200012E RID: 302
	internal class BadComplexTypeReference : EdmComplexTypeReference, IEdmCheckable
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x0000E1F1 File Offset: 0x0000C3F1
		public BadComplexTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadComplexType(qualifiedName, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000E208 File Offset: 0x0000C408
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000E210 File Offset: 0x0000C410
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000232 RID: 562
		private readonly IEnumerable<EdmError> errors;
	}
}
