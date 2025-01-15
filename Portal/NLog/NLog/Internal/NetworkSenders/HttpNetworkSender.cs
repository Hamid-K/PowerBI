using System;
using System.IO;
using System.Net;
using NLog.Common;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x02000152 RID: 338
	internal class HttpNetworkSender : NetworkSender
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00029B51 File Offset: 0x00027D51
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x00029B59 File Offset: 0x00027D59
		internal IWebRequestFactory WebRequestFactory { get; set; } = new WebRequestFactory();

		// Token: 0x0600101C RID: 4124 RVA: 0x00029B62 File Offset: 0x00027D62
		public HttpNetworkSender(string url)
			: base(url)
		{
			this._addressUri = new Uri(base.Address);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00029B88 File Offset: 0x00027D88
		protected override void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			WebRequest webRequest = this.WebRequestFactory.CreateWebRequest(this._addressUri);
			webRequest.Method = "POST";
			AsyncCallback onResponse = delegate(IAsyncResult r)
			{
				try
				{
					using (webRequest.EndGetResponse(r))
					{
					}
					asyncContinuation(null);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
					asyncContinuation(ex);
				}
			};
			AsyncCallback asyncCallback = delegate(IAsyncResult r)
			{
				try
				{
					using (Stream stream = webRequest.EndGetRequestStream(r))
					{
						stream.Write(bytes, offset, length);
					}
					webRequest.BeginGetResponse(onResponse, null);
				}
				catch (Exception ex2)
				{
					if (ex2.MustBeRethrown())
					{
						throw;
					}
					asyncContinuation(ex2);
				}
			};
			webRequest.BeginGetRequestStream(asyncCallback, null);
		}

		// Token: 0x04000454 RID: 1108
		private readonly Uri _addressUri;
	}
}
