using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x02000010 RID: 16
	internal static class DefaultBehavior
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002960 File Offset: 0x00000B60
		private static bool IsAjaxRequest(IOwinRequest request)
		{
			IReadableStringCollection query = request.Query;
			if (query != null && query["X-Requested-With"] == "XMLHttpRequest")
			{
				return true;
			}
			IHeaderDictionary headers = request.Headers;
			return headers != null && headers["X-Requested-With"] == "XMLHttpRequest";
		}

		// Token: 0x0400003B RID: 59
		internal static readonly Action<CookieApplyRedirectContext> ApplyRedirect = delegate(CookieApplyRedirectContext context)
		{
			if (DefaultBehavior.IsAjaxRequest(context.Request))
			{
				DefaultBehavior.RespondedJson respondedJson = new DefaultBehavior.RespondedJson
				{
					Status = context.Response.StatusCode,
					Headers = new DefaultBehavior.RespondedJson.RespondedJsonHeaders
					{
						Location = context.RedirectUri
					}
				};
				context.Response.StatusCode = 200;
				context.Response.Headers.Append("X-Responded-JSON", respondedJson.ToString());
				return;
			}
			context.Response.Redirect(context.RedirectUri);
		};

		// Token: 0x02000017 RID: 23
		[DataContract]
		private class RespondedJson
		{
			// Token: 0x17000026 RID: 38
			// (get) Token: 0x0600007F RID: 127 RVA: 0x0000386A File Offset: 0x00001A6A
			// (set) Token: 0x06000080 RID: 128 RVA: 0x00003872 File Offset: 0x00001A72
			[DataMember(Name = "status", Order = 1)]
			public int Status { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000081 RID: 129 RVA: 0x0000387B File Offset: 0x00001A7B
			// (set) Token: 0x06000082 RID: 130 RVA: 0x00003883 File Offset: 0x00001A83
			[DataMember(Name = "headers", Order = 2)]
			public DefaultBehavior.RespondedJson.RespondedJsonHeaders Headers { get; set; }

			// Token: 0x06000083 RID: 131 RVA: 0x0000388C File Offset: 0x00001A8C
			public override string ToString()
			{
				string text;
				using (MemoryStream memory = new MemoryStream())
				{
					DefaultBehavior.RespondedJson.Serializer.WriteObject(memory, this);
					string responded = Encoding.ASCII.GetString(memory.ToArray());
					text = responded;
				}
				return text;
			}

			// Token: 0x0400005C RID: 92
			public static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(DefaultBehavior.RespondedJson));

			// Token: 0x02000019 RID: 25
			[DataContract]
			public class RespondedJsonHeaders
			{
				// Token: 0x17000028 RID: 40
				// (get) Token: 0x06000089 RID: 137 RVA: 0x00003995 File Offset: 0x00001B95
				// (set) Token: 0x0600008A RID: 138 RVA: 0x0000399D File Offset: 0x00001B9D
				[DataMember(Name = "location", Order = 1)]
				public string Location { get; set; }
			}
		}
	}
}
