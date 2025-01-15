using System;
using System.ComponentModel;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000007 RID: 7
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpResponseMessageExtensions
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000255C File Offset: 0x0000075C
		public static bool TryGetContentValue<T>(this HttpResponseMessage response, out T value)
		{
			if (response == null)
			{
				throw Error.ArgumentNull("response");
			}
			ObjectContent objectContent = response.Content as ObjectContent;
			if (objectContent != null && objectContent.Value is T)
			{
				value = (T)((object)objectContent.Value);
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025AE File Offset: 0x000007AE
		internal static void EnsureResponseHasRequest(this HttpResponseMessage response, HttpRequestMessage request)
		{
			if (response != null && response.RequestMessage == null)
			{
				response.RequestMessage = request;
			}
		}
	}
}
