using System;
using System.Text.RegularExpressions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x02000146 RID: 326
	public sealed class NamingRules
	{
		// Token: 0x06001170 RID: 4464 RVA: 0x00047002 File Offset: 0x00045202
		public static bool IsValidVirtualServerName(string virtualServerName)
		{
			return NamingRules.virtualServerNameRegex.IsMatch(virtualServerName);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0004700F File Offset: 0x0004520F
		public static bool IsValidDatabaseName(string databaseName)
		{
			return NamingRules.databaseNameRegex.IsMatch(databaseName);
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0004701C File Offset: 0x0004521C
		public static int VirtualServerNameMaximumLength
		{
			get
			{
				return 64;
			}
		}

		// Token: 0x040003F0 RID: 1008
		private const string DATABASE_NAME_REGEX_FORMAT = "^[\\w-/\\\\.]{{{0},{1}}}$";

		// Token: 0x040003F1 RID: 1009
		private const string VIRTUAL_SERVER_NAME_REGEX_FORMAT = "^[a-zA-Z0-9_]{{{0},{1}}}$";

		// Token: 0x040003F2 RID: 1010
		private const int DATABASE_NAME_MINIMUM_LENGTH = 1;

		// Token: 0x040003F3 RID: 1011
		private const int DATABASE_NAME_MAXIMUM_LENGTH = 300;

		// Token: 0x040003F4 RID: 1012
		private const int VIRTUAL_SERVER_NAME_MINIMUM_LENGTH = 2;

		// Token: 0x040003F5 RID: 1013
		private const int VIRTUAL_SERVER_NAME_MAXIMUM_LENGTH = 64;

		// Token: 0x040003F6 RID: 1014
		private static Regex databaseNameRegex = new Regex("^[\\w-/\\\\.]{{{0},{1}}}$".FormatWithInvariantCulture(new object[] { 1, 300 }), RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x040003F7 RID: 1015
		private static Regex virtualServerNameRegex = new Regex("^[a-zA-Z0-9_]{{{0},{1}}}$".FormatWithInvariantCulture(new object[] { 2, 64 }), RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
