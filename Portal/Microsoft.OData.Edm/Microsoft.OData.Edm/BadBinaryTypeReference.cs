using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000052 RID: 82
	internal class BadBinaryTypeReference : EdmBinaryTypeReference, IEdmCheckable
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00004D98 File Offset: 0x00002F98
		public BadBinaryTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.Binary, errors), isNullable, false, null)
		{
			this.errors = errors;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00004DC5 File Offset: 0x00002FC5
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public override string ToString()
		{
			EdmError edmError = this.Errors.FirstOrDefault<EdmError>();
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040000A0 RID: 160
		private readonly IEnumerable<EdmError> errors;
	}
}
