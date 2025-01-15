using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200000D RID: 13
	internal static class ItemTypeHelpers
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002369 File Offset: 0x00000569
		public static string ConvertToString(ItemType type)
		{
			return type.ToString();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002378 File Offset: 0x00000578
		public static ItemType ConvertFromString(string value, bool throwOnError)
		{
			Enum @enum = null;
			if (!EnumHelpers.TryStringToEnum(typeof(ItemType), value, ref @enum))
			{
				if (throwOnError)
				{
					throw new ArgumentException("value");
				}
				@enum = ItemType.Unknown;
			}
			return (ItemType)@enum;
		}

		// Token: 0x04000078 RID: 120
		public static readonly int MaxItemType = 14;
	}
}
