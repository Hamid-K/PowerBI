using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x0200002C RID: 44
	internal class BadType : BadElement, IEdmType, IEdmElement
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00002E5D File Offset: 0x0000105D
		public BadType(IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002E66 File Offset: 0x00001066
		public virtual EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.None;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002E6C File Offset: 0x0000106C
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(base.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}
	}
}
