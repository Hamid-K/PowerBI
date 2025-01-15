using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Extensions;

namespace AngleSharp.Network.Default
{
	// Token: 0x020000AC RID: 172
	public sealed class HttpRequester : IRequester
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0001F8FC File Offset: 0x0001DAFC
		public HttpRequester(string userAgent = null)
		{
			this._timeOut = new TimeSpan(0, 0, 0, 45);
			this._headers = new Dictionary<string, string> { 
			{
				HeaderNames.UserAgent,
				userAgent ?? HttpRequester.AgentName
			} };
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001F934 File Offset: 0x0001DB34
		public IDictionary<string, string> Headers
		{
			get
			{
				return this._headers;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001F93C File Offset: 0x0001DB3C
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x0001F944 File Offset: 0x0001DB44
		public TimeSpan Timeout
		{
			get
			{
				return this._timeOut;
			}
			set
			{
				this._timeOut = value;
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001F94D File Offset: 0x0001DB4D
		public bool SupportsProtocol(string protocol)
		{
			return protocol.IsOneOf(ProtocolNames.Http, ProtocolNames.Https);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001F960 File Offset: 0x0001DB60
		public async Task<IResponse> RequestAsync(IRequest request, CancellationToken cancellationToken)
		{
			CancellationTokenSource cancellationTokenSource = HttpRequester.CreateTimeoutToken(this._timeOut);
			HttpRequester.RequestState requestState = new HttpRequester.RequestState(request, this._headers);
			IResponse response;
			using (cancellationToken.Register(new Action(cancellationTokenSource.Cancel)))
			{
				response = await requestState.RequestAsync(cancellationTokenSource.Token).ConfigureAwait(false);
			}
			return response;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001F9B5 File Offset: 0x0001DBB5
		private static CancellationTokenSource CreateTimeoutToken(TimeSpan elapsed)
		{
			return new CancellationTokenSource(elapsed);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001F9C0 File Offset: 0x0001DBC0
		private static void RaiseConnectionLimit(HttpWebRequest http)
		{
			FieldInfo field = typeof(HttpWebRequest).GetField("_ServicePoint");
			object obj = ((field != null) ? field.GetValue(http) : null);
			if (obj != null)
			{
				PropertyInfo property = obj.GetType().GetProperty("ConnectionLimit");
				if (property == null)
				{
					return;
				}
				property.SetValue(obj, 1024, null);
			}
		}

		// Token: 0x040003DB RID: 987
		private const int BufferSize = 4096;

		// Token: 0x040003DC RID: 988
		private static readonly string Version = typeof(HttpRequester).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

		// Token: 0x040003DD RID: 989
		private static readonly string AgentName = "AngleSharp/" + HttpRequester.Version;

		// Token: 0x040003DE RID: 990
		private static readonly Dictionary<string, PropertyInfo> PropCache = new Dictionary<string, PropertyInfo>();

		// Token: 0x040003DF RID: 991
		private static readonly List<string> Restricted = new List<string>();

		// Token: 0x040003E0 RID: 992
		private TimeSpan _timeOut;

		// Token: 0x040003E1 RID: 993
		private readonly Dictionary<string, string> _headers;

		// Token: 0x0200046B RID: 1131
		private sealed class RequestState
		{
			// Token: 0x060023BF RID: 9151 RVA: 0x0005CF6C File Offset: 0x0005B16C
			public RequestState(IRequest request, IDictionary<string, string> headers)
			{
				this._cookies = new CookieContainer();
				this._headers = headers;
				this._request = request;
				this._http = WebRequest.Create(request.Address) as HttpWebRequest;
				this._http.CookieContainer = this._cookies;
				this._http.Method = request.Method.ToString().ToUpperInvariant();
				this._buffer = new byte[4096];
				this.SetHeaders();
				this.SetCookies();
				this.AllowCompression();
				this.DisableAutoRedirect();
			}

			// Token: 0x060023C0 RID: 9152 RVA: 0x0005D010 File Offset: 0x0005B210
			public async Task<IResponse> RequestAsync(CancellationToken cancellationToken)
			{
				cancellationToken.Register(new Action(this._http.Abort));
				if (this._request.Method == HttpMethod.Post || this._request.Method == HttpMethod.Put)
				{
					Stream stream = await Task.Factory.FromAsync<Stream>(new Func<AsyncCallback, object, IAsyncResult>(this._http.BeginGetRequestStream), new Func<IAsyncResult, Stream>(this._http.EndGetRequestStream), null).ConfigureAwait(false);
					this.SendRequest(stream);
				}
				WebResponse webResponse = null;
				try
				{
					webResponse = await Task.Factory.FromAsync<WebResponse>(new Func<AsyncCallback, object, IAsyncResult>(this._http.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this._http.EndGetResponse), null).ConfigureAwait(false);
				}
				catch (WebException ex)
				{
					webResponse = ex.Response;
				}
				HttpRequester.RaiseConnectionLimit(this._http);
				return this.GetResponse(webResponse as HttpWebResponse);
			}

			// Token: 0x060023C1 RID: 9153 RVA: 0x0005D060 File Offset: 0x0005B260
			private void SendRequest(Stream target)
			{
				Stream content = this._request.Content;
				while (content != null)
				{
					int num = content.Read(this._buffer, 0, 4096);
					if (num == 0)
					{
						break;
					}
					target.Write(this._buffer, 0, num);
				}
			}

			// Token: 0x060023C2 RID: 9154 RVA: 0x0005D0A4 File Offset: 0x0005B2A4
			private Response GetResponse(HttpWebResponse response)
			{
				if (response != null)
				{
					CookieCollection cookies = this._cookies.GetCookies(response.ResponseUri);
					var enumerable = response.Headers.AllKeys.Select((string m) => new
					{
						Key = m,
						Value = response.Headers[m]
					});
					Response response2 = new Response
					{
						Content = response.GetResponseStream(),
						StatusCode = response.StatusCode,
						Address = Url.Convert(response.ResponseUri)
					};
					foreach (var <>f__AnonymousType in enumerable)
					{
						response2.Headers.Add(<>f__AnonymousType.Key, <>f__AnonymousType.Value);
					}
					if (cookies.Count > 0)
					{
						IEnumerable<string> enumerable2 = from m in cookies.OfType<Cookie>()
							select m.ToString();
						response2.Headers[HeaderNames.SetCookie] = string.Join(", ", enumerable2);
					}
					return response2;
				}
				return null;
			}

			// Token: 0x060023C3 RID: 9155 RVA: 0x0005D1E0 File Offset: 0x0005B3E0
			private void AddHeader(string key, string value)
			{
				if (key.Is(HeaderNames.Accept))
				{
					this._http.Accept = value;
					return;
				}
				if (key.Is(HeaderNames.ContentType))
				{
					this._http.ContentType = value;
					return;
				}
				if (key.Is(HeaderNames.Expect))
				{
					this.SetProperty(HeaderNames.Expect, value);
					return;
				}
				if (key.Is(HeaderNames.Date))
				{
					this.SetProperty(HeaderNames.Date, DateTime.Parse(value));
					return;
				}
				if (key.Is(HeaderNames.Host))
				{
					this.SetProperty(HeaderNames.Host, value);
					return;
				}
				if (key.Is(HeaderNames.IfModifiedSince))
				{
					this.SetProperty("IfModifiedSince", DateTime.Parse(value));
					return;
				}
				if (key.Is(HeaderNames.Referer))
				{
					this.SetProperty(HeaderNames.Referer, value);
					return;
				}
				if (key.Is(HeaderNames.UserAgent))
				{
					this.SetProperty("UserAgent", value);
					return;
				}
				if (!key.Is(HeaderNames.Connection) && !key.Is(HeaderNames.Range) && !key.Is(HeaderNames.ContentLength) && !key.Is(HeaderNames.TransferEncoding))
				{
					this._http.Headers[key] = value;
				}
			}

			// Token: 0x060023C4 RID: 9156 RVA: 0x0005D318 File Offset: 0x0005B518
			private void SetCookies()
			{
				string orDefault = this._request.Headers.GetOrDefault(HeaderNames.Cookie, string.Empty);
				this._cookies.SetCookies(this._http.RequestUri, orDefault.Replace(';', ','));
			}

			// Token: 0x060023C5 RID: 9157 RVA: 0x0005D360 File Offset: 0x0005B560
			private void SetHeaders()
			{
				foreach (KeyValuePair<string, string> keyValuePair in this._headers)
				{
					this.AddHeader(keyValuePair.Key, keyValuePair.Value);
				}
				foreach (KeyValuePair<string, string> keyValuePair2 in this._request.Headers)
				{
					this.AddHeader(keyValuePair2.Key, keyValuePair2.Value);
				}
			}

			// Token: 0x060023C6 RID: 9158 RVA: 0x0005D408 File Offset: 0x0005B608
			private void AllowCompression()
			{
				this.SetProperty("AutomaticDecompression", 3);
			}

			// Token: 0x060023C7 RID: 9159 RVA: 0x0005D41B File Offset: 0x0005B61B
			private void DisableAutoRedirect()
			{
				this.SetProperty("AllowAutoRedirect", false);
			}

			// Token: 0x060023C8 RID: 9160 RVA: 0x0005D430 File Offset: 0x0005B630
			private void SetProperty(string name, object value)
			{
				PropertyInfo propertyInfo = null;
				if (!HttpRequester.PropCache.TryGetValue(name, out propertyInfo))
				{
					Dictionary<string, PropertyInfo> propCache = HttpRequester.PropCache;
					lock (propCache)
					{
						if (!HttpRequester.PropCache.TryGetValue(name, out propertyInfo))
						{
							propertyInfo = this._http.GetType().GetProperty(name);
							HttpRequester.PropCache.Add(name, propertyInfo);
						}
					}
				}
				if (!HttpRequester.Restricted.Contains(name) && propertyInfo != null && propertyInfo.CanWrite)
				{
					try
					{
						propertyInfo.SetValue(this._http, value, null);
					}
					catch
					{
						List<string> restricted = HttpRequester.Restricted;
						lock (restricted)
						{
							if (!HttpRequester.Restricted.Contains(name))
							{
								HttpRequester.Restricted.Add(name);
							}
						}
					}
				}
			}

			// Token: 0x04000FF4 RID: 4084
			private readonly CookieContainer _cookies;

			// Token: 0x04000FF5 RID: 4085
			private readonly IDictionary<string, string> _headers;

			// Token: 0x04000FF6 RID: 4086
			private readonly HttpWebRequest _http;

			// Token: 0x04000FF7 RID: 4087
			private readonly IRequest _request;

			// Token: 0x04000FF8 RID: 4088
			private readonly byte[] _buffer;
		}
	}
}
