using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003E RID: 62
	internal class BadTemporalTypeReference : EdmTemporalTypeReference, IEdmCheckable
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x000096AC File Offset: 0x000078AC
		public BadTemporalTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.None, errors), isNullable, default(int?))
		{
			this.errors = errors;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000096D8 File Offset: 0x000078D8
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000096E0 File Offset: 0x000078E0
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000066 RID: 102
		private readonly IEnumerable<EdmError> errors;
	}
}
