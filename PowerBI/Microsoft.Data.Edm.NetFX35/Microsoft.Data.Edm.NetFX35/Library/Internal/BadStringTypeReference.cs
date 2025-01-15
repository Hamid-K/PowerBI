using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x0200010A RID: 266
	internal class BadStringTypeReference : EdmStringTypeReference, IEdmCheckable
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		public BadStringTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.String, errors), isNullable, false, default(int?), new bool?(false), new bool?(false), null)
		{
			this.errors = errors;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000CB67 File Offset: 0x0000AD67
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000CB70 File Offset: 0x0000AD70
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040001E4 RID: 484
		private readonly IEnumerable<EdmError> errors;
	}
}
