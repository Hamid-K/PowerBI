using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Channel
{
	// Token: 0x020000E6 RID: 230
	public class Transmission
	{
		// Token: 0x06000868 RID: 2152 RVA: 0x0001B228 File Offset: 0x00019428
		public Transmission(Uri address, byte[] content, string contentType, string contentEncoding, TimeSpan timeout = default(TimeSpan))
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.EndpointAddress = address;
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this.Content = content;
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			this.ContentType = contentType;
			this.ContentEncoding = contentEncoding;
			this.Timeout = ((timeout == default(TimeSpan)) ? Transmission.DefaultTimeout : timeout);
			this.Id = Convert.ToBase64String(BitConverter.GetBytes(WeakConcurrentRandom.Instance.Next()));
			this.TelemetryItems = null;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001B2C7 File Offset: 0x000194C7
		public Transmission(Uri address, ICollection<ITelemetry> telemetryItems, TimeSpan timeout = default(TimeSpan))
			: this(address, JsonSerializer.Serialize(telemetryItems, true), JsonSerializer.ContentType, JsonSerializer.CompressionType, timeout)
		{
			this.TelemetryItems = telemetryItems;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001B2E9 File Offset: 0x000194E9
		internal Transmission(Uri address, IEnumerable<ITelemetry> telemetryItems, string contentType, string contentEncoding, TimeSpan timeout = default(TimeSpan))
			: this(address, JsonSerializer.Serialize(telemetryItems, true), contentType, contentEncoding, timeout)
		{
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001B2FE File Offset: 0x000194FE
		internal Transmission(Uri address, byte[] content, HttpClient passedClient, string contentType, string contentEncoding, TimeSpan timeout = default(TimeSpan))
			: this(address, content, contentType, contentEncoding, timeout)
		{
			Transmission.client = passedClient;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001B314 File Offset: 0x00019514
		protected internal Transmission()
		{
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0001B31C File Offset: 0x0001951C
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0001B324 File Offset: 0x00019524
		public Uri EndpointAddress { get; private set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0001B32D File Offset: 0x0001952D
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x0001B335 File Offset: 0x00019535
		public byte[] Content { get; private set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0001B33E File Offset: 0x0001953E
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0001B346 File Offset: 0x00019546
		public string ContentType { get; private set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001B34F File Offset: 0x0001954F
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0001B357 File Offset: 0x00019557
		public string ContentEncoding { get; private set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001B360 File Offset: 0x00019560
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0001B368 File Offset: 0x00019568
		public TimeSpan Timeout { get; internal set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0001B371 File Offset: 0x00019571
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0001B379 File Offset: 0x00019579
		public string Id { get; private set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001B382 File Offset: 0x00019582
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0001B38A File Offset: 0x0001958A
		public ICollection<ITelemetry> TelemetryItems { get; private set; }

		// Token: 0x0600087B RID: 2171 RVA: 0x0001B394 File Offset: 0x00019594
		public virtual async Task<HttpWebResponseWrapper> SendAsync()
		{
			if (Interlocked.CompareExchange(ref this.isSending, 1, 0) != 0)
			{
				throw new InvalidOperationException("SendAsync is already in progress.");
			}
			HttpWebResponseWrapper httpWebResponseWrapper3;
			try
			{
				using (MemoryStream contentStream = new MemoryStream(this.Content))
				{
					HttpRequestMessage httpRequestMessage = this.CreateRequestMessage(this.EndpointAddress, contentStream);
					HttpWebResponseWrapper wrapper = null;
					try
					{
						using (CancellationTokenSource ct = new CancellationTokenSource(this.Timeout))
						{
							HttpResponseMessage httpResponseMessage = await Transmission.client.SendAsync(httpRequestMessage, ct.Token).ConfigureAwait(false);
							using (HttpResponseMessage response = httpResponseMessage)
							{
								if (response != null)
								{
									wrapper = new HttpWebResponseWrapper
									{
										StatusCode = (int)response.StatusCode,
										StatusDescription = response.ReasonPhrase
									};
									HttpWebResponseWrapper httpWebResponseWrapper = wrapper;
									HttpResponseHeaders headers = response.Headers;
									string text;
									if (headers == null)
									{
										text = null;
									}
									else
									{
										RetryConditionHeaderValue retryAfter = headers.RetryAfter;
										text = ((retryAfter != null) ? retryAfter.ToString() : null);
									}
									httpWebResponseWrapper.RetryAfterHeader = text;
									if (response.StatusCode == HttpStatusCode.PartialContent && response.Content != null)
									{
										HttpWebResponseWrapper httpWebResponseWrapper2 = wrapper;
										httpWebResponseWrapper2.Content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
										httpWebResponseWrapper2 = null;
									}
									if (CoreEventSource.IsVerboseEnabled && response.StatusCode != HttpStatusCode.PartialContent)
									{
										try
										{
											if (response.Content != null)
											{
												HttpWebResponseWrapper httpWebResponseWrapper2 = wrapper;
												httpWebResponseWrapper2.Content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
												httpWebResponseWrapper2 = null;
											}
										}
										catch (Exception)
										{
										}
									}
								}
							}
							HttpResponseMessage response = null;
						}
						CancellationTokenSource ct = null;
					}
					catch (OperationCanceledException)
					{
						wrapper = new HttpWebResponseWrapper
						{
							StatusCode = 408
						};
					}
					httpWebResponseWrapper3 = wrapper;
				}
			}
			finally
			{
				Interlocked.Exchange(ref this.isSending, 0);
			}
			return httpWebResponseWrapper3;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001B3DC File Offset: 0x000195DC
		public virtual Tuple<Transmission, Transmission> Split(Func<int, int> calculateLength)
		{
			Transmission transmission = this;
			Transmission transmission2 = null;
			if (this.TelemetryItems != null)
			{
				int num = calculateLength(this.TelemetryItems.Count);
				if (num != this.TelemetryItems.Count)
				{
					List<ITelemetry> list = new List<ITelemetry>();
					List<ITelemetry> list2 = new List<ITelemetry>();
					int num2 = 0;
					foreach (ITelemetry telemetry in this.TelemetryItems)
					{
						if (num2 < num)
						{
							list.Add(telemetry);
						}
						else
						{
							list2.Add(telemetry);
						}
						num2++;
					}
					transmission = new Transmission(this.EndpointAddress, list, default(TimeSpan));
					transmission2 = new Transmission(this.EndpointAddress, list2, default(TimeSpan));
				}
			}
			else if (this.ContentType == JsonSerializer.ContentType)
			{
				bool flag = this.ContentEncoding == JsonSerializer.CompressionType;
				string[] array = JsonSerializer.Deserialize(this.Content, flag).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
				int num3 = calculateLength(array.Length);
				if (num3 != array.Length)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					for (int i = 0; i < array.Length; i++)
					{
						if (i < num3)
						{
							if (!string.IsNullOrEmpty(text))
							{
								text += Environment.NewLine;
							}
							text += array[i];
						}
						else
						{
							if (!string.IsNullOrEmpty(text2))
							{
								text2 += Environment.NewLine;
							}
							text2 += array[i];
						}
					}
					transmission = new Transmission(this.EndpointAddress, JsonSerializer.ConvertToByteArray(text, flag), this.ContentType, this.ContentEncoding, default(TimeSpan));
					transmission2 = new Transmission(this.EndpointAddress, JsonSerializer.ConvertToByteArray(text2, flag), this.ContentType, this.ContentEncoding, default(TimeSpan));
				}
			}
			else if (calculateLength(1) == 0)
			{
				transmission = null;
				transmission2 = this;
			}
			return Tuple.Create<Transmission, Transmission>(transmission, transmission2);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001B5FC File Offset: 0x000197FC
		protected virtual HttpRequestMessage CreateRequestMessage(Uri address, Stream contentStream)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, address);
			httpRequestMessage.Content = new StreamContent(contentStream);
			if (!string.IsNullOrEmpty(this.ContentType))
			{
				httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(this.ContentType);
			}
			if (!string.IsNullOrEmpty(this.ContentEncoding))
			{
				httpRequestMessage.Content.Headers.Add("Content-Encoding", this.ContentEncoding);
			}
			return httpRequestMessage;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001B674 File Offset: 0x00019874
		[Obsolete("Use CreateRequestMessage instead as SendAsync is now using HttpClient to send HttpRequest.")]
		protected virtual WebRequest CreateRequest(Uri address)
		{
			WebRequest webRequest = WebRequest.Create(address);
			webRequest.Method = "POST";
			if (!string.IsNullOrEmpty(this.ContentType))
			{
				webRequest.ContentType = this.ContentType;
			}
			if (!string.IsNullOrEmpty(this.ContentEncoding))
			{
				webRequest.Headers["Content-Encoding"] = this.ContentEncoding;
			}
			return webRequest;
		}

		// Token: 0x04000335 RID: 821
		internal const string ContentEncodingHeader = "Content-Encoding";

		// Token: 0x04000336 RID: 822
		private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(100.0);

		// Token: 0x04000337 RID: 823
		private static HttpClient client = new HttpClient
		{
			Timeout = global::System.Threading.Timeout.InfiniteTimeSpan
		};

		// Token: 0x04000338 RID: 824
		private int isSending;
	}
}
