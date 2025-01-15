using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001FD RID: 509
	internal static class QueryExtensionSchemaUpgradeMessages
	{
		// Token: 0x06000DF0 RID: 3568 RVA: 0x0001B395 File Offset: 0x00019595
		internal static string UnsupportedExtensionSchemaVersion(int version)
		{
			return StringUtil.FormatInvariant("ExtensionSchema target version {0} is unsupported. Version must be either 0 or 1.", version);
		}
	}
}
