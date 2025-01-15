using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x02000134 RID: 308
	internal interface IValidator
	{
		// Token: 0x060014C3 RID: 5315
		IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalMemberEntry property);
	}
}
