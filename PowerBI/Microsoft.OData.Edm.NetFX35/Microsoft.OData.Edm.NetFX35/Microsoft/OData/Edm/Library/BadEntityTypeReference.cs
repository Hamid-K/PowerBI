using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000139 RID: 313
	internal class BadEntityTypeReference : EdmEntityTypeReference, IEdmCheckable
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x0000E428 File Offset: 0x0000C628
		public BadEntityTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadEntityType(qualifiedName, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000E43F File Offset: 0x0000C63F
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000E448 File Offset: 0x0000C648
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x0400023B RID: 571
		private readonly IEnumerable<EdmError> errors;
	}
}
