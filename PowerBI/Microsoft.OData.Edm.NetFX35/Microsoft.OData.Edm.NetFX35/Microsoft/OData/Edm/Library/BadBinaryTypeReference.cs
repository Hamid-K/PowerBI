using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000125 RID: 293
	internal class BadBinaryTypeReference : EdmBinaryTypeReference, IEdmCheckable
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		public BadBinaryTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.Binary, errors), isNullable, false, default(int?))
		{
			this.errors = errors;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000E105 File Offset: 0x0000C305
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000E110 File Offset: 0x0000C310
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x0400022E RID: 558
		private readonly IEnumerable<EdmError> errors;
	}
}
