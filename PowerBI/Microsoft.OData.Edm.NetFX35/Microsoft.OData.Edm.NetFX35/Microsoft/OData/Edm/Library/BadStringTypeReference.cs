using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000148 RID: 328
	internal class BadStringTypeReference : EdmStringTypeReference, IEdmCheckable
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x0000E95C File Offset: 0x0000CB5C
		public BadStringTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.String, errors), isNullable, false, default(int?), new bool?(false))
		{
			this.errors = errors;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0000E990 File Offset: 0x0000CB90
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0000E998 File Offset: 0x0000CB98
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000256 RID: 598
		private readonly IEnumerable<EdmError> errors;
	}
}
