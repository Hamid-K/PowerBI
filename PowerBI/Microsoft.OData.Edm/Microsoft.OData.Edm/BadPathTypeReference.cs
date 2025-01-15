using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000008 RID: 8
	internal class BadPathTypeReference : EdmPathTypeReference, IEdmCheckable
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002627 File Offset: 0x00000827
		public BadPathTypeReference(string qualifiedName, bool isNullable, IEnumerable<EdmError> errors)
			: base(new BadPathType(qualifiedName, errors), isNullable)
		{
			this.errors = errors;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000263E File Offset: 0x0000083E
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002648 File Offset: 0x00000848
		public override string ToString()
		{
			EdmError edmError = this.Errors.FirstOrDefault<EdmError>();
			string text = edmError.ErrorCode + ":";
			return text + this.ToTraceString();
		}

		// Token: 0x04000006 RID: 6
		private readonly IEnumerable<EdmError> errors;
	}
}
