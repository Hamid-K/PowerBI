using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	[StructLayout(0, Pack = 2)]
	public struct StringExtent : IEquatable<StringExtent>, IMemoryUsage, IString
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000029B9 File Offset: 0x00000BB9
		public StringExtent(string s)
		{
			this = new StringExtent(s.ToCharArray(), 0, s.Length);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000029CE File Offset: 0x00000BCE
		public StringExtent(char[] str)
		{
			this = new StringExtent(str, 0, str.Length);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000029DB File Offset: 0x00000BDB
		public StringExtent(ArraySegment<char> str)
		{
			this = new StringExtent(str.Array, str.Offset, str.Count);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000029F8 File Offset: 0x00000BF8
		public StringExtent(char[] str, int start, int length)
		{
			if (start > 2147483647 || length > 2147483647)
			{
				throw new ArgumentOutOfRangeException("The start or length specified to StringExtent goes beyond the maximum value of " + int.MaxValue + ".");
			}
			this.m_array = str;
			this.m_offset = start;
			this.m_count = length;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002A49 File Offset: 0x00000C49
		public long MemoryUsage
		{
			get
			{
				return (long)(this.Length * 2 + 8 + 4);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002A58 File Offset: 0x00000C58
		public static implicit operator StringExtent(ArraySegment<char> segment)
		{
			return new StringExtent(segment);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002A60 File Offset: 0x00000C60
		public static implicit operator ArraySegment<char>(StringExtent extent)
		{
			return new ArraySegment<char>(extent.Array, extent.Offset, extent.Length);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002A7C File Offset: 0x00000C7C
		public char[] ToArray()
		{
			char[] array = new char[this.m_count];
			global::System.Array.ConstrainedCopy(this.m_array, this.m_offset, array, 0, this.m_count);
			return array;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002AAF File Offset: 0x00000CAF
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002AB7 File Offset: 0x00000CB7
		public char[] Array
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_array;
			}
			set
			{
				this.m_array = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002AC0 File Offset: 0x00000CC0
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public int Offset
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_offset;
			}
			set
			{
				this.m_offset = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002AD1 File Offset: 0x00000CD1
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002AD9 File Offset: 0x00000CD9
		public int Length
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_count;
			}
			set
			{
				this.m_count = value;
			}
		}

		// Token: 0x17000012 RID: 18
		public char this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return this.Array[this.Offset + index];
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002AF3 File Offset: 0x00000CF3
		public override string ToString()
		{
			if (this.Array == null)
			{
				return string.Empty;
			}
			return new string(this.m_array, this.Offset, this.Length);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002B1A File Offset: 0x00000D1A
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (!this.IsEmpty && count > 0)
			{
				global::System.Array.ConstrainedCopy(this.m_array, this.Offset, destination, destinationIndex, count);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002B40 File Offset: 0x00000D40
		public StringExtent AllocClone(ISegmentAllocator<char> extentAllocator)
		{
			ArraySegment<char> arraySegment = extentAllocator.New(this.Length);
			if (!this.IsEmpty && this.Length > 0)
			{
				global::System.Array.ConstrainedCopy(this.m_array, this.Offset, arraySegment.Array, arraySegment.Offset, this.Length);
			}
			return new StringExtent(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002BAC File Offset: 0x00000DAC
		public StringExtent AllocClone()
		{
			StringExtent stringExtent = new StringExtent(new char[this.Length], 0, this.Length);
			if (!this.IsEmpty && this.Length > 0)
			{
				global::System.Array.ConstrainedCopy(this.m_array, this.Offset, stringExtent.m_array, stringExtent.Offset, this.Length);
			}
			return stringExtent;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002C08 File Offset: 0x00000E08
		public StringExtent SubExtent(int startIndex, int length)
		{
			if (this.Offset + startIndex + length > this.m_array.Length)
			{
				throw new ArgumentOutOfRangeException("The length specified to SubExtent goes beyond the length of the underlying string.");
			}
			return new StringExtent(this.Array, this.Offset + startIndex, length);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002C3D File Offset: 0x00000E3D
		public override int GetHashCode()
		{
			return Utilities.GetHashCode(this.Array, this.Offset, this.Length);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002C56 File Offset: 0x00000E56
		public override bool Equals(object obj)
		{
			return obj is StringExtent && this.Equals((StringExtent)obj);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002C70 File Offset: 0x00000E70
		public bool Equals(StringExtent strB)
		{
			if (this.Length != strB.Length)
			{
				return false;
			}
			if (this.Array != strB.Array || this.Offset != strB.Offset)
			{
				int num = this.Offset + this.Length;
				int i = this.Offset;
				int offset = strB.Offset;
				char[] array = this.m_array;
				char[] array2 = strB.Array;
				while (i < num)
				{
					if (array[i++] != array2[offset++])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002CF3 File Offset: 0x00000EF3
		public bool Equals(string strB)
		{
			return this.CompareTo(strB) == 0;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002CFF File Offset: 0x00000EFF
		public static int Compare(StringExtent x, StringExtent y)
		{
			return x.CompareTo(y);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002D0C File Offset: 0x00000F0C
		public int CompareTo(StringExtent strB)
		{
			int num = 0;
			char[] array = this.m_array;
			char[] array2 = strB.Array;
			if (array2 != array || strB.Offset != this.Offset || strB.Length != this.Length)
			{
				int num2 = this.Offset;
				int num3 = num2 + this.Length;
				int num4 = strB.Offset;
				int num5 = num4 + strB.Length;
				while (num2 < num3 && num4 < num5)
				{
					if (array[num2] != array2[num4])
					{
						num = ((array[num2] < array2[num4]) ? (-1) : 1);
						break;
					}
					num2++;
					num4++;
				}
				if (num == 0)
				{
					if (num2 < num3)
					{
						num = -1;
					}
					if (num4 < num5)
					{
						num = 1;
					}
				}
			}
			return num;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public int CompareTo(string strB)
		{
			int num = 0;
			char[] array = this.m_array;
			int num2 = this.Offset;
			int num3 = num2 + this.Length;
			int num4 = 0;
			int length = strB.Length;
			while (num2 < num3 && num4 < length)
			{
				if (array[num2] != strB.get_Chars(num4))
				{
					num = ((array[num2] < strB.get_Chars(num4)) ? (-1) : 1);
					break;
				}
				num2++;
				num4++;
			}
			if (num == 0)
			{
				if (num2 < num3)
				{
					num = -1;
				}
				if (num4 < length)
				{
					num = 1;
				}
			}
			return num;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002E32 File Offset: 0x00001032
		public bool IsEmpty
		{
			get
			{
				return this.m_array == null || this.Length == 0;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002E48 File Offset: 0x00001048
		public void Reset()
		{
			this.m_array = null;
			this.Offset = (this.Length = 0);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002E6C File Offset: 0x0000106C
		public void RTrim(char[] trimCharacters)
		{
			int num = this.Length;
			int num2 = trimCharacters.Length;
			int i = 0;
			int num3 = this.Offset + this.Length - 1;
			while (num3 >= this.Offset && i < num2)
			{
				while (i < num2)
				{
					if (this.m_array[num3] == trimCharacters[i])
					{
						num--;
						i = 0;
						break;
					}
					i++;
				}
				num3--;
			}
			this.Length = num;
		}

		// Token: 0x04000018 RID: 24
		public static readonly StringExtentEqualityComparer EqualityComparer = StringExtentEqualityComparer.Instance;

		// Token: 0x04000019 RID: 25
		public const int SizeOf = 16;

		// Token: 0x0400001A RID: 26
		public const int MaxLength = 2147483647;

		// Token: 0x0400001B RID: 27
		private char[] m_array;

		// Token: 0x0400001C RID: 28
		private int m_offset;

		// Token: 0x0400001D RID: 29
		private int m_count;
	}
}
