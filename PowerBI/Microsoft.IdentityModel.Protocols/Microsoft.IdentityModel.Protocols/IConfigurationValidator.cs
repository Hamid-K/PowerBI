using System;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x02000009 RID: 9
	public interface IConfigurationValidator<T>
	{
		// Token: 0x06000035 RID: 53
		ConfigurationValidationResult Validate(T configuration);
	}
}
