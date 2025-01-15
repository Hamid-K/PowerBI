using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000055 RID: 85
	internal class BadComplexTypeReference : EdmComplexTypeReference, IEdmCheckable
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00004E4B File Offset: 0x0000304B
		public BadComplexTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadComplexType(qualifiedName, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00004E62 File Offset: 0x00003062
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00004E6C File Offset: 0x0000306C
		public override string ToString()
		{
			EdmError edmError = this.Errors.FirstOrDefault<EdmError>();
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040000A2 RID: 162
		private readonly IEnumerable<EdmError> errors;
	}
}
