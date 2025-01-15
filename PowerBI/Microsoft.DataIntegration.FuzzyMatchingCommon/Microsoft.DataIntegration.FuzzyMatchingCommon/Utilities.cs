using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public static class Utilities
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		public static int GetHashCode(char[] buffer, int start, int len)
		{
			if (len <= 0 || buffer == null)
			{
				return 0;
			}
			int num = len & 1;
			len >>= 1;
			int num2 = start;
			uint num3 = (uint)len;
			while (len > 0)
			{
				num3 += (uint)buffer[num2++];
				uint num4 = (uint)((uint)buffer[num2++] << 11) ^ num3;
				num3 = (num3 << 16) ^ num4;
				num3 += num3 >> 11;
				len--;
			}
			if (num == 1)
			{
				num3 += (uint)buffer[num2];
				num3 ^= num3 << 10;
				num3 += num3 >> 1;
			}
			num3 ^= num3 << 3;
			num3 += num3 >> 5;
			num3 ^= num3 << 4;
			num3 += num3 >> 17;
			num3 ^= num3 << 25;
			return (int)(num3 + (num3 >> 6));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000F249 File Offset: 0x0000D449
		public static int GetHashCode(long i)
		{
			return Utilities.GetHashCode((int)i, (int)(i >> 32));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000F258 File Offset: 0x0000D458
		public static int GetHashCode(int i1, int i2)
		{
			int num = i1 + ~(i1 << 15);
			int num2 = num ^ (num >> 10);
			int num3 = num2 + (num2 << 3);
			int num4 = num3 ^ (num3 >> 6);
			int num5 = num4 + ~(num4 << 11);
			int num6 = (num5 ^ (num5 >> 16)) + i2;
			int num7 = num6 + ~(num6 << 15);
			int num8 = num7 ^ (num7 >> 10);
			int num9 = num8 + (num8 << 3);
			int num10 = num9 ^ (num9 >> 6);
			int num11 = num10 + ~(num10 << 11);
			return num11 ^ (num11 >> 16);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000F2A4 File Offset: 0x0000D4A4
		public static int GetHashCode(int key)
		{
			key += ~(key << 15);
			key ^= key >> 10;
			key += key << 3;
			key ^= key >> 6;
			key += ~(key << 11);
			key ^= key >> 16;
			return key;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000F2D7 File Offset: 0x0000D4D7
		public static long Int32ToInt64(int k1, int k2)
		{
			return (long)(((ulong)k1 << 32) | (ulong)k2);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000F2E1 File Offset: 0x0000D4E1
		public static int SizeOf(Type t)
		{
			if (t == typeof(char))
			{
				return 2;
			}
			return Marshal.SizeOf(t);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		public static long GetMemoryUsage(object obj)
		{
			long num;
			if (obj is IMemoryUsage)
			{
				num = (obj as IMemoryUsage).MemoryUsage;
			}
			else if (obj is string)
			{
				num = (long)((obj as string).Length * 2);
			}
			else if (obj is char[])
			{
				num = (long)((obj as char[]).Length * 2);
			}
			else
			{
				if (obj is Guid)
				{
					return 64L;
				}
				if (obj is DateTime)
				{
					return 64L;
				}
				Type type = obj.GetType();
				if (type.IsPrimitive)
				{
					num = 8L;
				}
				else if (type.IsArray)
				{
					num = (long)((obj as Array).Length * 8);
				}
				else
				{
					num = 8L;
				}
			}
			return 16L + num;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000F398 File Offset: 0x0000D598
		public static string GenerateUniqueSuffix()
		{
			DateTime now = DateTime.Now;
			string text = Guid.NewGuid().ToString().Replace('-', '_');
			return string.Format("_{0:0000}{1:00}{2:00}_{3:00}{4:00}{5:00}_{6}", new object[] { now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, text });
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000F438 File Offset: 0x0000D638
		public static string GenerateUniqueName(string prefix, int maxLength)
		{
			string text = Utilities.GenerateUniqueSuffix();
			int num = Math.Min(prefix.Length, maxLength - text.Length);
			return prefix.Substring(0, num) + text;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000F470 File Offset: 0x0000D670
		public static DataTable CloneDataTable(DataTable table)
		{
			DataTable dataTable = table.Clone();
			using (IDataReader dataReader = table.CreateDataReader())
			{
				while (dataReader.Read())
				{
					object[] array = new object[dataReader.FieldCount];
					dataReader.GetValues(array);
					dataTable.Rows.Add(array);
				}
			}
			return dataTable;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
		public static void PrintDataTableToConsole(DataTable dt)
		{
			using (DataTableReader dataTableReader = new DataTableReader(dt))
			{
				Utilities.PrintColumnNamesToConsole(dataTableReader.GetSchemaTable());
				Utilities.PrintDataReaderToConsole(dataTableReader);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000F518 File Offset: 0x0000D718
		public static void PrintColumnNamesToConsole(DataTable schemaTable)
		{
			string text = string.Empty;
			int num = 1;
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				text += string.Format("<{0}> {1} ", num++, dataRow[SchemaTableColumn.ColumnName].ToString());
			}
			Console.WriteLine(text);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		public static string ToString<T>(IEnumerable<T> list, string delimiter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (T t in list)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(delimiter);
				}
				stringBuilder.Append(t.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000F61C File Offset: 0x0000D81C
		public static void PrintDataReaderToConsole(IDataReader rdr)
		{
			string empty = string.Empty;
			int num = 0;
			while (rdr.Read())
			{
				Console.Write(string.Format("{0}: ", num++));
				Utilities.PrintDataRecordToConsole(rdr);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000F65C File Offset: 0x0000D85C
		public static void PrintDataRecordToConsole(IDataRecord record)
		{
			string text = string.Empty;
			for (int i = 0; i < record.FieldCount; i++)
			{
				string text2 = "(null)";
				if (!record.IsDBNull(i))
				{
					text2 = record.GetValue(i).ToString();
				}
				text += string.Format("<{0}> {1} ", i, text2);
			}
			Console.WriteLine(text);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000F6BC File Offset: 0x0000D8BC
		public static bool EnumParse<T, U>(string enumString, out T enumValue)
		{
			enumValue = default(T);
			string[] names = Enum.GetNames(typeof(T));
			Array values = Enum.GetValues(typeof(T));
			int num = Array.FindIndex<string>(names, (string s) => s.CompareTo(enumString) == 0);
			if (num < 0)
			{
				int num2 = 0;
				foreach (object obj in values)
				{
					U u = (U)((object)obj);
					if (u.ToString().CompareTo(enumString) == 0)
					{
						num = num2;
						break;
					}
					num2++;
				}
			}
			if (num > 0)
			{
				enumValue = (T)((object)values.GetValue(num));
				return true;
			}
			return false;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000F798 File Offset: 0x0000D998
		public static void WriteInt(byte[] b, int i)
		{
			b[0] = (byte)(i >> 24);
			b[1] = (byte)(i >> 16);
			b[2] = (byte)(i >> 8);
			b[3] = (byte)i;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000F7B8 File Offset: 0x0000D9B8
		public static void WriteInt(byte[] b, ref int idx, int i)
		{
			int num = idx;
			idx = num + 1;
			b[num] = (byte)(i >> 24);
			num = idx;
			idx = num + 1;
			b[num] = (byte)(i >> 16);
			num = idx;
			idx = num + 1;
			b[num] = (byte)(i >> 8);
			num = idx;
			idx = num + 1;
			b[num] = (byte)i;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000F801 File Offset: 0x0000DA01
		public static int ReadInt(byte[] b)
		{
			return ((int)b[0] << 24) | ((int)b[1] << 16) | ((int)b[2] << 8) | (int)b[3];
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000F81C File Offset: 0x0000DA1C
		public static int ReadInt(byte[] b, ref int idx)
		{
			int num = idx;
			idx = num + 1;
			int num2 = (int)b[num] << 24;
			num = idx;
			idx = num + 1;
			int num3 = num2 | ((int)b[num] << 16);
			num = idx;
			idx = num + 1;
			int num4 = num3 | ((int)b[num] << 8);
			num = idx;
			idx = num + 1;
			return num4 | (int)b[num];
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000F860 File Offset: 0x0000DA60
		public static bool IsVowel(char c)
		{
			if (c <= 'U')
			{
				if (c <= 'E')
				{
					if (c != 'A' && c != 'E')
					{
						return false;
					}
				}
				else if (c != 'I' && c != 'O' && c != 'U')
				{
					return false;
				}
			}
			else if (c <= 'e')
			{
				if (c != 'a' && c != 'e')
				{
					return false;
				}
			}
			else if (c != 'i' && c != 'o' && c != 'u')
			{
				return false;
			}
			return true;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000F8B8 File Offset: 0x0000DAB8
		public static bool IsPrefix(ArraySegment<char> token1, ArraySegment<char> token2, bool foldVowels, out int prefixMatchLength, out int maxLength)
		{
			prefixMatchLength = 0;
			maxLength = Math.Max(token1.Count, token2.Count);
			int num = 0;
			int num2 = 0;
			while (num < token1.Count && num2 < token2.Count)
			{
				if (token1.Array[token1.Offset + num] != token2.Array[token2.Offset + num2])
				{
					if (foldVowels)
					{
						bool flag = Utilities.IsVowel(token1.Array[token1.Offset + num]);
						bool flag2 = Utilities.IsVowel(token2.Array[token2.Offset + num2]);
						if (flag && !flag2)
						{
							num++;
							continue;
						}
						if (flag2 && !flag)
						{
							num2++;
							continue;
						}
						if (flag && flag2)
						{
							int num3 = 1;
							int num4 = 1;
							int num5 = num + 1;
							while (num5 < token1.Count && Utilities.IsVowel(token1.Array[token1.Offset + num5]))
							{
								num5++;
								num3++;
							}
							int num6 = num2 + 1;
							while (num6 < token2.Count && Utilities.IsVowel(token2.Array[token2.Offset + num6]))
							{
								num6++;
								num4++;
							}
							prefixMatchLength += Utilities.LongestCommonSubsequence(new ArraySegment<char>(token1.Array, token1.Offset + num, num3), new ArraySegment<char>(token2.Array, token2.Offset + num2, num4));
							num += num3;
							num2 += num4;
							continue;
						}
					}
					return false;
				}
				num++;
				num2++;
				prefixMatchLength++;
			}
			return num == token1.Count;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000FA54 File Offset: 0x0000DC54
		public static int LongestCommonSubsequence(ArraySegment<char> s1, ArraySegment<char> s2)
		{
			if (s1.Count == 0 || s2.Count == 0)
			{
				return 0;
			}
			if (s1.Array[s1.Offset + s1.Count - 1] == s2.Array[s2.Offset + s2.Count - 1])
			{
				return 1 + Utilities.LongestCommonSubsequence(new ArraySegment<char>(s1.Array, s1.Offset, s1.Count - 1), new StringExtent(s2.Array, s2.Offset, s2.Count - 1));
			}
			return Math.Max(Utilities.LongestCommonSubsequence(new ArraySegment<char>(s1.Array, s1.Offset, s1.Count), new ArraySegment<char>(s2.Array, s2.Offset, s2.Count - 1)), Utilities.LongestCommonSubsequence(new ArraySegment<char>(s1.Array, s1.Offset, s1.Count - 1), new ArraySegment<char>(s2.Array, s2.Offset, s2.Count)));
		}

		// Token: 0x020000C9 RID: 201
		private sealed class CharArrayEqualityComparer : IEqualityComparer<char[]>
		{
			// Token: 0x0600086F RID: 2159 RVA: 0x0002BBF4 File Offset: 0x00029DF4
			public int Compare(char[] x, char[] y)
			{
				int num = Math.Min(x.Length, y.Length);
				int i = 0;
				while (i < num)
				{
					if (x[i] != y[i])
					{
						if (x[i] >= y[i])
						{
							return 1;
						}
						return -1;
					}
					else
					{
						i++;
					}
				}
				if (x.Length == y.Length)
				{
					return 0;
				}
				if (x.Length >= y.Length)
				{
					return 1;
				}
				return -1;
			}

			// Token: 0x06000870 RID: 2160 RVA: 0x0002BC42 File Offset: 0x00029E42
			public bool Equals(char[] x, char[] y)
			{
				return this.Compare(x, y) == 0;
			}

			// Token: 0x06000871 RID: 2161 RVA: 0x0002BC4F File Offset: 0x00029E4F
			public int GetHashCode(char[] x)
			{
				return Utilities.GetHashCode(x, 0, x.Length);
			}

			// Token: 0x040001CB RID: 459
			public static readonly Utilities.CharArrayEqualityComparer Instance = new Utilities.CharArrayEqualityComparer();
		}
	}
}
