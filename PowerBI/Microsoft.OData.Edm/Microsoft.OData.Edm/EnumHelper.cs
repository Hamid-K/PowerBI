using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000031 RID: 49
	public static class EnumHelper
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00003550 File Offset: 0x00001750
		public static bool TryParseEnum(this IEdmEnumType enumType, string value, bool ignoreCase, out long parseResult)
		{
			char[] array = new char[] { ',' };
			parseResult = 0L;
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				return false;
			}
			ulong num = 0UL;
			string[] array2 = value.Split(array);
			if (!enumType.IsFlags && array2.Length > 1)
			{
				return false;
			}
			ulong[] array3;
			string[] array4;
			enumType.GetCachedValuesAndNames(out array3, out array4, true, true);
			if (char.IsDigit(value[0]) || value[0] == '-' || value[0] == '+')
			{
				ulong num2 = 0UL;
				for (int i = 0; i < array3.Length; i++)
				{
					num2 |= array3[i];
				}
				for (int j = 0; j < array2.Length; j++)
				{
					long num3;
					if (!long.TryParse(array2[j], out num3))
					{
						return false;
					}
					num |= (ulong)num3;
				}
			}
			else
			{
				for (int k = 0; k < array2.Length; k++)
				{
					array2[k] = array2[k].Trim();
					bool flag = false;
					int l = 0;
					while (l < array4.Length)
					{
						if (ignoreCase)
						{
							if (string.Compare(array4[l], array2[k], StringComparison.OrdinalIgnoreCase) == 0)
							{
								goto IL_0106;
							}
						}
						else if (array4[l].Equals(array2[k]))
						{
							goto IL_0106;
						}
						l++;
						continue;
						IL_0106:
						ulong num4 = array3[l];
						num |= num4;
						flag = true;
						break;
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			parseResult = (long)num;
			return true;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000369C File Offset: 0x0000189C
		public static string ToStringLiteral(this IEdmEnumTypeReference type, long value)
		{
			if (type != null)
			{
				IEdmEnumType edmEnumType = type.Definition as IEdmEnumType;
				if (edmEnumType != null)
				{
					if (!edmEnumType.IsFlags)
					{
						return edmEnumType.ToStringNoFlags(value);
					}
					return edmEnumType.ToStringWithFlags(value);
				}
			}
			return value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000036E0 File Offset: 0x000018E0
		private static string ToStringWithFlags(this IEdmEnumType enumType, long value)
		{
			ulong num = (ulong)value;
			ulong[] array;
			string[] array2;
			enumType.GetCachedValuesAndNames(out array, out array2, true, true);
			int num2 = array.Length - 1;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			ulong num3 = num;
			while (num2 >= 0 && (num2 != 0 || array[num2] != 0UL))
			{
				if ((num & array[num2]) == array[num2])
				{
					num -= array[num2];
					if (!flag)
					{
						stringBuilder.Insert(0, ", ");
					}
					stringBuilder.Insert(0, array2[num2]);
					flag = false;
				}
				num2--;
			}
			if (num != 0UL)
			{
				return value.ToString(CultureInfo.InvariantCulture);
			}
			if (num3 != 0UL)
			{
				return stringBuilder.ToString();
			}
			if (array.Length != 0 && array[0] == 0UL)
			{
				return array2[0];
			}
			return 0.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000378C File Offset: 0x0000198C
		private static string ToStringNoFlags(this IEdmEnumType enumType, long value)
		{
			ulong[] array;
			string[] array2;
			enumType.GetCachedValuesAndNames(out array, out array2, true, true);
			ulong num = (ulong)value;
			int num2 = Array.BinarySearch<ulong>(array, num);
			if (num2 < 0)
			{
				return value.ToString(CultureInfo.InvariantCulture);
			}
			return array2[num2];
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000037C4 File Offset: 0x000019C4
		private static void GetCachedValuesAndNames(this IEdmEnumType enumType, out ulong[] values, out string[] names, bool getValues, bool getNames)
		{
			EnumHelper.HashEntry hashEntry = EnumHelper.GetHashEntry(enumType);
			values = hashEntry.Values;
			if (values != null)
			{
				getValues = false;
			}
			names = hashEntry.Names;
			if (names != null)
			{
				getNames = false;
			}
			if (!getValues && !getNames)
			{
				return;
			}
			EnumHelper.GetEnumValuesAndNames(enumType, ref values, ref names, getValues, getNames);
			if (getValues)
			{
				hashEntry.Values = values;
			}
			if (getNames)
			{
				hashEntry.Names = names;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003820 File Offset: 0x00001A20
		private static void GetEnumValuesAndNames(IEdmEnumType enumType, ref ulong[] values, ref string[] names, bool getValues, bool getNames)
		{
			Dictionary<string, ulong> dictionary = new Dictionary<string, ulong>();
			foreach (IEdmEnumMember edmEnumMember in enumType.Members)
			{
				IEdmEnumMemberValue value = edmEnumMember.Value;
				if (value != null)
				{
					dictionary.Add(edmEnumMember.Name, (ulong)value.Value);
				}
			}
			Dictionary<string, ulong> dictionary2 = dictionary.OrderBy((KeyValuePair<string, ulong> d) => d.Value).ToDictionary((KeyValuePair<string, ulong> d) => d.Key, (KeyValuePair<string, ulong> d) => d.Value);
			values = dictionary2.Select((KeyValuePair<string, ulong> d) => d.Value).ToArray<ulong>();
			names = dictionary2.Select((KeyValuePair<string, ulong> d) => d.Key).ToArray<string>();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000394C File Offset: 0x00001B4C
		private static EnumHelper.HashEntry GetHashEntry(IEdmEnumType enumType)
		{
			EnumHelper.HashEntry hashEntry;
			try
			{
				hashEntry = EnumHelper.fieldInfoHash.GetOrAdd(enumType, (IEdmEnumType type) => new EnumHelper.HashEntry(null, null));
			}
			catch (OverflowException)
			{
				EnumHelper.fieldInfoHash.Clear();
				hashEntry = EnumHelper.fieldInfoHash.GetOrAdd(enumType, (IEdmEnumType type) => new EnumHelper.HashEntry(null, null));
			}
			return hashEntry;
		}

		// Token: 0x0400004F RID: 79
		private const int MaxHashElements = 100;

		// Token: 0x04000050 RID: 80
		private static readonly ConcurrentDictionary<IEdmEnumType, EnumHelper.HashEntry> fieldInfoHash = new ConcurrentDictionary<IEdmEnumType, EnumHelper.HashEntry>(4, 100);

		// Token: 0x0200020A RID: 522
		private class HashEntry
		{
			// Token: 0x06000DF9 RID: 3577 RVA: 0x00026644 File Offset: 0x00024844
			public HashEntry(string[] names, ulong[] values)
			{
				this.Names = names;
				this.Values = values;
			}

			// Token: 0x040007B3 RID: 1971
			public string[] Names;

			// Token: 0x040007B4 RID: 1972
			public ulong[] Values;
		}
	}
}
