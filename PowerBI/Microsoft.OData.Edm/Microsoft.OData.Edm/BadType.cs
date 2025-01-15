using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000069 RID: 105
	internal class BadType : BadElement, IEdmType, IEdmElement
	{
		// Token: 0x06000228 RID: 552 RVA: 0x0000521E File Offset: 0x0000341E
		public BadType(IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000026A6 File Offset: 0x000008A6
		public virtual EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.None;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00005500 File Offset: 0x00003700
		public override string ToString()
		{
			EdmError edmError = base.Errors.FirstOrDefault<EdmError>();
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}
	}
}
