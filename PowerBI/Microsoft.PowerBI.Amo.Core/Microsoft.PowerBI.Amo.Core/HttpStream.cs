using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AnalysisServices.Authentication;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.MsoId;
using Microsoft.AnalysisServices.Runtime;
using Microsoft.AnalysisServices.Security;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000040 RID: 64
	internal class HttpStream : TransportCapabilitiesAwareXmlaStream
	{
		// Token: 0x06000305 RID: 773 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		private HttpStream(HttpStream.HttpChannelController controller, ConnectionInfo info, Uri dataSource, UserContext context, string coreServer, XmlaDataType desiredRequestType, XmlaDataType desiredResponseType, int readTimeout)
			: base(false, desiredRequestType, desiredResponseType)
		{
			this.controller = controller;
			this.info = info;
			this.dataSource = dataSource;
			this.userContext = context;
			this.defaultHeaders = this.controller.GetDefaultStreamHeaders(this.info, this.dataSource, coreServer);
			this.ReadTimeout = readTimeout;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000EF0F File Offset: 0x0000D10F
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000EF17 File Offset: 0x0000D117
		public override int ReadTimeout { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000EF20 File Offset: 0x0000D120
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000EF28 File Offset: 0x0000D128
		internal XmlaStreamException StreamException { get; private set; }

		// Token: 0x0600030B RID: 779 RVA: 0x0000EF34 File Offset: 0x0000D134
		public static HttpStream Create(IConnectivityOwner owner, ConnectionInfo info, XmlaDataType desiredRequestType, XmlaDataType desiredResponseType, int readTimeout, out bool isSessionTokenNeeded)
		{
			CookieContainer cookieContainer = new CookieContainer();
			Uri uri;
			if (info.IsForRedirector)
			{
				bool flag = !string.IsNullOrEmpty(info.DataSourceVersion);
				string text;
				string text2;
				string text3;
				HttpStream.LegacyController.GetSandboxDatabaseInformation(info.GetRedirectorUrlForDatabase(), info.UserID, info.Password, flag, cookieContainer, out text, out text2, out text3);
				if (!flag)
				{
					info.DataSourceVersion = text3;
				}
				info.SetCatalog(text2);
				uri = new Uri(info.GetRedirectorUrlForRedirect(text, flag));
			}
			else
			{
				uri = info.DatasourceUriInitializeHelper();
			}
			string text4 = null;
			if (info.IsPaaSInfrastructure)
			{
				text4 = info.AcquireAADTokenAndResolveHTTPConnectionPropertiesForPaaSInfrastructure(owner, ref uri);
				if (info.IsPaaSInfrastructure && string.IsNullOrEmpty(text4))
				{
					throw new InvalidDataException("Failed to resolve the core XMLA server");
				}
			}
			else if (info.IsPbiDataset && info.AuthHandle == null)
			{
				info.BuildExternalAuthenticationHandle(owner);
			}
			HttpStream.HttpChannelController httpChannelController;
			UserContext userContext;
			HttpStream.HttpChannelManager.GetControllerAndUserContext(info, cookieContainer, out httpChannelController, out userContext);
			isSessionTokenNeeded = info.ClientCertificateThumbprint != null || info.IntegratedSecurity == IntegratedSecurity.Federated;
			return new HttpStream(httpChannelController, info, uri, userContext, text4, desiredRequestType, desiredResponseType, readTimeout);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000F024 File Offset: 0x0000D224
		public override void WriteSoapActionHeader(string action)
		{
			if (string.IsNullOrEmpty(action))
			{
				this.soapActionHeader = null;
				this.soapActionValue = null;
				return;
			}
			int num = action.IndexOf(':');
			if (num < 0)
			{
				throw new ArgumentException("action");
			}
			this.soapActionHeader = action.Substring(0, num);
			this.soapActionValue = action.Substring(num + 1);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000F080 File Offset: 0x0000D280
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 1)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size + offset > buffer.Length)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument, "buffer");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				this.EnsureOperationIsReadyForWrite();
				this.operation.WriteRequestPayload(buffer, offset, size);
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (WebException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
			catch (HttpRequestException ex3)
			{
				throw new XmlaStreamException(ex3);
			}
			catch (TaskCanceledException ex4)
			{
				throw new XmlaStreamException(ex4);
			}
			catch (SecurityException ex5)
			{
				throw new XmlaStreamException(ex5);
			}
			catch (ProtocolViolationException ex6)
			{
				throw new XmlaStreamException(ex6);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000F178 File Offset: 0x0000D378
		public override void WriteEndOfMessage()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				this.EnsureOperationIsReadyForWrite();
				this.operation.CompleteRequest();
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		public override XmlaDataType GetResponseDataType()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			XmlaDataType responseDataType;
			try
			{
				bool flag;
				this.EnsureOperationIsReadyForRead(out flag);
				responseDataType = this.operation.GetResponseDataType();
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (WebException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
			catch (HttpRequestException ex3)
			{
				throw new XmlaStreamException(ex3);
			}
			catch (TaskCanceledException ex4)
			{
				throw new XmlaStreamException(ex4);
			}
			catch (ProtocolViolationException ex5)
			{
				throw new XmlaStreamException(ex5);
			}
			return responseDataType;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000F274 File Offset: 0x0000D474
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 1)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size + offset > buffer.Length)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument, "buffer");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			int num;
			try
			{
				bool flag;
				this.EnsureOperationIsReadyForRead(out flag);
				if (flag)
				{
					num = 0;
				}
				else
				{
					num = this.operation.ReadResponsePayload(buffer, offset, size);
				}
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (WebException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
			catch (HttpRequestException ex3)
			{
				throw new XmlaStreamException(ex3);
			}
			catch (TaskCanceledException ex4)
			{
				throw new XmlaStreamException(ex4);
			}
			catch (ProtocolViolationException ex5)
			{
				throw new XmlaStreamException(ex5);
			}
			return num;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000F364 File Offset: 0x0000D564
		public override string GetExtendedErrorInfo()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (this.operation == null)
			{
				return string.Empty;
			}
			return this.operation.GetExtendedErrorInfo();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000F390 File Offset: 0x0000D590
		public override void Skip()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				if (this.operation != null)
				{
					this.operation.Dispose();
					this.operation = null;
				}
				this.StreamException = null;
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000F409 File Offset: 0x0000D609
		public override void Close()
		{
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000F40C File Offset: 0x0000D60C
		public override void Dispose()
		{
			try
			{
				if (this.operation != null)
				{
					this.operation.Dispose();
					this.operation = null;
				}
				HttpStream.HttpChannelManager.ReleaseController(this.controller);
				this.userContext.Dispose();
			}
			catch (IOException)
			{
			}
			catch (WebException)
			{
			}
			catch (HttpRequestException)
			{
			}
			catch (TaskCanceledException)
			{
			}
			catch (ProtocolViolationException)
			{
			}
			catch (SecurityException)
			{
			}
			finally
			{
				base.Dispose(true);
				this.disposed = true;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
		private void EnsureOperationIsReadyForWrite()
		{
			if (this.operation != null)
			{
				switch (this.operation.Status)
				{
				case HttpStream.HttpXmlaOperationStatus.Request:
					return;
				case HttpStream.HttpXmlaOperationStatus.Response:
				case HttpStream.HttpXmlaOperationStatus.Completed:
				case HttpStream.HttpXmlaOperationStatus.Error:
					this.operation.Dispose();
					break;
				}
			}
			this.operation = this.controller.StartNewOperation(this);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000F520 File Offset: 0x0000D720
		private void EnsureOperationIsReadyForRead(out bool isCompleted)
		{
			if (this.operation == null)
			{
				throw new InvalidOperationException("The stream does not have an active operation!");
			}
			HttpStream.HttpXmlaOperationStatus status = this.operation.Status;
			isCompleted = status == HttpStream.HttpXmlaOperationStatus.Completed;
			bool flag = false;
			switch (status)
			{
			case HttpStream.HttpXmlaOperationStatus.Request:
			case HttpStream.HttpXmlaOperationStatus.Error:
				this.operation.Dispose();
				this.operation = null;
				break;
			case HttpStream.HttpXmlaOperationStatus.Response:
			case HttpStream.HttpXmlaOperationStatus.Completed:
				flag = true;
				break;
			}
			if (!flag)
			{
				throw new XmlaStreamException(XmlaSR.HttpStream_InvalidReadRequest(status.ToString()));
			}
		}

		// Token: 0x04000238 RID: 568
		private const string ContentTypeParameter = "content_type";

		// Token: 0x04000239 RID: 569
		protected static readonly Regex contentTypeRegex = new Regex(string.Format("^(\\s*)(?<{0}>(({1})|({2})|({3})|({4})))(\\s*)((;(.*))|)(\\s*)\\z", new object[]
		{
			"content_type",
			"text/xml".Replace("+", "\\+"),
			"application/xml+xpress".Replace("+", "\\+"),
			"application/sx".Replace("+", "\\+"),
			"application/sx+xpress".Replace("+", "\\+")
		}), RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400023A RID: 570
		private readonly HttpStream.HttpChannelController controller;

		// Token: 0x0400023B RID: 571
		private readonly ConnectionInfo info;

		// Token: 0x0400023C RID: 572
		private readonly Uri dataSource;

		// Token: 0x0400023D RID: 573
		private readonly UserContext userContext;

		// Token: 0x0400023E RID: 574
		private readonly IDictionary<string, string> defaultHeaders;

		// Token: 0x0400023F RID: 575
		private string soapActionHeader;

		// Token: 0x04000240 RID: 576
		private string soapActionValue;

		// Token: 0x04000241 RID: 577
		private bool outdatedVersion;

		// Token: 0x04000242 RID: 578
		private HttpStream.HttpXmlaOperation operation;

		// Token: 0x0200017D RID: 381
		private abstract class HttpChannelController : Disposable
		{
			// Token: 0x06001245 RID: 4677 RVA: 0x0003F72C File Offset: 0x0003D92C
			protected HttpChannelController(HttpStream.ChannelOptions options, CookieContainer cookieContainer)
			{
				this.options = options;
				this.cookieContainer = cookieContainer;
				if (!string.IsNullOrEmpty(options.CertificateThumbprint))
				{
					this.clientCertificate = CertUtils.LoadCertificateByThumbprint(options.CertificateThumbprint, true);
				}
				bool flag;
				this.InitializeHttpClient(out flag);
			}

			// Token: 0x06001246 RID: 4678 RVA: 0x0003F776 File Offset: 0x0003D976
			private protected HttpChannelController(IList<object> elements, out int offest, out bool hasElementsUpdate)
			{
				this.options = HttpStream.ChannelOptions.DeserializeFromGlobalObject(elements, 0);
				this.RefreshFromGlobalObjectImpl(elements, out offest, out hasElementsUpdate);
			}

			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x06001247 RID: 4679 RVA: 0x0003F794 File Offset: 0x0003D994
			public HttpStream.HttpChannelMode Mode
			{
				get
				{
					return this.options.Mode;
				}
			}

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x06001248 RID: 4680 RVA: 0x0003F7A1 File Offset: 0x0003D9A1
			internal string CacheKey
			{
				get
				{
					return this.options.CacheKey;
				}
			}

			// Token: 0x06001249 RID: 4681 RVA: 0x0003F7B0 File Offset: 0x0003D9B0
			public IDictionary<string, string> GetDefaultStreamHeaders(ConnectionInfo info, Uri dataSource, string coreServer)
			{
				base.ThrowIfAlreadyDisposed();
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				this.GetDefaultStreamHeaders(info, dataSource, coreServer, dictionary);
				return dictionary;
			}

			// Token: 0x0600124A RID: 4682 RVA: 0x0003F7D4 File Offset: 0x0003D9D4
			public HttpStream.HttpXmlaOperation StartNewOperation(HttpStream stream)
			{
				base.ThrowIfAlreadyDisposed();
				bool flag;
				switch (stream.info.HttpHandling)
				{
				case HttpChannelHandling.Default:
					flag = ClientFeaturesManager.CheckIfHttpClientIsSupportedByDefault() && this.httpClient != null && stream.ReadTimeout == this.options.DefaultTimeoutInMiliseconds;
					break;
				case HttpChannelHandling.WebRequestBased:
					flag = false;
					break;
				case HttpChannelHandling.PreferHttpClient:
					flag = this.httpClient != null && stream.ReadTimeout == this.options.DefaultTimeoutInMiliseconds;
					break;
				default:
					flag = false;
					break;
				}
				if (flag && this.options.Credentials != null)
				{
					flag = false;
				}
				HttpStream.HttpXmlaOperation httpXmlaOperation = this.CreateNewOperation(stream, flag);
				httpXmlaOperation.StartRequest();
				return httpXmlaOperation;
			}

			// Token: 0x0600124B RID: 4683 RVA: 0x0003F876 File Offset: 0x0003DA76
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.httpClient != null)
				{
					this.httpClient.Dispose();
					this.httpClient = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x0600124C RID: 4684 RVA: 0x0003F89C File Offset: 0x0003DA9C
			protected virtual void GetDefaultStreamHeaders(ConnectionInfo info, Uri webSite, string coreServer, IDictionary<string, string> headers)
			{
				headers.Add("User-Agent", "AMO");
				headers.Add("X-AS-AcquireTokenStats", "AppName=");
			}

			// Token: 0x0600124D RID: 4685 RVA: 0x0003F8C0 File Offset: 0x0003DAC0
			protected virtual void GetPerRequestHeaders(HttpStream owner, object context, IList<KeyValuePair<string, string>> headers)
			{
				headers.Add(new KeyValuePair<string, string>("Content-Type", DataTypes.GetDataTypeFromEnum(owner.GetRequestDataType())));
			}

			// Token: 0x0600124E RID: 4686 RVA: 0x0003F8DD File Offset: 0x0003DADD
			protected virtual void HandleResponseStatusCode(int statusCode, Exception ex)
			{
			}

			// Token: 0x0600124F RID: 4687 RVA: 0x0003F8DF File Offset: 0x0003DADF
			protected virtual void ProcessWebResponse(HttpStream owner, object context, HttpStream.HttpChannelController.WebResponseInfo response)
			{
			}

			// Token: 0x06001250 RID: 4688
			protected abstract HttpStream.HttpXmlaOperation CreateNewOperation(HttpStream owner, bool useHttpClient);

			// Token: 0x06001251 RID: 4689
			protected abstract HttpStream.HttpChannelController.WebErrorClass ClassifyWebError(HttpStream owner, object context, HttpStream.HttpChannelController.WebErrorInfo error, out Exception exception);

			// Token: 0x06001252 RID: 4690 RVA: 0x0003F8E4 File Offset: 0x0003DAE4
			internal HttpWebRequest StartWebRequestBasedOperation(HttpStream owner, object context, out Stream requestStream)
			{
				HttpWebRequest request = this.PrepareWebRequestImpl(owner, context);
				requestStream = owner.userContext.ExecuteInUserContext<Stream>(() => new BufferedStream(request.GetRequestStream(), ClientFeaturesManager.GetHttpStreamBufferSize()));
				return request;
			}

			// Token: 0x06001253 RID: 4691 RVA: 0x0003F924 File Offset: 0x0003DB24
			internal bool CompleteWebRequestBasedOperation(HttpStream owner, object context, HttpWebRequest request, out HttpWebResponse response, out Stream responseStream)
			{
				response = this.GetWebResponseImpl(owner, context, request);
				if (response == null)
				{
					responseStream = null;
					return false;
				}
				this.ProcessWebResponse(owner, context, new HttpStream.HttpChannelController.WebResponseInfo(response));
				responseStream = new BufferedStream(response.GetResponseStream(), ClientFeaturesManager.GetHttpStreamBufferSize());
				return true;
			}

			// Token: 0x06001254 RID: 4692 RVA: 0x0003F963 File Offset: 0x0003DB63
			internal HttpRequestMessage StartHttpClientBasedOperation(HttpStream owner, object context, CancellationTokenSource cts, out Stream requestStream, out Task<HttpResponseMessage> pendingResponse)
			{
				requestStream = new HttpStream.HttpChannelController.RequestPayloadStream(ClientFeaturesManager.GetHttpClientPayloadQueueLimit(), ClientFeaturesManager.GetHttpStreamBufferSize());
				return this.StartHttpClientBasedOperationImpl(owner, context, cts, requestStream, out pendingResponse);
			}

			// Token: 0x06001255 RID: 4693 RVA: 0x0003F985 File Offset: 0x0003DB85
			internal HttpRequestMessage StartHttpClientBasedOperationWithoutPayload(HttpStream owner, object context, CancellationTokenSource cts, out Task<HttpResponseMessage> pendingResponse)
			{
				return this.StartHttpClientBasedOperationImpl(owner, context, cts, null, out pendingResponse);
			}

			// Token: 0x06001256 RID: 4694 RVA: 0x0003F994 File Offset: 0x0003DB94
			internal Stream CompleteHttpClientBasedOperation(HttpStream owner, object context, Task<HttpResponseMessage> pendingResponse)
			{
				HttpResponseMessage responseMessageImpl = this.GetResponseMessageImpl(owner, context, pendingResponse);
				if (responseMessageImpl == null)
				{
					return null;
				}
				this.ProcessWebResponse(owner, context, new HttpStream.HttpChannelController.WebResponseInfo(responseMessageImpl));
				return new BufferedStream(responseMessageImpl.GetResponseContent(), ClientFeaturesManager.GetHttpStreamBufferSize());
			}

			// Token: 0x06001257 RID: 4695 RVA: 0x0003F9CE File Offset: 0x0003DBCE
			private protected virtual void SerializeToGlobalObject(IList<object> elements)
			{
				this.options.SerializeToGlobalObject(elements);
				elements.Add(this.cookieContainer);
				elements.Add(this.clientCertificate);
				elements.Add(this.httpClient);
			}

			// Token: 0x06001258 RID: 4696 RVA: 0x0003FA00 File Offset: 0x0003DC00
			private protected virtual void RefreshFromGlobalObject(IList<object> elements, out int offest, out bool hasElementsUpdate)
			{
				this.RefreshFromGlobalObjectImpl(elements, out offest, out hasElementsUpdate);
			}

			// Token: 0x06001259 RID: 4697 RVA: 0x0003FA0C File Offset: 0x0003DC0C
			internal IList<object> ToGlobalObject()
			{
				IList<object> list = new List<object>(16);
				this.SerializeToGlobalObject(list);
				return list;
			}

			// Token: 0x0600125A RID: 4698 RVA: 0x0003FA2C File Offset: 0x0003DC2C
			internal void RefreshFromGlobalObject(IList<object> elements, out bool hasElementsUpdate)
			{
				int num;
				this.RefreshFromGlobalObject(elements, out num, out hasElementsUpdate);
			}

			// Token: 0x0600125B RID: 4699 RVA: 0x0003FA44 File Offset: 0x0003DC44
			private static bool AreNetCoreClientsLoaded()
			{
				if (HttpStream.HttpChannelController.netCoreClientsLoaded != null)
				{
					return HttpStream.HttpChannelController.netCoreClientsLoaded.Value;
				}
				if (!FrameworkRuntimeHelper.IsNetCoreDomain)
				{
					HttpStream.HttpChannelController.netCoreClientsLoaded = new bool?(false);
					return false;
				}
				if (FrameworkRuntimeHelper.IsAssemblyLoaded("Microsoft.AnalysisServices.Runtime.Core", null, null, "89845dcd8080cc91"))
				{
					HttpStream.HttpChannelController.netCoreClientsLoaded = new bool?(true);
					return true;
				}
				return false;
			}

			// Token: 0x0600125C RID: 4700 RVA: 0x0003FAA0 File Offset: 0x0003DCA0
			private void RefreshFromGlobalObjectImpl(IList<object> elements, out int offest, out bool hasElementsUpdate)
			{
				offest = 6;
				int num = offest;
				offest = num + 1;
				this.cookieContainer = (CookieContainer)elements[num];
				num = offest;
				offest = num + 1;
				this.clientCertificate = (X509Certificate2)elements[num];
				num = offest;
				offest = num + 1;
				this.httpClient = (HttpClient)elements[num];
				this.InitializeHttpClient(out hasElementsUpdate);
			}

			// Token: 0x0600125D RID: 4701 RVA: 0x0003FB08 File Offset: 0x0003DD08
			private void InitializeHttpClient(out bool wasNewClientCreated)
			{
				if (this.httpClient == null)
				{
					bool flag = true;
					if (flag && HttpStream.HttpChannelController.AreNetCoreClientsLoaded())
					{
						flag = false;
					}
					if (flag)
					{
						WebRequestHandler webRequestHandler = new WebRequestHandler
						{
							AllowAutoRedirect = this.options.AllowAutoRedirect,
							AutomaticDecompression = (this.options.AllowGzipCompression ? DecompressionMethods.GZip : DecompressionMethods.None),
							CookieContainer = this.cookieContainer,
							UnsafeAuthenticatedConnectionSharing = true
						};
						if (this.clientCertificate != null)
						{
							webRequestHandler.ContinueTimeout = TimeSpan.FromMilliseconds(30000.0);
							webRequestHandler.ClientCertificates.Add(this.clientCertificate);
						}
						if (this.options.Credentials != null)
						{
							webRequestHandler.Credentials = this.options.Credentials;
						}
						this.httpClient = new HttpClient(webRequestHandler);
						this.httpClient.Timeout = TimeSpan.FromMilliseconds((double)this.options.DefaultTimeoutInMiliseconds);
						this.httpClient.DefaultRequestHeaders.TransferEncodingChunked = new bool?(true);
						this.httpClient.DefaultRequestHeaders.Connection.Add("Keep-Alive");
					}
					wasNewClientCreated = this.httpClient != null;
					return;
				}
				wasNewClientCreated = false;
			}

			// Token: 0x0600125E RID: 4702 RVA: 0x0003FC28 File Offset: 0x0003DE28
			private HttpRequestMessage StartHttpClientBasedOperationImpl(HttpStream owner, object context, CancellationTokenSource cts, Stream requestStream, out Task<HttpResponseMessage> pendingResponse)
			{
				HttpRequestMessage request = this.PrepareRequestMessageImpl(owner, context, requestStream);
				pendingResponse = owner.userContext.ExecuteInUserContext<Task<HttpResponseMessage>>(() => this.httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, (cts != null) ? cts.Token : CancellationToken.None));
				return request;
			}

			// Token: 0x0600125F RID: 4703 RVA: 0x0003FC7C File Offset: 0x0003DE7C
			private HttpWebRequest PrepareWebRequestImpl(HttpStream owner, object context)
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(owner.dataSource);
				httpWebRequest.Method = "POST";
				httpWebRequest.ServicePoint.SetTcpKeepAlive(true, 30000, 30000);
				httpWebRequest.AllowAutoRedirect = this.options.AllowAutoRedirect;
				httpWebRequest.SendChunked = true;
				httpWebRequest.KeepAlive = true;
				httpWebRequest.UnsafeAuthenticatedConnectionSharing = true;
				httpWebRequest.AutomaticDecompression = (this.options.AllowGzipCompression ? DecompressionMethods.GZip : DecompressionMethods.None);
				httpWebRequest.Timeout = owner.ReadTimeout;
				httpWebRequest.CookieContainer = this.cookieContainer;
				if (this.clientCertificate != null)
				{
					httpWebRequest.ContinueTimeout = 30000;
					httpWebRequest.ClientCertificates.Add(this.clientCertificate);
				}
				if (this.options.Mode == HttpStream.HttpChannelMode.PaasInfra || !owner.IsSessionTokenNeeded)
				{
					httpWebRequest.Headers.Remove("X-AS-GetSessionToken");
				}
				HttpHelper.ApplyHeadersToRequest(owner.defaultHeaders, httpWebRequest);
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
				this.GetPerRequestHeaders(owner, context, list);
				HttpHelper.ApplyHeadersToRequest(list, httpWebRequest);
				owner.userContext.UpdateHttpRequest(httpWebRequest);
				return httpWebRequest;
			}

			// Token: 0x06001260 RID: 4704 RVA: 0x0003FD8C File Offset: 0x0003DF8C
			private HttpWebResponse GetWebResponseImpl(HttpStream owner, object context, HttpWebRequest request)
			{
				HttpWebResponse httpWebResponse;
				try
				{
					httpWebResponse = owner.userContext.ExecuteInUserContext<HttpWebResponse>(() => (HttpWebResponse)request.GetResponse());
					this.HandleResponseStatusCode((int)httpWebResponse.StatusCode, null);
				}
				catch (WebException ex)
				{
					if (!(ex.Response is HttpWebResponse))
					{
						throw;
					}
					httpWebResponse = (HttpWebResponse)ex.Response;
					this.HandleResponseStatusCode((int)httpWebResponse.StatusCode, ex);
					Exception ex2;
					switch (this.ClassifyWebError(owner, context, new HttpStream.HttpChannelController.WebErrorInfo(ex), out ex2))
					{
					case HttpStream.HttpChannelController.WebErrorClass.Raise:
						if (ex2 != null)
						{
							throw ex2;
						}
						throw;
					case HttpStream.HttpChannelController.WebErrorClass.Ignore:
						break;
					case HttpStream.HttpChannelController.WebErrorClass.Retry:
						httpWebResponse = null;
						break;
					default:
						throw;
					}
				}
				return httpWebResponse;
			}

			// Token: 0x06001261 RID: 4705 RVA: 0x0003FE40 File Offset: 0x0003E040
			private HttpRequestMessage PrepareRequestMessageImpl(HttpStream owner, object context, Stream requestStream)
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, owner.dataSource);
				if (requestStream != null)
				{
					httpRequestMessage.Content = new StreamContent(requestStream);
				}
				else
				{
					httpRequestMessage.Content = new HttpStream.HttpChannelController.EmptyHttpContent();
				}
				if (!HttpStream.HttpChannelController.AreNetCoreClientsLoaded())
				{
					ServicePoint servicePoint = ServicePointManager.FindServicePoint(httpRequestMessage.RequestUri);
					if (servicePoint != null)
					{
						servicePoint.SetTcpKeepAlive(true, 30000, 30000);
					}
				}
				if (this.options.Mode == HttpStream.HttpChannelMode.PaasInfra || !owner.IsSessionTokenNeeded)
				{
					httpRequestMessage.Headers.Remove("X-AS-GetSessionToken");
				}
				HttpHelper.ApplyHeadersToRequest(owner.defaultHeaders, httpRequestMessage);
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
				this.GetPerRequestHeaders(owner, context, list);
				HttpHelper.ApplyHeadersToRequest(list, httpRequestMessage);
				owner.userContext.UpdateHttpRequest(httpRequestMessage);
				return httpRequestMessage;
			}

			// Token: 0x06001262 RID: 4706 RVA: 0x0003FEF8 File Offset: 0x0003E0F8
			private HttpResponseMessage GetResponseMessageImpl(HttpStream owner, object context, Task<HttpResponseMessage> pendingResponse)
			{
				HttpResponseMessage httpResponseMessage = pendingResponse.WaitForTaskCompletionAndGetResult<HttpResponseMessage>();
				this.HandleResponseStatusCode((int)httpResponseMessage.StatusCode, null);
				if (!httpResponseMessage.IsSuccessStatusCode)
				{
					Exception ex;
					switch (this.ClassifyWebError(owner, context, new HttpStream.HttpChannelController.WebErrorInfo(httpResponseMessage), out ex))
					{
					case HttpStream.HttpChannelController.WebErrorClass.Raise:
						if (ex != null)
						{
							throw ex;
						}
						httpResponseMessage.EnsureSuccessStatusCode();
						throw new HttpRequestException(XmlaSR.HttpStream_ResponseWithFailedStatus(((int)httpResponseMessage.StatusCode).ToString(), httpResponseMessage.ReasonPhrase));
					case HttpStream.HttpChannelController.WebErrorClass.Ignore:
						break;
					case HttpStream.HttpChannelController.WebErrorClass.Retry:
						httpResponseMessage = null;
						break;
					default:
						throw new HttpRequestException(XmlaSR.HttpStream_ResponseWithFailedStatus(((int)httpResponseMessage.StatusCode).ToString(), httpResponseMessage.ReasonPhrase));
					}
				}
				return httpResponseMessage;
			}

			// Token: 0x04000BCC RID: 3020
			internal const int GlobalObjectOptionsOffset = 0;

			// Token: 0x04000BCD RID: 3021
			private const int TCP_KEEP_ALIVE_TIME_IN_MS = 30000;

			// Token: 0x04000BCE RID: 3022
			private const int TCP_KEEP_ALIVE_INTERVAL_IN_MS = 30000;

			// Token: 0x04000BCF RID: 3023
			private const int CONTINUE_TIMEOUT_IN_MS = 30000;

			// Token: 0x04000BD0 RID: 3024
			private const string ASRuntimeCoreAssemblyName = "Microsoft.AnalysisServices.Runtime.Core";

			// Token: 0x04000BD1 RID: 3025
			private const string ASRuntimeCorePublicKeyToken = "89845dcd8080cc91";

			// Token: 0x04000BD2 RID: 3026
			private static bool? netCoreClientsLoaded;

			// Token: 0x04000BD3 RID: 3027
			protected HttpStream.ChannelOptions options;

			// Token: 0x04000BD4 RID: 3028
			private CookieContainer cookieContainer;

			// Token: 0x04000BD5 RID: 3029
			private X509Certificate2 clientCertificate;

			// Token: 0x04000BD6 RID: 3030
			private HttpClient httpClient;

			// Token: 0x020001FE RID: 510
			protected enum WebErrorClass
			{
				// Token: 0x040011DC RID: 4572
				Raise,
				// Token: 0x040011DD RID: 4573
				Ignore,
				// Token: 0x040011DE RID: 4574
				Retry
			}

			// Token: 0x020001FF RID: 511
			protected struct WebResponseInfo
			{
				// Token: 0x0600145D RID: 5213 RVA: 0x000453F2 File Offset: 0x000435F2
				public WebResponseInfo(HttpWebResponse response)
				{
					this.webResponse = response;
					this.responseMessage = null;
				}

				// Token: 0x0600145E RID: 5214 RVA: 0x00045402 File Offset: 0x00043602
				public WebResponseInfo(HttpResponseMessage response)
				{
					this.webResponse = null;
					this.responseMessage = response;
				}

				// Token: 0x17000683 RID: 1667
				// (get) Token: 0x0600145F RID: 5215 RVA: 0x00045412 File Offset: 0x00043612
				public bool IsValid
				{
					get
					{
						return (this.webResponse != null) ^ (this.responseMessage != null);
					}
				}

				// Token: 0x17000684 RID: 1668
				// (get) Token: 0x06001460 RID: 5216 RVA: 0x00045427 File Offset: 0x00043627
				public HttpStatusCode StatusCode
				{
					get
					{
						if (this.responseMessage != null)
						{
							return this.responseMessage.StatusCode;
						}
						return this.webResponse.StatusCode;
					}
				}

				// Token: 0x06001461 RID: 5217 RVA: 0x00045448 File Offset: 0x00043648
				public string GetResponseHeaderValue(string header)
				{
					string text;
					if (!this.TryGetResponseHeaderValue(header, out text))
					{
						text = null;
					}
					return text;
				}

				// Token: 0x06001462 RID: 5218 RVA: 0x00045463 File Offset: 0x00043663
				public bool TryGetResponseHeaderValue(string header, out string value)
				{
					if (this.responseMessage != null)
					{
						return HttpHelper.TryGetHeaderValueFromResponse(this.responseMessage, header, out value);
					}
					return HttpHelper.TryGetHeaderValueFromResponse(this.webResponse, header, out value);
				}

				// Token: 0x040011DF RID: 4575
				private HttpWebResponse webResponse;

				// Token: 0x040011E0 RID: 4576
				private HttpResponseMessage responseMessage;
			}

			// Token: 0x02000200 RID: 512
			protected struct WebErrorInfo
			{
				// Token: 0x06001463 RID: 5219 RVA: 0x00045488 File Offset: 0x00043688
				public WebErrorInfo(WebException error)
				{
					this.error = error;
					this.response = null;
				}

				// Token: 0x06001464 RID: 5220 RVA: 0x00045498 File Offset: 0x00043698
				public WebErrorInfo(HttpResponseMessage response)
				{
					this.error = null;
					this.response = response;
				}

				// Token: 0x17000685 RID: 1669
				// (get) Token: 0x06001465 RID: 5221 RVA: 0x000454A8 File Offset: 0x000436A8
				public bool IsValid
				{
					get
					{
						return (this.error != null) ^ (this.response != null);
					}
				}

				// Token: 0x17000686 RID: 1670
				// (get) Token: 0x06001466 RID: 5222 RVA: 0x000454BD File Offset: 0x000436BD
				public HttpStatusCode StatusCode
				{
					get
					{
						if (this.response != null)
						{
							return this.response.StatusCode;
						}
						return ((HttpWebResponse)this.error.Response).StatusCode;
					}
				}

				// Token: 0x17000687 RID: 1671
				// (get) Token: 0x06001467 RID: 5223 RVA: 0x000454E8 File Offset: 0x000436E8
				public WebExceptionStatus? ErrorStatus
				{
					get
					{
						if (this.response != null)
						{
							return null;
						}
						return new WebExceptionStatus?(this.error.Status);
					}
				}

				// Token: 0x06001468 RID: 5224 RVA: 0x00045517 File Offset: 0x00043717
				public bool TryGetResponseHeaderValue(string header, out string value)
				{
					if (this.response != null)
					{
						return HttpHelper.TryGetHeaderValueFromResponse(this.response, header, out value);
					}
					return HttpHelper.TryGetHeaderValueFromResponse((HttpWebResponse)this.error.Response, header, out value);
				}

				// Token: 0x06001469 RID: 5225 RVA: 0x00045548 File Offset: 0x00043748
				public XmlaStreamException CreateStreamException(ConnectionExceptionCause? exceptionCause = null)
				{
					if (this.response != null)
					{
						if (exceptionCause != null)
						{
							return new XmlaStreamException(XmlaSR.HttpStream_ResponseWithFailedStatus(((int)this.response.StatusCode).ToString(), this.response.ReasonPhrase), exceptionCause.Value);
						}
						return new XmlaStreamException(XmlaSR.HttpStream_ResponseWithFailedStatus(((int)this.response.StatusCode).ToString(), this.response.ReasonPhrase));
					}
					else
					{
						if (exceptionCause != null)
						{
							return new XmlaStreamException(this.error, exceptionCause.Value);
						}
						return new XmlaStreamException(this.error);
					}
				}

				// Token: 0x0600146A RID: 5226 RVA: 0x000455E6 File Offset: 0x000437E6
				public Exception CreatePaasInfraConnectionException(AsInstanceType asInstanceType)
				{
					if (this.response != null)
					{
						return ASAzureUtility.GetConnectionException(this.response, asInstanceType);
					}
					return ASAzureUtility.GetConnectionException(this.error, asInstanceType);
				}

				// Token: 0x040011E1 RID: 4577
				private WebException error;

				// Token: 0x040011E2 RID: 4578
				private HttpResponseMessage response;
			}

			// Token: 0x02000201 RID: 513
			protected sealed class RequestPayloadStream : Stream
			{
				// Token: 0x0600146B RID: 5227 RVA: 0x00045609 File Offset: 0x00043809
				public RequestPayloadStream(long maxQueueSize, int defaultBufferSize)
				{
					this.maxQueueSize = maxQueueSize;
					this.defaultBufferSize = defaultBufferSize;
					this.pendingChunks = new Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk>();
				}

				// Token: 0x17000688 RID: 1672
				// (get) Token: 0x0600146C RID: 5228 RVA: 0x0004562A File Offset: 0x0004382A
				public override bool CanRead
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000689 RID: 1673
				// (get) Token: 0x0600146D RID: 5229 RVA: 0x0004562D File Offset: 0x0004382D
				public override bool CanWrite
				{
					get
					{
						return !this.IsCompleted();
					}
				}

				// Token: 0x1700068A RID: 1674
				// (get) Token: 0x0600146E RID: 5230 RVA: 0x00045638 File Offset: 0x00043838
				public override bool CanSeek
				{
					get
					{
						return false;
					}
				}

				// Token: 0x1700068B RID: 1675
				// (get) Token: 0x0600146F RID: 5231 RVA: 0x0004563B File Offset: 0x0004383B
				// (set) Token: 0x06001470 RID: 5232 RVA: 0x00045643 File Offset: 0x00043843
				public override long Position
				{
					get
					{
						return this.position;
					}
					set
					{
						throw new NotSupportedException(XmlaSR.HttpStream_RequestPayloadStream_InvalidStreamOperation);
					}
				}

				// Token: 0x1700068C RID: 1676
				// (get) Token: 0x06001471 RID: 5233 RVA: 0x0004564F File Offset: 0x0004384F
				public override long Length
				{
					get
					{
						return this.length;
					}
				}

				// Token: 0x06001472 RID: 5234 RVA: 0x00045658 File Offset: 0x00043858
				public void MarkAsCompleted()
				{
					Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> queue = this.pendingChunks;
					lock (queue)
					{
						if (this.HasPendingReadRequest() && !this.pendingReadRequest.IsReady(this.defaultBufferSize))
						{
							this.pendingReadRequest.Complete(this, null);
						}
						if (this.HasActiveWriteChunk())
						{
							this.PushWriteChunkToQueue();
						}
						this.isCompleted = true;
					}
				}

				// Token: 0x06001473 RID: 5235 RVA: 0x000456D0 File Offset: 0x000438D0
				public override int Read(byte[] buffer, int offset, int count)
				{
					if (buffer == null)
					{
						throw new ArgumentNullException("buffer");
					}
					if (offset < 0 || offset > buffer.Length - 1)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count <= 0 || offset + count > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					if (this.IsCompleted())
					{
						return 0;
					}
					Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> queue = this.pendingChunks;
					int num2;
					lock (queue)
					{
						int num;
						if (this.TryCompleteReadRequest(buffer, offset, count, out num))
						{
							num2 = num;
						}
						else
						{
							num2 = this.WaitForReadRequestCompletion(buffer, offset, offset + num, count - num);
						}
					}
					return num2;
				}

				// Token: 0x06001474 RID: 5236 RVA: 0x00045774 File Offset: 0x00043974
				public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				{
					if (buffer == null)
					{
						throw new ArgumentNullException("buffer");
					}
					if (offset < 0 || offset > buffer.Length - 1)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count <= 0 || offset + count > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					if (this.IsCompleted())
					{
						return HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.CreateCompleted(callback, state, 0);
					}
					Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> queue = this.pendingChunks;
					int num;
					lock (queue)
					{
						if (!this.TryCompleteReadRequest(buffer, offset, count, out num))
						{
							return this.QueueIncompleteAsyncReadRequest(buffer, offset, offset + num, count - num, callback, state);
						}
					}
					return HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.CreateCompleted(callback, state, num);
				}

				// Token: 0x06001475 RID: 5237 RVA: 0x0004582C File Offset: 0x00043A2C
				public override int EndRead(IAsyncResult asyncResult)
				{
					return HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.End(asyncResult);
				}

				// Token: 0x06001476 RID: 5238 RVA: 0x00045834 File Offset: 0x00043A34
				public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
				{
					if (buffer == null)
					{
						throw new ArgumentNullException("buffer");
					}
					if (offset < 0 || offset > buffer.Length - 1)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count <= 0 || offset + count > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					if (cancellationToken.IsCancellationRequested)
					{
						return Task.FromCanceled<int>(cancellationToken);
					}
					if (this.IsCompleted())
					{
						return AsyncHelper.GetCompletedTaskWithDefaultValue<int>();
					}
					Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> queue = this.pendingChunks;
					IAsyncResult asyncResult;
					lock (queue)
					{
						int num;
						if (this.TryCompleteReadRequest(buffer, offset, count, out num))
						{
							return Task.FromResult<int>(num);
						}
						asyncResult = this.QueueIncompleteAsyncReadRequest(buffer, offset, offset + num, count - num, null, null);
					}
					return new TaskFactory<int>(cancellationToken).FromAsync(asyncResult, new Func<IAsyncResult, int>(HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.End));
				}

				// Token: 0x06001477 RID: 5239 RVA: 0x00045910 File Offset: 0x00043B10
				public override void Write(byte[] buffer, int offset, int count)
				{
					if (this.isCompleted)
					{
						throw new InvalidOperationException(XmlaSR.HttpStream_RequestPayloadStream_WriteAfterComplete);
					}
					Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> queue = this.pendingChunks;
					lock (queue)
					{
						do
						{
							if (this.IsOutgoingQueueFull())
							{
								this.WaitForAdditionalSpaceInOutgoingQueue();
							}
							this.WriteNextChunk(buffer, ref offset, ref count);
						}
						while (count > 0);
					}
				}

				// Token: 0x06001478 RID: 5240 RVA: 0x0004597C File Offset: 0x00043B7C
				public override void Flush()
				{
				}

				// Token: 0x06001479 RID: 5241 RVA: 0x0004597E File Offset: 0x00043B7E
				public override long Seek(long offset, SeekOrigin origin)
				{
					throw new NotSupportedException(XmlaSR.HttpStream_RequestPayloadStream_InvalidStreamOperation);
				}

				// Token: 0x0600147A RID: 5242 RVA: 0x0004598A File Offset: 0x00043B8A
				public override void SetLength(long value)
				{
					throw new NotSupportedException(XmlaSR.HttpStream_RequestPayloadStream_InvalidStreamOperation);
				}

				// Token: 0x0600147B RID: 5243 RVA: 0x00045998 File Offset: 0x00043B98
				private bool TryCompleteReadRequest(byte[] buffer, int offset, int count, out int bytesRead)
				{
					int num = offset;
					int num2;
					do
					{
						num2 = this.ReadNextChunk(buffer, offset, count);
						if (num2 > 0)
						{
							offset += num2;
							count -= num2;
						}
					}
					while (num2 > 0 && !HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest.IsReady(this.defaultBufferSize, num, offset, count));
					bytesRead = offset - num;
					return HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest.IsReady(this.defaultBufferSize, num, offset, count) || this.IsCompleted();
				}

				// Token: 0x0600147C RID: 5244 RVA: 0x000459F4 File Offset: 0x00043BF4
				private int ReadNextChunk(byte[] buffer, int offset, int count)
				{
					int num;
					if (this.HasActiveReadChunk())
					{
						num = this.currentReadChunk.Read(buffer, offset, count);
						this.position += (long)num;
						if (this.currentReadChunk.Offset >= this.currentReadChunk.Count)
						{
							if (this.pendingChunks.Count > 0)
							{
								this.currentReadChunk = this.pendingChunks.Dequeue();
							}
							else
							{
								this.currentReadChunk = default(HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk);
							}
						}
					}
					else if (this.HasActiveWriteChunk() && this.currentWriteChunk.Offset < this.currentWriteChunk.Count)
					{
						num = this.currentWriteChunk.Read(buffer, offset, count);
						this.position += (long)num;
					}
					else
					{
						num = 0;
					}
					if (this.hasPendingWriteRequest && num > 0)
					{
						Monitor.Pulse(this.pendingChunks);
					}
					return num;
				}

				// Token: 0x0600147D RID: 5245 RVA: 0x00045AC8 File Offset: 0x00043CC8
				private int WaitForReadRequestCompletion(byte[] buffer, int initialOffset, int offset, int count)
				{
					int num;
					try
					{
						this.pendingReadRequest = new HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest(buffer, initialOffset, offset, count, null);
						Monitor.Wait(this.pendingChunks);
						if (this.pendingReadRequest.Error != null)
						{
							throw this.pendingReadRequest.Error;
						}
						num = this.pendingReadRequest.Offset - this.pendingReadRequest.InitialOffset;
					}
					finally
					{
						this.pendingReadRequest = default(HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest);
					}
					return num;
				}

				// Token: 0x0600147E RID: 5246 RVA: 0x00045B44 File Offset: 0x00043D44
				private IAsyncResult QueueIncompleteAsyncReadRequest(byte[] buffer, int initialOffset, int offset, int count, AsyncCallback callback, object state)
				{
					HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult readAsyncResult = new HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult(callback, state);
					this.pendingReadRequest = new HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest(buffer, initialOffset, offset, count, readAsyncResult);
					return readAsyncResult;
				}

				// Token: 0x0600147F RID: 5247 RVA: 0x00045B70 File Offset: 0x00043D70
				private void WaitForAdditionalSpaceInOutgoingQueue()
				{
					try
					{
						this.hasPendingWriteRequest = true;
						Monitor.Wait(this.pendingChunks);
					}
					finally
					{
						this.hasPendingWriteRequest = false;
					}
				}

				// Token: 0x06001480 RID: 5248 RVA: 0x00045BAC File Offset: 0x00043DAC
				private void WriteNextChunk(byte[] buffer, ref int offset, ref int count)
				{
					if (this.HasPendingReadRequest() && !this.pendingReadRequest.IsReady(this.defaultBufferSize))
					{
						try
						{
							int num = this.pendingReadRequest.Write(buffer, ref offset, ref count);
							this.length += (long)num;
							this.position += (long)num;
							if (this.pendingReadRequest.IsReady(this.defaultBufferSize))
							{
								this.pendingReadRequest.Complete(this, null);
							}
							return;
						}
						catch (Exception ex)
						{
							if (FrameworkRuntimeHelper.IsFatalException(ex))
							{
								throw;
							}
							this.pendingReadRequest.Complete(this, ex);
							throw new XmlaStreamException(XmlaSR.HttpStream_RequestPayloadStream_ErrorInWrite(ex.Message, Environment.NewLine));
						}
					}
					if (this.HasActiveWriteChunk())
					{
						int num = this.currentWriteChunk.Write(buffer, ref offset, ref count);
						this.length += (long)num;
					}
					else
					{
						long num2 = this.maxQueueSize - (this.length - this.position);
						if (num2 >= (long)count && num2 >= (long)this.defaultBufferSize)
						{
							this.currentWriteChunk = new HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk(this.defaultBufferSize, buffer, offset, count);
							int num = count;
							offset += count;
							count = 0;
							this.length += (long)num;
						}
						else
						{
							this.currentWriteChunk = new HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk((int)num2);
							int num = this.currentWriteChunk.Write(buffer, ref offset, ref count);
							this.length += (long)num;
						}
					}
					if (this.currentWriteChunk.IsFull)
					{
						this.PushWriteChunkToQueue();
					}
				}

				// Token: 0x06001481 RID: 5249 RVA: 0x00045D2C File Offset: 0x00043F2C
				private void PushWriteChunkToQueue()
				{
					if (this.currentWriteChunk.Offset < this.currentWriteChunk.Count)
					{
						if (this.HasActiveReadChunk())
						{
							this.pendingChunks.Enqueue(this.currentWriteChunk);
						}
						else
						{
							this.currentReadChunk = this.currentWriteChunk;
						}
					}
					this.currentWriteChunk = default(HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk);
				}

				// Token: 0x06001482 RID: 5250 RVA: 0x00045D84 File Offset: 0x00043F84
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool IsOutgoingQueueFull()
				{
					return this.length - this.position >= this.maxQueueSize;
				}

				// Token: 0x06001483 RID: 5251 RVA: 0x00045D9E File Offset: 0x00043F9E
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool HasPendingReadRequest()
				{
					return this.pendingReadRequest.Buffer != null;
				}

				// Token: 0x06001484 RID: 5252 RVA: 0x00045DAE File Offset: 0x00043FAE
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool HasActiveReadChunk()
				{
					return this.currentReadChunk.Buffer != null;
				}

				// Token: 0x06001485 RID: 5253 RVA: 0x00045DBE File Offset: 0x00043FBE
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool HasActiveWriteChunk()
				{
					return this.currentWriteChunk.Buffer != null;
				}

				// Token: 0x06001486 RID: 5254 RVA: 0x00045DCE File Offset: 0x00043FCE
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool IsCompleted()
				{
					return this.isCompleted && this.position == this.length;
				}

				// Token: 0x040011E3 RID: 4579
				private readonly long maxQueueSize;

				// Token: 0x040011E4 RID: 4580
				private readonly int defaultBufferSize;

				// Token: 0x040011E5 RID: 4581
				private readonly Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> pendingChunks;

				// Token: 0x040011E6 RID: 4582
				private HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk currentReadChunk;

				// Token: 0x040011E7 RID: 4583
				private HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk currentWriteChunk;

				// Token: 0x040011E8 RID: 4584
				private HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest pendingReadRequest;

				// Token: 0x040011E9 RID: 4585
				private bool hasPendingWriteRequest;

				// Token: 0x040011EA RID: 4586
				private long position;

				// Token: 0x040011EB RID: 4587
				private long length;

				// Token: 0x040011EC RID: 4588
				private bool isCompleted;

				// Token: 0x02000222 RID: 546
				private struct ReadRequest
				{
					// Token: 0x06001508 RID: 5384 RVA: 0x00046B47 File Offset: 0x00044D47
					public ReadRequest(byte[] buffer, int initialOffset, int offset, int count, HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult asyncResult)
					{
						this.Buffer = buffer;
						this.InitialOffset = initialOffset;
						this.Offset = offset;
						this.Count = count;
						this.asyncResult = asyncResult;
						this.Error = null;
					}

					// Token: 0x1700069A RID: 1690
					// (get) Token: 0x06001509 RID: 5385 RVA: 0x00046B75 File Offset: 0x00044D75
					public byte[] Buffer { get; }

					// Token: 0x1700069B RID: 1691
					// (get) Token: 0x0600150A RID: 5386 RVA: 0x00046B7D File Offset: 0x00044D7D
					public int InitialOffset { get; }

					// Token: 0x1700069C RID: 1692
					// (get) Token: 0x0600150B RID: 5387 RVA: 0x00046B85 File Offset: 0x00044D85
					// (set) Token: 0x0600150C RID: 5388 RVA: 0x00046B8D File Offset: 0x00044D8D
					public int Offset { get; private set; }

					// Token: 0x1700069D RID: 1693
					// (get) Token: 0x0600150D RID: 5389 RVA: 0x00046B96 File Offset: 0x00044D96
					// (set) Token: 0x0600150E RID: 5390 RVA: 0x00046B9E File Offset: 0x00044D9E
					public int Count { get; private set; }

					// Token: 0x1700069E RID: 1694
					// (get) Token: 0x0600150F RID: 5391 RVA: 0x00046BA7 File Offset: 0x00044DA7
					// (set) Token: 0x06001510 RID: 5392 RVA: 0x00046BAF File Offset: 0x00044DAF
					public Exception Error { get; private set; }

					// Token: 0x06001511 RID: 5393 RVA: 0x00046BB8 File Offset: 0x00044DB8
					[MethodImpl(MethodImplOptions.AggressiveInlining)]
					public static bool IsReady(int minSize, int initialOffset, int offset, int count)
					{
						return count == 0 || offset - initialOffset >= minSize;
					}

					// Token: 0x06001512 RID: 5394 RVA: 0x00046BC8 File Offset: 0x00044DC8
					[MethodImpl(MethodImplOptions.AggressiveInlining)]
					public bool IsReady(int minSize)
					{
						return HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest.IsReady(minSize, this.InitialOffset, this.Offset, this.Count);
					}

					// Token: 0x06001513 RID: 5395 RVA: 0x00046BE4 File Offset: 0x00044DE4
					public int Write(byte[] buffer, ref int offset, ref int count)
					{
						int num = ((this.Count < count) ? this.Count : count);
						global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, this.Offset, num);
						this.Offset += num;
						this.Count -= num;
						offset += num;
						count -= num;
						return num;
					}

					// Token: 0x06001514 RID: 5396 RVA: 0x00046C44 File Offset: 0x00044E44
					public void Complete(HttpStream.HttpChannelController.RequestPayloadStream owner, Exception error)
					{
						if (this.asyncResult != null)
						{
							try
							{
								this.asyncResult.Complete(this.Offset - this.InitialOffset, error);
								return;
							}
							finally
							{
								owner.pendingReadRequest = default(HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest);
							}
						}
						this.Error = error;
						Monitor.Pulse(owner.pendingChunks);
					}

					// Token: 0x0400122C RID: 4652
					private HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult asyncResult;
				}

				// Token: 0x02000223 RID: 547
				private struct PayloadChunk
				{
					// Token: 0x06001515 RID: 5397 RVA: 0x00046CA8 File Offset: 0x00044EA8
					public PayloadChunk(int bufferSize)
					{
						this.Buffer = new byte[bufferSize];
						this.Offset = 0;
						this.Count = 0;
					}

					// Token: 0x06001516 RID: 5398 RVA: 0x00046CC4 File Offset: 0x00044EC4
					public PayloadChunk(int bufferSize, byte[] buffer, int offset, int count)
					{
						this.Buffer = ((count > bufferSize) ? new byte[count] : new byte[bufferSize]);
						this.Offset = 0;
						this.Count = count;
						global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, 0, count);
					}

					// Token: 0x1700069F RID: 1695
					// (get) Token: 0x06001517 RID: 5399 RVA: 0x00046CFF File Offset: 0x00044EFF
					public byte[] Buffer { get; }

					// Token: 0x170006A0 RID: 1696
					// (get) Token: 0x06001518 RID: 5400 RVA: 0x00046D07 File Offset: 0x00044F07
					// (set) Token: 0x06001519 RID: 5401 RVA: 0x00046D0F File Offset: 0x00044F0F
					public int Offset { get; private set; }

					// Token: 0x170006A1 RID: 1697
					// (get) Token: 0x0600151A RID: 5402 RVA: 0x00046D18 File Offset: 0x00044F18
					// (set) Token: 0x0600151B RID: 5403 RVA: 0x00046D20 File Offset: 0x00044F20
					public int Count { get; private set; }

					// Token: 0x170006A2 RID: 1698
					// (get) Token: 0x0600151C RID: 5404 RVA: 0x00046D29 File Offset: 0x00044F29
					public bool IsFull
					{
						[MethodImpl(MethodImplOptions.AggressiveInlining)]
						get
						{
							return this.Count == this.Buffer.Length;
						}
					}

					// Token: 0x0600151D RID: 5405 RVA: 0x00046D3C File Offset: 0x00044F3C
					public int Read(byte[] buffer, int offset, int count)
					{
						int num = this.Count - this.Offset;
						if (num == 0)
						{
							return 0;
						}
						int num2 = ((num < count) ? num : count);
						global::System.Buffer.BlockCopy(this.Buffer, this.Offset, buffer, offset, num2);
						this.Offset += num2;
						return num2;
					}

					// Token: 0x0600151E RID: 5406 RVA: 0x00046D88 File Offset: 0x00044F88
					public int Write(byte[] buffer, ref int offset, ref int count)
					{
						int num = this.Buffer.Length - this.Count;
						int num2 = ((num < count) ? num : count);
						global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, this.Count, num2);
						this.Count += num2;
						offset += num2;
						count -= num2;
						return num2;
					}
				}

				// Token: 0x02000224 RID: 548
				private sealed class ReadAsyncResult : IAsyncResult
				{
					// Token: 0x0600151F RID: 5407 RVA: 0x00046DE0 File Offset: 0x00044FE0
					public ReadAsyncResult(AsyncCallback callback, object state)
					{
						this.callback = callback;
						this.AsyncState = state;
						this.@lock = new object();
					}

					// Token: 0x170006A3 RID: 1699
					// (get) Token: 0x06001520 RID: 5408 RVA: 0x00046E01 File Offset: 0x00045001
					public object AsyncState { get; }

					// Token: 0x170006A4 RID: 1700
					// (get) Token: 0x06001521 RID: 5409 RVA: 0x00046E0C File Offset: 0x0004500C
					public WaitHandle AsyncWaitHandle
					{
						get
						{
							if (this.completionEvent != null)
							{
								return this.completionEvent;
							}
							object obj = this.@lock;
							WaitHandle waitHandle;
							lock (obj)
							{
								if (this.completionEvent == null)
								{
									this.completionEvent = new ManualResetEvent(this.IsCompleted);
								}
								waitHandle = this.completionEvent;
							}
							return waitHandle;
						}
					}

					// Token: 0x170006A5 RID: 1701
					// (get) Token: 0x06001522 RID: 5410 RVA: 0x00046E78 File Offset: 0x00045078
					// (set) Token: 0x06001523 RID: 5411 RVA: 0x00046E80 File Offset: 0x00045080
					public bool CompletedSynchronously { get; private set; }

					// Token: 0x170006A6 RID: 1702
					// (get) Token: 0x06001524 RID: 5412 RVA: 0x00046E89 File Offset: 0x00045089
					// (set) Token: 0x06001525 RID: 5413 RVA: 0x00046E91 File Offset: 0x00045091
					public bool IsCompleted { get; private set; }

					// Token: 0x06001526 RID: 5414 RVA: 0x00046E9A File Offset: 0x0004509A
					public static HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult CreateCompleted(AsyncCallback callback, object state, int bytesRead)
					{
						HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult readAsyncResult = new HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult(callback, state);
						readAsyncResult.bytesRead = bytesRead;
						readAsyncResult.CompleteImpl(true);
						return readAsyncResult;
					}

					// Token: 0x06001527 RID: 5415 RVA: 0x00046EB4 File Offset: 0x000450B4
					public static int End(IAsyncResult asyncResult)
					{
						if (asyncResult == null)
						{
							throw new ArgumentNullException("asyncResult");
						}
						HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult readAsyncResult = asyncResult as HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult;
						if (readAsyncResult == null)
						{
							throw new ArgumentException(XmlaSR.HttpStream_RequestPayloadStream_InvalidAsyncResultType, "asyncResult");
						}
						if (readAsyncResult.endWasCalled)
						{
							throw new InvalidOperationException(XmlaSR.HttpStream_RequestPayloadStream_EndReadAlreadyCalled);
						}
						readAsyncResult.endWasCalled = true;
						object obj = readAsyncResult.@lock;
						WaitHandle waitHandle;
						lock (obj)
						{
							if (readAsyncResult.IsCompleted)
							{
								waitHandle = null;
							}
							else
							{
								if (readAsyncResult.completionEvent == null)
								{
									readAsyncResult.completionEvent = new ManualResetEvent(false);
								}
								waitHandle = readAsyncResult.completionEvent;
							}
						}
						if (waitHandle != null)
						{
							waitHandle.WaitOne();
						}
						if (readAsyncResult.completionEvent != null)
						{
							readAsyncResult.completionEvent.Close();
						}
						if (readAsyncResult.ex != null)
						{
							throw readAsyncResult.ex;
						}
						return readAsyncResult.bytesRead;
					}

					// Token: 0x06001528 RID: 5416 RVA: 0x00046F8C File Offset: 0x0004518C
					public void Complete(int bytesRead, Exception ex)
					{
						this.bytesRead = bytesRead;
						this.ex = ex;
						this.CompleteImpl(false);
					}

					// Token: 0x06001529 RID: 5417 RVA: 0x00046FA4 File Offset: 0x000451A4
					private void CompleteImpl(bool completedSynchronously)
					{
						this.CompletedSynchronously = completedSynchronously;
						if (completedSynchronously)
						{
							this.IsCompleted = true;
						}
						else
						{
							object obj = this.@lock;
							lock (obj)
							{
								this.IsCompleted = true;
								if (this.completionEvent != null)
								{
									this.completionEvent.Set();
								}
							}
						}
						if (this.callback != null)
						{
							if (completedSynchronously)
							{
								try
								{
									this.callback(this);
									return;
								}
								catch (Exception ex)
								{
									if (FrameworkRuntimeHelper.IsFatalException(ex))
									{
										throw;
									}
									throw new InvalidOperationException(XmlaSR.HttpStream_RequestPayloadStream_ErrorInCallback, ex);
								}
							}
							ThreadPool.QueueUserWorkItem(HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.onDeferredCallback, this);
						}
					}

					// Token: 0x04001235 RID: 4661
					private static WaitCallback onDeferredCallback = delegate(object state)
					{
						try
						{
							HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult readAsyncResult = (HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult)state;
							readAsyncResult.callback(readAsyncResult);
						}
						catch (Exception ex)
						{
							if (FrameworkRuntimeHelper.IsFatalException(ex))
							{
								throw;
							}
						}
					};

					// Token: 0x04001236 RID: 4662
					private readonly object @lock;

					// Token: 0x04001237 RID: 4663
					private readonly AsyncCallback callback;

					// Token: 0x04001238 RID: 4664
					private ManualResetEvent completionEvent;

					// Token: 0x04001239 RID: 4665
					private bool endWasCalled;

					// Token: 0x0400123A RID: 4666
					private int bytesRead;

					// Token: 0x0400123B RID: 4667
					private Exception ex;
				}
			}

			// Token: 0x02000202 RID: 514
			private sealed class EmptyHttpContent : HttpContent
			{
				// Token: 0x06001488 RID: 5256 RVA: 0x00045DF0 File Offset: 0x00043FF0
				protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
				{
					if (stream == null)
					{
						throw new ArgumentNullException("stream");
					}
					return AsyncHelper.GetCompletedTaskWithDefaultValue<object>();
				}

				// Token: 0x06001489 RID: 5257 RVA: 0x00045E05 File Offset: 0x00044005
				protected override bool TryComputeLength(out long length)
				{
					length = 0L;
					return true;
				}
			}
		}

		// Token: 0x0200017E RID: 382
		private enum HttpChannelMode
		{
			// Token: 0x04000BD8 RID: 3032
			Unknonw,
			// Token: 0x04000BD9 RID: 3033
			Legacy,
			// Token: 0x04000BDA RID: 3034
			PaasInfra
		}

		// Token: 0x0200017F RID: 383
		private struct ChannelOptions
		{
			// Token: 0x06001263 RID: 4707 RVA: 0x0003FF98 File Offset: 0x0003E198
			public ChannelOptions(ConnectionInfo info, out UserContext context)
			{
				this.defaultTimeoutInMiliseconds = ((info.Timeout > 0) ? (info.Timeout * 1000) : (-1));
				this.certificateThumbprint = info.ClientCertificateThumbprint ?? string.Empty;
				this.flags = HttpStream.ChannelOptions.BitmapOptions.None;
				if (info.IsPaaSInfrastructure)
				{
					this.flags |= (HttpStream.ChannelOptions.BitmapOptions)2;
				}
				else
				{
					this.flags |= (HttpStream.ChannelOptions.BitmapOptions)1;
				}
				if (info.AllowAutoRedirect)
				{
					this.flags |= HttpStream.ChannelOptions.BitmapOptions.AutoRedirect;
				}
				if (info.IsPaaSInfrastructure)
				{
					this.flags |= HttpStream.ChannelOptions.BitmapOptions.PaasInfrastructure;
				}
				if (info.IsPbiDataset)
				{
					this.flags |= HttpStream.ChannelOptions.BitmapOptions.PbiDataset;
				}
				if (info.IsPaaSInfrastructure || info.IsPbiDataset)
				{
					context = new AuthenticatedUserContext(info.AuthHandle);
				}
				else if (info.IntegratedSecurity == IntegratedSecurity.Federated)
				{
					if (string.IsNullOrEmpty(info.IdentityProvider) || string.Compare(info.IdentityProvider, "MsoID", CultureInfo.InvariantCulture, CompareOptions.OrdinalIgnoreCase) != 0)
					{
						throw new NotSupportedException(XmlaSR.ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated);
					}
					UserContext userContext;
					if (!string.IsNullOrEmpty(info.UserID))
					{
						MsoIdUserContext msoIdUserContext = new MsoIdUserContext(info.UserID, info.Password);
						msoIdUserContext.CacheAccessToken = true;
						userContext = msoIdUserContext;
						msoIdUserContext.OnAuthenticationException = new MsoIdAuthenticationExceptionHandler(HttpStream.ChannelOptions.OnMsoIdAuthenticationException);
					}
					else
					{
						MsoIdUserContext msoIdUserContext2 = new MsoIdUserContext();
						msoIdUserContext2.CacheAccessToken = true;
						userContext = msoIdUserContext2;
						msoIdUserContext2.OnAuthenticationException = new MsoIdAuthenticationExceptionHandler(HttpStream.ChannelOptions.OnMsoIdAuthenticationException);
					}
					context = userContext;
				}
				else
				{
					if (info.UserID == null)
					{
						context = WindowsIdentityUserContext.GetCurrent(true, false);
					}
					else
					{
						if (info.Password == null)
						{
							throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Password", "null"));
						}
						context = new HttpStream.ChannelOptions.NetworkCredentialUserContext(info.UserID, info.Password);
					}
					if (info.AuthHandle != null)
					{
						context = new HttpStream.ChannelOptions.AuthenticatedComposedUserContext(info.AuthHandle, context);
					}
				}
				context.TryGetCredentials(out this.credentials, out this.groupName);
				this.groupName = this.groupName ?? string.Empty;
				this.cacheKey = HttpStream.ChannelOptions.BuildCacheKey(this.flags, this.defaultTimeoutInMiliseconds, this.certificateThumbprint, this.groupName);
			}

			// Token: 0x06001264 RID: 4708 RVA: 0x000401B6 File Offset: 0x0003E3B6
			private ChannelOptions(int defaultTimeoutInMiliseconds, string certificateThumbprint, ICredentials credentials, string groupName, HttpStream.ChannelOptions.BitmapOptions flags, string cacheKey)
			{
				this.defaultTimeoutInMiliseconds = defaultTimeoutInMiliseconds;
				this.certificateThumbprint = certificateThumbprint;
				this.credentials = credentials;
				this.groupName = groupName;
				this.flags = flags;
				this.cacheKey = cacheKey;
			}

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x06001265 RID: 4709 RVA: 0x000401E5 File Offset: 0x0003E3E5
			public int DefaultTimeoutInMiliseconds
			{
				get
				{
					return this.defaultTimeoutInMiliseconds;
				}
			}

			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x06001266 RID: 4710 RVA: 0x000401ED File Offset: 0x0003E3ED
			public string CertificateThumbprint
			{
				get
				{
					return this.certificateThumbprint;
				}
			}

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x06001267 RID: 4711 RVA: 0x000401F5 File Offset: 0x0003E3F5
			public ICredentials Credentials
			{
				get
				{
					return this.credentials;
				}
			}

			// Token: 0x17000615 RID: 1557
			// (get) Token: 0x06001268 RID: 4712 RVA: 0x000401FD File Offset: 0x0003E3FD
			public HttpStream.HttpChannelMode Mode
			{
				get
				{
					return this.GetMode();
				}
			}

			// Token: 0x17000616 RID: 1558
			// (get) Token: 0x06001269 RID: 4713 RVA: 0x00040205 File Offset: 0x0003E405
			public bool AllowAutoRedirect
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.AutoRedirect);
				}
			}

			// Token: 0x17000617 RID: 1559
			// (get) Token: 0x0600126A RID: 4714 RVA: 0x00040212 File Offset: 0x0003E412
			public bool AllowGzipCompression
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.GzipCompression);
				}
			}

			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x0600126B RID: 4715 RVA: 0x0004021F File Offset: 0x0003E41F
			public bool IsPaasInfrastructure
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.PaasInfrastructure);
				}
			}

			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x0600126C RID: 4716 RVA: 0x0004022C File Offset: 0x0003E42C
			public bool IsPbiDataset
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.PbiDataset);
				}
			}

			// Token: 0x1700061A RID: 1562
			// (get) Token: 0x0600126D RID: 4717 RVA: 0x00040239 File Offset: 0x0003E439
			internal string CacheKey
			{
				get
				{
					return this.cacheKey;
				}
			}

			// Token: 0x0600126E RID: 4718 RVA: 0x00040241 File Offset: 0x0003E441
			public static string GetCacheKeyFromGlobalObject(IList<object> elements, int index)
			{
				return (string)elements[index + 5];
			}

			// Token: 0x0600126F RID: 4719 RVA: 0x00040254 File Offset: 0x0003E454
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("HTTP Channel:\r\n\tOptions=");
				stringBuilder.Append(this.GetMode().ToString());
				for (int i = 8; i < 32; i++)
				{
					int num = 1 << i;
					if ((this.flags & (HttpStream.ChannelOptions.BitmapOptions)num) != HttpStream.ChannelOptions.BitmapOptions.None)
					{
						HttpStream.ChannelOptions.BitmapOptions bitmapOptions = (HttpStream.ChannelOptions.BitmapOptions)num;
						stringBuilder.AppendFormat(" | {0}", bitmapOptions.ToString());
					}
				}
				stringBuilder.AppendFormat(" [{0}]", (int)this.flags);
				if (this.defaultTimeoutInMiliseconds != -1)
				{
					stringBuilder.AppendFormat("\r\n\tTimeout={0}", this.defaultTimeoutInMiliseconds);
				}
				if (!string.IsNullOrEmpty(this.certificateThumbprint))
				{
					stringBuilder.AppendFormat("\r\n\tCertificate={0}", this.certificateThumbprint);
				}
				if (!string.IsNullOrEmpty(this.groupName))
				{
					stringBuilder.AppendFormat("\r\n\tGroup={0}", this.groupName);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06001270 RID: 4720 RVA: 0x0004033C File Offset: 0x0003E53C
			internal static HttpStream.ChannelOptions DeserializeFromGlobalObject(IList<object> elements, int index)
			{
				return new HttpStream.ChannelOptions((int)elements[index], (string)elements[index + 1], (ICredentials)elements[index + 2], (string)elements[index + 3], (HttpStream.ChannelOptions.BitmapOptions)((int)elements[index + 4]), (string)elements[index + 5]);
			}

			// Token: 0x06001271 RID: 4721 RVA: 0x000403A0 File Offset: 0x0003E5A0
			internal void SerializeToGlobalObject(IList<object> elements)
			{
				elements.Add(this.defaultTimeoutInMiliseconds);
				elements.Add(this.certificateThumbprint);
				elements.Add(this.credentials);
				elements.Add(this.groupName);
				elements.Add((int)this.flags);
				elements.Add(this.cacheKey);
			}

			// Token: 0x06001272 RID: 4722 RVA: 0x00040400 File Offset: 0x0003E600
			private static string BuildCacheKey(HttpStream.ChannelOptions.BitmapOptions flags, int defaultTimeoutInMiliseconds, string certificateThumbprint, string groupName)
			{
				StringBuilder stringBuilder = new StringBuilder("HTTP Channel: ");
				stringBuilder.AppendFormat("Options=0x{0:X8}", (int)flags);
				if (defaultTimeoutInMiliseconds != -1)
				{
					stringBuilder.AppendFormat(", Timeout={0}", defaultTimeoutInMiliseconds);
				}
				if (!string.IsNullOrEmpty(certificateThumbprint))
				{
					stringBuilder.AppendFormat(", Certificate={0}", certificateThumbprint);
				}
				if (!string.IsNullOrEmpty(groupName))
				{
					stringBuilder.AppendFormat(", Group={0}", groupName);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06001273 RID: 4723 RVA: 0x00040470 File Offset: 0x0003E670
			private static void OnMsoIdAuthenticationException(MsoIdAuthenticationException error, out bool ignoreError)
			{
				switch (error.ErrorType)
				{
				case MsoIdAuthenticationError.NotInstalled:
				case MsoIdAuthenticationError.LoadFailure:
					throw new ConnectionException(XmlaSR.Authentication_MsoID_MissingSignInAssistant, error);
				case MsoIdAuthenticationError.InitFailure:
				case MsoIdAuthenticationError.OperationalError:
					throw new ConnectionException(XmlaSR.Authentication_MsoID_InternalError, error);
				case MsoIdAuthenticationError.SsoWithNonDomainUser:
					throw new ConnectionException(XmlaSR.Authentication_MsoID_SsoFailedNonDomainUser, error);
				case MsoIdAuthenticationError.MissingPassword:
					throw new ConnectionException(XmlaSR.ConnectionString_MissingPassword, error);
				case MsoIdAuthenticationError.SsoAuthenticationFailed:
					throw new ConnectionException(XmlaSR.Authentication_MsoID_SsoFailed, error);
				case MsoIdAuthenticationError.AuthenticationFailed:
					throw new ConnectionException(XmlaSR.Authentication_MsoID_InvalidCredentials, error);
				}
				throw new ConnectionException(XmlaSR.InternalError, error);
			}

			// Token: 0x06001274 RID: 4724 RVA: 0x0004050D File Offset: 0x0003E70D
			private HttpStream.HttpChannelMode GetMode()
			{
				return (HttpStream.HttpChannelMode)(this.flags & HttpStream.ChannelOptions.BitmapOptions.ChannelModeMask);
			}

			// Token: 0x06001275 RID: 4725 RVA: 0x0004051B File Offset: 0x0003E71B
			private bool IsEnabled(HttpStream.ChannelOptions.BitmapOptions options)
			{
				return (this.flags & options) == options;
			}

			// Token: 0x04000BDB RID: 3035
			internal const int GlobalObjectElementCount = 6;

			// Token: 0x04000BDC RID: 3036
			private const int GlobalObjectElementOffset_DefaultTimeout = 0;

			// Token: 0x04000BDD RID: 3037
			private const int GlobalObjectElementOffset_CertificateThumbprint = 1;

			// Token: 0x04000BDE RID: 3038
			private const int GlobalObjectElementOffset_Credentials = 2;

			// Token: 0x04000BDF RID: 3039
			private const int GlobalObjectElementOffset_GroupName = 3;

			// Token: 0x04000BE0 RID: 3040
			private const int GlobalObjectElementOffset_Options = 4;

			// Token: 0x04000BE1 RID: 3041
			private const int GlobalObjectElementOffset_CacheKey = 5;

			// Token: 0x04000BE2 RID: 3042
			private readonly int defaultTimeoutInMiliseconds;

			// Token: 0x04000BE3 RID: 3043
			private readonly string certificateThumbprint;

			// Token: 0x04000BE4 RID: 3044
			private readonly ICredentials credentials;

			// Token: 0x04000BE5 RID: 3045
			private readonly string groupName;

			// Token: 0x04000BE6 RID: 3046
			private readonly HttpStream.ChannelOptions.BitmapOptions flags;

			// Token: 0x04000BE7 RID: 3047
			private readonly string cacheKey;

			// Token: 0x02000206 RID: 518
			[Flags]
			private enum BitmapOptions
			{
				// Token: 0x040011F3 RID: 4595
				None = 0,
				// Token: 0x040011F4 RID: 4596
				ChannelModeMask = 255,
				// Token: 0x040011F5 RID: 4597
				AutoRedirect = 256,
				// Token: 0x040011F6 RID: 4598
				GzipCompression = 512,
				// Token: 0x040011F7 RID: 4599
				PaasInfrastructure = 65536,
				// Token: 0x040011F8 RID: 4600
				PbiDataset = 131072
			}

			// Token: 0x02000207 RID: 519
			private sealed class NetworkCredentialUserContext : UserContext
			{
				// Token: 0x06001490 RID: 5264 RVA: 0x00045E80 File Offset: 0x00044080
				public NetworkCredentialUserContext(string userId, string password)
				{
					this.credentials = new NetworkCredential(userId, password);
					using (HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider())
					{
						byte[] array = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
						byte[] bytes = Encoding.UTF8.GetBytes(userId);
						this.connectionGroupName = string.Format("{0}|{1}", Convert.ToBase64String(bytes), Convert.ToBase64String(array));
					}
				}

				// Token: 0x06001491 RID: 5265 RVA: 0x00045EFC File Offset: 0x000440FC
				public override bool TryGetCredentials(out ICredentials credentials, out string groupName)
				{
					credentials = this.credentials;
					groupName = this.connectionGroupName;
					return true;
				}

				// Token: 0x06001492 RID: 5266 RVA: 0x00045F0F File Offset: 0x0004410F
				protected override void ExecuteInUserContextImpl(Action action)
				{
					action();
				}

				// Token: 0x06001493 RID: 5267 RVA: 0x00045F17 File Offset: 0x00044117
				protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
				{
					return action();
				}

				// Token: 0x06001494 RID: 5268 RVA: 0x00045F1F File Offset: 0x0004411F
				protected override void UpdateHttpRequestImpl(HttpWebRequest request)
				{
					request.Credentials = this.credentials;
					request.ConnectionGroupName = this.connectionGroupName;
				}

				// Token: 0x06001495 RID: 5269 RVA: 0x00045F39 File Offset: 0x00044139
				protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
				{
				}

				// Token: 0x040011F9 RID: 4601
				private ICredentials credentials;

				// Token: 0x040011FA RID: 4602
				private string connectionGroupName;
			}

			// Token: 0x02000208 RID: 520
			private sealed class AuthenticatedComposedUserContext : UserContext
			{
				// Token: 0x06001496 RID: 5270 RVA: 0x00045F3B File Offset: 0x0004413B
				public AuthenticatedComposedUserContext(AuthenticationHandle handle, UserContext context)
				{
					this.handle = handle;
					this.context = context;
				}

				// Token: 0x06001497 RID: 5271 RVA: 0x00045F51 File Offset: 0x00044151
				public override bool TryGetCredentials(out ICredentials credentials, out string groupName)
				{
					return this.context.TryGetCredentials(out credentials, out groupName);
				}

				// Token: 0x06001498 RID: 5272 RVA: 0x00045F60 File Offset: 0x00044160
				protected override void ExecuteInUserContextImpl(Action action)
				{
					this.context.ExecuteInUserContext(action);
				}

				// Token: 0x06001499 RID: 5273 RVA: 0x00045F6E File Offset: 0x0004416E
				protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
				{
					return this.context.ExecuteInUserContext<TResult>(action);
				}

				// Token: 0x0600149A RID: 5274 RVA: 0x00045F7C File Offset: 0x0004417C
				protected override void UpdateHttpRequestImpl(HttpWebRequest request)
				{
					this.context.UpdateHttpRequest(request);
					request.Headers.Add(HttpRequestHeader.Authorization, string.Format("{0} {1}", this.handle.AuthenticationScheme, this.handle.GetAccessToken()));
				}

				// Token: 0x0600149B RID: 5275 RVA: 0x00045FB7 File Offset: 0x000441B7
				protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
				{
					this.context.UpdateHttpRequest(request);
					request.Headers.Authorization = new AuthenticationHeaderValue(this.handle.AuthenticationScheme, this.handle.GetAccessToken());
				}

				// Token: 0x040011FB RID: 4603
				private AuthenticationHandle handle;

				// Token: 0x040011FC RID: 4604
				private UserContext context;
			}
		}

		// Token: 0x02000180 RID: 384
		private static class HttpChannelManager
		{
			// Token: 0x06001276 RID: 4726 RVA: 0x00040528 File Offset: 0x0003E728
			public static void GetControllerAndUserContext(ConnectionInfo info, CookieContainer cookieContainer, out HttpStream.HttpChannelController controller, out UserContext context)
			{
				HttpStream.ChannelOptions channelOptions = new HttpStream.ChannelOptions(info, out context);
				if (!HttpStream.HttpChannelManager.controllers.TryGetResource<HttpStream.HttpChannelController>(channelOptions.CacheKey, out controller))
				{
					HttpStream.HttpChannelMode mode = channelOptions.Mode;
					if (mode != HttpStream.HttpChannelMode.Legacy)
					{
						if (mode != HttpStream.HttpChannelMode.PaasInfra)
						{
							throw new NotSupportedException(string.Format("Invalid channel mode - '{0}' is not supported!", channelOptions.Mode));
						}
						controller = new HttpStream.PaasInfraController(channelOptions, cookieContainer);
					}
					else
					{
						controller = new HttpStream.LegacyController(channelOptions, cookieContainer);
					}
					HttpStream.HttpChannelManager.controllers.Insert<HttpStream.HttpChannelController>(channelOptions.CacheKey, ref controller);
				}
			}

			// Token: 0x06001277 RID: 4727 RVA: 0x000405A8 File Offset: 0x0003E7A8
			public static void ReleaseController(HttpStream.HttpChannelController controller)
			{
				HttpStream.HttpChannelManager.controllers.Remove(controller.CacheKey);
			}

			// Token: 0x06001278 RID: 4728 RVA: 0x000405BC File Offset: 0x0003E7BC
			private static void PrepareItemForCaching(string cacheName, ref object item, out string typeCode)
			{
				HttpStream.HttpChannelController httpChannelController = item as HttpStream.HttpChannelController;
				if (httpChannelController != null)
				{
					HttpStream.HttpChannelMode mode = httpChannelController.Mode;
					if (mode != HttpStream.HttpChannelMode.Legacy)
					{
						if (mode != HttpStream.HttpChannelMode.PaasInfra)
						{
							typeCode = null;
						}
						else
						{
							typeCode = "HttpStream.PaasInfraController";
						}
					}
					else
					{
						typeCode = "HttpStream.LegacyController";
					}
					if (!string.IsNullOrEmpty(typeCode))
					{
						item = httpChannelController.ToGlobalObject();
					}
					if (!cacheName.Equals("XmlaLibControllerCache", StringComparison.Ordinal))
					{
						return;
					}
					IDictionary<string, HttpStream.HttpChannelController> dictionary = HttpStream.HttpChannelManager.activeControllers;
					lock (dictionary)
					{
						HttpStream.HttpChannelManager.activeControllers.Remove(httpChannelController.CacheKey);
						return;
					}
				}
				typeCode = null;
			}

			// Token: 0x06001279 RID: 4729 RVA: 0x0004065C File Offset: 0x0003E85C
			private static object ConvertCachedItem(string cacheName, object cachedItem, string typeCode)
			{
				bool flag;
				if (!(typeCode == "HttpStream.LegacyController"))
				{
					if (!(typeCode == "HttpStream.PaasInfraController"))
					{
						return cachedItem;
					}
					flag = false;
				}
				else
				{
					flag = true;
				}
				IList<object> list = (IList<object>)cachedItem;
				HttpStream.HttpChannelController httpChannelController;
				if (cacheName.Equals("XmlaLibControllerManager", StringComparison.Ordinal))
				{
					string cacheKeyFromGlobalObject = HttpStream.ChannelOptions.GetCacheKeyFromGlobalObject(list, 0);
					IDictionary<string, HttpStream.HttpChannelController> dictionary = HttpStream.HttpChannelManager.activeControllers;
					bool flag3;
					lock (dictionary)
					{
						if (HttpStream.HttpChannelManager.activeControllers.TryGetValue(cacheKeyFromGlobalObject, out httpChannelController))
						{
							httpChannelController.RefreshFromGlobalObject(list, out flag3);
						}
						else
						{
							httpChannelController = HttpStream.HttpChannelManager.CreateControllerFromGlobalObject(flag, list, out flag3);
							HttpStream.HttpChannelManager.activeControllers.Add(cacheKeyFromGlobalObject, httpChannelController);
						}
					}
					if (flag3)
					{
						HttpStream.HttpChannelManager.controllers.ReplaceSharedResource(httpChannelController.CacheKey, httpChannelController.ToGlobalObject());
					}
				}
				else
				{
					bool flag2;
					httpChannelController = HttpStream.HttpChannelManager.CreateControllerFromGlobalObject(flag, list, out flag2);
				}
				return httpChannelController;
			}

			// Token: 0x0600127A RID: 4730 RVA: 0x00040738 File Offset: 0x0003E938
			private static HttpStream.HttpChannelController CreateControllerFromGlobalObject(bool isLegacyController, IList<object> elements, out bool hasElementsUpdate)
			{
				if (isLegacyController)
				{
					return new HttpStream.LegacyController(elements, out hasElementsUpdate);
				}
				return new HttpStream.PaasInfraController(elements, out hasElementsUpdate);
			}

			// Token: 0x04000BE8 RID: 3048
			private const string ControllerCacheName = "XmlaLibControllerCache";

			// Token: 0x04000BE9 RID: 3049
			private const string ControllerManagerName = "XmlaLibControllerManager";

			// Token: 0x04000BEA RID: 3050
			private const string ControllerTypeCode_Legacy = "HttpStream.LegacyController";

			// Token: 0x04000BEB RID: 3051
			private const string ControllerTypeCode_PaasInfra = "HttpStream.PaasInfraController";

			// Token: 0x04000BEC RID: 3052
			private const int ControllerCacheTimeoutInMinutes = 10;

			// Token: 0x04000BED RID: 3053
			private static SharedMemoryCache controllerCache = SharedMemoryCache.Create("XmlaLibControllerCache", MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)), StringComparer.OrdinalIgnoreCase, new PrepareItemForCaching(HttpStream.HttpChannelManager.PrepareItemForCaching), new ConvertCachedItem(HttpStream.HttpChannelManager.ConvertCachedItem));

			// Token: 0x04000BEE RID: 3054
			private static SharedResourceManager controllers = SharedResourceManager.Create("XmlaLibControllerManager", StringComparer.OrdinalIgnoreCase, HttpStream.HttpChannelManager.controllerCache, new PrepareItemForCaching(HttpStream.HttpChannelManager.PrepareItemForCaching), new ConvertCachedItem(HttpStream.HttpChannelManager.ConvertCachedItem));

			// Token: 0x04000BEF RID: 3055
			private static IDictionary<string, HttpStream.HttpChannelController> activeControllers = new Dictionary<string, HttpStream.HttpChannelController>();
		}

		// Token: 0x02000181 RID: 385
		private sealed class LegacyController : HttpStream.HttpChannelController
		{
			// Token: 0x0600127C RID: 4732 RVA: 0x000407D3 File Offset: 0x0003E9D3
			public LegacyController(HttpStream.ChannelOptions options, CookieContainer cookieContainer)
				: base(options, cookieContainer)
			{
			}

			// Token: 0x0600127D RID: 4733 RVA: 0x000407E0 File Offset: 0x0003E9E0
			internal LegacyController(IList<object> elements, out bool hasElementsUpdate)
			{
				int num;
				base..ctor(elements, out num, out hasElementsUpdate);
			}

			// Token: 0x0600127E RID: 4734 RVA: 0x000407F8 File Offset: 0x0003E9F8
			protected override void GetDefaultStreamHeaders(ConnectionInfo info, Uri dataSource, string coreServer, IDictionary<string, string> headers)
			{
				base.GetDefaultStreamHeaders(info, dataSource, coreServer, headers);
				if (!string.IsNullOrEmpty(info.ApplicationName))
				{
					headers.Add("SspropInitAppName", info.ApplicationName);
				}
				if (info.IsPbiDataset)
				{
					if (!object.Equals(info.ConnectionActivityId, Guid.Empty))
					{
						headers.Add("X-AS-ActivityID", info.ConnectionActivityId.ToString());
					}
					if (!string.IsNullOrEmpty(info.ServiceToServiceToken))
					{
						headers.Add("x-ms-xls2stoken", info.ServiceToServiceToken);
					}
					headers.Add("x-ms-xmlaintendedusage", info.IntendedUsage.ToString("D"));
				}
			}

			// Token: 0x0600127F RID: 4735 RVA: 0x000408B4 File Offset: 0x0003EAB4
			protected override void GetPerRequestHeaders(HttpStream owner, object context, IList<KeyValuePair<string, string>> headers)
			{
				base.GetPerRequestHeaders(owner, context, headers);
				if (!string.IsNullOrEmpty(owner.soapActionHeader))
				{
					headers.Add(new KeyValuePair<string, string>(owner.soapActionHeader, owner.soapActionValue));
				}
				headers.Add(new KeyValuePair<string, string>("X-Transport-Caps-Negotiation-Flags", owner.GetTransportCapabilitiesString()));
				if (!string.IsNullOrEmpty(owner.SessionID))
				{
					headers.Add(new KeyValuePair<string, string>("X-AS-SessionID", owner.SessionID));
				}
				if (owner.IsSessionTokenNeeded)
				{
					headers.Add(new KeyValuePair<string, string>("X-AS-GetSessionToken", true.ToString()));
				}
				Guid guid = owner.ActivityID;
				if (object.Equals(guid, Guid.Empty))
				{
					guid = Guid.NewGuid();
				}
				headers.Add(new KeyValuePair<string, string>("X-AS-ActivityID", guid.ToString()));
				if (!object.Equals(owner.RequestID, Guid.Empty))
				{
					headers.Add(new KeyValuePair<string, string>("X-AS-RequestID", owner.RequestID.ToString()));
				}
				if (!object.Equals(owner.CurrentActivityID, Guid.Empty))
				{
					headers.Add(new KeyValuePair<string, string>("X-AS-CurrentActivityID", owner.CurrentActivityID.ToString()));
				}
				if (!string.IsNullOrEmpty(owner.info.RoutingToken))
				{
					headers.Add(new KeyValuePair<string, string>("X-AS-Routing", owner.info.RoutingToken));
				}
			}

			// Token: 0x06001280 RID: 4736 RVA: 0x00040A38 File Offset: 0x0003EC38
			protected override void ProcessWebResponse(HttpStream owner, object context, HttpStream.HttpChannelController.WebResponseInfo response)
			{
				base.ProcessWebResponse(owner, context, response);
				string text;
				if (response.TryGetResponseHeaderValue("OutdatedVersion", out text))
				{
					owner.outdatedVersion = true;
					throw new ConnectionException(XmlaSR.Connection_WorkbookIsOutdated);
				}
				TransportCapabilities transportCapabilities = new TransportCapabilities();
				string text2;
				if (response.TryGetResponseHeaderValue("X-Transport-Caps-Negotiation-Flags", out text2))
				{
					transportCapabilities.FromString(text2);
				}
				transportCapabilities.ContentTypeNegotiated = true;
				owner.SetTransportCapabilities(transportCapabilities);
				owner.info.RoutingToken = response.GetResponseHeaderValue("X-AS-Routing");
			}

			// Token: 0x06001281 RID: 4737 RVA: 0x00040AB2 File Offset: 0x0003ECB2
			protected override HttpStream.HttpXmlaOperation CreateNewOperation(HttpStream owner, bool useHttpClient)
			{
				if (useHttpClient)
				{
					return new HttpStream.LegacyController.HttpClientXmlaOperation(owner, this);
				}
				return new HttpStream.LegacyController.WebRequestXmlaOperation(owner, this);
			}

			// Token: 0x06001282 RID: 4738 RVA: 0x00040AC8 File Offset: 0x0003ECC8
			protected override HttpStream.HttpChannelController.WebErrorClass ClassifyWebError(HttpStream owner, object context, HttpStream.HttpChannelController.WebErrorInfo error, out Exception exception)
			{
				if (error.StatusCode == HttpStatusCode.InternalServerError)
				{
					string text;
					if (!error.TryGetResponseHeaderValue("Content-Type", out text))
					{
						exception = null;
						return HttpStream.HttpChannelController.WebErrorClass.Raise;
					}
					if (!HttpStream.contentTypeRegex.Match(text).Success)
					{
						exception = null;
						return HttpStream.HttpChannelController.WebErrorClass.Raise;
					}
					owner.StreamException = error.CreateStreamException(null);
					exception = null;
					return HttpStream.HttpChannelController.WebErrorClass.Ignore;
				}
				else
				{
					if ((error.ErrorStatus == null || error.ErrorStatus.Value == WebExceptionStatus.ProtocolError) && error.StatusCode == HttpStatusCode.Unauthorized)
					{
						exception = error.CreateStreamException(new ConnectionExceptionCause?(ConnectionExceptionCause.AuthenticationFailed));
						return HttpStream.HttpChannelController.WebErrorClass.Raise;
					}
					exception = null;
					return HttpStream.HttpChannelController.WebErrorClass.Raise;
				}
			}

			// Token: 0x06001283 RID: 4739 RVA: 0x00040B78 File Offset: 0x0003ED78
			internal static void GetSandboxDatabaseInformation(string url, string user, string password, bool specificVersion, CookieContainer cookieContainer, out string databaseId, out string databaseName, out string dataSourceVersion)
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Method = "POST";
				httpWebRequest.Timeout = -1;
				ICredentials credentials2;
				if (!string.IsNullOrEmpty(user))
				{
					ICredentials credentials = new NetworkCredential(user, password);
					credentials2 = credentials;
				}
				else
				{
					credentials2 = CredentialCache.DefaultCredentials;
				}
				httpWebRequest.Credentials = credentials2;
				httpWebRequest.UserAgent = "AMO";
				httpWebRequest.ContentLength = 0L;
				httpWebRequest.CookieContainer = cookieContainer;
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					if (httpWebResponse.ContentLength > 0L)
					{
						using (XmlTextReader xmlTextReader = new XmlTextReader(httpWebResponse.GetResponseStream())
						{
							DtdProcessing = DtdProcessing.Prohibit
						})
						{
							try
							{
								xmlTextReader.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
								if (xmlTextReader.IsStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/"))
								{
									xmlTextReader.Skip();
								}
								xmlTextReader.ReadStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
								XmlaClient.CheckForSoapFault(xmlTextReader, new XmlaResult(), true);
								throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected soap:Fault, got {0}", xmlTextReader.Name));
							}
							catch (XmlException ex)
							{
								throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex);
							}
							catch (IOException ex2)
							{
								throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
							}
						}
					}
					databaseId = httpWebResponse.Headers["DatabaseId"];
					if (databaseId != null)
					{
						databaseId = databaseId.Trim();
					}
					if (string.IsNullOrEmpty(databaseId))
					{
						throw new ConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo);
					}
					databaseName = httpWebResponse.Headers["DatabaseName"];
					if (databaseName != null)
					{
						databaseName = databaseName.Trim();
					}
					if (string.IsNullOrEmpty(databaseName))
					{
						throw new ConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo);
					}
					if (!specificVersion)
					{
						dataSourceVersion = httpWebResponse.Headers["DataSourceVersion"];
						if (!string.IsNullOrEmpty(dataSourceVersion))
						{
							dataSourceVersion = dataSourceVersion.Trim();
						}
						if (string.IsNullOrEmpty(dataSourceVersion))
						{
							throw new ConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo);
						}
					}
					else
					{
						dataSourceVersion = null;
					}
				}
			}

			// Token: 0x02000209 RID: 521
			internal static class XmlaHttpHeaders
			{
				// Token: 0x040011FD RID: 4605
				public const string AcquireTokenStats = "X-AS-AcquireTokenStats";

				// Token: 0x040011FE RID: 4606
				public const string ActivityId = "X-AS-ActivityID";

				// Token: 0x040011FF RID: 4607
				public const string TransportCapabilitiesNegotiation = "X-Transport-Caps-Negotiation-Flags";

				// Token: 0x04001200 RID: 4608
				public const string ApplicationName = "SspropInitAppName";

				// Token: 0x04001201 RID: 4609
				public const string SessionId = "X-AS-SessionID";

				// Token: 0x04001202 RID: 4610
				public const string SessionTokenRequest = "X-AS-GetSessionToken";

				// Token: 0x04001203 RID: 4611
				public const string RequestId = "X-AS-RequestID";

				// Token: 0x04001204 RID: 4612
				public const string CurrentActivityId = "X-AS-CurrentActivityID";

				// Token: 0x04001205 RID: 4613
				public const string XASRouting = "X-AS-Routing";

				// Token: 0x04001206 RID: 4614
				public const string OutdatedVersion = "OutdatedVersion";
			}

			// Token: 0x0200020A RID: 522
			private abstract class LegacyXmlaOperation : HttpStream.HttpXmlaOperation
			{
				// Token: 0x0600149C RID: 5276 RVA: 0x00045FEB File Offset: 0x000441EB
				protected LegacyXmlaOperation(HttpStream owner, HttpStream.LegacyController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x0600149D RID: 5277 RVA: 0x00045FF5 File Offset: 0x000441F5
				public sealed override string GetExtendedErrorInfo()
				{
					return string.Empty;
				}

				// Token: 0x0600149E RID: 5278 RVA: 0x00045FFC File Offset: 0x000441FC
				protected sealed override void EnsureCanRead()
				{
					if (base.Owner.outdatedVersion)
					{
						throw new ConnectionException(XmlaSR.Connection_WorkbookIsOutdated);
					}
					base.EnsureCanRead();
				}

				// Token: 0x0600149F RID: 5279 RVA: 0x0004601C File Offset: 0x0004421C
				protected sealed override XmlaDataType GetResponseDataTypeImpl()
				{
					string text;
					if (!this.TryGetResponseHeaderValueImpl("Content-Type", out text))
					{
						return XmlaDataType.Unknown;
					}
					return HttpStream.HttpXmlaOperation.GetResponseDataTypeFromContentType(text);
				}
			}

			// Token: 0x0200020B RID: 523
			private sealed class WebRequestXmlaOperation : HttpStream.LegacyController.LegacyXmlaOperation
			{
				// Token: 0x060014A0 RID: 5280 RVA: 0x00046040 File Offset: 0x00044240
				public WebRequestXmlaOperation(HttpStream owner, HttpStream.LegacyController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x060014A1 RID: 5281 RVA: 0x0004604A File Offset: 0x0004424A
				protected override Stream StartRequestImpl()
				{
					return this.manager.StartRequest(this, true);
				}

				// Token: 0x060014A2 RID: 5282 RVA: 0x00046059 File Offset: 0x00044259
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x060014A3 RID: 5283 RVA: 0x00046067 File Offset: 0x00044267
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x060014A4 RID: 5284 RVA: 0x0004607B File Offset: 0x0004427B
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04001207 RID: 4615
				private HttpStream.HttpXmlaOperation.WebRequestOperationManager manager;
			}

			// Token: 0x0200020C RID: 524
			private sealed class HttpClientXmlaOperation : HttpStream.LegacyController.LegacyXmlaOperation
			{
				// Token: 0x060014A5 RID: 5285 RVA: 0x00046094 File Offset: 0x00044294
				public HttpClientXmlaOperation(HttpStream owner, HttpStream.LegacyController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x060014A6 RID: 5286 RVA: 0x0004609E File Offset: 0x0004429E
				protected override Stream StartRequestImpl()
				{
					return this.manager.StartRequest(this, true);
				}

				// Token: 0x060014A7 RID: 5287 RVA: 0x000460AD File Offset: 0x000442AD
				protected override void CompleteRequestImpl()
				{
					((HttpStream.HttpChannelController.RequestPayloadStream)base.RequestPayload).MarkAsCompleted();
				}

				// Token: 0x060014A8 RID: 5288 RVA: 0x000460BF File Offset: 0x000442BF
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x060014A9 RID: 5289 RVA: 0x000460CD File Offset: 0x000442CD
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x060014AA RID: 5290 RVA: 0x000460E1 File Offset: 0x000442E1
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04001208 RID: 4616
				private HttpStream.HttpXmlaOperation.HttpClientOperationManager manager;
			}
		}

		// Token: 0x02000182 RID: 386
		private sealed class PaasInfraController : HttpStream.HttpChannelController
		{
			// Token: 0x06001284 RID: 4740 RVA: 0x00040DB8 File Offset: 0x0003EFB8
			public PaasInfraController(HttpStream.ChannelOptions options, CookieContainer cookieContainer)
				: base(options, cookieContainer)
			{
			}

			// Token: 0x06001285 RID: 4741 RVA: 0x00040DC4 File Offset: 0x0003EFC4
			internal PaasInfraController(IList<object> elements, out bool hasElementsUpdate)
			{
				int num;
				base..ctor(elements, out num, out hasElementsUpdate);
			}

			// Token: 0x06001286 RID: 4742 RVA: 0x00040DDC File Offset: 0x0003EFDC
			protected override void GetDefaultStreamHeaders(ConnectionInfo info, Uri dataSource, string coreServer, IDictionary<string, string> headers)
			{
				if (info.IsPbiPremiumInternal && string.IsNullOrEmpty(info.Catalog))
				{
					throw new InvalidDataException("Invalid XMLA database");
				}
				HttpStream.PaasInfraController.TransientModelMode transientModelMode = HttpStream.PaasInfraController.TransientModelMode.Disabled;
				if (string.Equals(info.TransientModelMode, "Enabled", StringComparison.InvariantCultureIgnoreCase))
				{
					if (!info.IsPbiPremiumInternal)
					{
						throw new InvalidDataException("Invalid TransientModelMode");
					}
					transientModelMode = HttpStream.PaasInfraController.TransientModelMode.Enabled;
				}
				if ((info.IsPbiPremiumXmlaEp && string.IsNullOrEmpty(info.PbiPremiumWorkspaceObjectId)) || (!info.IsPbiPremiumXmlaEp && !string.IsNullOrEmpty(info.PbiPremiumWorkspaceObjectId)))
				{
					throw new InvalidDataException("Invalid PbiPremiumWorkspaceObjectId");
				}
				base.GetDefaultStreamHeaders(info, dataSource, coreServer, headers);
				headers.Add("x-ms-xmlaserver", coreServer);
				if (info.IsPbiPremiumInternal)
				{
					if (!string.IsNullOrEmpty(info.Catalog))
					{
						headers.Add("x-ms-xmladatabase", info.Catalog);
					}
					if (string.Equals(info.BypassAuthorization, "true", StringComparison.InvariantCultureIgnoreCase))
					{
						headers.Add("x-ms-xmlabypassauthorization", "true");
						if (!string.IsNullOrEmpty(info.RestrictUser))
						{
							headers.Add("x-ms-xmlauser", info.RestrictUser);
						}
						if (!string.IsNullOrEmpty(info.RestrictRoles))
						{
							headers.Add("x-ms-xmlaroles", info.RestrictRoles);
						}
					}
					headers.Add("x-ms-xmlaintendedusage", info.IntendedUsage.ToString("D"));
				}
				if (((info.IsPbiPremiumXmlaEp && !info.IsPbiPremiumXmlaEpWithPowerBIEmbedToken) || info.IsPbiPremiumInternal) && !string.IsNullOrEmpty(info.ContextualIdentity))
				{
					headers.Add("x-ms-xmlacontextualidentity", info.ContextualIdentity);
				}
				if (info.IsPbiPremiumInternal)
				{
					headers.Add("x-ms-xmlatransientmodelmode", Convert.ToString((int)transientModelMode, CultureInfo.InvariantCulture));
				}
				if (info.IsPbiPremiumXmlaEp && info.UseAadTokenInPublicXmlaEP)
				{
					headers.Add("x-ms-xmlaworkspaceobjectid", info.PbiPremiumWorkspaceObjectId);
				}
				if ((info.IsPbiPremiumXmlaEp || info.IsPbiPremiumInternal) && (info.IntendedUsage == IntendedUsage.ScheduledProcessing || info.IntendedUsage == IntendedUsage.InteractiveProcessing))
				{
					headers.Add("x-ms-routing-scenario", "Processing");
				}
				if (info.IsPbiPremiumXmlaEp || info.IsPbiPremiumInternal)
				{
					switch (info.PbipAccessMode)
					{
					case ConnectionAccessMode.ReadWrite:
						headers.Add("x-ms-xmlareadonly", "0");
						break;
					case ConnectionAccessMode.ReadOnly:
						headers.Add("x-ms-xmlareadonly", "1");
						break;
					case ConnectionAccessMode.RestrictedReadOnly:
						headers.Add("x-ms-xmlareadonly", "2");
						break;
					}
					if (info.IsScaleOutAutoSyncEnabled)
					{
						headers.Add("x-ms-xmlascaleoutautosync", "1");
					}
				}
				AuthenticationEndpoint endpoint = info.AuthHandle.Endpoint;
				string text;
				if (endpoint != AuthenticationEndpoint.AadV1)
				{
					if (endpoint != AuthenticationEndpoint.AadV2)
					{
						text = "Unknown";
					}
					else
					{
						text = "MSAL";
					}
				}
				else
				{
					text = "ADAL";
				}
				headers.Add("x-ms-xmlaapp-general-info", AsPaasHelper.BuildGeneralInfoHeader(text, info.GetTokensCacheUsage(), !string.IsNullOrEmpty(info.UserID), info.IsLinkReference, info.ApplicationName));
				if (info.IsBinarySupported)
				{
					headers.Add("x-ms-xmlaclienttraits", "1");
				}
				if (info.IsPbiDataset)
				{
					if (!object.Equals(info.ConnectionActivityId, Guid.Empty))
					{
						headers.Add("X-AS-ActivityID", info.ConnectionActivityId.ToString());
					}
					if (!string.IsNullOrEmpty(info.ServiceToServiceToken))
					{
						headers.Add("x-ms-xls2stoken", info.ServiceToServiceToken);
					}
					headers.Add("x-ms-xmlaintendedusage", info.IntendedUsage.ToString("D"));
				}
				headers.Add("x-ms-xmladedicatedconnection", info.DedicatedAdminConnection ? "1" : "0");
			}

			// Token: 0x06001287 RID: 4743 RVA: 0x00041174 File Offset: 0x0003F374
			protected override void GetPerRequestHeaders(HttpStream owner, object context, IList<KeyValuePair<string, string>> headers)
			{
				HttpStream.PaasInfraController.XmlaOperationContext xmlaOperationContext = (HttpStream.PaasInfraController.XmlaOperationContext)context;
				base.GetPerRequestHeaders(owner, context, headers);
				headers.Add(new KeyValuePair<string, string>("x-ms-xmlacaps-negotiation-flags", xmlaOperationContext.RequestTransportCapabilities));
				if (!string.IsNullOrEmpty(owner.SessionID))
				{
					headers.Add(new KeyValuePair<string, string>("x-ms-xmlasession-id", owner.SessionID));
				}
				headers.Add(new KeyValuePair<string, string>("x-ms-request-registration-id", xmlaOperationContext.RegistrationId.ToString()));
				headers.Add(new KeyValuePair<string, string>("x-ms-accepts-continuations", "1"));
				headers.Add(new KeyValuePair<string, string>("x-ms-round-trip-id", xmlaOperationContext.RoundTripId.ToString()));
				if (!string.IsNullOrEmpty(xmlaOperationContext.RootActivityId))
				{
					headers.Add(new KeyValuePair<string, string>("x-ms-root-activity-id", xmlaOperationContext.RootActivityId));
				}
				headers.Add(new KeyValuePair<string, string>("x-ms-parent-activity-id", xmlaOperationContext.ParentActivityId.ToString()));
				if (owner.info.IsPbiPremiumXmlaEp || owner.info.IsPbiPremiumInternal)
				{
					if (!string.IsNullOrEmpty(owner.info.PbipWorkloadResourceMoniker))
					{
						headers.Add(new KeyValuePair<string, string>("x-ms-workload-resource-moniker", owner.info.PbipWorkloadResourceMoniker));
					}
					if (!string.IsNullOrEmpty(owner.info.PbipCoreServiceRoutingHint))
					{
						headers.Add(new KeyValuePair<string, string>("x-ms-routing-hint", owner.info.PbipCoreServiceRoutingHint));
					}
				}
				if (owner.info.IsPbiPremiumInternal && !xmlaOperationContext.IsUnderLROProtocol && !object.Equals(owner.info.SourceCapacityObjectId, Guid.Empty))
				{
					headers.Add(new KeyValuePair<string, string>("x-ms-src-capacity-id", owner.info.SourceCapacityObjectId.ToString()));
				}
			}

			// Token: 0x06001288 RID: 4744 RVA: 0x0004133B File Offset: 0x0003F53B
			protected override void HandleResponseStatusCode(int statusCode, Exception ex)
			{
				if (statusCode >= 300 && statusCode <= 399)
				{
					AsPaasEndpointInfo.InvalidateCache();
					throw new ConnectionException(XmlaSR.Connection_AnalysisServicesInstanceWasMoved, ex, ConnectionExceptionCause.ServerHasMoved);
				}
				base.HandleResponseStatusCode(statusCode, ex);
			}

			// Token: 0x06001289 RID: 4745 RVA: 0x00041368 File Offset: 0x0003F568
			protected override void ProcessWebResponse(HttpStream owner, object context, HttpStream.HttpChannelController.WebResponseInfo response)
			{
				base.ProcessWebResponse(owner, context, response);
				if (owner.info.IsPbiPremiumInternal)
				{
					if (response.StatusCode == HttpStatusCode.OK)
					{
						owner.info.PbipCoreServiceRoutingHint = response.GetResponseHeaderValue("x-ms-routing-hint");
					}
					else
					{
						owner.info.PbipCoreServiceRoutingHint = string.Empty;
					}
				}
				else if (owner.info.IsPbiPremiumXmlaEp)
				{
					if (response.StatusCode == HttpStatusCode.OK)
					{
						owner.info.PbipWorkloadResourceMoniker = response.GetResponseHeaderValue("x-ms-workload-resource-moniker");
						owner.info.PbipCoreServiceRoutingHint = response.GetResponseHeaderValue("x-ms-routing-hint");
					}
					else
					{
						owner.info.PbipWorkloadResourceMoniker = string.Empty;
						owner.info.PbipCoreServiceRoutingHint = string.Empty;
					}
				}
				string text;
				if (!response.TryGetResponseHeaderValue("x-ms-xmlacaps-negotiation-flags", out text))
				{
					throw new InvalidDataException(string.Format("The '{0}' header is missing from the HTTP response!", "x-ms-xmlacaps-negotiation-flags"));
				}
				((HttpStream.PaasInfraController.XmlaOperationContext)context).ResponseTransportCapabilities = text;
			}

			// Token: 0x0600128A RID: 4746 RVA: 0x00041460 File Offset: 0x0003F660
			protected override HttpStream.HttpXmlaOperation CreateNewOperation(HttpStream owner, bool useHttpClient)
			{
				object obj = new HttpStream.PaasInfraController.XmlaOperationContext(owner);
				if (useHttpClient)
				{
					return new HttpStream.PaasInfraController.HttpClientXmlaOperation(owner, this)
					{
						Context = obj
					};
				}
				return new HttpStream.PaasInfraController.WebRequestXmlaOperation(owner, this)
				{
					Context = obj
				};
			}

			// Token: 0x0600128B RID: 4747 RVA: 0x00041494 File Offset: 0x0003F694
			protected override HttpStream.HttpChannelController.WebErrorClass ClassifyWebError(HttpStream owner, object context, HttpStream.HttpChannelController.WebErrorInfo error, out Exception exception)
			{
				string text;
				if ((owner.info.IsPbiPremiumInternal || owner.info.IsPbiPremiumXmlaEp) && error.StatusCode == HttpStatusCode.BadRequest && error.TryGetResponseHeaderValue("x-ms-wrong-node", out text) && text == "1")
				{
					exception = null;
					return HttpStream.HttpChannelController.WebErrorClass.Ignore;
				}
				if (((HttpStream.PaasInfraController.XmlaOperationContext)context).IsUnderLROProtocol)
				{
					HttpStatusCode statusCode = error.StatusCode;
					if (statusCode == HttpStatusCode.InternalServerError || statusCode == HttpStatusCode.ServiceUnavailable)
					{
						exception = null;
						return HttpStream.HttpChannelController.WebErrorClass.Retry;
					}
				}
				exception = error.CreatePaasInfraConnectionException(owner.info.AsInstanceType);
				return HttpStream.HttpChannelController.WebErrorClass.Raise;
			}

			// Token: 0x0600128C RID: 4748 RVA: 0x0004152D File Offset: 0x0003F72D
			private static int GetMinimumWaitTimeForRetryAttempt(int attempt)
			{
				if (HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt.Length > attempt)
				{
					return HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt[attempt];
				}
				return HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt[HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt.Length - 1];
			}

			// Token: 0x04000BF0 RID: 3056
			private const int MAX_IDLE_CONNECTION_TIMEOUT_IN_MS = 120000;

			// Token: 0x04000BF1 RID: 3057
			private const int LRO_CLIENT_RECONNECT_REQUESTED = 1;

			// Token: 0x04000BF2 RID: 3058
			private const int LRO_CLIENT_REQUEST_FINISHED = 0;

			// Token: 0x04000BF3 RID: 3059
			private static TimeoutUtils.OnTimeoutAction onConnectTimeout = delegate(bool isOnDispose)
			{
				if (!isOnDispose)
				{
					throw new ConnectionException(XmlaSR.XmlaClient_ConnectTimedOut);
				}
				return true;
			};

			// Token: 0x04000BF4 RID: 3060
			private static readonly int[] MinimumWaitTimeInMsForRetryAttempt = new int[] { 100, 200, 400, 800, 1600, 3200, 6400, 10000 };

			// Token: 0x0200020D RID: 525
			private enum TransientModelMode
			{
				// Token: 0x0400120A RID: 4618
				Disabled,
				// Token: 0x0400120B RID: 4619
				Enabled
			}

			// Token: 0x0200020E RID: 526
			private sealed class XmlaOperationContext
			{
				// Token: 0x060014AB RID: 5291 RVA: 0x000460FC File Offset: 0x000442FC
				public XmlaOperationContext(HttpStream owner)
				{
					this.RegistrationId = Guid.NewGuid();
					this.RootActivityId = null;
					this.ParentActivityId = owner.RequestID;
					if (object.Equals(this.ParentActivityId, Guid.Empty))
					{
						this.ParentActivityId = Guid.NewGuid();
					}
					this.RequestTransportCapabilities = owner.GetTransportCapabilitiesString();
					this.roundTripId = 0;
				}

				// Token: 0x1700068D RID: 1677
				// (get) Token: 0x060014AC RID: 5292 RVA: 0x00046167 File Offset: 0x00044367
				// (set) Token: 0x060014AD RID: 5293 RVA: 0x0004616F File Offset: 0x0004436F
				public Guid RegistrationId { get; private set; }

				// Token: 0x1700068E RID: 1678
				// (get) Token: 0x060014AE RID: 5294 RVA: 0x00046178 File Offset: 0x00044378
				// (set) Token: 0x060014AF RID: 5295 RVA: 0x00046180 File Offset: 0x00044380
				public string RootActivityId { get; set; }

				// Token: 0x1700068F RID: 1679
				// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00046189 File Offset: 0x00044389
				// (set) Token: 0x060014B1 RID: 5297 RVA: 0x00046191 File Offset: 0x00044391
				public Guid ParentActivityId { get; private set; }

				// Token: 0x17000690 RID: 1680
				// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0004619A File Offset: 0x0004439A
				public string RequestTransportCapabilities { get; }

				// Token: 0x17000691 RID: 1681
				// (get) Token: 0x060014B3 RID: 5299 RVA: 0x000461A2 File Offset: 0x000443A2
				// (set) Token: 0x060014B4 RID: 5300 RVA: 0x000461AA File Offset: 0x000443AA
				public string ResponseTransportCapabilities { get; set; }

				// Token: 0x17000692 RID: 1682
				// (get) Token: 0x060014B5 RID: 5301 RVA: 0x000461B3 File Offset: 0x000443B3
				public int RoundTripId
				{
					get
					{
						return this.roundTripId;
					}
				}

				// Token: 0x17000693 RID: 1683
				// (get) Token: 0x060014B6 RID: 5302 RVA: 0x000461BB File Offset: 0x000443BB
				public bool IsUnderLROProtocol
				{
					get
					{
						return this.isUnderLROProtocol;
					}
				}

				// Token: 0x17000694 RID: 1684
				// (get) Token: 0x060014B7 RID: 5303 RVA: 0x000461C3 File Offset: 0x000443C3
				// (set) Token: 0x060014B8 RID: 5304 RVA: 0x000461DF File Offset: 0x000443DF
				public XmlaDataType ResponseDataType
				{
					get
					{
						if (this.responseDataType != null)
						{
							return this.responseDataType.Value;
						}
						return XmlaDataType.Unknown;
					}
					set
					{
						this.responseDataType = new XmlaDataType?(value);
					}
				}

				// Token: 0x17000695 RID: 1685
				// (get) Token: 0x060014B9 RID: 5305 RVA: 0x000461ED File Offset: 0x000443ED
				public bool HasResponseDataType
				{
					get
					{
						return this.responseDataType != null;
					}
				}

				// Token: 0x060014BA RID: 5306 RVA: 0x000461FA File Offset: 0x000443FA
				internal void MarkAsUnderLROProtocol()
				{
					this.isUnderLROProtocol = true;
					this.roundTripId++;
				}

				// Token: 0x060014BB RID: 5307 RVA: 0x00046211 File Offset: 0x00044411
				internal void SetDataBytes(byte[] buffer)
				{
					if (buffer != null)
					{
						this.firstDataByte = new byte?(buffer[0]);
						this.lastDataByte = new byte?(buffer[1]);
						return;
					}
					this.firstDataByte = null;
					this.lastDataByte = null;
				}

				// Token: 0x060014BC RID: 5308 RVA: 0x0004624B File Offset: 0x0004444B
				internal bool TryReadFirstDataByte(byte[] buffer, int offset)
				{
					if (this.firstDataByte == null)
					{
						return false;
					}
					buffer[offset] = this.firstDataByte.Value;
					this.firstDataByte = null;
					return true;
				}

				// Token: 0x060014BD RID: 5309 RVA: 0x00046277 File Offset: 0x00044477
				internal void SwitchLastDataByte(byte[] buffer, int offset, byte lastDataByte)
				{
					buffer[offset] = this.lastDataByte.Value;
					this.lastDataByte = new byte?(lastDataByte);
				}

				// Token: 0x060014BE RID: 5310 RVA: 0x00046293 File Offset: 0x00044493
				internal bool TryGetLastDataByte(out byte lastByte)
				{
					if (this.lastDataByte == null)
					{
						lastByte = 0;
						return false;
					}
					lastByte = this.lastDataByte.Value;
					this.lastDataByte = null;
					return true;
				}

				// Token: 0x0400120C RID: 4620
				private int roundTripId;

				// Token: 0x0400120D RID: 4621
				private XmlaDataType? responseDataType;

				// Token: 0x0400120E RID: 4622
				private bool isUnderLROProtocol;

				// Token: 0x0400120F RID: 4623
				private byte? firstDataByte;

				// Token: 0x04001210 RID: 4624
				private byte? lastDataByte;
			}

			// Token: 0x0200020F RID: 527
			private abstract class PaasInfraXmlaOperation : HttpStream.HttpXmlaOperation
			{
				// Token: 0x060014BF RID: 5311 RVA: 0x000462C1 File Offset: 0x000444C1
				protected PaasInfraXmlaOperation(HttpStream owner, HttpStream.PaasInfraController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x060014C0 RID: 5312 RVA: 0x000462CB File Offset: 0x000444CB
				public sealed override string GetExtendedErrorInfo()
				{
					if (!base.Owner.info.IsAsAzure && !base.Owner.info.IsPbiPremiumXmlaEp)
					{
						return string.Empty;
					}
					return this.GetExtendedErrorInfoFromResponse();
				}

				// Token: 0x060014C1 RID: 5313
				protected abstract Stream StartRequestAndGetRequestPayload(bool isReconnect);

				// Token: 0x060014C2 RID: 5314
				protected abstract string GetExtendedErrorInfoFromResponse();

				// Token: 0x060014C3 RID: 5315 RVA: 0x000462FD File Offset: 0x000444FD
				protected sealed override Stream StartRequestImpl()
				{
					return this.StartRequestAndGetRequestPayload(false);
				}

				// Token: 0x060014C4 RID: 5316 RVA: 0x00046308 File Offset: 0x00044508
				protected sealed override void EnsureCanRead()
				{
					if (base.ResponsePayload == null)
					{
						((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).SetDataBytes(null);
						byte[] array = new byte[2];
						TimeoutUtils.TimeLeft timeLeft = TimeoutUtils.TimeLeft.FromMs(120000);
						Stopwatch stopwatch = new Stopwatch();
						using (TimeoutUtils.TimeRestrictedMonitor timeRestrictedMonitor = new TimeoutUtils.TimeRestrictedMonitor(timeLeft, HttpStream.PaasInfraController.onConnectTimeout))
						{
							int num = 0;
							bool flag;
							do
							{
								flag = false;
								try
								{
									stopwatch.Reset();
									stopwatch.Start();
									base.EnsureCanRead();
									if (base.ResponsePayload != null)
									{
										timeRestrictedMonitor.Restart();
										num = 0;
										int num2 = base.ResponsePayload.Read(array, 0, 2);
										if (num2 == 0)
										{
											throw new ConnectionException(XmlaSR.ConnectionBroken);
										}
										bool flag2;
										if (num2 != 1)
										{
											flag2 = true;
										}
										else if (base.ResponsePayload.Read(array, 1, 1) == 0)
										{
											byte b = array[0];
											if (b != 0)
											{
												if (b != 1)
												{
													throw new ConnectionException(XmlaSR.UnknownServerResponseFormat, ConnectionExceptionCause.TransportProtocolError);
												}
												flag = true;
												((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).MarkAsUnderLROProtocol();
											}
											flag2 = false;
										}
										else
										{
											flag2 = true;
										}
										if (flag2)
										{
											((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).SetDataBytes(array);
										}
									}
									else
									{
										flag = true;
										num++;
									}
									if (flag)
									{
										string text;
										if (string.IsNullOrEmpty(((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).RootActivityId) && base.Owner.info.IsPaaSInfrastructure && this.TryGetResponseHeaderValueImpl("x-ms-root-activity-id", out text))
										{
											((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).RootActivityId = text;
										}
										timeRestrictedMonitor.CheckNow();
										int minimumWaitTimeForRetryAttempt = HttpStream.PaasInfraController.GetMinimumWaitTimeForRetryAttempt(num);
										long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
										if ((long)minimumWaitTimeForRetryAttempt > elapsedMilliseconds)
										{
											Thread.Sleep((int)((long)minimumWaitTimeForRetryAttempt - elapsedMilliseconds));
										}
										this.Reset(true, true, false);
										this.StartRequestAndGetRequestPayload(true);
									}
								}
								finally
								{
									if (stopwatch.IsRunning)
									{
										stopwatch.Stop();
									}
								}
							}
							while (flag);
							TransportCapabilities transportCapabilities = new TransportCapabilities();
							transportCapabilities.FromString(((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).ResponseTransportCapabilities);
							transportCapabilities.ContentTypeNegotiated = true;
							base.Owner.SetTransportCapabilities(transportCapabilities);
						}
					}
				}

				// Token: 0x060014C5 RID: 5317 RVA: 0x00046524 File Offset: 0x00044724
				protected sealed override XmlaDataType GetResponseDataTypeImpl()
				{
					if (((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).HasResponseDataType)
					{
						return ((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).ResponseDataType;
					}
					string text;
					XmlaDataType xmlaDataType;
					if (this.TryGetResponseHeaderValueImpl("Content-Type", out text))
					{
						xmlaDataType = HttpStream.HttpXmlaOperation.GetResponseDataTypeFromContentType(text);
						((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).ResponseDataType = xmlaDataType;
					}
					else
					{
						xmlaDataType = XmlaDataType.Unknown;
					}
					return xmlaDataType;
				}

				// Token: 0x060014C6 RID: 5318 RVA: 0x00046580 File Offset: 0x00044780
				protected sealed override int ReadResponsePayloadImpl(byte[] buffer, int offset, int size)
				{
					for (;;)
					{
						int num;
						bool flag;
						if (((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).TryReadFirstDataByte(buffer, offset))
						{
							num = 1;
							flag = false;
						}
						else
						{
							byte[] array = ((size == 1) ? new byte[1] : buffer);
							int num2 = ((size == 1) ? 0 : (offset + 1));
							int num3 = ((size == 1) ? 1 : (size - 1));
							num = base.ResponsePayload.Read(array, num2, num3);
							if (num > 0)
							{
								((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).SwitchLastDataByte(buffer, offset, (size == 1) ? array[0] : buffer[offset + num]);
								flag = false;
							}
							else
							{
								byte b;
								if (!((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).TryGetLastDataByte(out b))
								{
									goto IL_0122;
								}
								if (b != 0)
								{
									if (b != 1)
									{
										break;
									}
									flag = true;
									((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).MarkAsUnderLROProtocol();
									string text;
									if (string.IsNullOrEmpty(((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).RootActivityId) && base.Owner.info.IsPaaSInfrastructure && this.TryGetResponseHeaderValueImpl("x-ms-root-activity-id", out text))
									{
										((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).RootActivityId = text;
									}
									this.Reset(true, true, false);
									this.StartRequestAndGetRequestPayload(true);
									this.EnsureCanRead();
								}
								else
								{
									flag = false;
								}
							}
						}
						if (!flag)
						{
							return num;
						}
					}
					throw new ConnectionException(XmlaSR.UnknownServerResponseFormat, ConnectionExceptionCause.TransportProtocolError);
					IL_0122:
					throw new XmlaStreamException(XmlaSR.HttpStream_InvalidReadRequest(base.Status.ToString()));
				}
			}

			// Token: 0x02000210 RID: 528
			private sealed class WebRequestXmlaOperation : HttpStream.PaasInfraController.PaasInfraXmlaOperation
			{
				// Token: 0x060014C7 RID: 5319 RVA: 0x000466D6 File Offset: 0x000448D6
				public WebRequestXmlaOperation(HttpStream owner, HttpStream.PaasInfraController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x060014C8 RID: 5320 RVA: 0x000466E0 File Offset: 0x000448E0
				protected override Stream StartRequestAndGetRequestPayload(bool isReconnect)
				{
					return this.manager.StartRequest(this, !isReconnect);
				}

				// Token: 0x060014C9 RID: 5321 RVA: 0x000466F2 File Offset: 0x000448F2
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x060014CA RID: 5322 RVA: 0x00046700 File Offset: 0x00044900
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x060014CB RID: 5323 RVA: 0x00046714 File Offset: 0x00044914
				protected override string GetExtendedErrorInfoFromResponse()
				{
					return AsPaasHelper.GetTechnicalDetailsFromPaasInfraResponse(this.manager.Response);
				}

				// Token: 0x060014CC RID: 5324 RVA: 0x00046726 File Offset: 0x00044926
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04001216 RID: 4630
				private HttpStream.HttpXmlaOperation.WebRequestOperationManager manager;
			}

			// Token: 0x02000211 RID: 529
			private sealed class HttpClientXmlaOperation : HttpStream.PaasInfraController.PaasInfraXmlaOperation
			{
				// Token: 0x060014CD RID: 5325 RVA: 0x0004673F File Offset: 0x0004493F
				public HttpClientXmlaOperation(HttpStream owner, HttpStream.PaasInfraController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x060014CE RID: 5326 RVA: 0x00046749 File Offset: 0x00044949
				protected override Stream StartRequestAndGetRequestPayload(bool isReconnect)
				{
					return this.manager.StartRequest(this, !isReconnect);
				}

				// Token: 0x060014CF RID: 5327 RVA: 0x0004675B File Offset: 0x0004495B
				protected override void CompleteRequestImpl()
				{
					((HttpStream.HttpChannelController.RequestPayloadStream)base.RequestPayload).MarkAsCompleted();
				}

				// Token: 0x060014D0 RID: 5328 RVA: 0x0004676D File Offset: 0x0004496D
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x060014D1 RID: 5329 RVA: 0x0004677B File Offset: 0x0004497B
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x060014D2 RID: 5330 RVA: 0x0004678F File Offset: 0x0004498F
				protected override string GetExtendedErrorInfoFromResponse()
				{
					return AsPaasHelper.GetTechnicalDetailsFromPaasInfraResponse(this.manager.Response);
				}

				// Token: 0x060014D3 RID: 5331 RVA: 0x000467A1 File Offset: 0x000449A1
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04001217 RID: 4631
				private HttpStream.HttpXmlaOperation.HttpClientOperationManager manager;
			}
		}

		// Token: 0x02000183 RID: 387
		private enum HttpXmlaOperationStatus
		{
			// Token: 0x04000BF6 RID: 3062
			Idle,
			// Token: 0x04000BF7 RID: 3063
			Request,
			// Token: 0x04000BF8 RID: 3064
			Response,
			// Token: 0x04000BF9 RID: 3065
			Completed,
			// Token: 0x04000BFA RID: 3066
			Error
		}

		// Token: 0x02000184 RID: 388
		private abstract class HttpXmlaOperation : Disposable
		{
			// Token: 0x0600128E RID: 4750 RVA: 0x0004157D File Offset: 0x0003F77D
			protected HttpXmlaOperation(HttpStream owner, HttpStream.HttpChannelController controller)
			{
				this.Owner = owner;
				this.Controller = controller;
			}

			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x0600128F RID: 4751 RVA: 0x00041593 File Offset: 0x0003F793
			public HttpStream Owner { get; }

			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x06001290 RID: 4752 RVA: 0x0004159B File Offset: 0x0003F79B
			public HttpStream.HttpChannelController Controller { get; }

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x06001291 RID: 4753 RVA: 0x000415A3 File Offset: 0x0003F7A3
			// (set) Token: 0x06001292 RID: 4754 RVA: 0x000415AB File Offset: 0x0003F7AB
			public object Context { get; set; }

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x06001293 RID: 4755 RVA: 0x000415B4 File Offset: 0x0003F7B4
			// (set) Token: 0x06001294 RID: 4756 RVA: 0x000415BC File Offset: 0x0003F7BC
			public HttpStream.HttpXmlaOperationStatus Status { get; private set; }

			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x06001295 RID: 4757 RVA: 0x000415C5 File Offset: 0x0003F7C5
			// (set) Token: 0x06001296 RID: 4758 RVA: 0x000415CD File Offset: 0x0003F7CD
			private protected Stream RequestPayload { protected get; private set; }

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x06001297 RID: 4759 RVA: 0x000415D6 File Offset: 0x0003F7D6
			// (set) Token: 0x06001298 RID: 4760 RVA: 0x000415DE File Offset: 0x0003F7DE
			private protected Stream ResponsePayload { protected get; private set; }

			// Token: 0x06001299 RID: 4761 RVA: 0x000415E8 File Offset: 0x0003F7E8
			public void StartRequest()
			{
				try
				{
					this.RequestPayload = this.StartRequestImpl();
					this.Status = HttpStream.HttpXmlaOperationStatus.Request;
				}
				catch (Exception)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Error;
					throw;
				}
			}

			// Token: 0x0600129A RID: 4762 RVA: 0x00041624 File Offset: 0x0003F824
			public void WriteRequestPayload(byte[] buffer, int offset, int size)
			{
				base.ThrowIfAlreadyDisposed();
				if (this.RequestPayload == null)
				{
					throw new InvalidOperationException("Cannot write into the request-payload of a completed request!");
				}
				try
				{
					this.WriteRequestPayloadImpl(buffer, offset, size);
				}
				catch (Exception)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Error;
					throw;
				}
			}

			// Token: 0x0600129B RID: 4763 RVA: 0x00041670 File Offset: 0x0003F870
			public void CompleteRequest()
			{
				base.ThrowIfAlreadyDisposed();
				if (this.RequestPayload == null)
				{
					throw new InvalidOperationException("Cannot complete the request of a completed request!");
				}
				try
				{
					this.CompleteRequestImpl();
					this.RequestPayload = null;
					this.Status = HttpStream.HttpXmlaOperationStatus.Response;
				}
				catch (Exception)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Error;
					throw;
				}
			}

			// Token: 0x0600129C RID: 4764 RVA: 0x000416C8 File Offset: 0x0003F8C8
			public XmlaDataType GetResponseDataType()
			{
				base.ThrowIfAlreadyDisposed();
				if (this.RequestPayload != null)
				{
					throw new InvalidOperationException("Cannot get the response data-type when the request wasn't already completed!");
				}
				XmlaDataType responseDataTypeImpl;
				try
				{
					this.EnsureCanRead();
					responseDataTypeImpl = this.GetResponseDataTypeImpl();
				}
				catch (Exception)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Error;
					throw;
				}
				return responseDataTypeImpl;
			}

			// Token: 0x0600129D RID: 4765 RVA: 0x0004171C File Offset: 0x0003F91C
			public int ReadResponsePayload(byte[] buffer, int offset, int size)
			{
				base.ThrowIfAlreadyDisposed();
				if (this.RequestPayload != null)
				{
					throw new InvalidOperationException("Cannot read the response payload when the request wasn't already completed!");
				}
				int num2;
				try
				{
					this.EnsureCanRead();
					int num = this.ReadResponsePayloadImpl(buffer, offset, size);
					if (num == 0)
					{
						this.Status = HttpStream.HttpXmlaOperationStatus.Completed;
					}
					num2 = num;
				}
				catch (Exception)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Error;
					throw;
				}
				return num2;
			}

			// Token: 0x0600129E RID: 4766
			public abstract string GetExtendedErrorInfo();

			// Token: 0x0600129F RID: 4767 RVA: 0x0004177C File Offset: 0x0003F97C
			protected static XmlaDataType GetResponseDataTypeFromContentType(string contentType)
			{
				Match match = HttpStream.contentTypeRegex.Match(contentType);
				if (!match.Success)
				{
					throw new ResponseFormatException(XmlaSR.UnsupportedDataFormat(contentType), string.Empty);
				}
				XmlaDataType dataTypeFromString = DataTypes.GetDataTypeFromString(match.Groups["content_type"].Value);
				if (dataTypeFromString == XmlaDataType.Undetermined || dataTypeFromString == XmlaDataType.Unknown)
				{
					throw new ResponseFormatException(XmlaSR.UnsupportedDataFormat(contentType), string.Empty);
				}
				return dataTypeFromString;
			}

			// Token: 0x060012A0 RID: 4768
			protected abstract Stream StartRequestImpl();

			// Token: 0x060012A1 RID: 4769
			protected abstract Stream GetResponseImpl();

			// Token: 0x060012A2 RID: 4770
			protected abstract bool TryGetResponseHeaderValueImpl(string header, out string value);

			// Token: 0x060012A3 RID: 4771
			protected abstract XmlaDataType GetResponseDataTypeImpl();

			// Token: 0x060012A4 RID: 4772 RVA: 0x000417E0 File Offset: 0x0003F9E0
			protected virtual void WriteRequestPayloadImpl(byte[] buffer, int offset, int size)
			{
				this.RequestPayload.Write(buffer, offset, size);
			}

			// Token: 0x060012A5 RID: 4773 RVA: 0x000417F0 File Offset: 0x0003F9F0
			protected virtual void CompleteRequestImpl()
			{
				this.RequestPayload.Flush();
				this.RequestPayload.Close();
			}

			// Token: 0x060012A6 RID: 4774 RVA: 0x00041808 File Offset: 0x0003FA08
			protected virtual void EnsureCanRead()
			{
				if (this.ResponsePayload == null)
				{
					this.ResponsePayload = this.GetResponseImpl();
				}
			}

			// Token: 0x060012A7 RID: 4775 RVA: 0x0004181E File Offset: 0x0003FA1E
			protected virtual int ReadResponsePayloadImpl(byte[] buffer, int offset, int size)
			{
				return this.ResponsePayload.Read(buffer, offset, size);
			}

			// Token: 0x060012A8 RID: 4776 RVA: 0x00041830 File Offset: 0x0003FA30
			protected virtual void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
			{
				if (this.RequestPayload != null)
				{
					if (closeRequest)
					{
						this.RequestPayload.Close();
					}
					this.RequestPayload = null;
				}
				if (this.ResponsePayload != null)
				{
					if (closeResponse)
					{
						this.ResponsePayload.Close();
					}
					this.ResponsePayload = null;
				}
				if (resetStatus)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Idle;
				}
			}

			// Token: 0x060012A9 RID: 4777 RVA: 0x00041884 File Offset: 0x0003FA84
			protected sealed override void Dispose(bool disposing)
			{
				if (disposing)
				{
					bool flag = this.Status == HttpStream.HttpXmlaOperationStatus.Error;
					try
					{
						this.Reset(true, true, true);
					}
					catch (Exception)
					{
						this.Status = HttpStream.HttpXmlaOperationStatus.Error;
						if (!flag)
						{
							throw;
						}
					}
				}
				base.Dispose(disposing);
			}

			// Token: 0x02000213 RID: 531
			protected struct WebRequestOperationManager
			{
				// Token: 0x17000696 RID: 1686
				// (get) Token: 0x060014D7 RID: 5335 RVA: 0x000467DF File Offset: 0x000449DF
				public HttpWebRequest Request
				{
					get
					{
						return this.request;
					}
				}

				// Token: 0x17000697 RID: 1687
				// (get) Token: 0x060014D8 RID: 5336 RVA: 0x000467E7 File Offset: 0x000449E7
				public HttpWebResponse Response
				{
					get
					{
						return this.response;
					}
				}

				// Token: 0x060014D9 RID: 5337 RVA: 0x000467F0 File Offset: 0x000449F0
				public Stream StartRequest(HttpStream.HttpXmlaOperation operation, bool hasXmlaPayload)
				{
					Stream stream;
					this.request = operation.Controller.StartWebRequestBasedOperation(operation.Owner, operation.Context, out stream);
					if (!hasXmlaPayload)
					{
						stream.Close();
						stream = null;
					}
					return stream;
				}

				// Token: 0x060014DA RID: 5338 RVA: 0x00046828 File Offset: 0x00044A28
				public Stream GetResponse(HttpStream.HttpXmlaOperation operation)
				{
					Stream stream;
					if (!operation.Controller.CompleteWebRequestBasedOperation(operation.Owner, operation.Context, this.request, out this.response, out stream))
					{
						return null;
					}
					return stream;
				}

				// Token: 0x060014DB RID: 5339 RVA: 0x00046860 File Offset: 0x00044A60
				public void Reset(bool closeRequest, ref bool closeResponse)
				{
					if (closeRequest && this.request != null)
					{
						if (this.response == null)
						{
							this.request.Abort();
							closeResponse = false;
						}
						this.request = null;
					}
					if (closeResponse && this.response != null)
					{
						this.response.Close();
						this.response = null;
						closeResponse = false;
					}
				}

				// Token: 0x04001219 RID: 4633
				private HttpWebRequest request;

				// Token: 0x0400121A RID: 4634
				private HttpWebResponse response;
			}

			// Token: 0x02000214 RID: 532
			protected struct HttpClientOperationManager
			{
				// Token: 0x17000698 RID: 1688
				// (get) Token: 0x060014DC RID: 5340 RVA: 0x000468B6 File Offset: 0x00044AB6
				public HttpRequestMessage Request
				{
					get
					{
						return this.request;
					}
				}

				// Token: 0x17000699 RID: 1689
				// (get) Token: 0x060014DD RID: 5341 RVA: 0x000468BE File Offset: 0x00044ABE
				public HttpResponseMessage Response
				{
					get
					{
						if (this.response == null || !this.response.IsCompleted)
						{
							return null;
						}
						return this.response.Result;
					}
				}

				// Token: 0x060014DE RID: 5342 RVA: 0x000468E4 File Offset: 0x00044AE4
				public Stream StartRequest(HttpStream.HttpXmlaOperation operation, bool hasXmlaPayload)
				{
					this.cts = new CancellationTokenSource();
					Stream stream;
					if (hasXmlaPayload)
					{
						this.request = operation.Controller.StartHttpClientBasedOperation(operation.Owner, operation.Context, this.cts, out stream, out this.response);
					}
					else
					{
						this.request = operation.Controller.StartHttpClientBasedOperationWithoutPayload(operation.Owner, operation.Context, this.cts, out this.response);
						stream = null;
					}
					return stream;
				}

				// Token: 0x060014DF RID: 5343 RVA: 0x00046958 File Offset: 0x00044B58
				public Stream GetResponse(HttpStream.HttpXmlaOperation operation)
				{
					Stream stream;
					try
					{
						stream = operation.Controller.CompleteHttpClientBasedOperation(operation.Owner, operation.Context, this.response);
					}
					finally
					{
						this.request.Dispose();
						this.request = null;
						this.cts.Dispose();
						this.cts = null;
					}
					return stream;
				}

				// Token: 0x060014E0 RID: 5344 RVA: 0x000469BC File Offset: 0x00044BBC
				public void Reset(bool closeRequest, ref bool closeResponse)
				{
					if (closeRequest && this.request != null)
					{
						if (this.response != null && !this.response.IsTaskInTerminalState())
						{
							this.response.EnsureTaskInTerminalState(this.cts);
							if (this.response.IsCompleted)
							{
								this.response.Result.Dispose();
							}
							this.response = null;
							closeResponse = false;
						}
						this.request.Dispose();
						this.request = null;
						this.cts.Dispose();
						this.cts = null;
					}
					if (closeResponse && this.response != null)
					{
						if (this.response.IsCompleted)
						{
							this.response.Result.Dispose();
						}
						this.response = null;
					}
				}

				// Token: 0x0400121B RID: 4635
				private CancellationTokenSource cts;

				// Token: 0x0400121C RID: 4636
				private HttpRequestMessage request;

				// Token: 0x0400121D RID: 4637
				private Task<HttpResponseMessage> response;
			}
		}
	}
}
