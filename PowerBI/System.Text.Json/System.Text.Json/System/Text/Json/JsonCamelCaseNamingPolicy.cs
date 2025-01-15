using System;

namespace System.Text.Json
{
	// Token: 0x0200002C RID: 44
	internal sealed class JsonCamelCaseNamingPolicy : JsonNamingPolicy
	{
		// Token: 0x0600021C RID: 540 RVA: 0x000052B8 File Offset: 0x000034B8
		public override string ConvertName(string name)
		{
			if (string.IsNullOrEmpty(name) || !char.IsUpper(name[0]))
			{
				return name;
			}
			char[] array = name.ToCharArray();
			JsonCamelCaseNamingPolicy.FixCasing(array);
			return new string(array);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000052F8 File Offset: 0x000034F8
		private unsafe static void FixCasing(Span<char> chars)
		{
			int num = 0;
			while (num < chars.Length && (num != 1 || char.IsUpper(*chars[num])))
			{
				bool flag = num + 1 < chars.Length;
				if (num > 0 && flag && !char.IsUpper(*chars[num + 1]))
				{
					if (*chars[num + 1] == ' ')
					{
						*chars[num] = char.ToLowerInvariant(*chars[num]);
						return;
					}
					break;
				}
				else
				{
					*chars[num] = char.ToLowerInvariant(*chars[num]);
					num++;
				}
			}
		}
	}
}
