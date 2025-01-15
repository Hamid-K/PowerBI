using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000131 RID: 305
	internal class BadDecimalTypeReference : EdmDecimalTypeReference, IEdmCheckable
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		public BadDecimalTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.Decimal, errors), isNullable, default(int?), default(int?))
		{
			this.errors = errors;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000E2E1 File Offset: 0x0000C4E1
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000235 RID: 565
		private readonly IEnumerable<EdmError> errors;
	}
}
