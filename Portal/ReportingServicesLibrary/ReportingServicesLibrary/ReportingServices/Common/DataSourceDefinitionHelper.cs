using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200035F RID: 863
	public static class DataSourceDefinitionHelper
	{
		// Token: 0x06001C86 RID: 7302 RVA: 0x000733EE File Offset: 0x000715EE
		public static string GenerateOnPremiseModelIdentityClaims(string userObjectId, string puid, string tenantName, string upn)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2} {3}", new object[] { userObjectId, puid, tenantName, upn });
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x00073418 File Offset: 0x00071618
		public static void ParseOnPremiseModelIdentityClaims(string identityClaims, out string userObjectId, out string puid, out string tenantId, out string upn)
		{
			if (string.IsNullOrEmpty(identityClaims))
			{
				throw new IdentityClaimsMissingOrInvalidException(identityClaims);
			}
			string[] array = identityClaims.Split(DataSourceDefinitionHelper.s_userIdSeparator);
			if (array.Length != 4)
			{
				throw new IdentityClaimsMissingOrInvalidException(identityClaims);
			}
			userObjectId = array[0];
			puid = array[1];
			tenantId = array[2];
			upn = array[3];
		}

		// Token: 0x04000BD1 RID: 3025
		private static char[] s_userIdSeparator = new char[] { ' ' };
	}
}
