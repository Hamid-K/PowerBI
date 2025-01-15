using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001CF RID: 463
	internal static class ScopeHelper
	{
		// Token: 0x06001454 RID: 5204 RVA: 0x000452A0 File Offset: 0x000434A0
		public static string OrderScopesAlphabetically(string originalScopes)
		{
			string[] array = originalScopes.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			Array.Sort<string>(array, StringComparer.OrdinalIgnoreCase);
			return string.Join(" ", array);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x000452D8 File Offset: 0x000434D8
		public static bool ScopeContains(ISet<string> outerSet, IEnumerable<string> possibleContainedSet)
		{
			foreach (string text in possibleContainedSet)
			{
				if (!outerSet.Contains(text) && !string.IsNullOrEmpty(text))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00045334 File Offset: 0x00043534
		public static HashSet<string> GetMsalScopes(HashSet<string> userScopes)
		{
			return new HashSet<string>(userScopes.Concat(OAuth2Value.ReservedScopes));
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00045346 File Offset: 0x00043546
		public static string GetMsalRuntimeScopes()
		{
			return string.Join(" ", OAuth2Value.ReservedScopes);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00045358 File Offset: 0x00043558
		public static bool HasNonMsalScopes(HashSet<string> userScopes)
		{
			if (userScopes == null)
			{
				return false;
			}
			foreach (string text in userScopes)
			{
				if (!string.IsNullOrWhiteSpace(text) && !OAuth2Value.ReservedScopes.Contains(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x000453C0 File Offset: 0x000435C0
		public static HashSet<string> ConvertStringToScopeSet(string singleString)
		{
			if (string.IsNullOrEmpty(singleString))
			{
				return new HashSet<string>();
			}
			return new HashSet<string>(singleString.Split(new char[] { ' ' }), StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x000453EB File Offset: 0x000435EB
		public static HashSet<string> CreateScopeSet(IEnumerable<string> input)
		{
			if (input == null)
			{
				return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			}
			return new HashSet<string>(input, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00045408 File Offset: 0x00043608
		public static string ScopesToResource(string[] scopes)
		{
			if (scopes == null)
			{
				throw new MsalClientException("exactly_one_scope_expected", "[Managed Identity] To acquire token for managed identity, exactly one scope must be passed.");
			}
			if (scopes.Length != 1)
			{
				throw new MsalClientException("exactly_one_scope_expected", "[Managed Identity] To acquire token for managed identity, exactly one scope must be passed.");
			}
			if (!scopes[0].EndsWith("/.default", StringComparison.Ordinal))
			{
				return scopes[0];
			}
			return scopes[0].Remove(scopes[0].LastIndexOf("/.default", StringComparison.Ordinal));
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00045468 File Offset: 0x00043668
		public static string RemoveDefaultSuffixIfPresent(string resource)
		{
			if (!resource.EndsWith("/.default", StringComparison.Ordinal))
			{
				return resource;
			}
			return resource.Remove(resource.LastIndexOf("/.default", StringComparison.Ordinal));
		}

		// Token: 0x04000852 RID: 2130
		private const string DefaultSuffix = "/.default";
	}
}
