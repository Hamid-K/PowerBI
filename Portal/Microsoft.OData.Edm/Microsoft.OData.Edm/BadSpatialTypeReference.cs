using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000065 RID: 101
	internal class BadSpatialTypeReference : EdmSpatialTypeReference, IEdmCheckable
	{
		// Token: 0x06000219 RID: 537 RVA: 0x00005364 File Offset: 0x00003564
		public BadSpatialTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.None, errors), isNullable, null)
		{
			this.errors = errors;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00005390 File Offset: 0x00003590
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00005398 File Offset: 0x00003598
		public override string ToString()
		{
			EdmError edmError = this.Errors.FirstOrDefault<EdmError>();
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040000C2 RID: 194
		private readonly IEnumerable<EdmError> errors;
	}
}
