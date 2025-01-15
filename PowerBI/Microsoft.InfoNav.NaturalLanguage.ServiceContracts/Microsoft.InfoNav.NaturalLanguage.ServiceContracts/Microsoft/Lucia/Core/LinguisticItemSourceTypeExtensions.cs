using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000058 RID: 88
	internal static class LinguisticItemSourceTypeExtensions
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00004363 File Offset: 0x00002563
		internal static bool IsUser(this LinguisticItemSourceType sourceType, LinguisticItemSource source)
		{
			return sourceType == LinguisticItemSourceType.User || (sourceType == LinguisticItemSourceType.Default && (source == LinguisticItemSource.User || source == LinguisticItemSource.Deleted));
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004379 File Offset: 0x00002579
		internal static bool IsInternal(this LinguisticItemSourceType sourceType, LinguisticItemSource source)
		{
			return sourceType == LinguisticItemSourceType.Internal || (sourceType == LinguisticItemSourceType.Default && (source == LinguisticItemSource.Generated || source == LinguisticItemSource.Suggested));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004390 File Offset: 0x00002590
		internal static bool IsExternal(this LinguisticItemSourceType sourceType)
		{
			return sourceType == LinguisticItemSourceType.External;
		}
	}
}
