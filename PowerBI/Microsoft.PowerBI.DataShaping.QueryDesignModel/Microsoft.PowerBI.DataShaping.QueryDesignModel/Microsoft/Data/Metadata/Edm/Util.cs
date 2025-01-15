using System;
using System.Data;
using System.Diagnostics;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000B4 RID: 180
	internal static class Util
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x0001E12D File Offset: 0x0001C32D
		internal static void ThrowIfReadOnly(MetadataItem item)
		{
			if (item.IsReadOnly)
			{
				throw EntityUtil.OperationOnReadOnlyItem();
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0001E13D File Offset: 0x0001C33D
		[Conditional("DEBUG")]
		internal static void AssertItemHasIdentity(MetadataItem item, string argumentName)
		{
			EntityUtil.GenericCheckArgumentNull<MetadataItem>(item, argumentName);
		}
	}
}
