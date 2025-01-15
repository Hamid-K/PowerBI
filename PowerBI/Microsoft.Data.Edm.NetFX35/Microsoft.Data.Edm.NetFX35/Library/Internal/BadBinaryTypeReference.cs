using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000E9 RID: 233
	internal class BadBinaryTypeReference : EdmBinaryTypeReference, IEdmCheckable
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000C334 File Offset: 0x0000A534
		public BadBinaryTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.Binary, errors), isNullable, false, default(int?), new bool?(false))
		{
			this.errors = errors;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000C367 File Offset: 0x0000A567
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000C370 File Offset: 0x0000A570
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(this.Errors);
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040001BF RID: 447
		private readonly IEnumerable<EdmError> errors;
	}
}
