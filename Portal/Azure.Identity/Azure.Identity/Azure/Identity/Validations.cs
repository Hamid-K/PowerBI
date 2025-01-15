using System;
using System.Runtime.InteropServices;

namespace Azure.Identity
{
	// Token: 0x0200008B RID: 139
	internal static class Validations
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		public static string ValidateTenantId(string tenantId, string argumentName = null, bool allowNull = false)
		{
			if (tenantId != null)
			{
				if (tenantId.Length == 0)
				{
					throw (argumentName != null) ? new ArgumentException("Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names", argumentName) : new ArgumentException("Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names");
				}
				for (int i = 0; i < tenantId.Length; i++)
				{
					if (!Validations.IsValidTenantCharacter(tenantId[i]))
					{
						throw (argumentName != null) ? new ArgumentException("Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names", argumentName) : new ArgumentException("Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names");
					}
				}
			}
			else if (!allowNull)
			{
				throw (argumentName != null) ? new ArgumentNullException(argumentName, "Tenant id cannot be null. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names") : new ArgumentNullException("Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names", null);
			}
			return tenantId;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000E039 File Offset: 0x0000C239
		public static Uri ValidateAuthorityHost(Uri authorityHost)
		{
			if (authorityHost != null && string.Compare(authorityHost.Scheme, "https", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException("Authority host must be a TLS protected (https) endpoint.");
			}
			return authorityHost;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000E063 File Offset: 0x0000C263
		public static bool CanUseLegacyPowerShell(bool useLegacyPowerShell)
		{
			if (useLegacyPowerShell && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				throw new ArgumentException("PowerShell Legacy is only supported in Windows.");
			}
			return useLegacyPowerShell;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000E080 File Offset: 0x0000C280
		private static bool IsValidTenantCharacter(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '.' || c == '-';
		}

		// Token: 0x0400028D RID: 653
		internal const string InvalidTenantIdErrorMessage = "Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names";

		// Token: 0x0400028E RID: 654
		private const string NullTenantIdErrorMessage = "Tenant id cannot be null. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names";

		// Token: 0x0400028F RID: 655
		private const string NonTlsAuthorityHostErrorMessage = "Authority host must be a TLS protected (https) endpoint.";

		// Token: 0x04000290 RID: 656
		internal const string NoWindowsPowerShellLegacyErrorMessage = "PowerShell Legacy is only supported in Windows.";
	}
}
