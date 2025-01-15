using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200013E RID: 318
	internal class BadPrimitiveTypeReference : EdmPrimitiveTypeReference, IEdmCheckable
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		public BadPrimitiveTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.None, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000245 RID: 581
		private readonly IEnumerable<EdmError> errors;
	}
}
