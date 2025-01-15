using System;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x02000291 RID: 657
	public static class AbstractConfidentialClientAcquireTokenParameterBuilderExtension
	{
		// Token: 0x0600191E RID: 6430 RVA: 0x00052BCD File Offset: 0x00050DCD
		public static AbstractAcquireTokenParameterBuilder<T> OnBeforeTokenRequest<T>(this AbstractAcquireTokenParameterBuilder<T> builder, Func<OnBeforeTokenRequestData, Task> onBeforeTokenRequestHandler) where T : AbstractAcquireTokenParameterBuilder<T>
		{
			builder.CommonParameters.OnBeforeTokenRequestHandler = onBeforeTokenRequestHandler;
			return builder;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00052BDC File Offset: 0x00050DDC
		public static AbstractAcquireTokenParameterBuilder<T> WithProofOfPosessionKeyId<T>(this AbstractAcquireTokenParameterBuilder<T> builder, string keyId, string expectedTokenTypeFromAad = "Bearer") where T : AbstractAcquireTokenParameterBuilder<T>
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
