using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Handlers
{
	// Token: 0x0200002A RID: 42
	public class ProgressMessageHandler : DelegatingHandler
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00005ABA File Offset: 0x00003CBA
		public ProgressMessageHandler()
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005AC2 File Offset: 0x00003CC2
		public ProgressMessageHandler(HttpMessageHandler innerHandler)
			: base(innerHandler)
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000190 RID: 400 RVA: 0x00005ACC File Offset: 0x00003CCC
		// (remove) Token: 0x06000191 RID: 401 RVA: 0x00005B04 File Offset: 0x00003D04
		public event EventHandler<HttpProgressEventArgs> HttpSendProgress;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000192 RID: 402 RVA: 0x00005B3C File Offset: 0x00003D3C
		// (remove) Token: 0x06000193 RID: 403 RVA: 0x00005B74 File Offset: 0x00003D74
		public event EventHandler<HttpProgressEventArgs> HttpReceiveProgress;

		// Token: 0x06000194 RID: 404 RVA: 0x00005BAC File Offset: 0x00003DAC
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			this.AddRequestProgress(request);
			HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken);
			HttpResponseMessage response = httpResponseMessage;
			if (this.HttpReceiveProgress != null && response != null && response.Content != null)
			{
				cancellationToken.ThrowIfCancellationRequested();
				await this.AddResponseProgressAsync(request, response);
			}
			return response;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005C01 File Offset: 0x00003E01
		protected internal virtual void OnHttpRequestProgress(HttpRequestMessage request, HttpProgressEventArgs e)
		{
			if (this.HttpSendProgress != null)
			{
				this.HttpSendProgress(request, e);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005C18 File Offset: 0x00003E18
		protected internal virtual void OnHttpResponseProgress(HttpRequestMessage request, HttpProgressEventArgs e)
		{
			if (this.HttpReceiveProgress != null)
			{
				this.HttpReceiveProgress(request, e);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005C30 File Offset: 0x00003E30
		private void AddRequestProgress(HttpRequestMessage request)
		{
			if (this.HttpSendProgress != null && request != null && request.Content != null)
			{
				HttpContent httpContent = new ProgressContent(request.Content, this, request);
				request.Content = httpContent;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005C68 File Offset: 0x00003E68
		private async Task<HttpResponseMessage> AddResponseProgressAsync(HttpRequestMessage request, HttpResponseMessage response)
		{
			TaskAwaiter<Stream> taskAwaiter = response.Content.ReadAsStreamAsync().GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<Stream> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<Stream>);
			}
			HttpContent httpContent = new StreamContent(new ProgressStream(taskAwaiter.GetResult(), this, request, response));
			response.Content.Headers.CopyTo(httpContent.Headers);
			response.Content = httpContent;
			return response;
		}
	}
}
