using System;

namespace antlr
{
	// Token: 0x02000023 RID: 35
	internal class StringUtils
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00004CB5 File Offset: 0x00002EB5
		public static string stripBack(string s, char c)
		{
			while (s.Length > 0 && s.get_Chars(s.Length - 1) == c)
			{
				s = s.Substring(0, s.Length - 1);
			}
			return s;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004CE8 File Offset: 0x00002EE8
		public static string stripBack(string s, string remove)
		{
			bool flag;
			do
			{
				flag = false;
				for (int i = 0; i < remove.Length; i++)
				{
					char c = remove.get_Chars(i);
					while (s.Length > 0 && s.get_Chars(s.Length - 1) == c)
					{
						flag = true;
						s = s.Substring(0, s.Length - 1);
					}
				}
			}
			while (flag);
			return s;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004D43 File Offset: 0x00002F43
		public static string stripFront(string s, char c)
		{
			while (s.Length > 0 && s.get_Chars(0) == c)
			{
				s = s.Substring(1);
			}
			return s;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004D64 File Offset: 0x00002F64
		public static string stripFront(string s, string remove)
		{
			bool flag;
			do
			{
				flag = false;
				for (int i = 0; i < remove.Length; i++)
				{
					char c = remove.get_Chars(i);
					while (s.Length > 0 && s.get_Chars(0) == c)
					{
						flag = true;
						s = s.Substring(1);
					}
				}
			}
			while (flag);
			return s;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public static string stripFrontBack(string src, string head, string tail)
		{
			int num = src.IndexOf(head, 4);
			int num2 = src.LastIndexOf(tail, 4);
			if (num == -1 || num2 == -1)
			{
				return src;
			}
			return src.Substring(num + 1, num2 - (num + 1));
		}
	}
}
