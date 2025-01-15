using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000061 RID: 97
	internal class BadPrimitiveTypeReference : EdmPrimitiveTypeReference, IEdmCheckable
	{
		// Token: 0x060001FF RID: 511 RVA: 0x000051AD File Offset: 0x000033AD
		public BadPrimitiveTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.None, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000051C5 File Offset: 0x000033C5
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000051D0 File Offset: 0x000033D0
		public override string ToString()
		{
			EdmError edmError = this.Errors.FirstOrDefault<EdmError>();
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040000B9 RID: 185
		private readonly IEnumerable<EdmError> errors;
	}
}
