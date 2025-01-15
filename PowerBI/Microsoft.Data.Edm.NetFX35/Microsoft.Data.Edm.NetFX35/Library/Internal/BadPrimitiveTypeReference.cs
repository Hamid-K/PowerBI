using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x02000101 RID: 257
	internal class BadPrimitiveTypeReference : EdmPrimitiveTypeReference, IEdmCheckable
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		public BadPrimitiveTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.None, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000C808 File Offset: 0x0000AA08
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000C810 File Offset: 0x0000AA10
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040001D6 RID: 470
		private readonly IEnumerable<EdmError> errors;
	}
}
