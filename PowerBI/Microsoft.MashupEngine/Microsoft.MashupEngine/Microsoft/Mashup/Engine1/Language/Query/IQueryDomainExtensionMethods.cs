using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017D7 RID: 6103
	internal static class IQueryDomainExtensionMethods
	{
		// Token: 0x06009A48 RID: 39496 RVA: 0x001FEC70 File Offset: 0x001FCE70
		public static bool TryGetCompatibleDomain(this IQueryDomain domain1, IQueryDomain domain2, out IQueryDomain compatibleDomain)
		{
			if (domain1.IsCompatibleWith(domain2))
			{
				compatibleDomain = domain1;
				return true;
			}
			if (domain2.IsCompatibleWith(domain1))
			{
				compatibleDomain = domain2;
				return true;
			}
			compatibleDomain = null;
			return false;
		}
	}
}
