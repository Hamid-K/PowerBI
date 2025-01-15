using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000028 RID: 40
	internal class BadBinaryTypeReference : EdmBinaryTypeReference, IEdmCheckable
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00008FE4 File Offset: 0x000071E4
		public BadBinaryTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.Binary, errors), isNullable, false, default(int?))
		{
			this.errors = errors;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00009011 File Offset: 0x00007211
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000901C File Offset: 0x0000721C
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000046 RID: 70
		private readonly IEnumerable<EdmError> errors;
	}
}
