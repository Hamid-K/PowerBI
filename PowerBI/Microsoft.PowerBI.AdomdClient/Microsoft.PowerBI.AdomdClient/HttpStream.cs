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
using Microsoft.AnalysisServices.AdomdClient.Authentication;
using Microsoft.AnalysisServices.AdomdClient.Hosting;
using Microsoft.AnalysisServices.AdomdClient.MsoId;
using Microsoft.AnalysisServices.AdomdClient.Runtime;
using Microsoft.AnalysisServices.AdomdClient.Security;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000028 RID: 40
	internal class HttpStream : TransportCapabilitiesAwareXmlaStream
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000BCEC File Offset: 0x00009EEC
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000BD48 File Offset: 0x00009F48
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000BD4B File Offset: 0x00009F4B
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000BD53 File Offset: 0x00009F53
		public override int ReadTimeout { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000BD5C File Offset: 0x00009F5C
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000BD64 File Offset: 0x00009F64
		internal XmlaStreamException StreamException { get; private set; }

		// Token: 0x06000272 RID: 626 RVA: 0x0000BD70 File Offset: 0x00009F70
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

		// Token: 0x06000273 RID: 627 RVA: 0x0000BE60 File Offset: 0x0000A060
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

		// Token: 0x06000274 RID: 628 RVA: 0x0000BEBC File Offset: 0x0000A0BC
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

		// Token: 0x06000275 RID: 629 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
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

		// Token: 0x06000276 RID: 630 RVA: 0x0000C00C File Offset: 0x0000A20C
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

		// Token: 0x06000277 RID: 631 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
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

		// Token: 0x06000278 RID: 632 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
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

		// Token: 0x06000279 RID: 633 RVA: 0x0000C1CC File Offset: 0x0000A3CC
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

		// Token: 0x0600027A RID: 634 RVA: 0x0000C234 File Offset: 0x0000A434
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C245 File Offset: 0x0000A445
		public override void Close()
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C248 File Offset: 0x0000A448
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

		// Token: 0x0600027D RID: 637 RVA: 0x0000C300 File Offset: 0x0000A500
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

		// Token: 0x0600027E RID: 638 RVA: 0x0000C35C File Offset: 0x0000A55C
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

		// Token: 0x040001E6 RID: 486
		private const string ContentTypeParameter = "content_type";

		// Token: 0x040001E7 RID: 487
		protected static readonly Regex contentTypeRegex = new Regex(string.Format("^(\\s*)(?<{0}>(({1})|({2})|({3})|({4})))(\\s*)((;(.*))|)(\\s*)\\z", new object[]
		{
			"content_type",
			"text/xml".Replace("+", "\\+"),
			"application/xml+xpress".Replace("+", "\\+"),
			"application/sx".Replace("+", "\\+"),
			"application/sx+xpress".Replace("+", "\\+")
		}), RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040001E8 RID: 488
		private readonly HttpStream.HttpChannelController controller;

		// Token: 0x040001E9 RID: 489
		private readonly ConnectionInfo info;

		// Token: 0x040001EA RID: 490
		private readonly Uri dataSource;

		// Token: 0x040001EB RID: 491
		private readonly UserContext userContext;

		// Token: 0x040001EC RID: 492
		private readonly IDictionary<string, string> defaultHeaders;

		// Token: 0x040001ED RID: 493
		private string soapActionHeader;

		// Token: 0x040001EE RID: 494
		private string soapActionValue;

		// Token: 0x040001EF RID: 495
		private bool outdatedVersion;

		// Token: 0x040001F0 RID: 496
		private HttpStream.HttpXmlaOperation operation;

		// Token: 0x02000181 RID: 385
		private abstract class HttpChannelController : Disposable
		{
			// Token: 0x0600119A RID: 4506 RVA: 0x0003CB9C File Offset: 0x0003AD9C
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

			// Token: 0x0600119B RID: 4507 RVA: 0x0003CBE6 File Offset: 0x0003ADE6
			private protected HttpChannelController(IList<object> elements, out int offest, out bool hasElementsUpdate)
			{
				this.options = HttpStream.ChannelOptions.DeserializeFromGlobalObject(elements, 0);
				this.RefreshFromGlobalObjectImpl(elements, out offest, out hasElementsUpdate);
			}

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x0600119C RID: 4508 RVA: 0x0003CC04 File Offset: 0x0003AE04
			public HttpStream.HttpChannelMode Mode
			{
				get
				{
					return this.options.Mode;
				}
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x0600119D RID: 4509 RVA: 0x0003CC11 File Offset: 0x0003AE11
			internal string CacheKey
			{
				get
				{
					return this.options.CacheKey;
				}
			}

			// Token: 0x0600119E RID: 4510 RVA: 0x0003CC20 File Offset: 0x0003AE20
			public IDictionary<string, string> GetDefaultStreamHeaders(ConnectionInfo info, Uri dataSource, string coreServer)
			{
				base.ThrowIfAlreadyDisposed();
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				this.GetDefaultStreamHeaders(info, dataSource, coreServer, dictionary);
				return dictionary;
			}

			// Token: 0x0600119F RID: 4511 RVA: 0x0003CC44 File Offset: 0x0003AE44
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

			// Token: 0x060011A0 RID: 4512 RVA: 0x0003CCE6 File Offset: 0x0003AEE6
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.httpClient != null)
				{
					this.httpClient.Dispose();
					this.httpClient = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x060011A1 RID: 4513 RVA: 0x0003CD0C File Offset: 0x0003AF0C
			protected virtual void GetDefaultStreamHeaders(ConnectionInfo info, Uri webSite, string coreServer, IDictionary<string, string> headers)
			{
				headers.Add("User-Agent", "ADOMD.NET");
				headers.Add("X-AS-AcquireTokenStats", "AppName=");
			}

			// Token: 0x060011A2 RID: 4514 RVA: 0x0003CD30 File Offset: 0x0003AF30
			protected virtual void GetPerRequestHeaders(HttpStream owner, object context, IList<KeyValuePair<string, string>> headers)
			{
				headers.Add(new KeyValuePair<string, string>("Content-Type", DataTypes.GetDataTypeFromEnum(owner.GetRequestDataType())));
			}

			// Token: 0x060011A3 RID: 4515 RVA: 0x0003CD4D File Offset: 0x0003AF4D
			protected virtual void HandleResponseStatusCode(int statusCode, Exception ex)
			{
			}

			// Token: 0x060011A4 RID: 4516 RVA: 0x0003CD4F File Offset: 0x0003AF4F
			protected virtual void ProcessWebResponse(HttpStream owner, object context, HttpStream.HttpChannelController.WebResponseInfo response)
			{
			}

			// Token: 0x060011A5 RID: 4517
			protected abstract HttpStream.HttpXmlaOperation CreateNewOperation(HttpStream owner, bool useHttpClient);

			// Token: 0x060011A6 RID: 4518
			protected abstract HttpStream.HttpChannelController.WebErrorClass ClassifyWebError(HttpStream owner, object context, HttpStream.HttpChannelController.WebErrorInfo error, out Exception exception);

			// Token: 0x060011A7 RID: 4519 RVA: 0x0003CD54 File Offset: 0x0003AF54
			internal HttpWebRequest StartWebRequestBasedOperation(HttpStream owner, object context, out Stream requestStream)
			{
				HttpWebRequest request = this.PrepareWebRequestImpl(owner, context);
				requestStream = owner.userContext.ExecuteInUserContext<Stream>(() => new BufferedStream(request.GetRequestStream(), ClientFeaturesManager.GetHttpStreamBufferSize()));
				return request;
			}

			// Token: 0x060011A8 RID: 4520 RVA: 0x0003CD94 File Offset: 0x0003AF94
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

			// Token: 0x060011A9 RID: 4521 RVA: 0x0003CDD3 File Offset: 0x0003AFD3
			internal HttpRequestMessage StartHttpClientBasedOperation(HttpStream owner, object context, CancellationTokenSource cts, out Stream requestStream, out Task<HttpResponseMessage> pendingResponse)
			{
				requestStream = new HttpStream.HttpChannelController.RequestPayloadStream(ClientFeaturesManager.GetHttpClientPayloadQueueLimit(), ClientFeaturesManager.GetHttpStreamBufferSize());
				return this.StartHttpClientBasedOperationImpl(owner, context, cts, requestStream, out pendingResponse);
			}

			// Token: 0x060011AA RID: 4522 RVA: 0x0003CDF5 File Offset: 0x0003AFF5
			internal HttpRequestMessage StartHttpClientBasedOperationWithoutPayload(HttpStream owner, object context, CancellationTokenSource cts, out Task<HttpResponseMessage> pendingResponse)
			{
				return this.StartHttpClientBasedOperationImpl(owner, context, cts, null, out pendingResponse);
			}

			// Token: 0x060011AB RID: 4523 RVA: 0x0003CE04 File Offset: 0x0003B004
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

			// Token: 0x060011AC RID: 4524 RVA: 0x0003CE3E File Offset: 0x0003B03E
			private protected virtual void SerializeToGlobalObject(IList<object> elements)
			{
				this.options.SerializeToGlobalObject(elements);
				elements.Add(this.cookieContainer);
				elements.Add(this.clientCertificate);
				elements.Add(this.httpClient);
			}

			// Token: 0x060011AD RID: 4525 RVA: 0x0003CE70 File Offset: 0x0003B070
			private protected virtual void RefreshFromGlobalObject(IList<object> elements, out int offest, out bool hasElementsUpdate)
			{
				this.RefreshFromGlobalObjectImpl(elements, out offest, out hasElementsUpdate);
			}

			// Token: 0x060011AE RID: 4526 RVA: 0x0003CE7C File Offset: 0x0003B07C
			internal IList<object> ToGlobalObject()
			{
				IList<object> list = new List<object>(16);
				this.SerializeToGlobalObject(list);
				return list;
			}

			// Token: 0x060011AF RID: 4527 RVA: 0x0003CE9C File Offset: 0x0003B09C
			internal void RefreshFromGlobalObject(IList<object> elements, out bool hasElementsUpdate)
			{
				int num;
				this.RefreshFromGlobalObject(elements, out num, out hasElementsUpdate);
			}

			// Token: 0x060011B0 RID: 4528 RVA: 0x0003CEB4 File Offset: 0x0003B0B4
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

			// Token: 0x060011B1 RID: 4529 RVA: 0x0003CF10 File Offset: 0x0003B110
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

			// Token: 0x060011B2 RID: 4530 RVA: 0x0003CF78 File Offset: 0x0003B178
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

			// Token: 0x060011B3 RID: 4531 RVA: 0x0003D098 File Offset: 0x0003B298
			private HttpRequestMessage StartHttpClientBasedOperationImpl(HttpStream owner, object context, CancellationTokenSource cts, Stream requestStream, out Task<HttpResponseMessage> pendingResponse)
			{
				HttpRequestMessage request = this.PrepareRequestMessageImpl(owner, context, requestStream);
				pendingResponse = owner.userContext.ExecuteInUserContext<Task<HttpResponseMessage>>(() => this.httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, (cts != null) ? cts.Token : CancellationToken.None));
				return request;
			}

			// Token: 0x060011B4 RID: 4532 RVA: 0x0003D0EC File Offset: 0x0003B2EC
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

			// Token: 0x060011B5 RID: 4533 RVA: 0x0003D1FC File Offset: 0x0003B3FC
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

			// Token: 0x060011B6 RID: 4534 RVA: 0x0003D2B0 File Offset: 0x0003B4B0
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

			// Token: 0x060011B7 RID: 4535 RVA: 0x0003D368 File Offset: 0x0003B568
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

			// Token: 0x04000C01 RID: 3073
			internal const int GlobalObjectOptionsOffset = 0;

			// Token: 0x04000C02 RID: 3074
			private const int TCP_KEEP_ALIVE_TIME_IN_MS = 30000;

			// Token: 0x04000C03 RID: 3075
			private const int TCP_KEEP_ALIVE_INTERVAL_IN_MS = 30000;

			// Token: 0x04000C04 RID: 3076
			private const int CONTINUE_TIMEOUT_IN_MS = 30000;

			// Token: 0x04000C05 RID: 3077
			private const string ASRuntimeCoreAssemblyName = "Microsoft.AnalysisServices.Runtime.Core";

			// Token: 0x04000C06 RID: 3078
			private const string ASRuntimeCorePublicKeyToken = "89845dcd8080cc91";

			// Token: 0x04000C07 RID: 3079
			private static bool? netCoreClientsLoaded;

			// Token: 0x04000C08 RID: 3080
			protected HttpStream.ChannelOptions options;

			// Token: 0x04000C09 RID: 3081
			private CookieContainer cookieContainer;

			// Token: 0x04000C0A RID: 3082
			private X509Certificate2 clientCertificate;

			// Token: 0x04000C0B RID: 3083
			private HttpClient httpClient;

			// Token: 0x0200021F RID: 543
			protected enum WebErrorClass
			{
				// Token: 0x04000F10 RID: 3856
				Raise,
				// Token: 0x04000F11 RID: 3857
				Ignore,
				// Token: 0x04000F12 RID: 3858
				Retry
			}

			// Token: 0x02000220 RID: 544
			protected struct WebResponseInfo
			{
				// Token: 0x060014F5 RID: 5365 RVA: 0x00046C96 File Offset: 0x00044E96
				public WebResponseInfo(HttpWebResponse response)
				{
					this.webResponse = response;
					this.responseMessage = null;
				}

				// Token: 0x060014F6 RID: 5366 RVA: 0x00046CA6 File Offset: 0x00044EA6
				public WebResponseInfo(HttpResponseMessage response)
				{
					this.webResponse = null;
					this.responseMessage = response;
				}

				// Token: 0x17000738 RID: 1848
				// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00046CB6 File Offset: 0x00044EB6
				public bool IsValid
				{
					get
					{
						return (this.webResponse != null) ^ (this.responseMessage != null);
					}
				}

				// Token: 0x17000739 RID: 1849
				// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00046CCB File Offset: 0x00044ECB
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

				// Token: 0x060014F9 RID: 5369 RVA: 0x00046CEC File Offset: 0x00044EEC
				public string GetResponseHeaderValue(string header)
				{
					string text;
					if (!this.TryGetResponseHeaderValue(header, out text))
					{
						text = null;
					}
					return text;
				}

				// Token: 0x060014FA RID: 5370 RVA: 0x00046D07 File Offset: 0x00044F07
				public bool TryGetResponseHeaderValue(string header, out string value)
				{
					if (this.responseMessage != null)
					{
						return HttpHelper.TryGetHeaderValueFromResponse(this.responseMessage, header, out value);
					}
					return HttpHelper.TryGetHeaderValueFromResponse(this.webResponse, header, out value);
				}

				// Token: 0x04000F13 RID: 3859
				private HttpWebResponse webResponse;

				// Token: 0x04000F14 RID: 3860
				private HttpResponseMessage responseMessage;
			}

			// Token: 0x02000221 RID: 545
			protected struct WebErrorInfo
			{
				// Token: 0x060014FB RID: 5371 RVA: 0x00046D2C File Offset: 0x00044F2C
				public WebErrorInfo(WebException error)
				{
					this.error = error;
					this.response = null;
				}

				// Token: 0x060014FC RID: 5372 RVA: 0x00046D3C File Offset: 0x00044F3C
				public WebErrorInfo(HttpResponseMessage response)
				{
					this.error = null;
					this.response = response;
				}

				// Token: 0x1700073A RID: 1850
				// (get) Token: 0x060014FD RID: 5373 RVA: 0x00046D4C File Offset: 0x00044F4C
				public bool IsValid
				{
					get
					{
						return (this.error != null) ^ (this.response != null);
					}
				}

				// Token: 0x1700073B RID: 1851
				// (get) Token: 0x060014FE RID: 5374 RVA: 0x00046D61 File Offset: 0x00044F61
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

				// Token: 0x1700073C RID: 1852
				// (get) Token: 0x060014FF RID: 5375 RVA: 0x00046D8C File Offset: 0x00044F8C
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

				// Token: 0x06001500 RID: 5376 RVA: 0x00046DBB File Offset: 0x00044FBB
				public bool TryGetResponseHeaderValue(string header, out string value)
				{
					if (this.response != null)
					{
						return HttpHelper.TryGetHeaderValueFromResponse(this.response, header, out value);
					}
					return HttpHelper.TryGetHeaderValueFromResponse((HttpWebResponse)this.error.Response, header, out value);
				}

				// Token: 0x06001501 RID: 5377 RVA: 0x00046DEC File Offset: 0x00044FEC
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

				// Token: 0x06001502 RID: 5378 RVA: 0x00046E8A File Offset: 0x0004508A
				public Exception CreatePaasInfraConnectionException(AsInstanceType asInstanceType)
				{
					if (this.response != null)
					{
						return ASAzureUtility.GetConnectionException(this.response, asInstanceType);
					}
					return ASAzureUtility.GetConnectionException(this.error, asInstanceType);
				}

				// Token: 0x04000F15 RID: 3861
				private WebException error;

				// Token: 0x04000F16 RID: 3862
				private HttpResponseMessage response;
			}

			// Token: 0x02000222 RID: 546
			protected sealed class RequestPayloadStream : Stream
			{
				// Token: 0x06001503 RID: 5379 RVA: 0x00046EAD File Offset: 0x000450AD
				public RequestPayloadStream(long maxQueueSize, int defaultBufferSize)
				{
					this.maxQueueSize = maxQueueSize;
					this.defaultBufferSize = defaultBufferSize;
					this.pendingChunks = new Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk>();
				}

				// Token: 0x1700073D RID: 1853
				// (get) Token: 0x06001504 RID: 5380 RVA: 0x00046ECE File Offset: 0x000450CE
				public override bool CanRead
				{
					get
					{
						return true;
					}
				}

				// Token: 0x1700073E RID: 1854
				// (get) Token: 0x06001505 RID: 5381 RVA: 0x00046ED1 File Offset: 0x000450D1
				public override bool CanWrite
				{
					get
					{
						return !this.IsCompleted();
					}
				}

				// Token: 0x1700073F RID: 1855
				// (get) Token: 0x06001506 RID: 5382 RVA: 0x00046EDC File Offset: 0x000450DC
				public override bool CanSeek
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17000740 RID: 1856
				// (get) Token: 0x06001507 RID: 5383 RVA: 0x00046EDF File Offset: 0x000450DF
				// (set) Token: 0x06001508 RID: 5384 RVA: 0x00046EE7 File Offset: 0x000450E7
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

				// Token: 0x17000741 RID: 1857
				// (get) Token: 0x06001509 RID: 5385 RVA: 0x00046EF3 File Offset: 0x000450F3
				public override long Length
				{
					get
					{
						return this.length;
					}
				}

				// Token: 0x0600150A RID: 5386 RVA: 0x00046EFC File Offset: 0x000450FC
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

				// Token: 0x0600150B RID: 5387 RVA: 0x00046F74 File Offset: 0x00045174
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

				// Token: 0x0600150C RID: 5388 RVA: 0x00047018 File Offset: 0x00045218
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

				// Token: 0x0600150D RID: 5389 RVA: 0x000470D0 File Offset: 0x000452D0
				public override int EndRead(IAsyncResult asyncResult)
				{
					return HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.End(asyncResult);
				}

				// Token: 0x0600150E RID: 5390 RVA: 0x000470D8 File Offset: 0x000452D8
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

				// Token: 0x0600150F RID: 5391 RVA: 0x000471B4 File Offset: 0x000453B4
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

				// Token: 0x06001510 RID: 5392 RVA: 0x00047220 File Offset: 0x00045420
				public override void Flush()
				{
				}

				// Token: 0x06001511 RID: 5393 RVA: 0x00047222 File Offset: 0x00045422
				public override long Seek(long offset, SeekOrigin origin)
				{
					throw new NotSupportedException(XmlaSR.HttpStream_RequestPayloadStream_InvalidStreamOperation);
				}

				// Token: 0x06001512 RID: 5394 RVA: 0x0004722E File Offset: 0x0004542E
				public override void SetLength(long value)
				{
					throw new NotSupportedException(XmlaSR.HttpStream_RequestPayloadStream_InvalidStreamOperation);
				}

				// Token: 0x06001513 RID: 5395 RVA: 0x0004723C File Offset: 0x0004543C
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

				// Token: 0x06001514 RID: 5396 RVA: 0x00047298 File Offset: 0x00045498
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

				// Token: 0x06001515 RID: 5397 RVA: 0x0004736C File Offset: 0x0004556C
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

				// Token: 0x06001516 RID: 5398 RVA: 0x000473E8 File Offset: 0x000455E8
				private IAsyncResult QueueIncompleteAsyncReadRequest(byte[] buffer, int initialOffset, int offset, int count, AsyncCallback callback, object state)
				{
					HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult readAsyncResult = new HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult(callback, state);
					this.pendingReadRequest = new HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest(buffer, initialOffset, offset, count, readAsyncResult);
					return readAsyncResult;
				}

				// Token: 0x06001517 RID: 5399 RVA: 0x00047414 File Offset: 0x00045614
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

				// Token: 0x06001518 RID: 5400 RVA: 0x00047450 File Offset: 0x00045650
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

				// Token: 0x06001519 RID: 5401 RVA: 0x000475D0 File Offset: 0x000457D0
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

				// Token: 0x0600151A RID: 5402 RVA: 0x00047628 File Offset: 0x00045828
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool IsOutgoingQueueFull()
				{
					return this.length - this.position >= this.maxQueueSize;
				}

				// Token: 0x0600151B RID: 5403 RVA: 0x00047642 File Offset: 0x00045842
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool HasPendingReadRequest()
				{
					return this.pendingReadRequest.Buffer != null;
				}

				// Token: 0x0600151C RID: 5404 RVA: 0x00047652 File Offset: 0x00045852
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool HasActiveReadChunk()
				{
					return this.currentReadChunk.Buffer != null;
				}

				// Token: 0x0600151D RID: 5405 RVA: 0x00047662 File Offset: 0x00045862
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool HasActiveWriteChunk()
				{
					return this.currentWriteChunk.Buffer != null;
				}

				// Token: 0x0600151E RID: 5406 RVA: 0x00047672 File Offset: 0x00045872
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool IsCompleted()
				{
					return this.isCompleted && this.position == this.length;
				}

				// Token: 0x04000F17 RID: 3863
				private readonly long maxQueueSize;

				// Token: 0x04000F18 RID: 3864
				private readonly int defaultBufferSize;

				// Token: 0x04000F19 RID: 3865
				private readonly Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> pendingChunks;

				// Token: 0x04000F1A RID: 3866
				private HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk currentReadChunk;

				// Token: 0x04000F1B RID: 3867
				private HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk currentWriteChunk;

				// Token: 0x04000F1C RID: 3868
				private HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest pendingReadRequest;

				// Token: 0x04000F1D RID: 3869
				private bool hasPendingWriteRequest;

				// Token: 0x04000F1E RID: 3870
				private long position;

				// Token: 0x04000F1F RID: 3871
				private long length;

				// Token: 0x04000F20 RID: 3872
				private bool isCompleted;

				// Token: 0x02000244 RID: 580
				private struct ReadRequest
				{
					// Token: 0x060015A4 RID: 5540 RVA: 0x00048463 File Offset: 0x00046663
					public ReadRequest(byte[] buffer, int initialOffset, int offset, int count, HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult asyncResult)
					{
						this.Buffer = buffer;
						this.InitialOffset = initialOffset;
						this.Offset = offset;
						this.Count = count;
						this.asyncResult = asyncResult;
						this.Error = null;
					}

					// Token: 0x17000752 RID: 1874
					// (get) Token: 0x060015A5 RID: 5541 RVA: 0x00048491 File Offset: 0x00046691
					public byte[] Buffer { get; }

					// Token: 0x17000753 RID: 1875
					// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00048499 File Offset: 0x00046699
					public int InitialOffset { get; }

					// Token: 0x17000754 RID: 1876
					// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000484A1 File Offset: 0x000466A1
					// (set) Token: 0x060015A8 RID: 5544 RVA: 0x000484A9 File Offset: 0x000466A9
					public int Offset { get; private set; }

					// Token: 0x17000755 RID: 1877
					// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000484B2 File Offset: 0x000466B2
					// (set) Token: 0x060015AA RID: 5546 RVA: 0x000484BA File Offset: 0x000466BA
					public int Count { get; private set; }

					// Token: 0x17000756 RID: 1878
					// (get) Token: 0x060015AB RID: 5547 RVA: 0x000484C3 File Offset: 0x000466C3
					// (set) Token: 0x060015AC RID: 5548 RVA: 0x000484CB File Offset: 0x000466CB
					public Exception Error { get; private set; }

					// Token: 0x060015AD RID: 5549 RVA: 0x000484D4 File Offset: 0x000466D4
					[MethodImpl(MethodImplOptions.AggressiveInlining)]
					public static bool IsReady(int minSize, int initialOffset, int offset, int count)
					{
						return count == 0 || offset - initialOffset >= minSize;
					}

					// Token: 0x060015AE RID: 5550 RVA: 0x000484E4 File Offset: 0x000466E4
					[MethodImpl(MethodImplOptions.AggressiveInlining)]
					public bool IsReady(int minSize)
					{
						return HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest.IsReady(minSize, this.InitialOffset, this.Offset, this.Count);
					}

					// Token: 0x060015AF RID: 5551 RVA: 0x00048500 File Offset: 0x00046700
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

					// Token: 0x060015B0 RID: 5552 RVA: 0x00048560 File Offset: 0x00046760
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

					// Token: 0x04000F62 RID: 3938
					private HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult asyncResult;
				}

				// Token: 0x02000245 RID: 581
				private struct PayloadChunk
				{
					// Token: 0x060015B1 RID: 5553 RVA: 0x000485C4 File Offset: 0x000467C4
					public PayloadChunk(int bufferSize)
					{
						this.Buffer = new byte[bufferSize];
						this.Offset = 0;
						this.Count = 0;
					}

					// Token: 0x060015B2 RID: 5554 RVA: 0x000485E0 File Offset: 0x000467E0
					public PayloadChunk(int bufferSize, byte[] buffer, int offset, int count)
					{
						this.Buffer = ((count > bufferSize) ? new byte[count] : new byte[bufferSize]);
						this.Offset = 0;
						this.Count = count;
						global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, 0, count);
					}

					// Token: 0x17000757 RID: 1879
					// (get) Token: 0x060015B3 RID: 5555 RVA: 0x0004861B File Offset: 0x0004681B
					public byte[] Buffer { get; }

					// Token: 0x17000758 RID: 1880
					// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00048623 File Offset: 0x00046823
					// (set) Token: 0x060015B5 RID: 5557 RVA: 0x0004862B File Offset: 0x0004682B
					public int Offset { get; private set; }

					// Token: 0x17000759 RID: 1881
					// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00048634 File Offset: 0x00046834
					// (set) Token: 0x060015B7 RID: 5559 RVA: 0x0004863C File Offset: 0x0004683C
					public int Count { get; private set; }

					// Token: 0x1700075A RID: 1882
					// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00048645 File Offset: 0x00046845
					public bool IsFull
					{
						[MethodImpl(MethodImplOptions.AggressiveInlining)]
						get
						{
							return this.Count == this.Buffer.Length;
						}
					}

					// Token: 0x060015B9 RID: 5561 RVA: 0x00048658 File Offset: 0x00046858
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

					// Token: 0x060015BA RID: 5562 RVA: 0x000486A4 File Offset: 0x000468A4
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

				// Token: 0x02000246 RID: 582
				private sealed class ReadAsyncResult : IAsyncResult
				{
					// Token: 0x060015BB RID: 5563 RVA: 0x000486FC File Offset: 0x000468FC
					public ReadAsyncResult(AsyncCallback callback, object state)
					{
						this.callback = callback;
						this.AsyncState = state;
						this.@lock = new object();
					}

					// Token: 0x1700075B RID: 1883
					// (get) Token: 0x060015BC RID: 5564 RVA: 0x0004871D File Offset: 0x0004691D
					public object AsyncState { get; }

					// Token: 0x1700075C RID: 1884
					// (get) Token: 0x060015BD RID: 5565 RVA: 0x00048728 File Offset: 0x00046928
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

					// Token: 0x1700075D RID: 1885
					// (get) Token: 0x060015BE RID: 5566 RVA: 0x00048794 File Offset: 0x00046994
					// (set) Token: 0x060015BF RID: 5567 RVA: 0x0004879C File Offset: 0x0004699C
					public bool CompletedSynchronously { get; private set; }

					// Token: 0x1700075E RID: 1886
					// (get) Token: 0x060015C0 RID: 5568 RVA: 0x000487A5 File Offset: 0x000469A5
					// (set) Token: 0x060015C1 RID: 5569 RVA: 0x000487AD File Offset: 0x000469AD
					public bool IsCompleted { get; private set; }

					// Token: 0x060015C2 RID: 5570 RVA: 0x000487B6 File Offset: 0x000469B6
					public static HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult CreateCompleted(AsyncCallback callback, object state, int bytesRead)
					{
						HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult readAsyncResult = new HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult(callback, state);
						readAsyncResult.bytesRead = bytesRead;
						readAsyncResult.CompleteImpl(true);
						return readAsyncResult;
					}

					// Token: 0x060015C3 RID: 5571 RVA: 0x000487D0 File Offset: 0x000469D0
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

					// Token: 0x060015C4 RID: 5572 RVA: 0x000488A8 File Offset: 0x00046AA8
					public void Complete(int bytesRead, Exception ex)
					{
						this.bytesRead = bytesRead;
						this.ex = ex;
						this.CompleteImpl(false);
					}

					// Token: 0x060015C5 RID: 5573 RVA: 0x000488C0 File Offset: 0x00046AC0
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

					// Token: 0x04000F6B RID: 3947
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

					// Token: 0x04000F6C RID: 3948
					private readonly object @lock;

					// Token: 0x04000F6D RID: 3949
					private readonly AsyncCallback callback;

					// Token: 0x04000F6E RID: 3950
					private ManualResetEvent completionEvent;

					// Token: 0x04000F6F RID: 3951
					private bool endWasCalled;

					// Token: 0x04000F70 RID: 3952
					private int bytesRead;

					// Token: 0x04000F71 RID: 3953
					private Exception ex;
				}
			}

			// Token: 0x02000223 RID: 547
			private sealed class EmptyHttpContent : HttpContent
			{
				// Token: 0x06001520 RID: 5408 RVA: 0x00047694 File Offset: 0x00045894
				protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
				{
					if (stream == null)
					{
						throw new ArgumentNullException("stream");
					}
					return AsyncHelper.GetCompletedTaskWithDefaultValue<object>();
				}

				// Token: 0x06001521 RID: 5409 RVA: 0x000476A9 File Offset: 0x000458A9
				protected override bool TryComputeLength(out long length)
				{
					length = 0L;
					return true;
				}
			}
		}

		// Token: 0x02000182 RID: 386
		private enum HttpChannelMode
		{
			// Token: 0x04000C0D RID: 3085
			Unknonw,
			// Token: 0x04000C0E RID: 3086
			Legacy,
			// Token: 0x04000C0F RID: 3087
			PaasInfra
		}

		// Token: 0x02000183 RID: 387
		private struct ChannelOptions
		{
			// Token: 0x060011B8 RID: 4536 RVA: 0x0003D408 File Offset: 0x0003B608
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

			// Token: 0x060011B9 RID: 4537 RVA: 0x0003D626 File Offset: 0x0003B826
			private ChannelOptions(int defaultTimeoutInMiliseconds, string certificateThumbprint, ICredentials credentials, string groupName, HttpStream.ChannelOptions.BitmapOptions flags, string cacheKey)
			{
				this.defaultTimeoutInMiliseconds = defaultTimeoutInMiliseconds;
				this.certificateThumbprint = certificateThumbprint;
				this.credentials = credentials;
				this.groupName = groupName;
				this.flags = flags;
				this.cacheKey = cacheKey;
			}

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x060011BA RID: 4538 RVA: 0x0003D655 File Offset: 0x0003B855
			public int DefaultTimeoutInMiliseconds
			{
				get
				{
					return this.defaultTimeoutInMiliseconds;
				}
			}

			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x060011BB RID: 4539 RVA: 0x0003D65D File Offset: 0x0003B85D
			public string CertificateThumbprint
			{
				get
				{
					return this.certificateThumbprint;
				}
			}

			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x060011BC RID: 4540 RVA: 0x0003D665 File Offset: 0x0003B865
			public ICredentials Credentials
			{
				get
				{
					return this.credentials;
				}
			}

			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x060011BD RID: 4541 RVA: 0x0003D66D File Offset: 0x0003B86D
			public HttpStream.HttpChannelMode Mode
			{
				get
				{
					return this.GetMode();
				}
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x060011BE RID: 4542 RVA: 0x0003D675 File Offset: 0x0003B875
			public bool AllowAutoRedirect
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.AutoRedirect);
				}
			}

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x060011BF RID: 4543 RVA: 0x0003D682 File Offset: 0x0003B882
			public bool AllowGzipCompression
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.GzipCompression);
				}
			}

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0003D68F File Offset: 0x0003B88F
			public bool IsPaasInfrastructure
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.PaasInfrastructure);
				}
			}

			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0003D69C File Offset: 0x0003B89C
			public bool IsPbiDataset
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.PbiDataset);
				}
			}

			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0003D6A9 File Offset: 0x0003B8A9
			internal string CacheKey
			{
				get
				{
					return this.cacheKey;
				}
			}

			// Token: 0x060011C3 RID: 4547 RVA: 0x0003D6B1 File Offset: 0x0003B8B1
			public static string GetCacheKeyFromGlobalObject(IList<object> elements, int index)
			{
				return (string)elements[index + 5];
			}

			// Token: 0x060011C4 RID: 4548 RVA: 0x0003D6C4 File Offset: 0x0003B8C4
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

			// Token: 0x060011C5 RID: 4549 RVA: 0x0003D7AC File Offset: 0x0003B9AC
			internal static HttpStream.ChannelOptions DeserializeFromGlobalObject(IList<object> elements, int index)
			{
				return new HttpStream.ChannelOptions((int)elements[index], (string)elements[index + 1], (ICredentials)elements[index + 2], (string)elements[index + 3], (HttpStream.ChannelOptions.BitmapOptions)((int)elements[index + 4]), (string)elements[index + 5]);
			}

			// Token: 0x060011C6 RID: 4550 RVA: 0x0003D810 File Offset: 0x0003BA10
			internal void SerializeToGlobalObject(IList<object> elements)
			{
				elements.Add(this.defaultTimeoutInMiliseconds);
				elements.Add(this.certificateThumbprint);
				elements.Add(this.credentials);
				elements.Add(this.groupName);
				elements.Add((int)this.flags);
				elements.Add(this.cacheKey);
			}

			// Token: 0x060011C7 RID: 4551 RVA: 0x0003D870 File Offset: 0x0003BA70
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

			// Token: 0x060011C8 RID: 4552 RVA: 0x0003D8E0 File Offset: 0x0003BAE0
			private static void OnMsoIdAuthenticationException(MsoIdAuthenticationException error, out bool ignoreError)
			{
				switch (error.ErrorType)
				{
				case MsoIdAuthenticationError.NotInstalled:
				case MsoIdAuthenticationError.LoadFailure:
					throw new AdomdConnectionException(XmlaSR.Authentication_MsoID_MissingSignInAssistant, error);
				case MsoIdAuthenticationError.InitFailure:
				case MsoIdAuthenticationError.OperationalError:
					throw new AdomdConnectionException(XmlaSR.Authentication_MsoID_InternalError, error);
				case MsoIdAuthenticationError.SsoWithNonDomainUser:
					throw new AdomdConnectionException(XmlaSR.Authentication_MsoID_SsoFailedNonDomainUser, error);
				case MsoIdAuthenticationError.MissingPassword:
					throw new AdomdConnectionException(XmlaSR.ConnectionString_MissingPassword, error);
				case MsoIdAuthenticationError.SsoAuthenticationFailed:
					throw new AdomdConnectionException(XmlaSR.Authentication_MsoID_SsoFailed, error);
				case MsoIdAuthenticationError.AuthenticationFailed:
					throw new AdomdConnectionException(XmlaSR.Authentication_MsoID_InvalidCredentials, error);
				}
				throw new AdomdConnectionException(XmlaSR.InternalError, error);
			}

			// Token: 0x060011C9 RID: 4553 RVA: 0x0003D97D File Offset: 0x0003BB7D
			private HttpStream.HttpChannelMode GetMode()
			{
				return (HttpStream.HttpChannelMode)(this.flags & HttpStream.ChannelOptions.BitmapOptions.ChannelModeMask);
			}

			// Token: 0x060011CA RID: 4554 RVA: 0x0003D98B File Offset: 0x0003BB8B
			private bool IsEnabled(HttpStream.ChannelOptions.BitmapOptions options)
			{
				return (this.flags & options) == options;
			}

			// Token: 0x04000C10 RID: 3088
			internal const int GlobalObjectElementCount = 6;

			// Token: 0x04000C11 RID: 3089
			private const int GlobalObjectElementOffset_DefaultTimeout = 0;

			// Token: 0x04000C12 RID: 3090
			private const int GlobalObjectElementOffset_CertificateThumbprint = 1;

			// Token: 0x04000C13 RID: 3091
			private const int GlobalObjectElementOffset_Credentials = 2;

			// Token: 0x04000C14 RID: 3092
			private const int GlobalObjectElementOffset_GroupName = 3;

			// Token: 0x04000C15 RID: 3093
			private const int GlobalObjectElementOffset_Options = 4;

			// Token: 0x04000C16 RID: 3094
			private const int GlobalObjectElementOffset_CacheKey = 5;

			// Token: 0x04000C17 RID: 3095
			private readonly int defaultTimeoutInMiliseconds;

			// Token: 0x04000C18 RID: 3096
			private readonly string certificateThumbprint;

			// Token: 0x04000C19 RID: 3097
			private readonly ICredentials credentials;

			// Token: 0x04000C1A RID: 3098
			private readonly string groupName;

			// Token: 0x04000C1B RID: 3099
			private readonly HttpStream.ChannelOptions.BitmapOptions flags;

			// Token: 0x04000C1C RID: 3100
			private readonly string cacheKey;

			// Token: 0x02000227 RID: 551
			[Flags]
			private enum BitmapOptions
			{
				// Token: 0x04000F27 RID: 3879
				None = 0,
				// Token: 0x04000F28 RID: 3880
				ChannelModeMask = 255,
				// Token: 0x04000F29 RID: 3881
				AutoRedirect = 256,
				// Token: 0x04000F2A RID: 3882
				GzipCompression = 512,
				// Token: 0x04000F2B RID: 3883
				PaasInfrastructure = 65536,
				// Token: 0x04000F2C RID: 3884
				PbiDataset = 131072
			}

			// Token: 0x02000228 RID: 552
			private sealed class NetworkCredentialUserContext : UserContext
			{
				// Token: 0x06001528 RID: 5416 RVA: 0x00047724 File Offset: 0x00045924
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

				// Token: 0x06001529 RID: 5417 RVA: 0x000477A0 File Offset: 0x000459A0
				public override bool TryGetCredentials(out ICredentials credentials, out string groupName)
				{
					credentials = this.credentials;
					groupName = this.connectionGroupName;
					return true;
				}

				// Token: 0x0600152A RID: 5418 RVA: 0x000477B3 File Offset: 0x000459B3
				protected override void ExecuteInUserContextImpl(Action action)
				{
					action();
				}

				// Token: 0x0600152B RID: 5419 RVA: 0x000477BB File Offset: 0x000459BB
				protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
				{
					return action();
				}

				// Token: 0x0600152C RID: 5420 RVA: 0x000477C3 File Offset: 0x000459C3
				protected override void UpdateHttpRequestImpl(HttpWebRequest request)
				{
					request.Credentials = this.credentials;
					request.ConnectionGroupName = this.connectionGroupName;
				}

				// Token: 0x0600152D RID: 5421 RVA: 0x000477DD File Offset: 0x000459DD
				protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
				{
				}

				// Token: 0x04000F2D RID: 3885
				private ICredentials credentials;

				// Token: 0x04000F2E RID: 3886
				private string connectionGroupName;
			}

			// Token: 0x02000229 RID: 553
			private sealed class AuthenticatedComposedUserContext : UserContext
			{
				// Token: 0x0600152E RID: 5422 RVA: 0x000477DF File Offset: 0x000459DF
				public AuthenticatedComposedUserContext(AuthenticationHandle handle, UserContext context)
				{
					this.handle = handle;
					this.context = context;
				}

				// Token: 0x0600152F RID: 5423 RVA: 0x000477F5 File Offset: 0x000459F5
				public override bool TryGetCredentials(out ICredentials credentials, out string groupName)
				{
					return this.context.TryGetCredentials(out credentials, out groupName);
				}

				// Token: 0x06001530 RID: 5424 RVA: 0x00047804 File Offset: 0x00045A04
				protected override void ExecuteInUserContextImpl(Action action)
				{
					this.context.ExecuteInUserContext(action);
				}

				// Token: 0x06001531 RID: 5425 RVA: 0x00047812 File Offset: 0x00045A12
				protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
				{
					return this.context.ExecuteInUserContext<TResult>(action);
				}

				// Token: 0x06001532 RID: 5426 RVA: 0x00047820 File Offset: 0x00045A20
				protected override void UpdateHttpRequestImpl(HttpWebRequest request)
				{
					this.context.UpdateHttpRequest(request);
					request.Headers.Add(HttpRequestHeader.Authorization, string.Format("{0} {1}", this.handle.AuthenticationScheme, this.handle.GetAccessToken()));
				}

				// Token: 0x06001533 RID: 5427 RVA: 0x0004785B File Offset: 0x00045A5B
				protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
				{
					this.context.UpdateHttpRequest(request);
					request.Headers.Authorization = new AuthenticationHeaderValue(this.handle.AuthenticationScheme, this.handle.GetAccessToken());
				}

				// Token: 0x04000F2F RID: 3887
				private AuthenticationHandle handle;

				// Token: 0x04000F30 RID: 3888
				private UserContext context;
			}
		}

		// Token: 0x02000184 RID: 388
		private static class HttpChannelManager
		{
			// Token: 0x060011CB RID: 4555 RVA: 0x0003D998 File Offset: 0x0003BB98
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

			// Token: 0x060011CC RID: 4556 RVA: 0x0003DA18 File Offset: 0x0003BC18
			public static void ReleaseController(HttpStream.HttpChannelController controller)
			{
				HttpStream.HttpChannelManager.controllers.Remove(controller.CacheKey);
			}

			// Token: 0x060011CD RID: 4557 RVA: 0x0003DA2C File Offset: 0x0003BC2C
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

			// Token: 0x060011CE RID: 4558 RVA: 0x0003DACC File Offset: 0x0003BCCC
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

			// Token: 0x060011CF RID: 4559 RVA: 0x0003DBA8 File Offset: 0x0003BDA8
			private static HttpStream.HttpChannelController CreateControllerFromGlobalObject(bool isLegacyController, IList<object> elements, out bool hasElementsUpdate)
			{
				if (isLegacyController)
				{
					return new HttpStream.LegacyController(elements, out hasElementsUpdate);
				}
				return new HttpStream.PaasInfraController(elements, out hasElementsUpdate);
			}

			// Token: 0x04000C1D RID: 3101
			private const string ControllerCacheName = "XmlaLibControllerCache";

			// Token: 0x04000C1E RID: 3102
			private const string ControllerManagerName = "XmlaLibControllerManager";

			// Token: 0x04000C1F RID: 3103
			private const string ControllerTypeCode_Legacy = "HttpStream.LegacyController";

			// Token: 0x04000C20 RID: 3104
			private const string ControllerTypeCode_PaasInfra = "HttpStream.PaasInfraController";

			// Token: 0x04000C21 RID: 3105
			private const int ControllerCacheTimeoutInMinutes = 10;

			// Token: 0x04000C22 RID: 3106
			private static SharedMemoryCache controllerCache = SharedMemoryCache.Create("XmlaLibControllerCache", MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)), StringComparer.OrdinalIgnoreCase, new PrepareItemForCaching(HttpStream.HttpChannelManager.PrepareItemForCaching), new ConvertCachedItem(HttpStream.HttpChannelManager.ConvertCachedItem));

			// Token: 0x04000C23 RID: 3107
			private static SharedResourceManager controllers = SharedResourceManager.Create("XmlaLibControllerManager", StringComparer.OrdinalIgnoreCase, HttpStream.HttpChannelManager.controllerCache, new PrepareItemForCaching(HttpStream.HttpChannelManager.PrepareItemForCaching), new ConvertCachedItem(HttpStream.HttpChannelManager.ConvertCachedItem));

			// Token: 0x04000C24 RID: 3108
			private static IDictionary<string, HttpStream.HttpChannelController> activeControllers = new Dictionary<string, HttpStream.HttpChannelController>();
		}

		// Token: 0x02000185 RID: 389
		private sealed class LegacyController : HttpStream.HttpChannelController
		{
			// Token: 0x060011D1 RID: 4561 RVA: 0x0003DC43 File Offset: 0x0003BE43
			public LegacyController(HttpStream.ChannelOptions options, CookieContainer cookieContainer)
				: base(options, cookieContainer)
			{
			}

			// Token: 0x060011D2 RID: 4562 RVA: 0x0003DC50 File Offset: 0x0003BE50
			internal LegacyController(IList<object> elements, out bool hasElementsUpdate)
			{
				int num;
				base..ctor(elements, out num, out hasElementsUpdate);
			}

			// Token: 0x060011D3 RID: 4563 RVA: 0x0003DC68 File Offset: 0x0003BE68
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

			// Token: 0x060011D4 RID: 4564 RVA: 0x0003DD24 File Offset: 0x0003BF24
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

			// Token: 0x060011D5 RID: 4565 RVA: 0x0003DEA8 File Offset: 0x0003C0A8
			protected override void ProcessWebResponse(HttpStream owner, object context, HttpStream.HttpChannelController.WebResponseInfo response)
			{
				base.ProcessWebResponse(owner, context, response);
				string text;
				if (response.TryGetResponseHeaderValue("OutdatedVersion", out text))
				{
					owner.outdatedVersion = true;
					throw new AdomdConnectionException(XmlaSR.Connection_WorkbookIsOutdated, null);
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

			// Token: 0x060011D6 RID: 4566 RVA: 0x0003DF2B File Offset: 0x0003C12B
			protected override HttpStream.HttpXmlaOperation CreateNewOperation(HttpStream owner, bool useHttpClient)
			{
				if (useHttpClient)
				{
					return new HttpStream.LegacyController.HttpClientXmlaOperation(owner, this);
				}
				return new HttpStream.LegacyController.WebRequestXmlaOperation(owner, this);
			}

			// Token: 0x060011D7 RID: 4567 RVA: 0x0003DF40 File Offset: 0x0003C140
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

			// Token: 0x060011D8 RID: 4568 RVA: 0x0003DFF0 File Offset: 0x0003C1F0
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
				httpWebRequest.UserAgent = "ADOMD.NET";
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
								throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected soap:Fault, got {0}", xmlTextReader.Name));
							}
							catch (XmlException ex)
							{
								throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
							}
							catch (IOException ex2)
							{
								throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
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
						throw new AdomdConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo, null);
					}
					databaseName = httpWebResponse.Headers["DatabaseName"];
					if (databaseName != null)
					{
						databaseName = databaseName.Trim();
					}
					if (string.IsNullOrEmpty(databaseName))
					{
						throw new AdomdConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo, null);
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
							throw new AdomdConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo, null);
						}
					}
					else
					{
						dataSourceVersion = null;
					}
				}
			}

			// Token: 0x0200022A RID: 554
			internal static class XmlaHttpHeaders
			{
				// Token: 0x04000F31 RID: 3889
				public const string AcquireTokenStats = "X-AS-AcquireTokenStats";

				// Token: 0x04000F32 RID: 3890
				public const string ActivityId = "X-AS-ActivityID";

				// Token: 0x04000F33 RID: 3891
				public const string TransportCapabilitiesNegotiation = "X-Transport-Caps-Negotiation-Flags";

				// Token: 0x04000F34 RID: 3892
				public const string ApplicationName = "SspropInitAppName";

				// Token: 0x04000F35 RID: 3893
				public const string SessionId = "X-AS-SessionID";

				// Token: 0x04000F36 RID: 3894
				public const string SessionTokenRequest = "X-AS-GetSessionToken";

				// Token: 0x04000F37 RID: 3895
				public const string RequestId = "X-AS-RequestID";

				// Token: 0x04000F38 RID: 3896
				public const string CurrentActivityId = "X-AS-CurrentActivityID";

				// Token: 0x04000F39 RID: 3897
				public const string XASRouting = "X-AS-Routing";

				// Token: 0x04000F3A RID: 3898
				public const string OutdatedVersion = "OutdatedVersion";
			}

			// Token: 0x0200022B RID: 555
			private abstract class LegacyXmlaOperation : HttpStream.HttpXmlaOperation
			{
				// Token: 0x06001534 RID: 5428 RVA: 0x0004788F File Offset: 0x00045A8F
				protected LegacyXmlaOperation(HttpStream owner, HttpStream.LegacyController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x06001535 RID: 5429 RVA: 0x00047899 File Offset: 0x00045A99
				public sealed override string GetExtendedErrorInfo()
				{
					return string.Empty;
				}

				// Token: 0x06001536 RID: 5430 RVA: 0x000478A0 File Offset: 0x00045AA0
				protected sealed override void EnsureCanRead()
				{
					if (base.Owner.outdatedVersion)
					{
						throw new AdomdConnectionException(XmlaSR.Connection_WorkbookIsOutdated, null);
					}
					base.EnsureCanRead();
				}

				// Token: 0x06001537 RID: 5431 RVA: 0x000478D4 File Offset: 0x00045AD4
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

			// Token: 0x0200022C RID: 556
			private sealed class WebRequestXmlaOperation : HttpStream.LegacyController.LegacyXmlaOperation
			{
				// Token: 0x06001538 RID: 5432 RVA: 0x000478F8 File Offset: 0x00045AF8
				public WebRequestXmlaOperation(HttpStream owner, HttpStream.LegacyController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x06001539 RID: 5433 RVA: 0x00047902 File Offset: 0x00045B02
				protected override Stream StartRequestImpl()
				{
					return this.manager.StartRequest(this, true);
				}

				// Token: 0x0600153A RID: 5434 RVA: 0x00047911 File Offset: 0x00045B11
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x0600153B RID: 5435 RVA: 0x0004791F File Offset: 0x00045B1F
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x0600153C RID: 5436 RVA: 0x00047933 File Offset: 0x00045B33
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04000F3B RID: 3899
				private HttpStream.HttpXmlaOperation.WebRequestOperationManager manager;
			}

			// Token: 0x0200022D RID: 557
			private sealed class HttpClientXmlaOperation : HttpStream.LegacyController.LegacyXmlaOperation
			{
				// Token: 0x0600153D RID: 5437 RVA: 0x0004794C File Offset: 0x00045B4C
				public HttpClientXmlaOperation(HttpStream owner, HttpStream.LegacyController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x0600153E RID: 5438 RVA: 0x00047956 File Offset: 0x00045B56
				protected override Stream StartRequestImpl()
				{
					return this.manager.StartRequest(this, true);
				}

				// Token: 0x0600153F RID: 5439 RVA: 0x00047965 File Offset: 0x00045B65
				protected override void CompleteRequestImpl()
				{
					((HttpStream.HttpChannelController.RequestPayloadStream)base.RequestPayload).MarkAsCompleted();
				}

				// Token: 0x06001540 RID: 5440 RVA: 0x00047977 File Offset: 0x00045B77
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x06001541 RID: 5441 RVA: 0x00047985 File Offset: 0x00045B85
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x06001542 RID: 5442 RVA: 0x00047999 File Offset: 0x00045B99
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04000F3C RID: 3900
				private HttpStream.HttpXmlaOperation.HttpClientOperationManager manager;
			}
		}

		// Token: 0x02000186 RID: 390
		private sealed class PaasInfraController : HttpStream.HttpChannelController
		{
			// Token: 0x060011D9 RID: 4569 RVA: 0x0003E24C File Offset: 0x0003C44C
			public PaasInfraController(HttpStream.ChannelOptions options, CookieContainer cookieContainer)
				: base(options, cookieContainer)
			{
			}

			// Token: 0x060011DA RID: 4570 RVA: 0x0003E258 File Offset: 0x0003C458
			internal PaasInfraController(IList<object> elements, out bool hasElementsUpdate)
			{
				int num;
				base..ctor(elements, out num, out hasElementsUpdate);
			}

			// Token: 0x060011DB RID: 4571 RVA: 0x0003E270 File Offset: 0x0003C470
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

			// Token: 0x060011DC RID: 4572 RVA: 0x0003E608 File Offset: 0x0003C808
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

			// Token: 0x060011DD RID: 4573 RVA: 0x0003E7CF File Offset: 0x0003C9CF
			protected override void HandleResponseStatusCode(int statusCode, Exception ex)
			{
				if (statusCode >= 300 && statusCode <= 399)
				{
					AsPaasEndpointInfo.InvalidateCache();
					throw new AdomdConnectionException(XmlaSR.Connection_AnalysisServicesInstanceWasMoved, ex, ConnectionExceptionCause.ServerHasMoved);
				}
				base.HandleResponseStatusCode(statusCode, ex);
			}

			// Token: 0x060011DE RID: 4574 RVA: 0x0003E7FC File Offset: 0x0003C9FC
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

			// Token: 0x060011DF RID: 4575 RVA: 0x0003E8F4 File Offset: 0x0003CAF4
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

			// Token: 0x060011E0 RID: 4576 RVA: 0x0003E928 File Offset: 0x0003CB28
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

			// Token: 0x060011E1 RID: 4577 RVA: 0x0003E9C1 File Offset: 0x0003CBC1
			private static int GetMinimumWaitTimeForRetryAttempt(int attempt)
			{
				if (HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt.Length > attempt)
				{
					return HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt[attempt];
				}
				return HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt[HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt.Length - 1];
			}

			// Token: 0x04000C25 RID: 3109
			private const int MAX_IDLE_CONNECTION_TIMEOUT_IN_MS = 120000;

			// Token: 0x04000C26 RID: 3110
			private const int LRO_CLIENT_RECONNECT_REQUESTED = 1;

			// Token: 0x04000C27 RID: 3111
			private const int LRO_CLIENT_REQUEST_FINISHED = 0;

			// Token: 0x04000C28 RID: 3112
			private static TimeoutUtils.OnTimeoutAction onConnectTimeout = delegate(bool isOnDispose)
			{
				if (!isOnDispose)
				{
					throw new AdomdConnectionException(XmlaSR.XmlaClient_ConnectTimedOut, null);
				}
				return true;
			};

			// Token: 0x04000C29 RID: 3113
			private static readonly int[] MinimumWaitTimeInMsForRetryAttempt = new int[] { 100, 200, 400, 800, 1600, 3200, 6400, 10000 };

			// Token: 0x0200022E RID: 558
			private enum TransientModelMode
			{
				// Token: 0x04000F3E RID: 3902
				Disabled,
				// Token: 0x04000F3F RID: 3903
				Enabled
			}

			// Token: 0x0200022F RID: 559
			private sealed class XmlaOperationContext
			{
				// Token: 0x06001543 RID: 5443 RVA: 0x000479B4 File Offset: 0x00045BB4
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

				// Token: 0x17000742 RID: 1858
				// (get) Token: 0x06001544 RID: 5444 RVA: 0x00047A1F File Offset: 0x00045C1F
				// (set) Token: 0x06001545 RID: 5445 RVA: 0x00047A27 File Offset: 0x00045C27
				public Guid RegistrationId { get; private set; }

				// Token: 0x17000743 RID: 1859
				// (get) Token: 0x06001546 RID: 5446 RVA: 0x00047A30 File Offset: 0x00045C30
				// (set) Token: 0x06001547 RID: 5447 RVA: 0x00047A38 File Offset: 0x00045C38
				public string RootActivityId { get; set; }

				// Token: 0x17000744 RID: 1860
				// (get) Token: 0x06001548 RID: 5448 RVA: 0x00047A41 File Offset: 0x00045C41
				// (set) Token: 0x06001549 RID: 5449 RVA: 0x00047A49 File Offset: 0x00045C49
				public Guid ParentActivityId { get; private set; }

				// Token: 0x17000745 RID: 1861
				// (get) Token: 0x0600154A RID: 5450 RVA: 0x00047A52 File Offset: 0x00045C52
				public string RequestTransportCapabilities { get; }

				// Token: 0x17000746 RID: 1862
				// (get) Token: 0x0600154B RID: 5451 RVA: 0x00047A5A File Offset: 0x00045C5A
				// (set) Token: 0x0600154C RID: 5452 RVA: 0x00047A62 File Offset: 0x00045C62
				public string ResponseTransportCapabilities { get; set; }

				// Token: 0x17000747 RID: 1863
				// (get) Token: 0x0600154D RID: 5453 RVA: 0x00047A6B File Offset: 0x00045C6B
				public int RoundTripId
				{
					get
					{
						return this.roundTripId;
					}
				}

				// Token: 0x17000748 RID: 1864
				// (get) Token: 0x0600154E RID: 5454 RVA: 0x00047A73 File Offset: 0x00045C73
				public bool IsUnderLROProtocol
				{
					get
					{
						return this.isUnderLROProtocol;
					}
				}

				// Token: 0x17000749 RID: 1865
				// (get) Token: 0x0600154F RID: 5455 RVA: 0x00047A7B File Offset: 0x00045C7B
				// (set) Token: 0x06001550 RID: 5456 RVA: 0x00047A97 File Offset: 0x00045C97
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

				// Token: 0x1700074A RID: 1866
				// (get) Token: 0x06001551 RID: 5457 RVA: 0x00047AA5 File Offset: 0x00045CA5
				public bool HasResponseDataType
				{
					get
					{
						return this.responseDataType != null;
					}
				}

				// Token: 0x06001552 RID: 5458 RVA: 0x00047AB2 File Offset: 0x00045CB2
				internal void MarkAsUnderLROProtocol()
				{
					this.isUnderLROProtocol = true;
					this.roundTripId++;
				}

				// Token: 0x06001553 RID: 5459 RVA: 0x00047AC9 File Offset: 0x00045CC9
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

				// Token: 0x06001554 RID: 5460 RVA: 0x00047B03 File Offset: 0x00045D03
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

				// Token: 0x06001555 RID: 5461 RVA: 0x00047B2F File Offset: 0x00045D2F
				internal void SwitchLastDataByte(byte[] buffer, int offset, byte lastDataByte)
				{
					buffer[offset] = this.lastDataByte.Value;
					this.lastDataByte = new byte?(lastDataByte);
				}

				// Token: 0x06001556 RID: 5462 RVA: 0x00047B4B File Offset: 0x00045D4B
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

				// Token: 0x04000F40 RID: 3904
				private int roundTripId;

				// Token: 0x04000F41 RID: 3905
				private XmlaDataType? responseDataType;

				// Token: 0x04000F42 RID: 3906
				private bool isUnderLROProtocol;

				// Token: 0x04000F43 RID: 3907
				private byte? firstDataByte;

				// Token: 0x04000F44 RID: 3908
				private byte? lastDataByte;
			}

			// Token: 0x02000230 RID: 560
			private abstract class PaasInfraXmlaOperation : HttpStream.HttpXmlaOperation
			{
				// Token: 0x06001557 RID: 5463 RVA: 0x00047B79 File Offset: 0x00045D79
				protected PaasInfraXmlaOperation(HttpStream owner, HttpStream.PaasInfraController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x06001558 RID: 5464 RVA: 0x00047B83 File Offset: 0x00045D83
				public sealed override string GetExtendedErrorInfo()
				{
					if (!base.Owner.info.IsAsAzure && !base.Owner.info.IsPbiPremiumXmlaEp)
					{
						return string.Empty;
					}
					return this.GetExtendedErrorInfoFromResponse();
				}

				// Token: 0x06001559 RID: 5465
				protected abstract Stream StartRequestAndGetRequestPayload(bool isReconnect);

				// Token: 0x0600155A RID: 5466
				protected abstract string GetExtendedErrorInfoFromResponse();

				// Token: 0x0600155B RID: 5467 RVA: 0x00047BB5 File Offset: 0x00045DB5
				protected sealed override Stream StartRequestImpl()
				{
					return this.StartRequestAndGetRequestPayload(false);
				}

				// Token: 0x0600155C RID: 5468 RVA: 0x00047BC0 File Offset: 0x00045DC0
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
											throw new AdomdConnectionException(XmlaSR.ConnectionBroken, null);
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
													throw new AdomdConnectionException(XmlaSR.UnknownServerResponseFormat, new ConnectionExceptionCause?(ConnectionExceptionCause.TransportProtocolError));
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

				// Token: 0x0600155D RID: 5469 RVA: 0x00047DEC File Offset: 0x00045FEC
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

				// Token: 0x0600155E RID: 5470 RVA: 0x00047E48 File Offset: 0x00046048
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
									goto IL_0127;
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
					throw new AdomdConnectionException(XmlaSR.UnknownServerResponseFormat, new ConnectionExceptionCause?(ConnectionExceptionCause.TransportProtocolError));
					IL_0127:
					throw new XmlaStreamException(XmlaSR.HttpStream_InvalidReadRequest(base.Status.ToString()));
				}
			}

			// Token: 0x02000231 RID: 561
			private sealed class WebRequestXmlaOperation : HttpStream.PaasInfraController.PaasInfraXmlaOperation
			{
				// Token: 0x0600155F RID: 5471 RVA: 0x00047FA3 File Offset: 0x000461A3
				public WebRequestXmlaOperation(HttpStream owner, HttpStream.PaasInfraController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x06001560 RID: 5472 RVA: 0x00047FAD File Offset: 0x000461AD
				protected override Stream StartRequestAndGetRequestPayload(bool isReconnect)
				{
					return this.manager.StartRequest(this, !isReconnect);
				}

				// Token: 0x06001561 RID: 5473 RVA: 0x00047FBF File Offset: 0x000461BF
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x06001562 RID: 5474 RVA: 0x00047FCD File Offset: 0x000461CD
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x06001563 RID: 5475 RVA: 0x00047FE1 File Offset: 0x000461E1
				protected override string GetExtendedErrorInfoFromResponse()
				{
					return AsPaasHelper.GetTechnicalDetailsFromPaasInfraResponse(this.manager.Response);
				}

				// Token: 0x06001564 RID: 5476 RVA: 0x00047FF3 File Offset: 0x000461F3
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04000F4A RID: 3914
				private HttpStream.HttpXmlaOperation.WebRequestOperationManager manager;
			}

			// Token: 0x02000232 RID: 562
			private sealed class HttpClientXmlaOperation : HttpStream.PaasInfraController.PaasInfraXmlaOperation
			{
				// Token: 0x06001565 RID: 5477 RVA: 0x0004800C File Offset: 0x0004620C
				public HttpClientXmlaOperation(HttpStream owner, HttpStream.PaasInfraController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x06001566 RID: 5478 RVA: 0x00048016 File Offset: 0x00046216
				protected override Stream StartRequestAndGetRequestPayload(bool isReconnect)
				{
					return this.manager.StartRequest(this, !isReconnect);
				}

				// Token: 0x06001567 RID: 5479 RVA: 0x00048028 File Offset: 0x00046228
				protected override void CompleteRequestImpl()
				{
					((HttpStream.HttpChannelController.RequestPayloadStream)base.RequestPayload).MarkAsCompleted();
				}

				// Token: 0x06001568 RID: 5480 RVA: 0x0004803A File Offset: 0x0004623A
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x06001569 RID: 5481 RVA: 0x00048048 File Offset: 0x00046248
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x0600156A RID: 5482 RVA: 0x0004805C File Offset: 0x0004625C
				protected override string GetExtendedErrorInfoFromResponse()
				{
					return AsPaasHelper.GetTechnicalDetailsFromPaasInfraResponse(this.manager.Response);
				}

				// Token: 0x0600156B RID: 5483 RVA: 0x0004806E File Offset: 0x0004626E
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04000F4B RID: 3915
				private HttpStream.HttpXmlaOperation.HttpClientOperationManager manager;
			}
		}

		// Token: 0x02000187 RID: 391
		private enum HttpXmlaOperationStatus
		{
			// Token: 0x04000C2B RID: 3115
			Idle,
			// Token: 0x04000C2C RID: 3116
			Request,
			// Token: 0x04000C2D RID: 3117
			Response,
			// Token: 0x04000C2E RID: 3118
			Completed,
			// Token: 0x04000C2F RID: 3119
			Error
		}

		// Token: 0x02000188 RID: 392
		private abstract class HttpXmlaOperation : Disposable
		{
			// Token: 0x060011E3 RID: 4579 RVA: 0x0003EA11 File Offset: 0x0003CC11
			protected HttpXmlaOperation(HttpStream owner, HttpStream.HttpChannelController controller)
			{
				this.Owner = owner;
				this.Controller = controller;
			}

			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0003EA27 File Offset: 0x0003CC27
			public HttpStream Owner { get; }

			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0003EA2F File Offset: 0x0003CC2F
			public HttpStream.HttpChannelController Controller { get; }

			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0003EA37 File Offset: 0x0003CC37
			// (set) Token: 0x060011E7 RID: 4583 RVA: 0x0003EA3F File Offset: 0x0003CC3F
			public object Context { get; set; }

			// Token: 0x17000654 RID: 1620
			// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0003EA48 File Offset: 0x0003CC48
			// (set) Token: 0x060011E9 RID: 4585 RVA: 0x0003EA50 File Offset: 0x0003CC50
			public HttpStream.HttpXmlaOperationStatus Status { get; private set; }

			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x060011EA RID: 4586 RVA: 0x0003EA59 File Offset: 0x0003CC59
			// (set) Token: 0x060011EB RID: 4587 RVA: 0x0003EA61 File Offset: 0x0003CC61
			private protected Stream RequestPayload { protected get; private set; }

			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x060011EC RID: 4588 RVA: 0x0003EA6A File Offset: 0x0003CC6A
			// (set) Token: 0x060011ED RID: 4589 RVA: 0x0003EA72 File Offset: 0x0003CC72
			private protected Stream ResponsePayload { protected get; private set; }

			// Token: 0x060011EE RID: 4590 RVA: 0x0003EA7C File Offset: 0x0003CC7C
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

			// Token: 0x060011EF RID: 4591 RVA: 0x0003EAB8 File Offset: 0x0003CCB8
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

			// Token: 0x060011F0 RID: 4592 RVA: 0x0003EB04 File Offset: 0x0003CD04
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

			// Token: 0x060011F1 RID: 4593 RVA: 0x0003EB5C File Offset: 0x0003CD5C
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

			// Token: 0x060011F2 RID: 4594 RVA: 0x0003EBB0 File Offset: 0x0003CDB0
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

			// Token: 0x060011F3 RID: 4595
			public abstract string GetExtendedErrorInfo();

			// Token: 0x060011F4 RID: 4596 RVA: 0x0003EC10 File Offset: 0x0003CE10
			protected static XmlaDataType GetResponseDataTypeFromContentType(string contentType)
			{
				Match match = HttpStream.contentTypeRegex.Match(contentType);
				if (!match.Success)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnsupportedDataFormat(contentType), string.Empty);
				}
				XmlaDataType dataTypeFromString = DataTypes.GetDataTypeFromString(match.Groups["content_type"].Value);
				if (dataTypeFromString == XmlaDataType.Undetermined || dataTypeFromString == XmlaDataType.Unknown)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnsupportedDataFormat(contentType), string.Empty);
				}
				return dataTypeFromString;
			}

			// Token: 0x060011F5 RID: 4597
			protected abstract Stream StartRequestImpl();

			// Token: 0x060011F6 RID: 4598
			protected abstract Stream GetResponseImpl();

			// Token: 0x060011F7 RID: 4599
			protected abstract bool TryGetResponseHeaderValueImpl(string header, out string value);

			// Token: 0x060011F8 RID: 4600
			protected abstract XmlaDataType GetResponseDataTypeImpl();

			// Token: 0x060011F9 RID: 4601 RVA: 0x0003EC74 File Offset: 0x0003CE74
			protected virtual void WriteRequestPayloadImpl(byte[] buffer, int offset, int size)
			{
				this.RequestPayload.Write(buffer, offset, size);
			}

			// Token: 0x060011FA RID: 4602 RVA: 0x0003EC84 File Offset: 0x0003CE84
			protected virtual void CompleteRequestImpl()
			{
				this.RequestPayload.Flush();
				this.RequestPayload.Close();
			}

			// Token: 0x060011FB RID: 4603 RVA: 0x0003EC9C File Offset: 0x0003CE9C
			protected virtual void EnsureCanRead()
			{
				if (this.ResponsePayload == null)
				{
					this.ResponsePayload = this.GetResponseImpl();
				}
			}

			// Token: 0x060011FC RID: 4604 RVA: 0x0003ECB2 File Offset: 0x0003CEB2
			protected virtual int ReadResponsePayloadImpl(byte[] buffer, int offset, int size)
			{
				return this.ResponsePayload.Read(buffer, offset, size);
			}

			// Token: 0x060011FD RID: 4605 RVA: 0x0003ECC4 File Offset: 0x0003CEC4
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

			// Token: 0x060011FE RID: 4606 RVA: 0x0003ED18 File Offset: 0x0003CF18
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

			// Token: 0x02000234 RID: 564
			protected struct WebRequestOperationManager
			{
				// Token: 0x1700074B RID: 1867
				// (get) Token: 0x0600156F RID: 5487 RVA: 0x000480C1 File Offset: 0x000462C1
				public HttpWebRequest Request
				{
					get
					{
						return this.request;
					}
				}

				// Token: 0x1700074C RID: 1868
				// (get) Token: 0x06001570 RID: 5488 RVA: 0x000480C9 File Offset: 0x000462C9
				public HttpWebResponse Response
				{
					get
					{
						return this.response;
					}
				}

				// Token: 0x06001571 RID: 5489 RVA: 0x000480D4 File Offset: 0x000462D4
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

				// Token: 0x06001572 RID: 5490 RVA: 0x0004810C File Offset: 0x0004630C
				public Stream GetResponse(HttpStream.HttpXmlaOperation operation)
				{
					Stream stream;
					if (!operation.Controller.CompleteWebRequestBasedOperation(operation.Owner, operation.Context, this.request, out this.response, out stream))
					{
						return null;
					}
					return stream;
				}

				// Token: 0x06001573 RID: 5491 RVA: 0x00048144 File Offset: 0x00046344
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

				// Token: 0x04000F4D RID: 3917
				private HttpWebRequest request;

				// Token: 0x04000F4E RID: 3918
				private HttpWebResponse response;
			}

			// Token: 0x02000235 RID: 565
			protected struct HttpClientOperationManager
			{
				// Token: 0x1700074D RID: 1869
				// (get) Token: 0x06001574 RID: 5492 RVA: 0x0004819A File Offset: 0x0004639A
				public HttpRequestMessage Request
				{
					get
					{
						return this.request;
					}
				}

				// Token: 0x1700074E RID: 1870
				// (get) Token: 0x06001575 RID: 5493 RVA: 0x000481A2 File Offset: 0x000463A2
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

				// Token: 0x06001576 RID: 5494 RVA: 0x000481C8 File Offset: 0x000463C8
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

				// Token: 0x06001577 RID: 5495 RVA: 0x0004823C File Offset: 0x0004643C
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

				// Token: 0x06001578 RID: 5496 RVA: 0x000482A0 File Offset: 0x000464A0
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

				// Token: 0x04000F4F RID: 3919
				private CancellationTokenSource cts;

				// Token: 0x04000F50 RID: 3920
				private HttpRequestMessage request;

				// Token: 0x04000F51 RID: 3921
				private Task<HttpResponseMessage> response;
			}
		}
	}
}
