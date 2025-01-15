using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000068 RID: 104
	internal class BadTemporalTypeReference : EdmTemporalTypeReference, IEdmCheckable
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000547C File Offset: 0x0000367C
		public BadTemporalTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPrimitiveType(qualifiedName, EdmPrimitiveTypeKind.None, errors), isNullable, null)
		{
			this.errors = errors;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000054A8 File Offset: 0x000036A8
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000054B0 File Offset: 0x000036B0
		public override string ToString()
		{
			EdmError edmError = this.Errors.FirstOrDefault<EdmError>();
			string text = ((edmError != null) ? (edmError.ErrorCode.ToString() + ":") : "");
			return text + this.ToTraceString();
		}

		// Token: 0x040000C4 RID: 196
		private readonly IEnumerable<EdmError> errors;
	}
}
