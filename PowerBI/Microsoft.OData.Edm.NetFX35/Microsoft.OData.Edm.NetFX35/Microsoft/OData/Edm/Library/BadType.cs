using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200004C RID: 76
	internal class BadType : BadElement, IEdmType, IEdmElement
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00003AC1 File Offset: 0x00001CC1
		public BadType(IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003ACA File Offset: 0x00001CCA
		public virtual EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.None;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(base.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}
	}
}
