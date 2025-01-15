using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200001E RID: 30
	public class TrustedWebApiProxy<ReturnType>
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00004008 File Offset: 0x00002208
		public TrustedWebApiProxy(string name, Uri uri)
		{
			this._name = name;
			this._uri = uri;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004020 File Offset: 0x00002220
		public async Task<ReturnType> Execute()
		{
			ReturnType returnType;
			using (ImpersonationContext.EnterServiceAccountContext())
			{
				WebRequest webRequest = WebRequest.Create(this._uri);
				webRequest.UseDefaultCredentials = true;
				TaskAwaiter<WebResponse> taskAwaiter = webRequest.GetResponseAsync().GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<WebResponse> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<WebResponse>);
				}
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)taskAwaiter.GetResult())
				{
					string text = new StreamReader(httpWebResponse.GetResponseStream()).ReadToEnd();
					if (httpWebResponse.StatusCode != HttpStatusCode.OK)
					{
						string text2 = string.Format("TrustedWebApi failed. Status code {0}", httpWebResponse.StatusCode);
						Logger.Error(text2, Array.Empty<object>());
						throw new WebApiException(text2, httpWebResponse.StatusCode);
					}
					returnType = JsonConvert.DeserializeObject<ReturnType>(text);
				}
			}
			return returnType;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004065 File Offset: 0x00002265
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0400007B RID: 123
		private readonly string _name;

		// Token: 0x0400007C RID: 124
		private readonly Uri _uri;
	}
}
