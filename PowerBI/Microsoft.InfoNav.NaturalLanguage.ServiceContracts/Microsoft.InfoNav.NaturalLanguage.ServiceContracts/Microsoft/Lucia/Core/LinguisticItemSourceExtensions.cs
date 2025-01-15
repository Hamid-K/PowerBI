using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000056 RID: 86
	internal static class LinguisticItemSourceExtensions
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00004351 File Offset: 0x00002551
		internal static bool IsGenerated(this LinguisticItemSource source)
		{
			return source == LinguisticItemSource.Generated;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004357 File Offset: 0x00002557
		internal static bool IsSuggested(this LinguisticItemSource source)
		{
			return source == LinguisticItemSource.Suggested;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000435D File Offset: 0x0000255D
		internal static bool IsDeleted(this LinguisticItemSource source)
		{
			return source == LinguisticItemSource.Deleted;
		}
	}
}
