using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common.Internal
{
	// Token: 0x02000087 RID: 135
	internal static class DataContractValidationExtensions
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x0000CF30 File Offset: 0x0000B130
		internal static bool IsValid(this Enum enumValue)
		{
			return enumValue != null && Enum.IsDefined(enumValue.GetType(), enumValue);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000CF44 File Offset: 0x0000B144
		internal static bool AreAllValid<T>(this IList<T> list) where T : IDataContractValidatable
		{
			int i = 0;
			while (i < list.Count)
			{
				if (list[i] != null)
				{
					T t = list[i];
					if (t.IsValid())
					{
						i++;
						continue;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000CF8A File Offset: 0x0000B18A
		internal static bool IsNullOrValid<T>(this T item) where T : IDataContractValidatable
		{
			return item == null || item.IsValid();
		}
	}
}
