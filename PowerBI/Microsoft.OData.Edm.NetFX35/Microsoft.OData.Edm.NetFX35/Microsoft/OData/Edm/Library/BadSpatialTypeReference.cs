using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000145 RID: 325
	internal class BadSpatialTypeReference : EdmSpatialTypeReference, IEdmCheckable
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x0000E864 File Offset: 0x0000CA64
		public BadSpatialTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.None, errors), isNullable, default(int?))
		{
			this.errors = errors;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0000E890 File Offset: 0x0000CA90
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000E898 File Offset: 0x0000CA98
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000252 RID: 594
		private readonly IEnumerable<EdmError> errors;
	}
}
