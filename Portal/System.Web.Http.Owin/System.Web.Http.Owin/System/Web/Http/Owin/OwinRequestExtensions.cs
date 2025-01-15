using System;
using System.Collections.Generic;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
	// Token: 0x02000012 RID: 18
	internal static class OwinRequestExtensions
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00003604 File Offset: 0x00001804
		public static void DisableBuffering(this IOwinRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			IDictionary<string, object> environment = request.Environment;
			if (environment == null)
			{
				return;
			}
			Action action;
			if (!environment.TryGetValue("server.DisableRequestBuffering", out action))
			{
				return;
			}
			action();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003640 File Offset: 0x00001840
		public static int? GetContentLength(this IOwinRequest request)
		{
			IHeaderDictionary headers = request.Headers;
			if (headers == null)
			{
				return null;
			}
			string[] array;
			if (!headers.TryGetValue("Content-Length", out array))
			{
				return null;
			}
			if (array == null || array.Length != 1)
			{
				return null;
			}
			string text = array[0];
			if (text == null)
			{
				return null;
			}
			int num;
			if (!int.TryParse(text, out num))
			{
				return null;
			}
			if (num < 0)
			{
				return null;
			}
			return new int?(num);
		}

		// Token: 0x0400002D RID: 45
		private const string ContentLengthHeaderName = "Content-Length";

		// Token: 0x0400002E RID: 46
		private const string DisableRequestBufferingKey = "server.DisableRequestBuffering";
	}
}
