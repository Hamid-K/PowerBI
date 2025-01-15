using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003C RID: 60
	internal class BadStringTypeReference : EdmStringTypeReference, IEdmCheckable
	{
		// Token: 0x06000299 RID: 665 RVA: 0x00009610 File Offset: 0x00007810
		public BadStringTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.String, errors), isNullable, false, default(int?), new bool?(false))
		{
			this.errors = errors;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00009644 File Offset: 0x00007844
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000964C File Offset: 0x0000784C
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000065 RID: 101
		private readonly IEnumerable<EdmError> errors;
	}
}
