using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	internal static class StringUtils
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000022F9 File Offset: 0x000004F9
		internal static string Copy(string source)
		{
			if (source == null)
			{
				return null;
			}
			return string.Copy(source);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002308 File Offset: 0x00000508
		public static string ReplaceStringCaseInsensitive(string original, string pattern, string replacement)
		{
			if (original == null || pattern == null)
			{
				return original;
			}
			if (pattern.Length == 0)
			{
				if (original.Length == 0)
				{
					return replacement;
				}
				return original;
			}
			else
			{
				if (replacement == null)
				{
					replacement = string.Empty;
				}
				int num = 0;
				int num2 = 0;
				char[] array = null;
				int num3;
				while ((num3 = original.IndexOf(pattern, num2, StringComparison.OrdinalIgnoreCase)) != -1)
				{
					if (array == null)
					{
						int num4 = original.Length / pattern.Length * (replacement.Length - pattern.Length);
						array = new char[original.Length + Math.Max(0, num4)];
					}
					for (int i = num2; i < num3; i++)
					{
						array[num++] = original[i];
					}
					for (int j = 0; j < replacement.Length; j++)
					{
						array[num++] = replacement[j];
					}
					num2 = num3 + pattern.Length;
				}
				if (num2 == 0)
				{
					return original;
				}
				for (int k = num2; k < original.Length; k++)
				{
					array[num++] = original[k];
				}
				return new string(array, 0, num);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000240C File Offset: 0x0000060C
		internal static bool IsValidJson(this string json)
		{
			bool flag = true;
			try
			{
				JToken.Parse(json);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000243C File Offset: 0x0000063C
		internal static string Truncate(string original, int length)
		{
			if (original == null || original.Length <= length)
			{
				return original;
			}
			return original.Substring(0, length);
		}
	}
}
