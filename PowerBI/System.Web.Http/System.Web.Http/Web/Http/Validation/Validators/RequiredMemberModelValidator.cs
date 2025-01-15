using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation.Validators
{
	// Token: 0x0200009A RID: 154
	public class RequiredMemberModelValidator : ModelValidator
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0000ADDA File Offset: 0x00008FDA
		public RequiredMemberModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders)
			: base(validatorProviders)
		{
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00006A55 File Offset: 0x00004C55
		public override bool IsRequired
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000ADE3 File Offset: 0x00008FE3
		public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
		{
			return Enumerable.Empty<ModelValidationResult>();
		}
	}
}
