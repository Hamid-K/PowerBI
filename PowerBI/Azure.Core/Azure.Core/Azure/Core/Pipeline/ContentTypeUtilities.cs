﻿using System;
using System.Text;

namespace Azure.Core.Pipeline
{
	// Token: 0x020000A5 RID: 165
	internal static class ContentTypeUtilities
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x0000FD90 File Offset: 0x0000DF90
		public static bool TryGetTextEncoding(string contentType, out Encoding encoding)
		{
			if (contentType == null)
			{
				encoding = null;
				return false;
			}
			int num = contentType.IndexOf("; charset=", StringComparison.OrdinalIgnoreCase);
			if (num != -1 && MemoryExtensions.StartsWith(MemoryExtensions.AsSpan(contentType).Slice(num + "; charset=".Length), MemoryExtensions.AsSpan("utf-8"), StringComparison.OrdinalIgnoreCase))
			{
				encoding = Encoding.UTF8;
				return true;
			}
			if (contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase) || contentType.EndsWith("json", StringComparison.OrdinalIgnoreCase) || contentType.EndsWith("xml", StringComparison.OrdinalIgnoreCase) || contentType.EndsWith("-urlencoded", StringComparison.OrdinalIgnoreCase) || contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase) || contentType.StartsWith("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
			{
				encoding = Encoding.UTF8;
				return true;
			}
			encoding = null;
			return false;
		}
	}
}
