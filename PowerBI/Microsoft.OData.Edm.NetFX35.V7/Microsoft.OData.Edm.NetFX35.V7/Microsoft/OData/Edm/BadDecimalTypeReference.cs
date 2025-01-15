using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200002C RID: 44
	internal class BadDecimalTypeReference : EdmDecimalTypeReference, IEdmCheckable
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000910C File Offset: 0x0000730C
		public BadDecimalTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.Decimal, errors), isNullable, default(int?), default(int?))
		{
			this.errors = errors;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00009141 File Offset: 0x00007341
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000914C File Offset: 0x0000734C
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000049 RID: 73
		private readonly IEnumerable<EdmError> errors;
	}
}
