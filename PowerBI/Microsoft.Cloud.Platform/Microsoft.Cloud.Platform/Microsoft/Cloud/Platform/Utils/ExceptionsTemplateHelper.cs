using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000200 RID: 512
	public static class ExceptionsTemplateHelper
	{
		// Token: 0x06000D91 RID: 3473 RVA: 0x0002F8F6 File Offset: 0x0002DAF6
		public static void IncrementMagicLevel()
		{
			ExceptionsTemplateHelper.ts_magicLevel++;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0002F904 File Offset: 0x0002DB04
		public static void DecrementMagicLevel()
		{
			ExceptionsTemplateHelper.ts_magicLevel--;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0002F912 File Offset: 0x0002DB12
		public static int MagicLevel
		{
			get
			{
				return ExceptionsTemplateHelper.ts_magicLevel;
			}
		}

		// Token: 0x04000558 RID: 1368
		[ThreadStatic]
		private static int ts_magicLevel;
	}
}
