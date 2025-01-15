using System;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C08 RID: 7176
	public static class PageHelpers
	{
		// Token: 0x0600B31B RID: 45851 RVA: 0x002470FC File Offset: 0x002452FC
		public static long Align(long size, long pageSize)
		{
			return PageHelpers.PageCount(size, pageSize) * pageSize;
		}

		// Token: 0x0600B31C RID: 45852 RVA: 0x00247108 File Offset: 0x00245308
		public static long PageCount(long size, long pageSize)
		{
			long num = size / pageSize;
			if (size % pageSize != 0L)
			{
				num += 1L;
			}
			return num;
		}
	}
}
