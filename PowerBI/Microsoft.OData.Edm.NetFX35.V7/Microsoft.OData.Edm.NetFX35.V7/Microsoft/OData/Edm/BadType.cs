using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003F RID: 63
	internal class BadType : BadElement, IEdmType, IEdmElement
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000919A File Offset: 0x0000739A
		public BadType(IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00008EC3 File Offset: 0x000070C3
		public virtual EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.None;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00009730 File Offset: 0x00007930
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(base.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}
	}
}
