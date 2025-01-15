using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200006B RID: 107
	internal static class AuthorizationChallengeParser
	{
		// Token: 0x06000399 RID: 921 RVA: 0x0000A9DC File Offset: 0x00008BDC
		[NullableContext(1)]
		[return: Nullable(2)]
		public static string GetChallengeParameterFromResponse(Response response, string challengeScheme, string challengeParameter)
		{
			string text;
			if (response.Status != 401 || !response.Headers.TryGetValue(HttpHeader.Names.WwwAuthenticate, out text))
			{
				return null;
			}
			ReadOnlySpan<char> readOnlySpan = MemoryExtensions.AsSpan(challengeScheme);
			ReadOnlySpan<char> readOnlySpan2 = MemoryExtensions.AsSpan(challengeParameter);
			ReadOnlySpan<char> readOnlySpan3 = MemoryExtensions.AsSpan(text);
			ReadOnlySpan<char> readOnlySpan4;
			while (AuthorizationChallengeParser.TryGetNextChallenge(ref readOnlySpan3, out readOnlySpan4))
			{
				ReadOnlySpan<char> readOnlySpan5;
				ReadOnlySpan<char> readOnlySpan6;
				while (AuthorizationChallengeParser.TryGetNextParameter(ref readOnlySpan3, out readOnlySpan5, out readOnlySpan6, '='))
				{
					if (MemoryExtensions.Equals(readOnlySpan4, readOnlySpan, StringComparison.OrdinalIgnoreCase) && MemoryExtensions.Equals(readOnlySpan5, readOnlySpan2, StringComparison.OrdinalIgnoreCase))
					{
						return readOnlySpan6.ToString();
					}
				}
			}
			return null;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000AA64 File Offset: 0x00008C64
		internal static bool TryGetNextChallenge(ref ReadOnlySpan<char> headerValue, out ReadOnlySpan<char> challengeKey)
		{
			challengeKey = default(ReadOnlySpan<char>);
			headerValue = MemoryExtensions.TrimStart(headerValue, ' ');
			int num = MemoryExtensions.IndexOf<char>(headerValue, ' ');
			if (num < 0)
			{
				return false;
			}
			challengeKey = headerValue.Slice(0, num);
			headerValue = headerValue.Slice(num + 1);
			return true;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		internal static bool TryGetNextParameter(ref ReadOnlySpan<char> headerValue, out ReadOnlySpan<char> paramKey, out ReadOnlySpan<char> paramValue, char separator = '=')
		{
			paramKey = default(ReadOnlySpan<char>);
			paramValue = default(ReadOnlySpan<char>);
			ReadOnlySpan<char> readOnlySpan = MemoryExtensions.AsSpan(" ,");
			headerValue = MemoryExtensions.TrimStart(headerValue, readOnlySpan);
			int num = MemoryExtensions.IndexOf<char>(headerValue, ' ');
			int num2 = MemoryExtensions.IndexOf<char>(headerValue, separator);
			if (num < num2 && num != -1)
			{
				return false;
			}
			if (num2 < 0)
			{
				return false;
			}
			paramKey = MemoryExtensions.Trim(headerValue.Slice(0, num2));
			headerValue = headerValue.Slice(num2 + 1);
			int num3 = MemoryExtensions.IndexOf<char>(headerValue, '"');
			headerValue = headerValue.Slice(num3 + 1);
			if (num3 >= 0)
			{
				paramValue = headerValue.Slice(0, MemoryExtensions.IndexOf<char>(headerValue, '"'));
			}
			else
			{
				int num4 = MemoryExtensions.IndexOfAny<char>(headerValue, readOnlySpan);
				if (num4 >= 0)
				{
					paramValue = headerValue.Slice(0, num4);
				}
				else
				{
					paramValue = headerValue;
				}
			}
			if (headerValue != paramValue)
			{
				headerValue = headerValue.Slice(paramValue.Length + 1);
			}
			return true;
		}
	}
}
