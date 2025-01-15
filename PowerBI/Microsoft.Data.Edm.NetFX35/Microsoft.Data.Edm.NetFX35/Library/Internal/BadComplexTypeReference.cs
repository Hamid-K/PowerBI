using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000F1 RID: 241
	internal class BadComplexTypeReference : EdmComplexTypeReference, IEdmCheckable
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x0000C451 File Offset: 0x0000A651
		public BadComplexTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadComplexType(qualifiedName, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000C468 File Offset: 0x0000A668
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000C470 File Offset: 0x0000A670
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040001C3 RID: 451
		private readonly IEnumerable<EdmError> errors;
	}
}
