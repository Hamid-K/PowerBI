using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003B RID: 59
	internal class BadSpatialTypeReference : EdmSpatialTypeReference, IEdmCheckable
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000958C File Offset: 0x0000778C
		public BadSpatialTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.None, errors), isNullable, default(int?))
		{
			this.errors = errors;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000095B8 File Offset: 0x000077B8
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000095C0 File Offset: 0x000077C0
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000064 RID: 100
		private readonly IEnumerable<EdmError> errors;
	}
}
