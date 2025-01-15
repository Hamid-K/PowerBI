using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public struct StringWrapper : IString
	{
		// Token: 0x06000084 RID: 132 RVA: 0x000028D2 File Offset: 0x00000AD2
		public StringWrapper(string s)
		{
			this.m_str = s;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000028DB File Offset: 0x00000ADB
		public int Length
		{
			get
			{
				return this.m_str.Length;
			}
		}

		// Token: 0x1700000D RID: 13
		public char this[int i]
		{
			get
			{
				return this.m_str.get_Chars(i);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000028F6 File Offset: 0x00000AF6
		public static implicit operator string(StringWrapper s)
		{
			return s.m_str;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000028FE File Offset: 0x00000AFE
		public static implicit operator StringWrapper(string s)
		{
			return new StringWrapper(s);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002908 File Offset: 0x00000B08
		public static int Compare<S, T>(S s, int s_start, T t, int t_start, int len) where S : IString where T : IString
		{
			int num = s_start;
			int num2 = t_start;
			int i = 0;
			while (i < len)
			{
				if (num < s.Length && num2 < t.Length)
				{
					if (s[num] != t[num2])
					{
						if (s[num] >= t[num2])
						{
							return 1;
						}
						return -1;
					}
				}
				else
				{
					if (num > s.Length)
					{
						return -1;
					}
					if (num2 > t.Length)
					{
						return 1;
					}
				}
				i++;
				num++;
				num2++;
			}
			return 0;
		}

		// Token: 0x04000017 RID: 23
		private readonly string m_str;
	}
}
