using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000041 RID: 65
	internal class BadTypeReference : EdmTypeReference, IEdmCheckable
	{
		// Token: 0x060002AE RID: 686 RVA: 0x000097B6 File Offset: 0x000079B6
		public BadTypeReference(BadType definition, bool isNullable)
			: base(definition, isNullable)
		{
			this.errors = definition.Errors;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000097CC File Offset: 0x000079CC
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000097D4 File Offset: 0x000079D4
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x04000069 RID: 105
		private readonly IEnumerable<EdmError> errors;
	}
}
