using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.OData.Edm.Library.Values;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000F7 RID: 247
	public static class EnumHelper
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
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
			if (char.IsDigit(value.get_Chars(0)) || value.get_Chars(0) == '-' || value.get_Chars(0) == '+')
			{
				ulong num2 = 0UL;
				for (int i = 0; i < array3.Length; i++)
				{
					num2 |= array3[i];
				}
				for (int j = 0; j < array2.Length; j++)
				{
					long num3;
					if (!long.TryParse(array2[j], ref num3))
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
							if (string.Compare(array4[l], array2[k], 5) == 0)
							{
								goto IL_010B;
							}
						}
						else if (array4[l].Equals(array2[k]))
						{
							goto IL_010B;
						}
						l++;
						continue;
						IL_010B:
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

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
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

		// Token: 0x060004DA RID: 1242 RVA: 0x0000CD38 File Offset: 0x0000AF38
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
			if (array.Length > 0 && array[0] == 0UL)
			{
				return array2[0];
			}
			return 0.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000CDEC File Offset: 0x0000AFEC
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

		// Token: 0x060004DC RID: 1244 RVA: 0x0000CE24 File Offset: 0x0000B024
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

		// Token: 0x060004DD RID: 1245 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		private static void GetEnumValuesAndNames(IEdmEnumType enumType, ref ulong[] values, ref string[] names, bool getValues, bool getNames)
		{
			Dictionary<string, ulong> dictionary = new Dictionary<string, ulong>();
			foreach (IEdmEnumMember edmEnumMember in enumType.Members)
			{
				EdmIntegerConstant edmIntegerConstant = edmEnumMember.Value as EdmIntegerConstant;
				if (edmIntegerConstant != null)
				{
					dictionary.Add(edmEnumMember.Name, (ulong)edmIntegerConstant.Value);
				}
			}
			Dictionary<string, ulong> dictionary2 = Enumerable.ToDictionary<KeyValuePair<string, ulong>, string, ulong>(Enumerable.OrderBy<KeyValuePair<string, ulong>, ulong>(dictionary, (KeyValuePair<string, ulong> d) => d.Value), (KeyValuePair<string, ulong> d) => d.Key, (KeyValuePair<string, ulong> d) => d.Value);
			values = Enumerable.ToArray<ulong>(Enumerable.Select<KeyValuePair<string, ulong>, ulong>(dictionary2, (KeyValuePair<string, ulong> d) => d.Value));
			names = Enumerable.ToArray<string>(Enumerable.Select<KeyValuePair<string, ulong>, string>(dictionary2, (KeyValuePair<string, ulong> d) => d.Key));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
		private static EnumHelper.HashEntry GetHashEntry(IEdmEnumType enumType)
		{
			if (EnumHelper.fieldInfoHash.Count > 100)
			{
				lock (EnumHelper.fieldInfoHash)
				{
					if (EnumHelper.fieldInfoHash.Count > 100)
					{
						EnumHelper.fieldInfoHash.Clear();
					}
				}
			}
			return EdmUtil.DictionaryGetOrUpdate<IEdmEnumType, EnumHelper.HashEntry>(EnumHelper.fieldInfoHash, enumType, (IEdmEnumType type) => new EnumHelper.HashEntry(null, null));
		}

		// Token: 0x040001D1 RID: 465
		private const int MaxHashElements = 100;

		// Token: 0x040001D2 RID: 466
		private static readonly IDictionary<IEdmEnumType, EnumHelper.HashEntry> fieldInfoHash = new Dictionary<IEdmEnumType, EnumHelper.HashEntry>();

		// Token: 0x020000F8 RID: 248
		private class HashEntry
		{
			// Token: 0x060004E6 RID: 1254 RVA: 0x0000D074 File Offset: 0x0000B274
			public HashEntry(string[] names, ulong[] values)
			{
				this.Names = names;
				this.Values = values;
			}

			// Token: 0x040001D9 RID: 473
			public string[] Names;

			// Token: 0x040001DA RID: 474
			public ulong[] Values;
		}
	}
}
