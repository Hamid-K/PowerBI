using System;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000075 RID: 117
	internal static class DbModelBuilderVersionExtensions
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x0000FA00 File Offset: 0x0000DC00
		public static double GetEdmVersion(this DbModelBuilderVersion modelBuilderVersion)
		{
			switch (modelBuilderVersion)
			{
			case DbModelBuilderVersion.Latest:
			case DbModelBuilderVersion.V5_0:
			case DbModelBuilderVersion.V6_0:
				return 3.0;
			case DbModelBuilderVersion.V4_1:
			case DbModelBuilderVersion.V5_0_Net4:
				return 2.0;
			default:
				throw new ArgumentOutOfRangeException("modelBuilderVersion");
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		public static bool IsEF6OrHigher(this DbModelBuilderVersion modelBuilderVersion)
		{
			return modelBuilderVersion >= DbModelBuilderVersion.V6_0 || modelBuilderVersion == DbModelBuilderVersion.Latest;
		}
	}
}
