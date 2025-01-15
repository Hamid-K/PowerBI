using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Http.Validation.Providers
{
	// Token: 0x0200009E RID: 158
	// (Invoke) Token: 0x060003D2 RID: 978
	public delegate ModelValidator DataAnnotationsModelValidationFactory(IEnumerable<ModelValidatorProvider> validatorProviders, ValidationAttribute attribute);
}
