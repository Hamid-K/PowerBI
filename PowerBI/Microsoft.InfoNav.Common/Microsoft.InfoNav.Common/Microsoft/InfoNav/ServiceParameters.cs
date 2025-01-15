using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000029 RID: 41
	internal static class ServiceParameters
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00006750 File Offset: 0x00004950
		internal static T GetValueChecked<T>(ReadOnlyCollection<T> values, int index)
		{
			return values[index];
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000675C File Offset: 0x0000495C
		internal static ReadOnlyCollection<TValue> PopulateValuesFromDictionary<TKey, TValue>(IDictionary<TKey, TValue> parameterValueMap, Func<int, TKey> castIntToParameter)
		{
			int[] array = (int[])Enum.GetValues(typeof(TKey));
			TValue[] array2 = new TValue[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				TKey tkey = castIntToParameter(array[i]);
				TValue tvalue;
				if (!parameterValueMap.TryGetValue(tkey, out tvalue))
				{
					throw new ArgumentException(StringUtil.FormatInvariant("Value not specified for '{0}'.", tkey));
				}
				array2[i] = tvalue;
			}
			return Array.AsReadOnly<TValue>(array2);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000067D4 File Offset: 0x000049D4
		internal static ReadOnlyCollection<TValue> CopyAndOverrideValuesFromCollection<TKey, TValue>(ReadOnlyCollection<TValue> parameterValues, IDictionary<TKey, TValue> parameterValueOverrides, Func<int, TKey> castIntToParameter)
		{
			if (parameterValueOverrides == null)
			{
				return parameterValues;
			}
			int[] array = (int[])Enum.GetValues(typeof(TKey));
			TValue[] array2 = new TValue[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				TKey tkey = castIntToParameter(array[i]);
				TValue tvalue;
				if (parameterValueOverrides.TryGetValue(tkey, out tvalue))
				{
					array2[i] = tvalue;
				}
				else
				{
					array2[i] = parameterValues[i];
				}
			}
			return Array.AsReadOnly<TValue>(array2);
		}
	}
}
