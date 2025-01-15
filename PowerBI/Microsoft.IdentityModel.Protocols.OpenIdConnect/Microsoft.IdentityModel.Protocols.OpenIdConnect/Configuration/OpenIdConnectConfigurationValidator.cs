using System;
using System.Linq;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect.Configuration
{
	// Token: 0x02000018 RID: 24
	public class OpenIdConnectConfigurationValidator : IConfigurationValidator<OpenIdConnectConfiguration>
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000043A8 File Offset: 0x000025A8
		public ConfigurationValidationResult Validate(OpenIdConnectConfiguration openIdConnectConfiguration)
		{
			if (openIdConnectConfiguration == null)
			{
				throw new ArgumentNullException("openIdConnectConfiguration");
			}
			if (openIdConnectConfiguration.JsonWebKeySet == null || openIdConnectConfiguration.JsonWebKeySet.Keys.Count == 0)
			{
				return new ConfigurationValidationResult
				{
					ErrorMessage = "IDX21817: The OpenIdConnectConfiguration did not contain any JsonWebKeys. This is required to validate the configuration.",
					Succeeded = false
				};
			}
			int num = openIdConnectConfiguration.JsonWebKeySet.Keys.Where((JsonWebKey key) => key.ConvertedSecurityKey != null).Count<JsonWebKey>();
			if (num < this.MinimumNumberOfKeys)
			{
				string text = string.Join("\n", from key in openIdConnectConfiguration.JsonWebKeySet.Keys
					where !string.IsNullOrEmpty(key.ConvertKeyInfo)
					select key.Kid.ToString() + ": " + key.ConvertKeyInfo);
				return new ConfigurationValidationResult
				{
					ErrorMessage = LogHelper.FormatInvariant("IDX21818: The OpenIdConnectConfiguration's valid signing keys cannot be less than {0}. Values: {1}. Invalid keys: {2}", new object[]
					{
						LogHelper.MarkAsNonPII(this.MinimumNumberOfKeys),
						LogHelper.MarkAsNonPII(num),
						string.IsNullOrEmpty(text) ? "None" : text
					}),
					Succeeded = false
				};
			}
			return new ConfigurationValidationResult
			{
				Succeeded = true
			};
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000044F9 File Offset: 0x000026F9
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004504 File Offset: 0x00002704
		public int MinimumNumberOfKeys
		{
			get
			{
				return this._minimumNumberOfKeys;
			}
			set
			{
				if (value < 1)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX21816: The number of signing keys must be greater or equal to '{0}'. Value: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(1),
						LogHelper.MarkAsNonPII(value)
					})));
				}
				this._minimumNumberOfKeys = value;
			}
		}

		// Token: 0x04000107 RID: 263
		private int _minimumNumberOfKeys = 1;

		// Token: 0x04000108 RID: 264
		private const int DefaultMinimumNumberOfKeys = 1;
	}
}
