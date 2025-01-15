using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000039 RID: 57
	internal class BadPrimitiveTypeReference : EdmPrimitiveTypeReference, IEdmCheckable
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000947E File Offset: 0x0000767E
		public BadPrimitiveTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.None, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00009496 File Offset: 0x00007696
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000094A0 File Offset: 0x000076A0
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x0400005F RID: 95
		private readonly IEnumerable<EdmError> errors;
	}
}
