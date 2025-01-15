using System;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation
{
	// Token: 0x0200008B RID: 139
	public interface IModelValidatorCache
	{
		// Token: 0x06000374 RID: 884
		ModelValidator[] GetValidators(ModelMetadata metadata);
	}
}
