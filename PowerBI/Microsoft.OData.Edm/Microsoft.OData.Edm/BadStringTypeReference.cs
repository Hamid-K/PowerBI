using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000066 RID: 102
	internal class BadStringTypeReference : EdmStringTypeReference, IEdmCheckable
	{
		// Token: 0x0600021C RID: 540 RVA: 0x000053E8 File Offset: 0x000035E8
		public BadStringTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.String, errors), isNullable, false, null, new bool?(false))
		{
			this.errors = errors;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000541C File Offset: 0x0000361C
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00005424 File Offset: 0x00003624
		public override string ToString()
		{
			EdmError edmError = this.Errors.FirstOrDefault<EdmError>();
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040000C3 RID: 195
		private readonly IEnumerable<EdmError> errors;
	}
}
