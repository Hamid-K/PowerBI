using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200014B RID: 331
	internal class BadTemporalTypeReference : EdmTemporalTypeReference, IEdmCheckable
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x0000EA0C File Offset: 0x0000CC0C
		public BadTemporalTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.None, errors), isNullable, default(int?))
		{
			this.errors = errors;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0000EA38 File Offset: 0x0000CC38
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000EA40 File Offset: 0x0000CC40
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000258 RID: 600
		private readonly IEnumerable<EdmError> errors;
	}
}
