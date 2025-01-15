using System;
using System.Text.RegularExpressions;

namespace Azure.Identity
{
	// Token: 0x0200007C RID: 124
	internal static class ScopeUtilities
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0000D274 File Offset: 0x0000B474
		public static string ScopesToResource(string[] scopes)
		{
			if (scopes == null)
			{
				throw new ArgumentNullException("scopes");
			}
			if (scopes.Length != 1)
			{
				throw new ArgumentException("To convert to a resource string the specified array must be exactly length 1", "scopes");
			}
			if (!scopes[0].EndsWith("/.default", StringComparison.Ordinal))
			{
				return scopes[0];
			}
			return scopes[0].Remove(scopes[0].LastIndexOf("/.default", StringComparison.Ordinal));
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000D2CF File Offset: 0x0000B4CF
		public static string[] ResourceToScopes(string resource)
		{
			return new string[] { resource + "/.default" };
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000D2E5 File Offset: 0x0000B4E5
		public static void ValidateScope(string scope)
		{
			if (!ScopeUtilities.scopeRegex.IsMatch(scope))
			{
				throw new ArgumentException("The specified scope is not in expected format. Only alphanumeric characters, '.', '-', ':', '_', and '/' are allowed", "scope");
			}
		}

		// Token: 0x04000268 RID: 616
		private const string DefaultSuffix = "/.default";

		// Token: 0x04000269 RID: 617
		private const string ScopePattern = "^[0-9a-zA-Z-_.:/]+$";

		// Token: 0x0400026A RID: 618
		internal const string InvalidScopeMessage = "The specified scope is not in expected format. Only alphanumeric characters, '.', '-', ':', '_', and '/' are allowed";

		// Token: 0x0400026B RID: 619
		private static readonly Regex scopeRegex = new Regex("^[0-9a-zA-Z-_.:/]+$");
	}
}
