using System;
using System.Globalization;

namespace Microsoft.Identity.Client.Instance
{
	// Token: 0x0200026F RID: 623
	internal class CiamAuthority : AadAuthority
	{
		// Token: 0x0600188C RID: 6284 RVA: 0x00051502 File Offset: 0x0004F702
		internal CiamAuthority(AuthorityInfo authorityInfo)
			: base(authorityInfo)
		{
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0005150C File Offset: 0x0004F70C
		internal override string GetTenantedAuthority(string tenantId, bool forceSpecifiedTenant = false)
		{
			if (!string.IsNullOrEmpty(tenantId) && (forceSpecifiedTenant || base.IsCommonOrganizationsOrConsumersTenant()))
			{
				Uri canonicalAuthority = base.AuthorityInfo.CanonicalAuthority;
				return string.Format(CultureInfo.InvariantCulture, "https://{0}/{1}/", canonicalAuthority.Authority, tenantId);
			}
			return base.AuthorityInfo.CanonicalAuthority.AbsoluteUri;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00051560 File Offset: 0x0004F760
		internal static Uri TransformAuthority(Uri ciamAuthority)
		{
			string text = ciamAuthority.Host + ciamAuthority.AbsolutePath;
			if (string.Equals(ciamAuthority.AbsolutePath, "/"))
			{
				string text2 = text.Substring(0, text.IndexOf(".ciamlogin.com", StringComparison.OrdinalIgnoreCase));
				string text3 = "https://" + text2 + ".ciamlogin.com/";
				string text4 = text2 + ".onmicrosoft.com";
				return new Uri(text3 + text4);
			}
			return ciamAuthority;
		}
	}
}
