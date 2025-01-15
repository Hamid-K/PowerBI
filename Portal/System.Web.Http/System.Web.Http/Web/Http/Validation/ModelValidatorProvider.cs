using System;
using System.Collections.Generic;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation
{
	// Token: 0x02000097 RID: 151
	public abstract class ModelValidatorProvider
	{
		// Token: 0x060003B3 RID: 947
		public abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders);
	}
}
