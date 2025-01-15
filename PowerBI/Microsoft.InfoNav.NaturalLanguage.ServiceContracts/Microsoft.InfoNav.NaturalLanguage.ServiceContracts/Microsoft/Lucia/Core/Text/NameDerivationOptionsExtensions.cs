using System;

namespace Microsoft.Lucia.Core.Text
{
	// Token: 0x02000150 RID: 336
	public static class NameDerivationOptionsExtensions
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x0000B78C File Offset: 0x0000998C
		public static bool ShouldStem(this NameDerivationOptions options)
		{
			return NameDerivationOptionsExtensions.HasFlag(options, NameDerivationOptions.Stem);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000B795 File Offset: 0x00009995
		public static bool ShouldCapitalizeWord(this NameDerivationOptions options)
		{
			return NameDerivationOptionsExtensions.HasFlag(options, NameDerivationOptions.CapitalizeWord);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000B79E File Offset: 0x0000999E
		private static bool HasFlag(NameDerivationOptions options, NameDerivationOptions flag)
		{
			return (options & flag) == flag;
		}
	}
}
