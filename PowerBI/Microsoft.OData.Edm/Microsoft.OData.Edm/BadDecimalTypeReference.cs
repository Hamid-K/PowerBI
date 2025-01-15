using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000056 RID: 86
	internal class BadDecimalTypeReference : EdmDecimalTypeReference, IEdmCheckable
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00004EBC File Offset: 0x000030BC
		public BadDecimalTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.Decimal, errors), isNullable, null, null)
		{
			this.errors = errors;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00004EF1 File Offset: 0x000030F1
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004EFC File Offset: 0x000030FC
		public override string ToString()
		{
			EdmError edmError = this.Errors.FirstOrDefault<EdmError>();
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040000A3 RID: 163
		private readonly IEnumerable<EdmError> errors;
	}
}
