using System;
using Microsoft.Cloud.ModelCommon;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000B2 RID: 178
	public static class VirtualServerNameGenerator
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x00011138 File Offset: 0x0000F338
		public static string GenerateRandomFixedLengthName()
		{
			char[] array = new char[10];
			for (int i = 0; i < 10; i++)
			{
				array[i] = VirtualServerNameGenerator.RandomCharFromAlphabet("abcdefghijklmnopqrstuvwxyz0123456789");
			}
			return new string(array);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00011170 File Offset: 0x0000F370
		public static string GenerateNameFromSubscriptionAndAuthority(string subscriptionId, string authorityId, string fqdn)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(subscriptionId, "subscriptionId");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(authorityId, "authorityId");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(fqdn, "fqdn");
			return Utils.NormalizeCase(ExtendedText.CalculateHash("{0}-{1}-{2}".FormatWithInvariantCulture(new object[] { subscriptionId, fqdn, authorityId })));
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000111C4 File Offset: 0x0000F3C4
		public static string GenerateNameFromSuffixAndAuthority(string suffix, string authorityId)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(suffix, "suffix");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(authorityId, "authorityId");
			return "{0}_{1}".FormatWithInvariantCulture(new object[] { authorityId, suffix }).ToLowerInvariant();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000111F9 File Offset: 0x0000F3F9
		private static char RandomCharFromAlphabet(string alphabet)
		{
			return alphabet[Randomizer.GetI32(0, alphabet.Length)];
		}

		// Token: 0x0400022D RID: 557
		private const string VirtualServerNameAlphabet = "abcdefghijklmnopqrstuvwxyz0123456789";

		// Token: 0x0400022E RID: 558
		private const int VirtualServerNameLength = 10;
	}
}
