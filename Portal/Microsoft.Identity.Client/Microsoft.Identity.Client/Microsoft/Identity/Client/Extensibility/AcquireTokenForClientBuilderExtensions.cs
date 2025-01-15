using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x02000292 RID: 658
	public static class AcquireTokenForClientBuilderExtensions
	{
		// Token: 0x06001920 RID: 6432 RVA: 0x00052C0F File Offset: 0x00050E0F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static AcquireTokenForClientParameterBuilder WithProofOfPosessionKeyId(this AcquireTokenForClientParameterBuilder builder, string keyId, string expectedTokenTypeFromAad = "Bearer")
		{
			if (string.IsNullOrEmpty(keyId))
			{
				throw new ArgumentNullException("keyId");
			}
			builder.ValidateUseOfExperimentalFeature("WithProofOfPosessionKeyId");
			builder.CommonParameters.AuthenticationScheme = new ExternalBoundTokenScheme(keyId, expectedTokenTypeFromAad);
			return builder;
		}
	}
}
