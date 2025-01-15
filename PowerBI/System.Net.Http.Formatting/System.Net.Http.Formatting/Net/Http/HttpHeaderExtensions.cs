using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	// Token: 0x0200001A RID: 26
	internal static class HttpHeaderExtensions
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00003C0C File Offset: 0x00001E0C
		public static void CopyTo(this HttpContentHeaders fromHeaders, HttpContentHeaders toHeaders)
		{
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in fromHeaders)
			{
				toHeaders.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}
